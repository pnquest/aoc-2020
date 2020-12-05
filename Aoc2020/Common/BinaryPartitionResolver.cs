using System;

namespace Common
{
    public class BinaryPartitionResolver
    {
        private int _min;
        private int _max;
        public bool IsResolved => _min == _max;
        public int? Result => IsResolved ? _min : null;

        public BinaryPartitionResolver(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public enum PartitionHalf
        {
            Upper,
            Lower,
        }

        public bool Partition(PartitionHalf half)
        {
            if(IsResolved)
            {
                throw new InvalidOperationException("Cannot partition when already solved");
            }

            int move = (_max - _min + 1) / 2;
            switch(half)
            {
                case PartitionHalf.Upper:
                    _min += move;
                    break;

                case PartitionHalf.Lower:
                    _max -= move;
                    break;
            }

            return IsResolved;
        }
    }
}
