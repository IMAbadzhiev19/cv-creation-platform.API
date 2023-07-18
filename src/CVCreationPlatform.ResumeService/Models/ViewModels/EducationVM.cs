using Data.Models.CV;

namespace CVCreationPlatform.ResumeService.Models.ViewModels;

public class EducationVM
{
    public EducationVM()
    {

    }
    public EducationVM(Education education)
    {
        Id = education.Id;
        InstituteName = education.InstituteName;
        Degree = education.Degree;
        FieldOfStudy = education.FieldOfStudy;
        StartDate = education.StartDate;
        EndDate = education.EndDate;
    }

    public int Id { get; set; }
    public string? InstituteName { get; set; }

    public string? Degree { get; set; }

    public string? FieldOfStudy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
