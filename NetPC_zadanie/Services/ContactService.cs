using NetPC_zadanie.Interface;
using NetPC_zadanie.Models;

namespace NetPC_zadanie.Services
{
    public class ContactService
    {
        public IContactRepository _contactRepository { get; set; }

        public ContactService (IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        /*
        * Tworzy nowy kontakt.
        * Sprawdza, czy e-mail już istnieje w bazie, w przeciwnym razie dodaje kontakt.
        * Rzuca wyjątek, jeśli e-mail już istnieje.
        */
        public bool CreateContact(Contact contact)
        {
            if (_contactRepository.ExistsByEmail(contact.Email))
            {
                throw new InvalidOperationException("E-mail already exists.");
            }

            return _contactRepository.CreateContact(contact);
        }

        /*
         * Aktualizuje dane kontaktu w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool UpdateContact(Contact contact)
        {
            return _contactRepository.UpdateContact(contact);
        }

        /*
         * Usuwa kontakt z bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool DeleteContact(Contact contact)
        {
            return _contactRepository.DeleteContact(contact);
        }

        /*
         * Pobiera kontakt na podstawie nazwy.
         * Zwraca obiekt Contact lub null, jeśli nie znaleziono.
         */
        public Contact? GetContactByName(string name)
        {
            return _contactRepository.GetContactByName(name);
        }

        /*
         * Pobiera kontakt na podstawie ID.
         * Zwraca obiekt Contact lub null, jeśli nie znaleziono.
         */
        public Contact? GetContactById(int id)
        {
            return _contactRepository.GetContactById(id);
        }

        /*
         * Zwraca listę wszystkich kontaktów w bazie danych.
         */
        public List<Contact> GetAllContacts()
        {
            return _contactRepository.GetContacts();
        }
    }
}
