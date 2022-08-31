using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());   
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContectRequest)
        {
            var contact = dbContext.Contacts.Find(id);

            if (contact != null)
            {
                contact.FullName = updateContectRequest.FullName;
                contact.Address = updateContectRequest.Address;
                contact.Phone = updateContectRequest.Phone;
                contact.Email = updateContectRequest.Email;

                dbContext.SaveChanges();
                return Ok(contact);
            }

            return NotFound();
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);

            if (contact != null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
                return Ok(contact);
            }

            return NotFound();
        }
    }
}
