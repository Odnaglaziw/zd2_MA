using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PhoneBook
{
    public partial class PhoneBookManager : Form
    {
        static PhoneBook Book = new PhoneBook();
        public PhoneBookManager()
        {
            InitializeComponent();
            listView1.ItemSelectionChanged += ListView1_ItemSelectionChanged;
            tabControl1.SelectedIndex = 1;
        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string name = e.Item.SubItems[0].Text;
                string number = e.Item.SubItems[1].Text;
                prevName.Text = name;
                prevNumber.Text = number;
                newName.Text = string.Empty;
                newNumber.Text = string.Empty;
                AddBtn.Enabled = false;
                AddBtn.Visible = false;
                DelBtn.Enabled = true;
                DelBtn.Visible = true;
                UpdBtn.Enabled = true;
                UpdBtn.Visible = true;
                tabControl1.SelectedIndex = 0;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {   
            Button button = sender as Button;
            if (button != null)
            {
                Display.Text += button.Text;
                textBox4.Text = string.Empty;
                DataUpdate(Book.GetContactsBy(PhoneNumber: Display.Text));
                tabControl1.SelectedIndex = 1;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            prevName.Text = string.Empty;
            prevNumber.Text = string.Empty;
            newName.Text = string.Empty;
            newNumber.Text = string.Empty;
            AddBtn.Enabled = true;
            AddBtn.Visible = true;
            DelBtn.Enabled = false;
            DelBtn.Visible = false;
            UpdBtn.Enabled = false;
            UpdBtn.Visible = false;
            tabControl1.SelectedIndex = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {                                                                   
            tabControl1.SelectedIndex = 1;
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            Book.RemoveContact(prevName.Text, prevNumber.Text);
            DataUpdate(Book.GetAllContacts());
            tabControl1.SelectedIndex = 1;
        }

        private void UpdBtn_Click(object sender, EventArgs e)
        {
            Book.UpdateContact(prevName.Text,prevNumber.Text, newName.Text ?? null, newNumber.Text ?? null);
            DataUpdate(Book.GetAllContacts());
            tabControl1.SelectedIndex = 1;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            Contact contact = new Contact(newName.Text.Trim(),newNumber.Text.Trim());
            Book.AddContact(contact);
            DataUpdate(Book.GetAllContacts());
        }
        private void DataUpdate(List<Contact> contacts)
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(
                contacts.Select(c => new ListViewItem(new string[] {c.Name,c.PhoneNumber})).ToArray());
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Display.Text))
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
            if (string.IsNullOrEmpty(Display.Text))
            {
                DataUpdate(Book.GetAllContacts());
            }
            else
            {
                DataUpdate(Book.GetContactsBy(PhoneNumber: Display.Text));
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, @"^[a-zA-ZА-Яа-я]+$"))
                {
                    textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 1);
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
                else
                {
                    Display.Text = string.Empty;
                    DataUpdate(Book.GetContactsBy(Name: textBox4.Text));
                }
            }
            else
            {
                DataUpdate(Book.GetAllContacts());
            }
        }
        private string GetFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Файлы csv(*.csv)|*.csv";
            openFileDialog.InitialDirectory = Directory.GetParent(Application.StartupPath).Parent.FullName;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                return filePath;
            }
            return "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            PhoneBookLoader.Load(Book,GetFilePath());
            DataUpdate(Book.GetAllContacts());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PhoneBookLoader.Save(Book,GetFilePath());
        }
    }
}
