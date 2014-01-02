using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class ContactRepository
    {
        private Entities db;

        public ContactRepository(Entities entities) { this.db = entities; }

        public IQueryable<Contact> FindAllSuppliers() { return db.Contacts; }

        public Contact GetContact(int id)
        {
            return db.Contacts.SingleOrDefault(contact => contact.ContactID == id);
        }
    }
}