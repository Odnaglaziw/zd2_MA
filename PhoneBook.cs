using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhoneBook
{
    public class PhoneBook
    {
        public PhoneBook()
        {
            Contacts = new List<Contact>();
        }
        public PhoneBook(List<Contact> contacts)
        {
            Contacts = contacts;
        }

        private List<Contact> Contacts;
        public List<Contact> GetAllContacts()
        {
            SortContactsByName();
            return Contacts;
        }
        public List<Contact> GetContactsBy(string Name = null, string PhoneNumber = null)
        {
            SortContactsByName();
            if (!string.IsNullOrEmpty(Name))
            {
                return Contacts.Where(contact => contact.Name.Contains(Name)).ToList();
            }
            else if (!string.IsNullOrEmpty(PhoneNumber))
            {
                return Contacts.Where(contact => contact.PhoneNumber.Contains(PhoneNumber)).ToList();
            }
            else
            {
                return new List<Contact>();
            }
        }
        public bool AddContact(Contact contact)
        {
            SortContactsByName();
            if (Contacts.Contains(contact))
            {
                return false;
            }else{
            Contacts.Add(contact);
                return true;
            }
        }
        public bool AddContact(string Name, string PhoneNumber)
        {
            SortContactsByName();
            Contact contact = new Contact(Name, PhoneNumber);
            if (Contacts.Contains(contact))
            {
                return false;
            }
            else
            {
                Contacts.Add(contact);
                return true;
            }
        }
        public bool RemoveContact(Contact contact)
        {
            SortContactsByName();
            if (!Contacts.Contains(contact))
            {
                return false;
            }
            else
            {
                Contacts.Remove(contact);
                return true;
            }
        }
        public bool RemoveContact(string Name, string PhoneNumber)
        {
            SortContactsByName();
            Contact contact = new Contact(Name, PhoneNumber);
            if (!Contacts.Contains(contact))
            {
                return false;
            }
            else
            {
                Contacts.Remove(contact);
                return true;
            }
        }
        public bool UpdateContact(string existingName, string existingPhoneNumber, string newName = null, string newPhoneNumber = null)
        {
            SortContactsByName();
            var contact = Contacts.FirstOrDefault(c =>
                c.Name.Equals(existingName, StringComparison.OrdinalIgnoreCase) &&
                c.PhoneNumber == existingPhoneNumber);

            if (contact == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(newName))
            {
                contact.Name = newName;
            }
            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                contact.PhoneNumber = newPhoneNumber;
            }

            return true;
        }
        private void SortContactsByName()
        {
            Contacts = Contacts.OrderBy(contact => contact.Name).ToList();
        }
    }
}
