using System.Collections.Generic;
using BusBooking.Shared.DTO;

namespace BusBooking.Business.Authenticate
{
    public interface IHome
    {
        List<BusDTO> GetSearchResults(string Source, string Destination, string Date, string Time, string TimeType);
    }
}