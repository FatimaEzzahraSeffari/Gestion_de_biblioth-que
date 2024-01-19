namespace GestionBibliotheque_TP.Models
{
    public class UserRegister
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRegister ToUser()
        {
            return new UserRegister
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }
    }
}

