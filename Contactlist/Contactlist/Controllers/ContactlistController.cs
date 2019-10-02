using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contactlist.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactlistController : ControllerBase
    {
        public static List<Contact> contacts = new List<Contact>();
        // GET: api/contacts
        [HttpGet]
        public IActionResult GetContacts()
        {
            Contact c = new Contact();
            
            c.id = 1;
            c.lastName = "Scheuchenpflug";
            c.firstName = "Michael";
            c.phonenumber = "0670-6080400";
            c.email = "michaelscheuchenpflug@gmx.at";
            contacts.Add(c);
            return Ok(contacts);
        }


        // POST: api/contacts
        [HttpPost]
        public IActionResult NewContact([FromBody] Contact c)
        {
            contacts.Add(c);
            return CreatedAtRoute(
                "GetSpecificItem", new { index = contacts.IndexOf(c) }, c);
        }


        // DELETE: api/contacts/[id]
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            foreach (Contact contact in contacts) // Loop through List with foreach
            {
                if (contact.id == id)
                {
                    contacts.Remove(contact);
                    return Ok("Success");
                }
            }
            return BadRequest("Invalid index");
        }

        [HttpGet]
        [Route("findByName")]
        public IActionResult GetContactByName([FromQuery]string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Parameter was empty");

            else
            {
                List<Contact> matches = new List<Contact>();
                foreach (Contact contact in contacts)
                {
                    if (contact.firstName.Equals(name) || contact.lastName.Equals(name)) matches.Add(contact);
                }
                return Ok(matches);
            }
        }
    }
}