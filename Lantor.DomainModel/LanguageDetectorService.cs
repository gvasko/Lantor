using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lantor.DomainModel
{
    public class LanguageDetectorService : ILanguageDetectorService
    {
        private readonly ISampleRepository sampleRepository;
        private readonly ILanguageVectorBuilder languageVectorBuilder;

        public LanguageDetectorService(ISampleRepository sampleRepository, ILanguageVectorBuilder languageVectorBuilder) 
        {
            this.sampleRepository = sampleRepository;
            this.languageVectorBuilder = languageVectorBuilder;
        }

        public async Task<LanguageSimilarityResult> AlphabetOrthoTest()
        {
            var abc = await sampleRepository.GetDefaultAlphabetAsync();
            LanguageSimilarityValue[] similarityValues = new LanguageSimilarityValue[abc.LetterVectors.Count];

            var i = 0;
            foreach (var lv1 in abc.LetterVectors)
            {
                var maxSim = 0.0;

                foreach (var lv2 in abc.LetterVectors)
                {
                    if (lv1.Letter == lv2.Letter)
                    {
                        continue;
                    }
                    var similarity = lv1.Vector.Similarity(lv2.Vector);
                    var absSim = Math.Abs(similarity);
                    if (absSim > maxSim)
                    {
                        maxSim = absSim;
                    }
                }
                similarityValues[i++] = new LanguageSimilarityValue
                {
                    Name = char.ToString(lv1.Letter),
                    Value = maxSim
                };
            }

            return new LanguageSimilarityResult(similarityValues, 0);
        }

        /// <summary>
        /// Detects languages for the given text, using the default alphabet and default language samples.
        /// </summary>
        /// <param name="text">The text, clients want to know which language it is written in</param>
        /// <returns>A list of languages with a correlation value, ordered descended, the most probable language first</returns>
        public async Task<LanguageSimilarityResult> Detect(string text)
        {
            var alphabet = await sampleRepository.GetDefaultAlphabetAsync();
            LoggerService.Logger.Debug("Default alphabet loaded");

            var languageSamples = await sampleRepository.GetDefaultSamplesAsync();
            LoggerService.Logger.Debug("Default samples loaded, count = {count}.", languageSamples.Languages.Count);

            return await Detect(alphabet, languageSamples, text);
        }

        public async Task<LanguageSimilarityResult> Detect(int sampleId, int alphabetId, string text)
        {
            var alphabet = await sampleRepository.GetAlphabetAsync(alphabetId);
            LoggerService.Logger.Debug("Alphabet loaded: {name}", alphabet.Name);

            var languageSamples = await sampleRepository.GetMultilingualSampleAsync(sampleId);

            if (alphabet == null || languageSamples == null)
            {
                LoggerService.Logger.Error("No alphabet or language samples found");
                return new LanguageSimilarityResult();
            }

            LoggerService.Logger.Debug("Samples loaded, count = {count}.", languageSamples.Languages.Count);

            return await Detect(alphabet, languageSamples, text);
        }

        private Task<LanguageSimilarityResult> Detect(Alphabet alphabet, MultilingualSample languageSamples, string text)
        {
            return Task.Run(async () =>
            {
                Stopwatch stopWatch = new();
                stopWatch.Start();
                LoggerService.Logger.Debug("Detection begins: {samplePrefix}", text.Substring(0, Math.Min(16, text.Length)));

                var sampleVector = languageVectorBuilder.BuildLanguageVector(alphabet, text);
                LoggerService.Logger.Debug("Sample vector built.");

                LanguageSimilarityValue[] similarityValues = new LanguageSimilarityValue[languageSamples.Languages.Count];
                int i = 0;
                foreach (var language in languageSamples.Languages)
                {
                    var languageVector = await sampleRepository.GetLanguageVectorFromCacheAsync(language, alphabet);

                    if (languageVector == null)
                    {
                        LoggerService.Logger.Debug("Language vector for {lang} not found in cache. Calculating...", language.Name);
                        languageVector = languageVectorBuilder.BuildLanguageVector(alphabet, language.Sample);
                        LoggerService.Logger.Debug("Language vector for {lang} generated.", language.Name);
                        await sampleRepository.AddLanguageVectorToCacheAsync(language, alphabet, languageVector);
                        await sampleRepository.Save();
                        LoggerService.Logger.Debug("Language vector for {lang} stored in cache.", language.Name);
                    }
                    else
                    {
                        LoggerService.Logger.Debug("Language vector for {lang} loaded from cache.", language.Name);
                    }
                    var similarity = languageVector.Similarity(sampleVector);
                    similarityValues[i++] = new LanguageSimilarityValue(language.Name, similarity);
                    LoggerService.Logger.Debug("Language vector similarity for {lang} calculated.", language.Name);
                }

                LoggerService.Logger.Debug("Similarity results sorted.");
                stopWatch.Stop();
                LoggerService.Logger.Debug("Detected in {elapsed} ms.", stopWatch.ElapsedMilliseconds);


                return new LanguageSimilarityResult(similarityValues, stopWatch.ElapsedMilliseconds, GetSignificantCount(similarityValues));
            });
        }

        private static int GetSignificantCount(LanguageSimilarityValue[] similarityValues)
        {
            double mean = 0.0;
            foreach (var similarityValue in similarityValues)
            {
                mean += similarityValue.Value;
            }
            mean /= similarityValues.Length;

            double stdDeviation = 0.0;
            foreach (var similarityValue in similarityValues)
            {
                var diff = similarityValue.Value - mean;
                stdDeviation += diff * diff;
            }
            stdDeviation = Math.Sqrt(stdDeviation / similarityValues.Length);

            int count = 0;
            foreach (var similarityValue in similarityValues)
            {
                if (similarityValue.Value - mean - stdDeviation > 0) count++;
            }

            return count;
        }

    }
}
