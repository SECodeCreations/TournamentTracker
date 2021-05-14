using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    /// <summary>
    /// Interaction logic for CreateTeamWindow.xaml
    /// </summary>
    public partial class CreateTeamWindow : Window
    {

        //private BindableCollection<PersonModel> availableTeamMembers = new BindableCollection<PersonModel>();
        //private BindableCollection<PersonModel> selectedTeamMembers = new BindableCollection<PersonModel>();
        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        
        public CreateTeamWindow()
        {
            InitializeComponent();
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile); //TODO - remove this when app is complete (needs to be in first window only - that that this is correct!).

            //CreateSampleData();
            LoadListData();
            WireUpLists();

            RemoveSelectedMemberButton.IsEnabled = false;

        }

        private void WireUpLists()
        {
            SelectTeamMemberDropdown.ItemsSource = null;
            //SelectTeamMemberDropdown.DataContext = availableTeamMembers;
            SelectTeamMemberDropdown.ItemsSource = availableTeamMembers;

            TeamMembersListBox.ItemsSource = null;
            //TeamMembersListBox.DataContext = selectedTeamMembers;
            TeamMembersListBox.ItemsSource = selectedTeamMembers;
        }

        private void LoadListData()
        {
            availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Steve", LastName = "Ellison" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Jimmy", LastName = "James" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Amy", LastName = "Lee" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Adrian", LastName = "Jones" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Donald", LastName = "McDonald" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Ragnar", LastName = "Smith" });
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

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeamMembers.Add(p);

                WireUpLists();

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

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = (PersonModel)SelectTeamMemberDropdown.SelectedItem;

            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists(); 
            }
        }

        private void RemoveSelectedMemberButton_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = (PersonModel)TeamMembersListBox.SelectedItem;
            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists(); 
            }
        }

        private void TeamMembersListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Enables the Remove Selected button only if an item is selected from the TeamMembersListBox.
            if (TeamMembersListBox.SelectedItem != null)
            {
                RemoveSelectedMemberButton.IsEnabled = true;
            }
            else
            {
                RemoveSelectedMemberButton.IsEnabled = false;
            }
        }

        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = TeamNameTextBox.Text;
            t.TeamMembers = selectedTeamMembers;

            t = GlobalConfig.Connection.CreateTeam(t);

            //TODO - If we aren't closing this form after creating a team, reset the form.
        }
    }
}
