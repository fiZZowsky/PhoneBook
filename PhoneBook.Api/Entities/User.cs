namespace PhoneBook.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string email { get; set; } = default!;
        public string password { get; set; } = default!;
    }
}
