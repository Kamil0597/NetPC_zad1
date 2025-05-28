using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetPC_zadanie.Data;
using NetPC_zadanie.Interface;
using NetPC_zadanie.Models;

namespace NetPC_zadanie.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
         * Dodaje nowy kontakt do bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            return Save();
        }

        /*
         * Usuwa kontakt z bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            return Save();
        }

        /*
         * Sprawdza, czy kontakt o podanym adresie email już istnieje w bazie.
         * Zwraca true, jeśli istnieje.
         */
        public bool ExistsByEmail(string email)
        {
            return _context.Contacts.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /*
         * Pobiera kontakt na podstawie ID.
         * Zwraca obiekt Contact lub null, jeśli nie znaleziono.
         */
        public Contact? GetContactById(int id)
        {
            return _context.Contacts.Where(contact => contact.Id == id).FirstOrDefault();
        }

        /*
         * Pobiera kontakt na podstawie nazwy.
         * Zwraca obiekt Contact lub null, jeśli nie znaleziono.
         */
        public Contact? GetContactByName(string name)
        {
            return _context.Contacts.Where(contact => contact.Name == name).FirstOrDefault();
        }

        /*
         * Zwraca listę wszystkich kontaktów w bazie danych.
         */
        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        /*
         * Aktualizuje dane kontaktu w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool UpdateContact(Contact contact)
        {
            var existingContact = _context.Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null) return false;

            existingContact.Name = contact.Name;
            existingContact.Surname = contact.Surname;
            existingContact.Password = contact.Password;
            existingContact.Email = contact.Email;
            existingContact.Phone = contact.Phone;
            existingContact.BirthDate = contact.BirthDate;
            existingContact.Category = contact.Category;
            existingContact.SubCategory = contact.SubCategory;

            return Save();
        }

        /*
         * Zapisuje zmiany w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
