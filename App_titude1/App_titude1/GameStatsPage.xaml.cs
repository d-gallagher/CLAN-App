using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_titude1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameStatsPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<GroupStatsModel> groupStats { get; set; }
        private ObservableCollection<StatsModel> gameStats;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(
           [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                   new PropertyChangedEventArgs(propertyName));
        }

        //this method calls OnPropertyChanged, which uses CallerMemberName to decide which property changed..
        //selected dog in mainpageviewmodel calls SetValue, which calls OnPropChanged
        //get selected dog prop name to onpropchanged
        //backing field is passed by value by default
        //explicitly tell method to pas by ref
        protected void SetValue<T>(ref T backingField, T value,
                                    [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            {
                backingField = value;

                OnPropertyChanged(propertyName);
            }
        }

        public ObservableCollection<StatsModel> StatsList
        {
            get { return gameStats; }
            private set { SetValue(ref gameStats, value); }

        }

        public GameStatsPage ()
		{
			InitializeComponent ();
            var lstView = new ListView();
            groupStats = new ObservableCollection<GroupStatsModel>();
            var gamesGroup = new GroupStatsModel() { LongName = "Games", ShortName = "Game" };
            var scoreGroup = new GroupStatsModel() { LongName = "Scores", ShortName = "Score" };
            var avgGroup = new GroupStatsModel() { LongName = "Averages", ShortName = "Avg" };
            gamesGroup.Add(new StatsModel("Total Games Played", "0") { });
            gamesGroup.Add(new StatsModel("Total Time Played", "0") { });
            gamesGroup.Add(new StatsModel("Top Game Played", "0") { });
            scoreGroup.Add(new StatsModel("Total Score", "0") { });
            scoreGroup.Add(new StatsModel("Total Colour Scores", "0") { });
            scoreGroup.Add(new StatsModel("Total Number Scores", "0") { });
            scoreGroup.Add(new StatsModel("Total Letter Scores", "0") { });
            avgGroup.Add(new StatsModel("Average Score per game", "0") { });
            avgGroup.Add(new StatsModel("Average Colour Scores", "0") { });
            avgGroup.Add(new StatsModel("Average Number Scores", "0") { });
            avgGroup.Add(new StatsModel("Average Letter Scores", "0") { });

            groupStats.Add(gamesGroup);
            groupStats.Add(scoreGroup);
            groupStats.Add(avgGroup);

            lstView.ItemsSource = groupStats;
            lstView.IsGroupingEnabled = true;
            lstView.GroupDisplayBinding = new Binding("LongName");
            lstView.GroupShortNameBinding = new Binding("ShortName");

            lstView.ItemTemplate = new DataTemplate(typeof(TextCell));
            lstView.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            lstView.ItemTemplate.SetBinding(TextCell.DetailProperty, "Comment");
            Content = lstView;
        }

        private void SaveStats()
        {
            StatsModel.SaveGameStatsData(gameStats);
        }

        public void ReadStats()
        {
            StatsList = StatsModel.ReadGameStatsData();
        }

        public ICommand SaveListCommand { get; private set; }
        public ICommand DeleteFromListCommand { get; private set; }

    }

    public class GroupStatsModel : ObservableCollection<StatsModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }

    public class StatsModel
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        
        public StatsModel(string name, string comment)
        {
            Name = name;
            Comment = comment;
        }

        public static ObservableCollection<StatsModel> ReadGameStatsData()
        {
            ObservableCollection<StatsModel> myList = new ObservableCollection<StatsModel>();
            string jsonText;

            try  // reading the localApplicationFolder first
            {
                string path = Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData);
                string filename = Path.Combine(path, "gameStats.txt");
                using (var reader = new StreamReader(filename))
                {
                    jsonText = reader.ReadToEnd();
                    // need json library
                }
            }
            catch // fallback is to read the default file
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(
                                                typeof(MainPage)).Assembly;
                // create the stream
                Stream stream = assembly.GetManifestResourceStream(
                                    "App_titude1.gameStats.txt");
                using (var reader = new StreamReader(stream))
                {
                    jsonText = reader.ReadToEnd();
                    // include JSON library now
                }
            }

            myList = JsonConvert.DeserializeObject<ObservableCollection<StatsModel>>(jsonText);

            return myList;
        }

        public static void SaveGameStatsData(ObservableCollection<StatsModel> saveList)
        {
            // need the path to the file
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, "gameStats.txt");
            // use a stream writer to write the list
            using (var writer = new StreamWriter(filename, false))
            {
                // stringify equivalent
                string jsonText = JsonConvert.SerializeObject(saveList);
                writer.WriteLine(jsonText);
            }
        }
    }
}