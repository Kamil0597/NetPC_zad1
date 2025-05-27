using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NetPC_zadanie.DTO;
using NetPC_zadanie.Models;
using NetPC_zadanie.Services;

namespace NetPC_zadanie.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly ILogger<AuthController> _logger;
        public ContactsController(ContactService contactService, ILogger<AuthController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAllContacts()
        {
            return _contactService.GetAllContacts();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Contact> GetContactById(int id)
        {
            var contact =  _contactService.GetContactById(id);

            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        public ActionResult<Contact> CreateContact([FromBody] ContactDto dto)
        {
            Console.WriteLine($"Received DTO: {dto}");
            var contact = new Contact
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Password = dto.Password,
                Email = dto.Email,
                Phone = dto.Phone,
            };

            if (!DateTime.TryParse(dto.BirthDate, out var birthDate))
            {
                return BadRequest("Invalid BirthDate format");
            }
            contact.BirthDate = birthDate;

            if (!Enum.TryParse(dto.Category, out Contact.CategoryEnum category))
            {
                return BadRequest("Invalid Category value");
            }
            contact.Category = category;

            contact.SubCategory = dto.SubCategory;

            _contactService.CreateContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateContact([FromBody] ContactDto dto, int id)
        {
            _logger.LogInformation("Próba aktualizacji kontaktu o ID: {Id}", id);
            _logger.LogInformation("Otrzymany obiekt DTO: {@ContactDto}", dto);

            if (dto == null)
                return BadRequest("Nieprawidłowe dane");

            var existingContact = _contactService.GetContactById(id);
            if (existingContact == null)
            {
                return NotFound("Kontakt nie istnieje");
            }

            // Uaktualnij pola kontaktu
            existingContact.Name = dto.Name;
            existingContact.Surname = dto.Surname;
            existingContact.Password = dto.Password;
            existingContact.Email = dto.Email;
            existingContact.Phone = dto.Phone;
            if (!DateTime.TryParse(dto.BirthDate, out var birthDate))
            {
                return BadRequest("Invalid BirthDate format");
            }
            existingContact.BirthDate = birthDate;
            if (!Enum.TryParse(dto.Category, out Contact.CategoryEnum category))
            {
                return BadRequest("Invalid Category value");
            }
            existingContact.Category = category;

            existingContact.SubCategory = dto.SubCategory;

            bool success = _contactService.UpdateContact(existingContact);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "There was an error trying to update a Contact");
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteContact(int id)
        {
            var existingContact = _contactService.GetContactById(id);
            if(existingContact == null) 
                return NotFound();
            bool deleted = _contactService.DeleteContact(existingContact);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "There was an error trying to delete a Contact");
            }
        }
    }
}
