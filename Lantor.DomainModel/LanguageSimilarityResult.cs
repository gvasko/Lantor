namespace Lantor.DomainModel
{
    public readonly struct LanguageSimilarityValue(string name, double value)
    {
        public string Name { get; init; } = name;
        public double Value { get; init; } = value;
    }

    public readonly struct LanguageSimilarityResult
    {
        public LanguageSimilarityResult(params LanguageSimilarityValue[] similarityValues)
        {
            var values = similarityValues;
            Array.Sort(values, (x, y) => x.Value < y.Value ? 1 : x.Value > y.Value ? -1 : 0);

            SimilarityValues = values;
        }

        public LanguageSimilarityValue[] SimilarityValues { get; init; }
    }
}
