using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using System.ComponentModel.DataAnnotations;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        //TODO
        private readonly IConferenceRepository _confs;

        public ConferenceService(IConferenceRepository conferences) => _confs = conferences;

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            //TODO
            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name }).ToList()
            };
            await _confs.AddAsync(entity);
            await _confs.SaveChangesAsync();
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
