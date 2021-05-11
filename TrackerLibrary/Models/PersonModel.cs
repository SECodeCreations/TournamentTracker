using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Represents the ID of the person.
        /// </summary>
        public int Id { get; set; }
        
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

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
