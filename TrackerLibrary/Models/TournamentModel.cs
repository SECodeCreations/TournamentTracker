using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        /// <summary>
        /// Represents the name of the tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represents the fee that players must pay for entering the tournament.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// List that contains the name of teams entered in a tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// List that contains all the prizes available for a given tournament.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// Represents the rounds taken from MatchupModel.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

    }
}
