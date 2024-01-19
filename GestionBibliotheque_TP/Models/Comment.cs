using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace GestionBibliotheque_TP.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Profession { get; set; }

        [Required]
        public string CommentText { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string? ImagePath { get; set; }
    }
}
