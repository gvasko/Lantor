using Lantor.DomainModel.UnitTest.TestDoubles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel.UnitTest
{
    public class LanguageDetectorServiceTest
    {
        private LanguageDetectorService _sut;
        private Mock<ISampleRepository> _sampleRepo;
        private Mock<ILanguageVectorBuilder> _vectorBuilderMock;
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

            _vectorBuilderMock = new Mock<ILanguageVectorBuilder>();
            var randomLangVector1 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _vectorBuilderMock.Setup(vb => vb.BuildLanguageVector(It.Is((string s) => s == ls1.Sample))).Returns(randomLangVector1);
            var randomLangVector2 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _vectorBuilderMock.Setup(vb => vb.BuildLanguageVector(It.Is((string s) => s == ls2.Sample))).Returns(randomLangVector2);
            var randomLangVector3 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _vectorBuilderMock.Setup(vb => vb.BuildLanguageVector(It.Is((string s) => s == ls3.Sample))).Returns(randomLangVector3);
            var randomLangVector4 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _vectorBuilderMock.Setup(vb => vb.BuildLanguageVector(It.IsAny<string>())).Returns(randomLangVector4);

            _sampleRepo = new Mock<ISampleRepository>();
            _sampleRepo.Setup(sr => sr.GetDefaultAlphabet()).Returns(abc);
            _sampleRepo.Setup(sr => sr.GetDefaultSamples()).Returns(_multiSample);

            var randomCachedVector1 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _sampleRepo.Setup(sr => sr.GetLanguageVectorFromCache(It.Is((LanguageSample ls) => ls.Sample == ls1.Sample), It.IsAny<Alphabet>())).Returns(randomCachedVector1);
            var randomCachedVector2 = HiDimBipolarVector.CreateRandomVector(FakeVectorFactory.DIM);
            _sampleRepo.Setup(sr => sr.GetLanguageVectorFromCache(It.Is((LanguageSample ls) => ls.Sample == ls2.Sample), It.IsAny<Alphabet>())).Returns(randomCachedVector2);
            // GetLanguageVectorFromCache(ls3.Sample), It.IsAny<Alphabet>())) -- returns null

            _sut = new LanguageDetectorService(_sampleRepo.Object, _vectorBuilderMock.Object);
        }

        [Test]
        public void WhenLanguageVectorNotFoundInCache_ThenItGeneratesAndAddToCache()
        {
            var result = _sut.Detect("Lorem ipsum.");
            _sampleRepo.Verify(sr => sr.GetLanguageVectorFromCache(It.IsAny<LanguageSample>(), It.IsAny<Alphabet>()), Times.Exactly(_multiSample.Languages.Count));
            _vectorBuilderMock.Verify(vb => vb.BuildLanguageVector(It.IsAny<string>()), Times.Exactly(2));
            _vectorBuilderMock.Verify(vb => vb.BuildLanguageVector(It.Is<string>(s => s == FakeSamples.SAMPLE_HU)), Times.Once);
            _sampleRepo.Verify(sr => sr.AddLanguageVectorToCache(It.Is<LanguageSample>(s => s.Sample == FakeSamples.SAMPLE_HU), It.IsAny<Alphabet>(), It.IsAny<HiDimBipolarVector>()), Times.Once);
        }

        [Test]
        // TODO: can be narrowed to LanguageSimilarityResult
        public void ResultIsOrderedBySimilarityValuesDesc()
        {
            var result = _sut.Detect("Lorem ipsum.");
            Assert.Multiple(() =>
            {
                Assert.That(_multiSample.Languages.Count, Is.EqualTo(3));
                Assert.That(result.SimilarityValues.Count, Is.EqualTo(3));
            });
            var ordered = result.SimilarityValues[0].Value >= result.SimilarityValues[1].Value
                && result.SimilarityValues[1].Value >= result.SimilarityValues[2].Value;

            Assert.That(ordered, Is.True, "resulted similarity values should be ordered");
        }
    }
}
