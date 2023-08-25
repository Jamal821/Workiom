using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkiomBackendDevTest.Entities;
using WorkiomBackendDevTest.Services;

namespace WorkiomBackendDevTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }


        //we use here Pagination for performance issues.
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetAllContacts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var contacts = await _contactService.GetPagedAsync(page, pageSize);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(string id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Contact>>> SearchContacts([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name filter is required.");

            var contacts = await _contactService.GetByNameAsync(name);
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            await _contactService.CreateAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(string id, Contact contact)
        {
            if (id != contact.Id)
                return BadRequest();

            await _contactService.UpdateAsync(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
