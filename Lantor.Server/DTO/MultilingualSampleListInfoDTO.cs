namespace Lantor.Server.DTO
{
    public class MultilingualSampleListInfoDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Comment { get; set; }
        public int LanguageCount { get; set; }
        public int OwnerId { get; set; }
    }
}
