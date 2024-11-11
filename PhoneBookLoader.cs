using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    static class PhoneBookLoader
    {
        public static void Load(PhoneBook phoneBook, string fileName)
        {
            if (!File.Exists(fileName) || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                var data = line.Split(';');
                if (data.Length == 2)
                {
                    string name = data[0];
                    string phoneNumber = data[1];
                    phoneBook.AddContact(new Contact(name, FormatPhoneNumber(phoneNumber)));
                }
            }
        }
        public static void Save(PhoneBook phoneBook, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            StringBuilder csvContent = new StringBuilder();
            foreach (var contact in phoneBook.GetAllContacts())
            {
                csvContent.AppendLine($"{contact.Name};{contact.PhoneNumber}");
            }

            File.WriteAllText(fileName, csvContent.ToString());
        }
        static private string FormatPhoneNumber(string phoneNumber)
        {
            var formattedNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());
            return formattedNumber;
        }
    }
}
