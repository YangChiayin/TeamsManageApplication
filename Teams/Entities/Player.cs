using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Teams.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Please enter a league registration number")]
        [RegularExpression(@"^[A-Za-z]{2}-\d{8}$", ErrorMessage = "League number must in a format \"AB-12345678\" - please try again.")]
        public string? LeagueRegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter a jersey number")]
        [Range(1, 99, ErrorMessage = "jersey number must be between 1 and 99.")]
        public int? Number { get; set; }

        public string? FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        // FK:
        public int? TeamId { get; set; }

        // And nav prop:
        public Team? Team { get; set; }
    }
}
