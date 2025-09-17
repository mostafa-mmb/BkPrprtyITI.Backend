using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkPrprtyITI.Core.DTOs
{
    // Response object (what we return to the client)
    public record UserDto(
        int Id,
        string FullName,
        string Email
    );

    // Request object (what client sends to create user)
    public record CreateUserDto(
        string FullName,
        string Email
    );
}
