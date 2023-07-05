using Data.Models.Auth;

namespace Data.Models.CV
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Heading { get; set; }
        public string? Subheading { get; set; }
        public string Data { get; set; }
        public bool HasSubSections { get; set; }
        public ICollection<Section>? SubSections { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}