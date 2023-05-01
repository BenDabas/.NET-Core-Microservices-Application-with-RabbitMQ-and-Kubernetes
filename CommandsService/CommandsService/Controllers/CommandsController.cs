using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/commands/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> GetCommandsForPlatform: {platformId}");

            if (!_repository.PlatformExists(platformId))
                return NotFound(); // Status 404.

            var commands = _repository.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        // Path: api/commands/{platformId}/commands/commandId.
        [HttpGet("{commandId}", Name = "GetCommandForPlatform")] // Name for CreatedAtRoute refference.
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> GetCommandForPlatform: {platformId} / {commandId}");

            if (!_repository.PlatformExists(platformId))
                return NotFound(); // Status 404.

            var command = _repository.GetCommand(platformId, commandId);

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> CreateCommandForPlatform: {platformId}");

            if (!_repository.PlatformExists(platformId))
                return NotFound(); // Status 404.

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                    new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }

    }
}
