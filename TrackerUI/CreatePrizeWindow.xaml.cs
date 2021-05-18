using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for CreatePrizeWindow.xaml
    /// </summary>
    public partial class CreatePrizeWindow : Window
    {
        IPrizeRequester callingForm;

        public CreatePrizeWindow(IPrizeRequester caller)
        {
            InitializeComponent();
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile); //TODO - remove this when app is complete (needs to be in first window only - that that this is correct!).
            callingForm = caller;
        }

        private void CreatePrizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    PlaceNameTextBox.Text,
                    PlaceNumberTextBox.Text,
                    PrizeAmountTextBox.Text,
                    PrizePercentageTextBox.Text);

                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);
                this.Close();

                //PlaceNameTextBox.Text = "";
                //PlaceNumberTextBox.Text = "";
                //PrizeAmountTextBox.Text = "0";
                //PrizePercentageTextBox.Text = "0";

            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check and try again.");
            }
        }

        private bool ValidateForm()
        {
            // Validating all textboxes in the CreatePrize window.
            bool output = true;

            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(PlaceNumberTextBox.Text, out placeNumber);  //Use Tuple instead of out?
            if (placeNumberValidNumber == false)
            {
                output = false;
            }
            if (placeNumber < 1)
            {
                output = false;
            }

            if (PlaceNameTextBox.Text.Length == 0)
            {
                output = false;
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;
            bool prizeAmountValid = decimal.TryParse(PrizeAmountTextBox.Text, out prizeAmount);
            bool prizePercentageValid = double.TryParse(PrizePercentageTextBox.Text, out prizePercentage);

            if (prizeAmountValid == false || prizePercentageValid == false)
            {
                output = false;
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }
            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
