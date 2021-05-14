using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel model);
        TeamModel CreateTeam(TeamModel model);
        List<PersonModel> GetPerson_All();
        
        
    }
}


// Think of an interface as a contract.
// In an interface you only ever have public items (as you would in a contract).
// You wouldn't have hidden items in a contract - only public.

// You don't put code in an interface.
// Whoever impliments IDataConnection will have all methods.