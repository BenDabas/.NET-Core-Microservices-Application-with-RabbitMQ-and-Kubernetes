using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Commandline { get; set; }
        public int PlatformId { get; set; }
    }
}
