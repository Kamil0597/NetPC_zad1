using NetPC_zadanie.Models;

namespace NetPC_zadanie.Interface
{
    public interface IContactRepository
    {
        List<Contact> GetContacts();
        Contact? GetContactById(int id);
        Contact? GetContactByName(string name);

        bool ExistsByEmail(string email);
        bool CreateContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(Contact contact);
    }
}
