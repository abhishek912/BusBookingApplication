using BusBooking.Data.Models;
using BusBooking.Shared.DTO;
using System.Collections.Generic;

namespace BusBooking.Business.Authenticate
{
    public interface IBusOperator
    {
        List<BusServiceProviderDTO> GetBusByOperator(int id);
        BusDTO GetBusDetailsByBusId(int BusId);
        bool EditBusDetails(BusDTO bus);
        string CreateBus(BusDTO bus);
        bool RemoveBus(int BusId);
        List<BusBookingDTO> GetBookingsOfBus(int busId);
    }
}