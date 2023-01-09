using BarGraph.Model;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
        }
        catch (Exception ex)
        {
            //Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error",
               $"Unable to get monkeys: {ex.Message}", "Ok");
        }
    }

    //Set graph
    //Series[0].Values = new double[] { 10000, 23000, 11000, 17000, 15000, 1300, 19000 };
    //XAxes[0].Labels = new string[] { "Baboon", "Capuchin Monkey", "Blue Monkey", "Howler Monkey", "Japanese Monkey", "Mandrill Monkey", "Proboscis Monkey" };

    //bool isHidden to hide the graph b4 clicking the button
    public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<string>
                {
                    Name = "Monkeys",
                    Values = new string[] { "Baboon", "Capuchin Monkey", "Blue Monkey", "Howler Monkey", "Japanese Monkey", "Mandrill Monkey", "Proboscis Monkey" }
                },
                new ColumnSeries<int> 
                {
                    Name = "Monkeys",
                    Values = new int[] { 10000, 23000, 11000, 17000, 15000, 1300, 19000 }
                }
            };

}
   