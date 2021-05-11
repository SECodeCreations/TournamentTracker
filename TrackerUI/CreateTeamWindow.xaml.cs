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
using TrackerLibrary.Models;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for CreateTeamWindow.xaml
    /// </summary>
    public partial class CreateTeamWindow : Window
    {
        public CreateTeamWindow()
        {   
            InitializeComponent();
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile); //TODO - remove this when app is complete (needs to be in first window only - that that this is correct!).
        }

        private void CreateMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = FirstNameTextBox.Text;
                p.LastName = LastNameTextBox.Text;
                p.EmailAddress = EmailTextBox.Text;
                p.ContactNumber = ContactNumberTextBox.Text;

                GlobalConfig.Connection.CreatePerson(p);

                FirstNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                EmailTextBox.Text = "";
                ContactNumberTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all the fields.");
            }
        }

        private bool ValidateForm()
        {
            if (FirstNameTextBox.Text.Length == 0)
            {
                return false;
            }

            if (LastNameTextBox.Text.Length == 0)
            {
                return false;
            }

            if (EmailTextBox.Text.Length == 0)
            {
                return false;
            }

            if (ContactNumberTextBox.Text.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
