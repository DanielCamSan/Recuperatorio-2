using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _confs;

        public ConferenceService(IConferenceRepository confs)
        {
            _confs = confs;
        }

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var conf = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Rooms = new List<Room>() // Inicializar la colección Rooms
            };

            if (dto.Rooms != null)
            {
                foreach (var r in dto.Rooms)
                {
                    conf.Rooms.Add(new Room { Name = r.Name });
                }
            }

            await _confs.AddAsync(conf);
            await _confs.SaveChangesAsync();
            return conf.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _confs.GetAgendaAsync(id);
            if (conf == null) return null;

            var dto = new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms
                            .Select(r => new RoomScheduleDto
                            {
                                Room = r.Name,
                                Talks = r.Talks
                                            .OrderBy(t => t.StartTime)
                                            .Select(t => new TalkDto
                                            {
                                                SpeakerId = t.SpeakerId,
                                                Speaker = t.Speaker.FullName,
                                                RoomId = t.RoomId,
                                                Room = r.Name,
                                                StartTime = t.StartTime,
                                                EndTime = t.EndTime
                                            }).ToList()
                            })
                            .ToList()
            };

            return dto;
        }
    }
}
