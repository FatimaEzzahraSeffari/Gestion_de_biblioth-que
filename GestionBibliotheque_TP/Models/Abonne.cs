    using System.ComponentModel.DataAnnotations.Schema;
    namespace GestionBibliotheque_TP.Models
    {



        public class Abonne
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }

            public List<Emprunt>? Emprunts { get; set; }
        }


    }



