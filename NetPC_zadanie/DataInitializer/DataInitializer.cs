using NetPC_zadanie.Data;
using NetPC_zadanie.Models;

namespace NetPC_zadanie.DataInitializer
{
    public class DataInitializer
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (context.Contacts.Any()) return;

                context.Contacts.AddRange(
                    new Contact
                    {
                        Name = "Jan",
                        Surname = "Kowalski",
                        Password = "testoweHaslo1",
                        Email = "jan@example.com",
                        Phone = "123456789",
                        BirthDate = new DateTime(1990, 1, 1),
                        Category = Contact.CategoryEnum.Private,
                        SubCategory = null
                    },
                    new Contact
                    {
                        Name = "Anna",
                        Surname = "Nowak",
                        Password = "testoweHaslo2",
                        Email = "anna@example.com",
                        Phone = "987654321",
                        BirthDate = new DateTime(1985, 5, 15),
                        Category = Contact.CategoryEnum.Work,
                        SubCategory = "Boss"
                    }
                );

                context.SaveChanges();
            }    
        }
    }
}
