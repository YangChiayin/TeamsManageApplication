using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamsManageApplication.DataAccess;

namespace TeamsManageApplication.Components
{
    public class UpcomingHomeGamesViewComponent : ViewComponent
    {
        private readonly TeamsDbContext _teamsDbContext;

        public UpcomingHomeGamesViewComponent(TeamsDbContext teamsDbContext)
        {
            _teamsDbContext = teamsDbContext;
        }

        public IViewComponentResult Invoke(int maxToDisplay, int numberOfWeeks)
        {
            DateTime now = DateTime.Now;
            DateTime futureDate = now.AddDays(7 * numberOfWeeks);

            var games = _teamsDbContext.Games
                    .Include(g => g.Team)
                    .Where(g => g.Date > now && g.Date <= futureDate)
                    .OrderByDescending(g => g.Date)
                    .Take(maxToDisplay)
                    .ToList();

            GamesViewModel upcomingGamesViewModel = new GamesViewModel()
            {
                Games = games,
                NumberOfWeeks = numberOfWeeks
            };

            return View(upcomingGamesViewModel);

        }
    }
}
