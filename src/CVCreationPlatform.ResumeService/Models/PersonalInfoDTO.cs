using Data.Models.CV;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CVCreationPlatform.ResumeService.Models;

public class PersonalInfoDTO
{
    public PersonalInfoDTO()
    {
        
    }
    public PersonalInfoDTO(PersonalInfo personalInfo)
	{
		if (personalInfo != null)
		{
            this.PhotoUrl = personalInfo.PhotoUrl;
            this.FullName = personalInfo.FullName;
			this.Description = personalInfo.Description;
            this.Address = personalInfo.Address;
            this.PhoneNumber = personalInfo.PhoneNumber;
            this.Email = personalInfo.Email;
        }
	}

	public string? PhotoUrl { get; set; }

	[Unicode(false)]
	public string? FullName { get; set; }

	public string? Description { get; set; }

	[Unicode(false)]
	public string? Address { get; set; }

	[Unicode(false)]
	public string? PhoneNumber { get; set; }

	[Unicode(false)]
	public string? Email { get; set; }

}
