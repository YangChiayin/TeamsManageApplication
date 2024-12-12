using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Entities;
using TeamsManageApplication.DataAccess;

namespace TeamsManageApplication.Controllers
{
    public class TeamController : Controller
    {
        public TeamController(TeamsDbContext teamsDbContext)
        {
            _teamsDbContext = teamsDbContext;
        }

        [HttpGet("/teams")]
        public IActionResult GetAllTeams()
        {
            var teams = _teamsDbContext.Teams
                    .Include(t => t.Players)
                    .Include(t => t.Games)
                    .OrderByDescending(t => t.DateCreated)
                    .ToList();

            return View("Items", teams);
        }

        [HttpGet("/teams/{id}")]
        public IActionResult GetTeamById(int id)
        {
            // TODO: implement this action method to query for
            // the required info for the details page and pass
            // it to that view:

            return View("Details");
        }

        [HttpGet("/teams/add-request")]
        public IActionResult GetAddTeamRequest()
        {
            return View("AddTeam", new Team());
        }

        [HttpPost("/teams")]
        public IActionResult AddNewTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamsDbContext.Teams.Add(team);
                _teamsDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The team \"{team.Name}\" was added.";

                return RedirectToAction("GetAllTeams", "Team");
            }
            else
            {
                return View("AddTeam", team);
            }
        }

        [HttpGet("/teams/{id}/edit-request")]
        public IActionResult GetEditRequestById(int id)
        {
            var team = _teamsDbContext.Teams.Find(id);
            return View("EditTeam", team);
        }

        [HttpPost("/teams/edit-requests")]
        public IActionResult ProcessEditRequest(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamsDbContext.Teams.Update(team);
                _teamsDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The team \"{team.Name}\" was updated.";

                return RedirectToAction("GetAllTeams", "Team");
            }
            else
            {
                return View("EditTeam", team);
            }
        }

        [HttpGet("/teams/{id}/delete-request")]
        public IActionResult GetDeleteRequestById(int id)
        {
            var team = _teamsDbContext.Teams.Find(id);
            return View("DeleteConfirmation", team);
        }

        [HttpPost("/teams/{id}/delete-requests")]
        public IActionResult ProcessDeleteRequestById(int id)
        {
            var team = _teamsDbContext.Teams.Find(id);

            _teamsDbContext.Teams.Remove(team);
            _teamsDbContext.SaveChanges();

            TempData["LastActionMessage"] = $"The team \"{team.Name}\" was deleted.";

            return RedirectToAction("GetAllTeams", "Team");
        }

        private TeamsDbContext _teamsDbContext;
    }
}
