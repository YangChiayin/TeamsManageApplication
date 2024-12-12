using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams.Entities
{
    public enum GameTypeOptions { Home, Away };

    public class Game
    {
        public int GameId { get; set; }

        public DateTime? Date { get; set; }

        public string? OpposingTeamName { get; set; }

        public GameTypeOptions GameType { get; set; }


        // FK:
        public int? TeamId { get; set; }

        // And nav prop:
        public Team? Team { get; set; }
    }
}
