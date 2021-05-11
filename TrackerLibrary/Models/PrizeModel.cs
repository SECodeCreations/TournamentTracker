using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize.
        /// </summary>
        public int Id { get; set; }
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



        // ----- CONSTRUCTORS ----- \\
        public PrizeModel()
        {

        }

        // This constructor creates the prize model on the fly and does all the parsing for the textboxes once.
        // Anywhere that calls it, can call this and have it parsed.
        // We don't check to see if validation passes or fails, we make sure it's a safe parse.
        // If given an invalid string, we change value to 0.
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;
            
            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
