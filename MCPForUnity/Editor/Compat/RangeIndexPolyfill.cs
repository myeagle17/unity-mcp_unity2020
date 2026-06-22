// Polyfill for System.Index / System.Range so C# 8 range/index syntax (x[1..], ^1)
// compiles on Unity 2020.3 (.NET Standard 2.0, which lacks these types).
// Disabled automatically on Unity 2021.2+ where the BCL provides them.
#if !UNITY_2021_2_OR_NEWER
namespace System
{
    internal readonly struct Index : IEquatable<Index>
    {
        private readonly int _value;

        public Index(int value, bool fromEnd = false)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Non-negative number required.");
            _value = fromEnd ? ~value : value;
        }

        private Index(int value) { _value = value; }

        public static Index Start => new Index(0);
        public static Index End => new Index(~0);
        public static Index FromStart(int value) => new Index(value, false);
        public static Index FromEnd(int value) => new Index(value, true);

        public int Value => _value < 0 ? ~_value : _value;
        public bool IsFromEnd => _value < 0;

        public int GetOffset(int length)
        {
            int offset = _value;
            if (_value < 0) offset += length + 1; // from end
            return offset;
        }

        public static implicit operator Index(int value) => new Index(value, false);

        public bool Equals(Index other) => _value == other._value;
        public override bool Equals(object obj) => obj is Index other && _value == other._value;
        public override int GetHashCode() => _value;
    }

    internal readonly struct Range : IEquatable<Range>
    {
        public Index Start { get; }
        public Index End { get; }

        public Range(Index start, Index end) { Start = start; End = end; }

        public static Range StartAt(Index start) => new Range(start, Index.End);
        public static Range EndAt(Index end) => new Range(Index.Start, end);
        public static Range All => new Range(Index.Start, Index.End);

        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start = Start.GetOffset(length);
            int end = End.GetOffset(length);
            return (start, end - start);
        }

        public bool Equals(Range other) => Start.Equals(other.Start) && End.Equals(other.End);
        public override bool Equals(object obj) => obj is Range other && Equals(other);
        public override int GetHashCode() => Start.GetHashCode() * 31 + End.GetHashCode();
    }
}
#endif
