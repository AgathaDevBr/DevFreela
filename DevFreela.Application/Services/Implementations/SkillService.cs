using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<List<SkillViewModel>> GetAllAsync()
        {
            var skills = await _skillRepository.GetAllAsync();

            return skills
                .Select(s => new SkillViewModel(s.Id, s.Description))
                .ToList();
        }
    }
}
