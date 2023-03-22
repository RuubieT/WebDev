using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Db.Dto_s.Contactform
{
    public class PostContactformDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Subject { get; set; }
        [Required] public string Description { get; set; }
    }
}
