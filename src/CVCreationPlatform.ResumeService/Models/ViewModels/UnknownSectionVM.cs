using CVCreationPlatform.Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

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
            Title = unknownSection.Title;
            Description = unknownSection.Description;
            StartDate = unknownSection.StartDate;
            EndDate = unknownSection.EndDate;
        }
    }
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
