using BarGraph.Model;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace BarGraph;


//NuGet\Install-Package LiveChartsCore.SkiaSharpView.Maui -Version 2.0.0-beta.700
public partial class MainPage : ContentPage
{
    public ObservableCollection<MonkeyModel> Monkeys { get; } = new();
    public MainPage()
    {
        InitializeComponent();
        Title = "Monkey Graph";
    }

    //click button to show the graph
    public async void ButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // Fetch the data from the remote API
            HttpClient httpClient;
            httpClient = new HttpClient();
            List<MonkeyModel> monkeyList = new();
            var url = "https://montemagno.com/monkeys.json";
            var response = await httpClient.GetAsync(url);
            monkeyList = await response.Content.ReadFromJsonAsync<List<MonkeyModel>>();

            //Itirate monkeys foreach
            foreach (var monkey in monkeyList)
            {
                Monkeys.Add(monkey);
            }
            //Draw Graph!
            //Series[0].Values = new double[] { 10000, 23000, 11000, 17000, 15000, 1300, 19000 };
            //XAxes[0].Labels = new string[] { "Baboon", "Capuchin Monkey", "Blue Monkey", "Howler Monkey", "Japanese Monkey", "Mandrill Monkey", "Proboscis Monkey" };

            //bool isHidden to hide the graph b4 clicking the button
        }
        catch (Exception ex)
        {
            //Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error",
               $"Unable to get monkeys: {ex.Message}", "Ok");
        }
    }

    public ISeries[] Series { get; set; } =
   {
        new ColumnSeries<double>
        {
            Values = new double[] { 10000, 23000, 11000, 17000, 15000, 1300, 19000  }, //number of bars and its max value
            Fill = new SolidColorPaint(SKColors.BlueViolet)
        }
    };
    public Axis[] XAxes { get; set; } =
    {
            new Axis
            {
                Name = "Monkeys", // Name of the y axes
                Labels = new string[] { "Baboon", "Capuchin Monkey", "Blue Monkey", "Howler Monkey", "Japanese Monkey", "Mandrill Monkey", "Proboscis Monkey" }, //the value of each bar
            }
        };

    public Axis[] YAxes { get; set; } =
    {
            new Axis
            {
                Name = "Population", // Name of the y axes
            }
        };

}
   