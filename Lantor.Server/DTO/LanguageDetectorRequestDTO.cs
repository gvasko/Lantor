namespace Lantor.Server.DTO
{
    public class LanguageDetectorRequestDTO
    {
        public string? Text { get; set; }
        public int SampleId { get; set; }
        public int AlphabetId { get; set; }
    }
}
