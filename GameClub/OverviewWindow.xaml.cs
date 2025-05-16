using GameClubClassLib.DataAccess;
using GameClubClassLib.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace GameClub
{
    /// <summary>
    /// Interaction logic for OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        private List<VideoGame> _videoGames = new List<VideoGame>();
        private List<BoardGame> _boardGames = new List<BoardGame>();
        private DataSet _ds;

        public OverviewWindow(string boardGameCsvPath, string videoGameCsvPath)
        {
            InitializeComponent();
            try
            {
                BoardGameData.InitialiseerBoardGameData(boardGameCsvPath);
                VideoGameData.InitialiseerVideoGameData(videoGameCsvPath);

                _boardGames = BoardGameData.GetBoardGameList();
                BoardGamesDataGrid.ItemsSource = _boardGames;
                _videoGames = VideoGameData.GetVideoGameList();
                VideoGamesListBox.ItemsSource = _videoGames;
                BoardGamesDataGrid.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("The incorrect file was chosen. Try again.");
            }
        }

        private void VideoGamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VideoGamesListBox.SelectedItem != null)
            {
                VideoGame videogame = (VideoGame)VideoGamesListBox.SelectedItem;
                VideoGameImage.Source = new BitmapImage(new Uri("/Images/VideoGames/" + videogame.Source, UriKind.Relative));
                GameNameTextBlock.Text = videogame.Title;
                VideoGameAmazonPriceTextBlock.Text = videogame.GetAmazonPrice().ToString("C");
                VideoGameGeekGameShopPriceTextBlock.Text = videogame.GetGeekGameShopPrice().ToString("C");
                AvgRatingTextBlock.Text = videogame.AverageRating.ToString();
                GeekRatingTextBlock.Text = videogame.GeekRating.ToString();
                NumberOfVotersTextBlock.Text = videogame.NumberOfVoters.ToString();
                YearTextBlock.Text = videogame.ReleaseYear.ToString();
                GameModeTextBlock.Text = videogame.IsSinglePlayerOnly ? "Singleplayer Only" : "Has Multiplayer";
            }
        }

        private void BoardGamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BoardGamesDataGrid.SelectedItem != null)
            {
                BoardGame boardgame = (BoardGame)BoardGamesDataGrid.SelectedItem;
                BoardGameImage.Source = new BitmapImage(new Uri("/Images/BoardGames/" + boardgame.Source, UriKind.Relative));
                GameNameTextBlock.Text = boardgame.Title;
                BoardGameAmazonPriceTextBlock.Text = $"{boardgame.GetAmazonPrice():c}";
                BoardGameGeekGameShopPriceTextBlock.Text = $"{boardgame.GetGeekGameShopPrice():c}";
            }
        }

        private void Top10_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardGamesDataGrid.ItemsSource = _boardGames
                .Where(x => x.Rank <= 10)
                .OrderBy(x => x.Rank);
        }

        private void Under50_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardGamesDataGrid.ItemsSource = _boardGames
                .Where(x => x.GetAmazonPrice() < 50 || x.GetGeekGameShopPrice() < 50)
                .OrderBy(x => Math.Min(x.GetAmazonPrice(), x.GetGeekGameShopPrice()));
        }

        private void ResetFilter_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardGamesDataGrid.ItemsSource = _boardGames.OrderBy(x => x.Rank).ToList();
        }

        private void Post2015Filter_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardGamesDataGrid.ItemsSource = _boardGames
                .Where(x => x.ReleaseYear > 2015)
                .OrderByDescending(x => x.ReleaseYear).ThenBy(x => x.Title);
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_ds == null)
            {
                _ds = new DataSet("Games");
                _ds.Tables.Add(BoardGameData.BoardGameDataTable);
                _ds.Tables.Add(VideoGameData.VideoGameDataTable);
            }
            _ds.WriteXml("Games.xml");
        }
    }
}
