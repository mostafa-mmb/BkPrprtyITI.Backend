using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkPrprtyITI.Core.DTOs
{
    // Response object (what we return to the client)
    public record PropertyDto(
        int Id,
        string Title,
        string Description,
        decimal PricePerNight,
        string Location
    );

    // Request object (what client sends to create property)
    public record CreatePropertyDto(
        string Title,
        string Description,
        decimal PricePerNight,
        string Location
    );
}
