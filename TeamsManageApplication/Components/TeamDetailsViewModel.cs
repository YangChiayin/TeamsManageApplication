using System.Diagnostics.Metrics;
using Teams.Entities;

namespace TeamsManageApplication.Components
{
    public class TeamDetailsViewModel
    {
        // a. The active Team entity
        public Team? Team { get; set; }

        // b. A potential New Player object
        public Player? NewPlayer { get; set; }

        // c. A potential New Game object
        public Game? NewGame { get; set; }

        // d. The relevant counts
        public int PlayerCount => Team?.Players?.Count ?? 0;
        public int GameCount => Team?.Games?.Count ?? 0;

        // Counts only the games where the GameType is Home.
        public int HomeGameCount => Team?.Games?.Count(g => g.GameType == GameTypeOptions.Home) ?? 0;

        // Counts only the games where the GameType is Away
        public int AwayGameCount => Team?.Games?.Count(g => g.GameType == GameTypeOptions.Away) ?? 0;
    }
}
