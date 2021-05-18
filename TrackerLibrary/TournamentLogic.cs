using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        // Order our list randomly - we want to randomise who gets picked in the rounds.
        // Check if the list is big enough - if not, then add in byes.
        // For a tournament to work, you need "2 to the N power (i.e. 2*2*2*2*2 etc)".
        // Create our first round of matchups.
        // Create every round after that (next rounds will divide quantity of previous rounds by 2).

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomisedTeams = RandomiseTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomisedTeams.Count);

        }

        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                output += 1;
                val *= 2;
            }
            return output;

        }

        private static List<TeamModel> RandomiseTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
