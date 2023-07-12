using CVCreationPlatform.Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class UnknownSectionDTO
{
    public UnknownSectionDTO()
    {
        
    }

    public UnknownSectionDTO(UnknownSection unknownSection)
    {
        if (unknownSection != null)
        {
            this.Title = unknownSection.Title;
            this.Description = unknownSection.Description;
            this.StartDate = unknownSection.StartDate;
            this.EndDate = unknownSection.EndDate;
        }
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
