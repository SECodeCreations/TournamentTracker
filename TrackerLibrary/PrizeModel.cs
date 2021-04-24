using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// Represents the place number of the prize.
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Represents the name of the place (e.g. Champion, First runner up, 2nd Place, etc.).
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Represents the prize money amount as a decimal for each place.
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Represents the prize money distribution as a percentage for each place.
        /// </summary>
        public double PrizePercentage { get; set; }
    }
}
