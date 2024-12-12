using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamsManageApplication.DataAccess;

namespace TeamsManageApplication.Components
{
    public class RecentPastGames : ViewComponent
    {
        public RecentPastGames(TeamsDbContext teamsDbContext)
        {
            _teamsDbContext = teamsDbContext;
        }

        public IViewComponentResult Invoke(int maxToDisplay, int numberOfWeeks)
        {
            DateTime now = DateTime.Now;
            DateTime oldestDate = now.Subtract(TimeSpan.FromDays(7 * numberOfWeeks));

            var games = _teamsDbContext.Games
                    .Include(g => g.Team)
                    .Where(g => g.Date < now && g.Date >= oldestDate)
                    .OrderByDescending(g => g.Date)
                    .Take(maxToDisplay)
                    .ToList();

            Console.WriteLine($"num matching games: {games.Count}");

            GamesViewModel recentGamesViewModel = new GamesViewModel()
            {
                Games = games,
                NumberOfWeeks = numberOfWeeks
            };

            return View(recentGamesViewModel);
        }


        private TeamsDbContext _teamsDbContext;
    }
}
