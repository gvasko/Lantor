namespace Lantor.DomainModel
{
    public class LanguageSample(string name, string sample)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Sample { get; set; } = sample;
    }
}
