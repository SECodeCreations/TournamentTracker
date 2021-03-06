using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents the ID of the matchup entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the indentifier for the team.
        /// </summary>
        public int TeamCompetingId { get; set; }

        /// <summary>
        /// Represents one team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particule team.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the indentifier for the parent matchup (team).
        /// </summary>
        public int ParentMatchupId { get; set; }

        /// <summary>
        /// Represents the matchup that this team came from as the winner (previous round).
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
