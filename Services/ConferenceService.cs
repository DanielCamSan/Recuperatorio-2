using _3ecexamen.DTOs;
using _3ecexamen.Entities;
using _3ecexamen.Repositories;

namespace _3ecexamen.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        public ConferenceService(IConferenceRepository conferenceRepository)=> _conferenceRepository = conferenceRepository;

        public async Task<int> CreateConferenceAsync(CreateConferenceDto dto)
        {
            var entity = new Conference
            {
                Title = dto.Title,
                City = dto.City,
                StartDate = DateTime.SpecifyKind(dto.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(dto.EndDate, DateTimeKind.Utc),
                Rooms = dto.Rooms.Select(r => new Room { Name = r.Name }).ToList()
            };
            await _conferenceRepository.AddAsync(entity);
            await _conferenceRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ConferenceAgendaDto?> GetAgendaAsync(int id)
        {
            var conf = await _conferenceRepository.GetAgendaAsync(id);
            if (conf == null) return null;
            //TODO  pista: devuelve usando ConferenceAgendaDto
            return new ConferenceAgendaDto
            {
                Conference = conf.Title,
                City = conf.City,
                Rooms = conf.Rooms.OrderBy(r => r.Name)
                    .Select(r => new RoomScheduleDto
                    {
                        Room = r.Name,
                        Talks = r.Talks.OrderBy(t => t.StartTime)
                            .Select(t => new TalkDto
                            {
                                SpeakerId = t.SpeakerId,
                                Speaker = t.Speaker.FullName,
                                RoomId = t.RoomId,
                                Room = r.Name,
                                StartTime = t.StartTime,
                                EndTime = t.EndTime
                            }).ToList()
                    }).ToList()
            };
           
        }
    }
}
