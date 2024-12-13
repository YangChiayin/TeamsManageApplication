﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teams.Entities;
using TeamsManageApplication.Components;
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
            var team = _teamsDbContext.Teams
        .Include(t => t.Players)
        .Include(t => t.Games)
        .FirstOrDefault(t => t.TeamId == id);

            var viewModel = new TeamDetailsViewModel
            {
                Team = team,
                NewPlayer = new Player(),
                NewGame = new Game()
            };

            return View("Details", viewModel);
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

        [HttpPost("/teams/{id}/players")]
        public IActionResult AddPlayer(int id, Player player)
        {
            if (ModelState.IsValid)
            {
                player.TeamId = id;
                _teamsDbContext.Players.Add(player);
                _teamsDbContext.SaveChanges();
                return RedirectToAction("GetTeamById", new { id = id });
            }

            // If validation fails, reload the team details with the current data
            var team = _teamsDbContext.Teams
                .Include(t => t.Players)
                .Include(t => t.Games)
                .FirstOrDefault(t => t.TeamId == id);

            var viewModel = new TeamDetailsViewModel
            {
                Team = team,
                NewPlayer = player,
                NewGame = new Game()
            };

            return View("Details", viewModel);
        }
    }
}
