using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        /// <summary>
        /// The unique identifier for a team.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the name of a team.
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// List that contains all members of a team taken from PersonModel.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();



    }
}
