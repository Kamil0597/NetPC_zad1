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

        public bool CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            return Save();
        }

        public bool DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            return Save();
        }

        public Contact? GetContactById(int id)
        {
            return _context.Contacts.Where(contact => contact.Id == id).FirstOrDefault();
        }

        public Contact? GetContactByName(string name)
        {
            return _context.Contacts.Where(contact => contact.Name == name).FirstOrDefault();
        }

        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

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

        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
