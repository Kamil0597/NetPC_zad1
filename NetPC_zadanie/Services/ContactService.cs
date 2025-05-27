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

        public bool CreateContact(Contact contact)
        {
            return _contactRepository.CreateContact(contact);
        }
        public bool UpdateContact(Contact contact)
        {
            return _contactRepository.UpdateContact(contact);
        }
        public bool DeleteContact(Contact contact)
        {
            return _contactRepository.DeleteContact(contact);
        }
        public Contact? GetContactByName(string name)
        {
            return _contactRepository.GetContactByName(name);
        }
        public Contact? GetContactById(int id)
        {
            return _contactRepository.GetContactById(id);
        }
        public List<Contact> GetAllContacts()
        {
            return _contactRepository.GetContacts();
        }
    }
}
