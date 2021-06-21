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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrackerLibrary.Models;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for TournamentViewerWindow.xaml
    /// </summary>
    public partial class TournamentViewerWindow : Window
    {
        private TournamentModel tournament;
        List<int> rounds = new List<int>();
        List<MatchupModel> selectedMatchups = new List<MatchupModel>();

        public TournamentViewerWindow(TournamentModel tournamentModel)
        {
            InitializeComponent();
            tournament = tournamentModel;
            LoadRounds();
            LoadFormData();
            WireUpRoundsLists();
        }

        private void LoadFormData()
        {
            // Set the Tournament Name Label as the selected Tournament's name.
            TournamentNameLabel.Content = tournament.TournamentName;
        }

        private void WireUpRoundsLists()
        {
            // Wire up the RoundDropDown combobox.
            RoundDropdown.ItemsSource = null;
            RoundDropdown.ItemsSource = rounds;
        }

        private void WireUpMatchupList()
        {
            // Wire up the MatchupListbox.
            MatchupListbox.ItemsSource = null;
            MatchupListbox.ItemsSource = selectedMatchups;

            // Whenever you select a round, a matchup should be selected, however if one isn't then the Team 1 and Team 2
            // names will be displayed as "No matchup selected" (prevents them from showing any previously selected matchup team names).
            if (MatchupListbox.SelectedItem == null)
            {
                TeamOneNameLabel.Content = "No matchup selected";
                TeamTwoNameLabel.Content = "No matchup selected";
            }
        }

        private void LoadRounds()
        {
            rounds = new List<int>();
            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    currRound = matchups.First().MatchupRound;
                    rounds.Add(currRound);
                }
            }

            LoadMatchups(1);
        }

        private void RoundDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoundDropdown.SelectedItem == null)
            {
                RoundDropdown.SelectedIndex = 0;
            }
            else
            {
                LoadMatchups((int)RoundDropdown.SelectedItem);
            }
            WireUpMatchupList();
        }

        private void LoadMatchups(int round)
        {
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups = matchups;
                }
            }

            LoadMatchup(selectedMatchups.First());
        }

        private void LoadMatchup(MatchupModel m)
        {
            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        TeamOneNameLabel.Content = m.Entries[0].TeamCompeting.TeamName;
                        TeamOneScoreValueTextBox.Text = m.Entries[0].Score.ToString();

                        TeamTwoNameLabel.Content = "<BYE>";
                        TeamTwoScoreValueTextBox.Text = "0";
                    }
                    else
                    {
                        TeamOneNameLabel.Content = "Not yet set";
                        TeamOneScoreValueTextBox.Text = "";
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        TeamTwoNameLabel.Content = m.Entries[1].TeamCompeting.TeamName;
                        TeamTwoScoreValueTextBox.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        TeamTwoNameLabel.Content = "Not yet set";
                        TeamTwoScoreValueTextBox.Text = "";
                    }
                }
            }
        }

        private void MatchupListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MatchupListbox.SelectedItem == null)
            {
                MatchupListbox.SelectedIndex = 0;
            }
            else
            {
                LoadMatchup((MatchupModel)MatchupListbox.SelectedItem);
            }
        }
    }
}
