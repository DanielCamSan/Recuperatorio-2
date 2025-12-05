using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferences;

        public ConferenceService(IConferenceRepository conferences) => _conferences = conferences;

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var entity = new Conference { Title = dto.Title, City = dto.City, StartDate = dto.StartDate, EndDate = dto.EndDate };
            await _conferences.AddAsync(entity);
            await _conferences.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
           
        }
    }
}
