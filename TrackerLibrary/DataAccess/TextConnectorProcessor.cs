using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

///<summary>
/// Load the text file
/// Convert the text to List<PrizeModel>
/// Find the max ID
/// Add the new record wit hthe new ID (max +1)
/// Convert the prizes to List<string>
/// Save the List<string> to the text file
///</summary>

namespace TrackerLibrary.DataAccess.TextHelpers
{
    // Changing the namespace (adding .TextHelpers) will ensure that only files with this using statement inherit the extra stuff and eliminate any other classes from inheriting from this and getting cluttered up with the stuff that's in here (i.e. you don't want the SqlConnector seeing this as it would never use it).
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) // file names such as PrizeModels.csv (using 'this' makes it an extension method).
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{fileName}";
        }

        public static List<string> LoadFile(this string file) // Extension method (using 'this')
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.ContactNumber = cols[4];
                output.Add(p);
            }
            return output;
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.ContactNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
