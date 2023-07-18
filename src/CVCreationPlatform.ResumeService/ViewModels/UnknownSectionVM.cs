using CVCreationPlatform.Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models;

public class UnknownSectionVM
{
    public UnknownSectionVM()
    {
        
    }

    public UnknownSectionVM(UnknownSection? unknownSection)
    {
        if (unknownSection != null)
        {
            Id = unknownSection.Id;
            this.Title = unknownSection.Title;
            this.Description = unknownSection.Description;
            this.StartDate = unknownSection.StartDate;
            this.EndDate = unknownSection.EndDate;
        }
    }
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
