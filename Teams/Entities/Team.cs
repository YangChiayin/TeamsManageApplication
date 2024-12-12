using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public string? Name { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        // Nav props:
        public ICollection<Player>? Players { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
