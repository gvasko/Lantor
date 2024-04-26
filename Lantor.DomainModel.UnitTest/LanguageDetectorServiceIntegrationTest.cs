using Lantor.DomainModel.UnitTest.TestDoubles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel.UnitTest
{
    public class LanguageDetectorServiceIntegrationTest
    {
        private LanguageDetectorService _sut;
        private MultilingualSample _multiSample;

        [SetUp] public void SetUp()
        {
            LanguageSample ls1 = new("en", FakeSamples.SAMPLE_EN);
            LanguageSample ls2 = new("de", FakeSamples.SAMPLE_DE);
            LanguageSample ls3 = new("hu", FakeSamples.SAMPLE_HU);

            _multiSample = new("test-multi", "some comment");
            _multiSample.Languages.Add(ls1);
            _multiSample.Languages.Add(ls2);
            _multiSample.Languages.Add(ls3);

            Alphabet abc = new("testABC", FakeVectorFactory.DIM, new FakeVectorFactory());

            Mock<ISampleRepository> _sampleRepo = new();
            _sampleRepo.Setup(sr => sr.GetDefaultAlphabet()).Returns(abc);
            _sampleRepo.Setup(sr => sr.GetDefaultSamples()).Returns(_multiSample);

            // TODO: why the new?
            _sut = new LanguageDetectorService(_sampleRepo.Object, new LanguageVectorBuilder());
        }

        [Test]
        public void TestFakeAlphabetOrthogonality()
        {
            var orthoResult = _sut.AlphabetOrthoTest();
            foreach (var letterResult in orthoResult.SimilarityValues)
            {
                Assert.That(letterResult.Value, Is.LessThanOrEqualTo(0.15), $"Similarity value should be smaller for {letterResult.Name}");
            }
        }

    }
}
