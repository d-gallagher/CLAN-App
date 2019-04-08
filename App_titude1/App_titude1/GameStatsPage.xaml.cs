using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_titude1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameStatsPage : ContentPage
	{
        private ObservableCollection<GroupStatsModel> groupStats { get; set; }

        public GameStatsPage ()
		{
			InitializeComponent ();
            var lstView = new ListView();
            groupStats = new ObservableCollection<GroupStatsModel>();
            var gamesGroup = new GroupStatsModel() { LongName = "Games", ShortName = "Game" };
            var scoreGroup = new GroupStatsModel() { LongName = "Scores", ShortName = "Score" };
            var avgGroup = new GroupStatsModel() { LongName = "Averages", ShortName = "Avg" };
            gamesGroup.Add(new StatsModel() { Name = "Total Games Played", Comment = "0" });
            gamesGroup.Add(new StatsModel() { Name = "Total Time Played",  Comment = "1" });
            gamesGroup.Add(new StatsModel() { Name = "Top Game Played",  Comment = "2" });
            scoreGroup.Add(new StatsModel() { Name = "Total Score", Comment = "3" });
            scoreGroup.Add(new StatsModel() { Name = "Total Colour Scores", Comment = "4" });
            scoreGroup.Add(new StatsModel() { Name = "Total Number Scores",  Comment = "5" });
            scoreGroup.Add(new StatsModel() { Name = "Total Letter Scores",  Comment = "6" });
            avgGroup.Add(new StatsModel() { Name = "Average Score per game",  Comment = "7" });
            avgGroup.Add(new StatsModel() { Name = "Average Colour Scores",  Comment = "8" });
            avgGroup.Add(new StatsModel() { Name = "Average Number Scores",  Comment = "9" });
            avgGroup.Add(new StatsModel() { Name = "Average Letter Scores",  Comment = "10" });

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
        public string Image { get; set; }
        public StatsModel()
        {
        }
    }
}