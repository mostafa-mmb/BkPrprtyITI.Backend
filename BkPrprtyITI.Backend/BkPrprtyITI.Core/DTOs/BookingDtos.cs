using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkPrprtyITI.Core.DTOs
{
    // Response object
    public record BookingDto(
        int Id,
        int UserId,
        string UserFullName,
        int PropertyId,
        string PropertyTitle,
        DateTime StartDate,
        DateTime EndDate
    );

    // Request object
    public record CreateBookingDto(
        int UserId,
        int PropertyId,
        DateTime StartDate,
        DateTime EndDate
    );
}
