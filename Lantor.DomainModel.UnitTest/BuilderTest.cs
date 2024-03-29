using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel.UnitTest
{
    public class BuilderTest
    {
        [Test]
        public void TestSumBuilder()
        {
            int[] intArray1 = [7, 15];
            int[] intArray2 = [31, 63];
            int[] intArray3 = [48, 96];
            var vb = new HiDimBipolarSumBuilder(64);
            vb.Add(new HiDimBipolarVector(intArray1));
            vb.Add(new HiDimBipolarVector(intArray2));
            vb.Add(new HiDimBipolarVector(intArray3));
            var sum = vb.BuildVector();
            Assert.Multiple(() =>
            {
                Assert.That(sum[0], Is.EqualTo(-1));
                Assert.That(sum[1], Is.EqualTo(-1));
                Assert.That(sum[2], Is.EqualTo(-1));
                Assert.That(sum[3], Is.EqualTo(1));
                Assert.That(sum[4], Is.EqualTo(-1));
                Assert.That(sum[5], Is.EqualTo(1));

                Assert.That(sum[32 + 0], Is.EqualTo(-1));
                Assert.That(sum[32 + 1], Is.EqualTo(-1));
                Assert.That(sum[32 + 2], Is.EqualTo(-1));
                Assert.That(sum[32 + 3], Is.EqualTo(-1));
                Assert.That(sum[32 + 4], Is.EqualTo(1));
                Assert.That(sum[32 + 5], Is.EqualTo(-1));
                Assert.That(sum[32 + 6], Is.EqualTo(1));
            });
        }

        [Test]
        public void TestLanguageVectorBuilder()
        {
            Alphabet abc = new("Test", 128);
            LanguageVectorBuilder lvb = new(abc);
            var loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
            var v1 = lvb.BuildLanguageVector(loremIpsum);
            var v2 = lvb.BuildLanguageVector(loremIpsum);
            var eps = 0.001;
            Assert.That(v1.Similarity(v2), Is.EqualTo(1.0).Within(eps));
        }

    }
}
