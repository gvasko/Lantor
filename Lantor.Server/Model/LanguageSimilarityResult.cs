namespace Lantor.Server.Model
{
    public class LanguageSimilarityValue
    {
        public LanguageSimilarityValue(string? name, double value)
        {
            Name = name;
            Value = value;
        }

        public string? Name { get; set; }
        public double Value { get; set; }
    }
    public class LanguageSimilarityResult
    {
        public LanguageSimilarityResult(params LanguageSimilarityValue[] similarityValues)
        {
            SimilarityValues = similarityValues;
        }

        public LanguageSimilarityValue[]? SimilarityValues { get; set; }
    }
}
