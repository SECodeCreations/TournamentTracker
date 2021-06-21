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
        /// Represents the ID of the matchup.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the list of the two teams competing in match.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// The ID from the database that will be used to identify the winner.
        /// </summary>
        public int WinnerId { get; set; }
        
        /// <summary>
        /// Represents the winner of the match (Taken from TeamModel).
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the Round in which the matchup is taking place.
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";

                foreach (MatchupEntryModel me in Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = me.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += $" vs. {me.TeamCompeting.TeamName}";
                        }
                    }
                    else
                    {
                        output = "Matchup not yet determined.";
                        break;
                    }
                }
                return output;
            }
        }
    }
}
