using NetPC_zadanie.Data;
using NetPC_zadanie.Models;
using NetPC_zadanie.Services; // Używamy PasswordService do haszowania hasła

namespace NetPC_zadanie.DataInitializer
{
    public class DataInitializer
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();

                // Dodawanie przykładowych kontaktów
                if (!context.Contacts.Any())
                {
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
                        },
                        new Contact
                        {
                            Name = "Piotr",
                            Surname = "Zieliński",
                            Password = "haslo123",
                            Email = "piotr@example.com",
                            Phone = "112233445",
                            BirthDate = new DateTime(1992, 7, 10),
                            Category = Contact.CategoryEnum.Other,
                            SubCategory = "Cousin"
                        },
                        new Contact
                        {
                            Name = "Maria",
                            Surname = "Kowalczyk",
                            Password = "mariapass",
                            Email = "maria@example.com",
                            Phone = "667788990",
                            BirthDate = new DateTime(1980, 3, 22),
                            Category = Contact.CategoryEnum.Private,
                            SubCategory = null
                        }
                    );
                }

                // Dodawanie użytkownika admin
                if (!context.Users.Any())
                {
                    string hashedPassword = PasswordService.HashPassword("admin");
                    context.Users.Add(new User("admin", hashedPassword));
                }

                context.SaveChanges();
            }
        }
    }
}