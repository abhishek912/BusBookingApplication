using BusBooking.Data.DAL;
using BusBooking.Data.Models;
using System;
using System.Linq;

namespace BusBooking.Business.Authenticate
{
    public class Account : IAccount
    {
        private readonly IReadData readObj;
        private readonly IWriteData writeObj;
        public Account(IReadData readObj, IWriteData writeObj)
        {
            this.readObj = readObj;
            this.writeObj = writeObj;
        }

        public bool ValidateUser(string username, string password)
        {
            var credentials = readObj.GetCredentials().Where(x => x.Contact.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
            return (credentials == null) ? false : true;
        }
        
        public string GetFullName(string contact)
        {
            var fullName = readObj.GetUsers().Where(x => x.Contact.Equals(contact)).Select(x => x.FullName).SingleOrDefault();
            return fullName;
        }
        public string AddUser(User user)
        {
            if(ContactExists(user.Contact))
            {
                return "false, Contact already exists";
            }
            if(EmailExists(user.Email))
            {
                return "false, Email already exists";
            }
            var result = writeObj.AddUser(user);
            if(result)
            {
                Credential cred = new Credential { Contact = user.Contact, Password = user.Password };
                writeObj.AddCredential(cred);
                return "true, User Registered";
            }
            return "false, Error occured at database end";
        }

        private bool ContactExists(string contact)
        {
            var result = readObj.GetUsers().Where(x => x.Contact.Equals(contact)).SingleOrDefault();

            if(result != null)
            {
                return true;
            }
            return false;
        }

        private bool EmailExists(string email)
        {
            var result = readObj.GetUsers().Where(x => x.Email.Equals(email)).SingleOrDefault();

            if(result != null)
            {
                return true;
            }
            return false;
        }

        public bool IsAdmin(string contact)
        {
            var isAdmin = readObj.GetUsers().Where(x=>x.Contact.Equals(contact)).Select(x=>x.Admin).FirstOrDefault();
            return isAdmin == 1 ? true : false;
        }

        public int GetUserIdByContact(string contact)
        {
            var id = readObj.GetUsers().Where(x => x.Contact.Equals(contact)).Select(x => x.UserId).FirstOrDefault();
            return id;
        }
    }
}
