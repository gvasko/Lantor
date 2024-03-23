using Lantor.DomainModel.Compute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    /// <summary>
    /// VO
    /// </summary>
    public class HiDimBipolarVector
    {
        //private int Size { get; init; }

        public BitArray Data { get; init; }

        public static HiDimBipolarVector CreateRandomVector(int size)
        {
            var v = new BitArray(size);

            var random = new Random();

            for (int i = 0; i < size / 2; i++)
            {
                while (true)
                {
                    var index = random.Next(size);
                    if (v[index])
                    {
                        continue;
                    }
                    else
                    {
                        v[index] = true;
                        break;
                    }
                }
            }

            return new HiDimBipolarVector(v);
        }

        public HiDimBipolarVector()
            : this(8, 0)
        {
            
        }

        public HiDimBipolarVector(int[] data)
        {
            //Size = data.Length * 32;
            Data = new BitArray(data);
        }

        public HiDimBipolarVector(byte[] data)
        {
            //Size = data.Length * 8;
            Data = new BitArray(data);
        }

        public HiDimBipolarVector(int size, int data)
        {
            //Size = size;
            Data = new BitArray(size);
            for (int i = 0; i < size; i++)
            {
                var value = (((data >> i) & 1) == 1);
                Data[i] = value;
            }
        }

        private HiDimBipolarVector(BitArray data)
        {
            //Size = data.Length;
            Data = data;
        }

        public int Length
        {
            get { return Data.Length; }
        }

        public int this[int index]
        {
            get
            {
                return IsPositive(index) ? 1 : -1;
            }
        }

        private bool IsPositive(int index)
        {
            return !Data[index];
        }

        //public NaiveHiDimBipolarVector Add(NaiveHiDimBipolarVector other)
        //{
        //    // adding 2 bipolar vectors does not make much sense
        //    throw new NotImplementedException();
        //}

        public HiDimBipolarVector Multiply(HiDimBipolarVector other)
        {
            var newData = new BitArray(Data);
            // TODO: var otherData = new BitArray(other.BinData);
            newData.Xor(other.Data);
            return new HiDimBipolarVector(newData);
        }

        public HiDimBipolarVector Permute()
        {
            var highestBit = Data[^1];
            var newData = new BitArray(Data);
            newData.LeftShift(1);
            newData[0] = highestBit;
            return new HiDimBipolarVector(newData);
        }

        public double Similarity(HiDimBipolarVector other)
        {
            if (Length != other.Length)
            {
                throw new ArgumentException("Vector dimensions not equal");
            }

            double hammingDistance = Multiply(other).NumberOfNegatives();
            return Math.Abs(hammingDistance - Length / 2) / (Length / 2);
        }

        public int NumberOfPositives()
        {
            int sum = 0;
            for (int i = 0; i < Length; ++i)
            {
                if (IsPositive(i))
                {
                    sum++;
                }
            }
            return sum;
        }

        public int NumberOfNegatives()
        {
            return Length - NumberOfPositives();
        }

        public int VectorLength()
        {
            return NumberOfPositives();
        }

    }
}
