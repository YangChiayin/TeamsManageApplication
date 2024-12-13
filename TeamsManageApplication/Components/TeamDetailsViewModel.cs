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
    }
}
