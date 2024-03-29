namespace Lantor.DomainModel.UnitTest
{
    public class HiDimBipolarVectorTest
    {
        private HiDimBipolarVector _basicSut = new();

        [SetUp]
        public void Setup()
        {
            _basicSut = new HiDimBipolarVector(new int[] { 1 + 2 * 256 + 4 * 512 + 8 * 768, 16 + 32 * 256 + 64 * 512 + 128 * 768, unchecked((int)0x40000000) });
        }

        [Test]
        public void DimensionIsRoundedTo32()
        {
            Assert.That(_basicSut.Length, Is.EqualTo(96));
        }


        [Test]
        public void TestBits()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_basicSut[0], Is.EqualTo(-1));
                Assert.That(_basicSut[1], Is.EqualTo(1));
                Assert.That(_basicSut[8], Is.EqualTo(1));
                Assert.That(_basicSut[9], Is.EqualTo(-1));
                Assert.That(_basicSut[35], Is.EqualTo(1));
                Assert.That(_basicSut[36], Is.EqualTo(-1));
                Assert.That(_basicSut[37], Is.EqualTo(1));
            });
        }

        [Test]
        public void TestPermut()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_basicSut[^1], Is.EqualTo(1));
                Assert.That(_basicSut[0], Is.EqualTo(-1));
            });

            var newVector1 = _basicSut.Permute();
            Assert.Multiple(() =>
            {
                Assert.That(newVector1[^1], Is.EqualTo(-1));
                Assert.That(newVector1[0], Is.EqualTo(1));
            });

            var newVector2 = newVector1.Permute();
            Assert.Multiple(() =>
            {
                Assert.That(newVector2[^1], Is.EqualTo(1));
                Assert.That(newVector2[0], Is.EqualTo(-1));
            });
        }

        [Test]
        public void TestMultiply()
        {
            var identity = _basicSut.Multiply(_basicSut);

            var index = 0;
            for (; index < identity.Length; index++)
            {
                if (identity[index] != 1)
                {
                    break;
                }
            }

            Assert.That(identity.Length, Is.EqualTo(index));
        }

        [Test]
        public void TestSimilarity()
        {
            var vect1 = new HiDimBipolarVector(2, 0);
            var vect2 = new HiDimBipolarVector(2, 1);
            var eps = 0.0001;
            Assert.Multiple(() =>
            {
                Assert.That(vect1.Similarity(vect1), Is.EqualTo(1.0).Within(eps));
                Assert.That(vect1.Similarity(vect2), Is.EqualTo(0.0).Within(eps));
            });
        }

        [Test]
        public void TestBitRepresentation()
        {
            int[] intArray = [7, 15];
            var vect = new HiDimBipolarVector(intArray);
            Assert.Multiple(() =>
            {
                Assert.That(vect[0], Is.EqualTo(-1));
                Assert.That(vect[1], Is.EqualTo(-1));
                Assert.That(vect[2], Is.EqualTo(-1));
                Assert.That(vect[3], Is.EqualTo(1));
                Assert.That(vect[31], Is.EqualTo(1));
                Assert.That(vect[32 + 0], Is.EqualTo(-1));
                Assert.That(vect[32 + 1], Is.EqualTo(-1));
                Assert.That(vect[32 + 2], Is.EqualTo(-1));
                Assert.That(vect[32 + 3], Is.EqualTo(-1));
                Assert.That(vect[32 + 4], Is.EqualTo(1));
                Assert.That(vect[32 + 31], Is.EqualTo(1));
            });
        }
    }
}