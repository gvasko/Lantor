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
            SimilarityValues = similarityValues;
        }

        public LanguageSimilarityValue[] SimilarityValues { get; init; }
    }
}
