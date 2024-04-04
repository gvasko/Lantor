namespace Lantor.DomainModel
{
    public readonly struct LanguageSimilarityValue(string name, double value)
    {
        public string Name { get; init; } = name;
        public double Value { get; init; } = value;
    }

    public readonly struct LanguageSimilarityResult
    {
        public LanguageSimilarityResult(LanguageSimilarityValue[] similarityValues, long duration, int significantCount = 0)
        {
            var values = similarityValues;
            Array.Sort(values, (x, y) => x.Value < y.Value ? 1 : x.Value > y.Value ? -1 : 0);

            SimilarityValues = values;
            DurationMillisec = duration;
            SignificantCount = significantCount;
        }

        public LanguageSimilarityValue[] SimilarityValues { get; init; }

        public long DurationMillisec { get; init; }

        public int SignificantCount { get; init; }
    }
}
