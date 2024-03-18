using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lantor.DomainModel.Compute
{
    internal class NaiveHiDimBipolarVector
    {
        private int _size;
        private BitArray _data;

        [JsonInclude]
        private int[] BinData
        {
            get
            {
                var intArray = new int[_size / 32 + (_size % 32 == 0 ? 0 : 1)];
                _data.CopyTo(intArray, 0);
                return intArray;
            }

            set
            {
                SetBitData(value);
            }
        }

        public static NaiveHiDimBipolarVector CreateRandomVector(int size)
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

            return new NaiveHiDimBipolarVector(v);
        }

        public NaiveHiDimBipolarVector()
            : this(32)
        {
        }

        public NaiveHiDimBipolarVector(int size)
        {
            _size = size;
            _data = new BitArray(size);
        }

        public NaiveHiDimBipolarVector(int[] data)
        {
            SetBitData(data);
        }

        private void SetBitData(int[] data)
        {
            _size = data.Length * 32;
            _data = new BitArray(data);
        }

        /// <summary>
        /// For low dimensions
        /// </summary>
        /// <param name="size"></param>
        /// <param name="data"></param>
        public NaiveHiDimBipolarVector(int size, int data)
        {
            _size = size;
            _data = new BitArray(size);
            for (int i = 0; i < size; i++)
            {
                var value = (((data >> i) & 1) == 1);
                _data[i] = value;
            }
        }

        private NaiveHiDimBipolarVector(BitArray data)
        {
            _size = data.Length;
            _data = data;
        }

        public int Length
        {
            get { return _data.Length; }
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
            return !_data[index];
        }

        //public NaiveHiDimBipolarVector Add(NaiveHiDimBipolarVector other)
        //{
        //    // adding 2 bipolar vectors does not make much sense
        //    throw new NotImplementedException();
        //}

        public NaiveHiDimBipolarVector Multiply(NaiveHiDimBipolarVector other)
        {
            var newData = new BitArray(_data);
            // TODO: var otherData = new BitArray(other.BinData);
            newData.Xor(other._data);
            return new NaiveHiDimBipolarVector(newData);
        }

        public NaiveHiDimBipolarVector Permute()
        {
            var highestBit = _data[^1];
            var newData = new BitArray(_data);
            newData.LeftShift(1);
            newData[0] = highestBit;
            return new NaiveHiDimBipolarVector(newData);
        }

        public double Similarity(NaiveHiDimBipolarVector other)
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
