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
using TrackerLibrary;
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
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || UnplayedOnlyCheckbox.IsChecked == false)
                        {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }

            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First());
            }

            DisplayMatchupInfo();
            WireUpMatchupList();

        }

        private void DisplayMatchupInfo()
        {
            //TODO - see how this can be changed to be neater code - look into binding for WPF (maybe the items have to be wrapped in bindable object).
            bool isVisible = (selectedMatchups.Count > 0);

            if (selectedMatchups.Count > 0)
            {
                TeamOneNameLabel.Visibility = Visibility.Visible;
                TeamOneScoreLabel.Visibility = Visibility.Visible;
                TeamOneScoreValueTextBox.Visibility = Visibility.Visible;

                VersusLabel.Visibility = Visibility.Visible;
                ScoreButton.Visibility = Visibility.Visible;

                TeamTwoNameLabel.Visibility = Visibility.Visible;
                TeamTwoScoreLabel.Visibility = Visibility.Visible;
                TeamTwoScoreValueTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                TeamOneNameLabel.Visibility = Visibility.Hidden;
                TeamOneScoreLabel.Visibility = Visibility.Hidden;
                TeamOneScoreValueTextBox.Visibility = Visibility.Hidden;

                VersusLabel.Visibility = Visibility.Hidden;
                ScoreButton.Visibility = Visibility.Hidden;

                TeamTwoNameLabel.Visibility = Visibility.Hidden;
                TeamTwoScoreLabel.Visibility = Visibility.Hidden;
                TeamTwoScoreValueTextBox.Visibility = Visibility.Hidden;
            }
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
                        TeamTwoScoreValueTextBox.Text = "";
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

        private void UnplayedOnlyCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            LoadMatchups((int)RoundDropdown.SelectedItem);
        }

        private void ScoreButton_Click(object sender, RoutedEventArgs e)
        {
            MatchupModel m = (MatchupModel)MatchupListbox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(TeamOneScoreValueTextBox.Text, out teamOneScore);

                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for Team 1");
                            return;
                        }
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(TeamTwoScoreValueTextBox.Text, out teamTwoScore);

                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for Team 2");
                            return;
                        }
                    }
                }
            }
            if (teamOneScore > teamTwoScore)
            {
                // Team one is the winner
                // Take Team Entry and put it into the matchup as the winner.
                m.Winner = m.Entries[0].TeamCompeting;
            }
            else if (teamTwoScore > teamOneScore)
            {
                m.Winner = m.Entries[1].TeamCompeting;
            }
            else
            {
                MessageBox.Show("Games cannot result in a draw.");
            }

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                foreach (MatchupModel rm in round)
                {
                    foreach (MatchupEntryModel me in rm.Entries)
                    {
                        if (me.ParentMatchup != null)
                        {
                            if (me.ParentMatchup.Id == m.Id)
                            {
                                me.TeamCompeting = m.Winner;
                                GlobalConfig.Connection.UpdateMatchup(rm);
                            } 
                        }
                    }
                }
            }

            LoadMatchups((int)RoundDropdown.SelectedItem);

            GlobalConfig.Connection.UpdateMatchup(m);
        }
    }
}
