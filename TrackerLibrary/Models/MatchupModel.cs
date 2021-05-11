using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents the list of the two teams competing in match.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Represents the winner of the match (Taken from TeamModel).
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the Round in which the matchup is taking place.
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
