using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkPrprtyITI.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign keys
        public int UserId { get; set; }
        public User? User { get; set; } = null!;

        public int PropertyId { get; set; }
        public Property? Property { get; set; } = null!;
    }
}
