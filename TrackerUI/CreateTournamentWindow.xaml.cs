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
    /// Interaction logic for CreateTournamentWindow.xaml
    /// </summary>
    public partial class CreateTournamentWindow : Window, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = new List<TeamModel>();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentWindow()
        {
            InitializeComponent();
            TournamentDashboardWindow.DatabaseConnectionType();
            //TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql); //TODO - remove this when app is complete (needs to be in first window only - that that this is correct!).
            LoadListData();
            WireUpLists();

        }

        private void WireUpLists()
        {
            SelectTeamDropdown.ItemsSource = null;
            SelectTeamDropdown.ItemsSource = availableTeams;

            TournamentTeamsListBox.ItemsSource = null;
            TournamentTeamsListBox.ItemsSource = selectedTeams;

            PrizesListBox.ItemsSource = null;
            PrizesListBox.ItemsSource = selectedPrizes;
        }

        private void LoadListData()
        {
            availableTeams = GlobalConfig.Connection.GetTeam_All();
        }

        private void AddTeamButton_Click(object sender, RoutedEventArgs e)
        {

            TeamModel t = (TeamModel)SelectTeamDropdown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void CreatePrizeButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the CreatePrizeWindow.
            CreatePrizeWindow frm = new CreatePrizeWindow(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model) // Get back from the form a PrizeModel.
        {
            // Take the PrizeModel and put it in the list of selected prizes.
            selectedPrizes.Add(model);
            WireUpLists();

        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void CreateNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTeamWindow frm = new CreateTeamWindow(this);
            frm.Show();
        }

        private void RemoveSelectedTeamButton_Click(object sender, RoutedEventArgs e)
        {
            TeamModel t = (TeamModel)TournamentTeamsListBox.SelectedItem;
            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }
        }

        private void RemoveSelectedPrizeButton_Click(object sender, RoutedEventArgs e)
        {
            PrizeModel p = (PrizeModel)PrizesListBox.SelectedItem;
            if (p != null)
            {
                selectedPrizes.Remove(p); //Removes prize from list but DOES NOT delete it from the database.

                WireUpLists();
            }
        }

        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate data.
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(EntryFeeTextBox.Text, out fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid entry fee (numbers only).",
                    "Invalid Fee",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            // Create our Tournament model.
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = TournamentNameTextBox.Text;
            tm.EntryFee = fee;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // Wire up the matchups.
            TournamentLogic.CreateRounds(tm);


            // Create Tournament Entry.
            // Create all of the prizes entries.

            // Create all of the team entries.
            GlobalConfig.Connection.CreateTournament(tm);
        }
    }
}
