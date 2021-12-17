using System.ComponentModel.DataAnnotations;

namespace WebApi6_0.Models.InputDto
{
    public class DemoDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
