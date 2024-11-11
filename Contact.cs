using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class Contact
    {
        public Contact(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Contact other)
            {
                return Name == other.Name && PhoneNumber == other.PhoneNumber;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hashName = Name == null ? 0 : Name.GetHashCode();
            int hashPhoneNumber = PhoneNumber == null ? 0 : PhoneNumber.GetHashCode();
            return hashName ^ hashPhoneNumber;
        }
    }
}
