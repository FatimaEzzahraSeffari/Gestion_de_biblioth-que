    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    namespace GestionBibliotheque_TP.Models
    {
    public class Livre
        {
            public int Id { get; set; }
            public string Titre { get; set; }
            public string Auteur { get; set; }
            public string Resume { get; set; }
            public bool EstEmprunte { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public string? ImagePath { get; set; }
        public List<Emprunt>? Emprunts { get; set; }
    }



    }


