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
    /// Interaction logic for TournamentDashboardWindow.xaml
    /// </summary>
    public partial class TournamentDashboardWindow : Window
    {
        private List<TournamentModel> tournaments = new List<TournamentModel>();

        public TournamentDashboardWindow()
        {
            DatabaseConnectionType();
            InitializeComponent();
            LoadListData();
            WireUpLists();
        }

        public static void DatabaseConnectionType()
        {
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);
        }

        private void LoadListData()
        {
            tournaments = GlobalConfig.Connection.GetTournament_All();
        }

        private void WireUpLists()
        {
            LoadExistingTournamentDropdown.ItemsSource = null;
            LoadExistingTournamentDropdown.ItemsSource = tournaments;
        }

    }
}
