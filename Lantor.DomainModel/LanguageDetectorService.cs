﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class LanguageDetectorService : ILanguageDetectorService
    {
        private readonly ISampleRepository sampleRepository;
        private readonly ILanguageVectorBuilder languageVectorBuilder;

        internal LanguageDetectorService(ISampleRepository sampleRepository, ILanguageVectorBuilder languageVectorBuilder) 
        {
            this.sampleRepository = sampleRepository;
            this.languageVectorBuilder = languageVectorBuilder;
        }

        public LanguageDetectorService(ISampleRepository sampleRepository)
            : this(sampleRepository, new LanguageVectorBuilder(sampleRepository.GetDefaultAlphabet()))
        {
        }

        /// <summary>
        /// Detects languages for the given text, using the default alphabet and default language samples.
        /// </summary>
        /// <param name="text">The text, clients want to know which language it is written in</param>
        /// <returns>A list of languages with a correlation value, ordered descended, the most probable language first</returns>
        public LanguageSimilarityResult Detect(string text)
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();

            LoggerService.Logger.Debug("Default detection begins: {samplePrefix}", text.Substring(0, Math.Min(16, text.Length)));

            var alphabet = sampleRepository.GetDefaultAlphabet();
            LoggerService.Logger.Debug("Default alphabet loaded");

            var sampleVector = languageVectorBuilder.BuildLanguageVector(text);
            LoggerService.Logger.Debug("Sample vector built.");

            var languageSamples = sampleRepository.GetDefaultSamples();
            LoggerService.Logger.Debug("Default samples loaded, count = {count}.", languageSamples.Languages.Count);

            LanguageSimilarityValue[] similarityValues = new LanguageSimilarityValue[languageSamples.Languages.Count];
            int i = 0;
            foreach (var language in languageSamples.Languages) 
            {
                var languageVector = sampleRepository.GetLanguageVectorFromCache(language, alphabet);
                if (languageVector == null)
                {
                    LoggerService.Logger.Debug("Language vector for {lang} not found in cache.", language.Name);
                    languageVector = languageVectorBuilder.BuildLanguageVector(language.Sample);
                    LoggerService.Logger.Debug("Language vector for {lang} generated.", language.Name);
                    sampleRepository.AddLanguageVectorToCache(language, alphabet, languageVector);
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

            Array.Sort(similarityValues, (x, y) => x.Value < y.Value ? 1 : x.Value > y.Value ? -1 : 0);

            LoggerService.Logger.Debug("Similarity results sorted.");
            stopWatch.Stop();
            LoggerService.Logger.Debug("Detected in {elapsed} ms.", stopWatch.ElapsedMilliseconds);


            return new LanguageSimilarityResult(similarityValues);
        }
    }
}
