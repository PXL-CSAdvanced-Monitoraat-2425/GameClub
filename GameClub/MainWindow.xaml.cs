using GameClubClassLib.DataAccess;
using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameClub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // check if user is one of admin list
            if (UserData.IsValidLogin(NameTextBox.Text, PasswordPasswordBox.Password))
            {
                string boardgamefilename = "";
                string videogamefilename = "";

                MessageBox.Show("Select the Board Game csv file.", "Select CSV file");
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    boardgamefilename = ofd.FileName;
                }

                MessageBox.Show("Select the Video Game csv file.", "Select CSV file");
                if (ofd.ShowDialog() == true)
                {
                    videogamefilename = ofd.FileName;
                }

                OverviewWindow overview = new OverviewWindow(boardgamefilename, videogamefilename);
                overview.ShowDialog();
            }
            else
            {
                MessageBox.Show("Name or Password incorrect!", "Incorrect Login");
            }
        }
    }
}