namespace NetPC_zadanie.Models
{
    public class Contact
    {
        public enum CategoryEnum
        {
            Work,
            Private,
            Other
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public CategoryEnum Category { get; set; }
        public string? SubCategory { get; set; }
        public Contact() { }
    }
}
