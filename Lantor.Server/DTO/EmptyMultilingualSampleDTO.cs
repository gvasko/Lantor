using Lantor.DomainModel;

namespace Lantor.Server.DTO
{
    public class EmptyMultilingualSampleDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Comment { get; set; }
        public int OwnerId { get; set; }
        public List<EmptyLanguageSampleDTO>? Languages { get; set; }
    }
}
