using BusBooking.Shared.DTO;
using System.Collections.Generic;


namespace BusBooking.Business.Authenticate
{
    public interface IBusBooking
    {
        List<BookingSummaryDTO> GetBookings(int userId);
        BookingDetailDTO GetBookingDetails(int BookingId);
        bool CreateBooking(BookingTicketDTO ticket);
        bool CancelBooking(int BookingId);
        bool CancelPassengersBooking(int BookingId, List<int> PsngrId, bool refund);
    }
}