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
    public partial class CreateTournamentWindow : Window
    {
        List<TeamModel> availableTeams = new List<TeamModel>();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentWindow()
        {
            InitializeComponent();
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql); //TODO - remove this when app is complete (needs to be in first window only - that that this is correct!).
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

            if(t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }
    }
}
