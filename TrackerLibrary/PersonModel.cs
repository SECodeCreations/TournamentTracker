using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class PersonModel
    {
        /// <summary>
        /// Represents a team member's First Name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents a team member's Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Represents a team member's Email Address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>                   
        /// Represents a team member's Contact Number.
        /// </summary>
        public string ContactNumber { get; set; }
    }
}
