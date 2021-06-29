using BusBooking.Data.Models;

namespace BusBooking.Business.Authenticate
{
    public interface IAccount
    {
        bool ValidateUser(string username, string password);
        string AddUser(User user);
        bool IsAdmin(string contact);
        int GetUserIdByContact(string contact);
        string GetFullName(string contact);
    }
}