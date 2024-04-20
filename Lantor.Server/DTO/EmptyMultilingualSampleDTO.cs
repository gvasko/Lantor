using Lantor.DomainModel;

namespace Lantor.Server.DTO
{
    public class EmptyMultilingualSampleDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<EmptyLanguageSampleDTO>? Languages { get; set; }
    }
}
