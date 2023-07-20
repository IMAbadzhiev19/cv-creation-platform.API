namespace CVCreationPlatform.ResumeService.Models.DTO
{
    public class ResumeDTO
    {
        public Guid UserId { get; set; }

        public string? Title { get; set; }

        public PersonalInfoDTO? PersonalInfo { get; set; }

        public UnknownSectionDTO? UnknownSection { get; set; }

        public TemplateDTO? Template { get; set; }
    }
}
