using System.ComponentModel.DataAnnotations;

namespace Api
{
    public class SavedJoke
    {
        public int Id { get; set; } 

        [Required]
        public string ApiId { get; set; } = default!; 

        [Required]
        public string Description { get; set; } = default!;
    }
}
