namespace Lantor.DomainModel
{
    public class LanguageSample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sample { get; set; }

        public LanguageSample()
        {
            Name = "";
            Sample = "";
        }

        public LanguageSample(string name, string sample)
        {
            Name = name;
            Sample = sample;
        }
    }
}
