using System.Text.Json.Serialization;
using WebMVC.Common;

namespace WebMVC.Dto
{
    public class EmployeeDto
    {

        [JsonConverter(typeof(HashIdJsonConverter))]
        public int Id { get; set; }

        public string Name { get; set; }

        public string BankCardDisplay { get; set; }

        public string MoneryDisplay { get; set; }

        public string other { get; set; }
    }
}
