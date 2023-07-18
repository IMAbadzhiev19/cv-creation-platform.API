using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models.DTO;
using Data.Data;
using Data.Models.CV;
using Microsoft.EntityFrameworkCore;

namespace CVCreationPlatform.ResumeService.Implementations;

public class SkillService : ISkillService
{
    private readonly ApplicationDbContext _context;

    public SkillService(ApplicationDbContext context)
        => _context = context;

    public async Task<bool> AssignSkillToResume(Guid resumeId, SkillDTO skillDTO)
    {
        var resume = await _context.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
        if (resume == null)
            throw new ArgumentException("Invalid resume id");

        var skillToAdd = new Skill
        {
            SkillName = skillDTO.SkillName
        };

        resume.Skills.Add(skillToAdd);
        resume.Skills.FirstOrDefault(x => x.SkillName == skillDTO.SkillName)!.Resumes.Add(resume);

        await this._context.Skills.AddAsync(skillToAdd);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateSkill(int skillId, SkillDTO newSkillDto)
    {
        var skill = await _context.Skills.FindAsync(skillId);
        if (skill == null)
            throw new ArgumentException("Invalid skill id");

        skill.SkillName = newSkillDto.SkillName;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSkill(int skillId)
    {
        var skillToRemove = await _context.Skills.FindAsync(skillId);
        if (skillToRemove == null)
            throw new ArgumentException("Invalid skill id");

        _context.Skills.Remove(skillToRemove);
        await this._context.SaveChangesAsync();
        return true;
    }
}
