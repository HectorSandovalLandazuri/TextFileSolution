// See https://aka.ms/new-console-template for more information
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

IConfiguration _config;
string textFile;
TextFileDataAccess db=new TextFileDataAccess();

InitializeConfiguration();
textFile = _config.GetValue<string>("TextFile");
/*
ContactModel user1=new ContactModel();
user1.FirstName = "Héctor";
user1.LastName="Sandoval";
user1.EmailAddress.Add("hola@hector.com");
user1.EmailAddress.Add("hector@aol.com");
user1.PhoneNumbers.Add("5555-555-555");
user1.PhoneNumbers.Add("3333-555-555");
*/

ContactModel user2 = new ContactModel(); 
user2.FirstName = "Claudia";
user2.LastName="Alvarado";
user2.EmailAddress.Add("hola@claudia.com");
user2.EmailAddress.Add("claudia@aol.com");
user2.PhoneNumbers.Add("5555-555-555");
user2.PhoneNumbers.Add("1111-555-555");

/*
CreateContact(db, textFile, user1);
CreateContact(db, textFile, user2);
*/
//UpdateContactsFirstName(db, textFile, user1, "Manuel");
//RemovePhoneNumberFromUser(db, textFile, user2, "5555-555-555");
//RemoveUser(db, textFile, user2);
GetAllContacts(db, textFile);
GetContactByName(db, textFile, "Claudia", "Alvarado");

Console.WriteLine("Done with text file");

static void RemoveUser(TextFileDataAccess db, string textFile, ContactModel contact)
{
    var contacts = db.ReadAllRecords(textFile);
    List<ContactModel> newContacts = new List<ContactModel>();
    foreach (var c in contacts)
    {
        if (c.FirstName != contact.FirstName || c.LastName != contact.LastName)
        {
            newContacts.Add(c);
        }
    }
    db.WriteAllRecords(newContacts, textFile);
}

static void RemovePhoneNumberFromUser(TextFileDataAccess db, string textFile, ContactModel contact,string phoneNumberToRemove)
{
    var contacts = db.ReadAllRecords(textFile);
    foreach (var c in contacts)
    {
        if (c.FirstName == contact.FirstName && c.LastName == contact.LastName)
        {
            c.PhoneNumbers.Remove(phoneNumberToRemove);
        }
    }
    db.WriteAllRecords(contacts, textFile);

}

static void UpdateContactsFirstName(TextFileDataAccess db, string textFile, ContactModel contact,string newFirstName)
{
    var contacts = db.ReadAllRecords(textFile);
    foreach (var c in contacts)
    {
        if (c.FirstName == contact.FirstName && c.LastName == contact.LastName)
        {
            c.FirstName = newFirstName;
        }
    }
    db.WriteAllRecords(contacts, textFile);
}


static void GetContactByName(TextFileDataAccess db, string textFile, string firstName,string lastName)
{
    var contacts = db.ReadAllRecords(textFile);
    foreach (var c in contacts)
    {
        if (c.FirstName == firstName && c.LastName == lastName)
        {
            Console.WriteLine($"{c.FirstName} {c.LastName}");
        }
    }
}

static void GetAllContacts(TextFileDataAccess db,string textFile)
{
    var contacts = db.ReadAllRecords(textFile);
    foreach ( var contact in contacts )
    {
        Console.WriteLine($"{contact.FirstName} {contact.LastName}");
    }
}

static void CreateContact(TextFileDataAccess db, string textFile, ContactModel contact)
{
    var contacts = db.ReadAllRecords(textFile);
    contacts.Add(contact);
    db.WriteAllRecords(contacts,textFile);
}



void InitializeConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
    _config = builder.Build();
}

