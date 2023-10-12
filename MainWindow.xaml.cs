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
using System.Net.Http;
using Newtonsoft.Json;

namespace MyWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RecentNews : Window
    {   
        
        public RecentNews()
        {
            InitializeComponent();
            DataContext = this;
            Entry();
        }
        
        
        public void Entry()
        {
    
            //fetch.GetAPI("https://api.coindesk.com/v1/bpi/currentprice.json");
            string APIKEY = "Hul4AwZ6iwHtCaUvAKgm1goi8CuGrYTz";
            string period = "1";

            GetAPI($"https://api.nytimes.com/svc/mostpopular/v2/viewed/{period}.json?api-key={APIKEY}");
        }
        public void GetAPI(string URL) {
            HttpClient client = new HttpClient();
            var responseTask = client.GetAsync(URL);
            responseTask.Wait();
            try {
                if (responseTask.IsCompleted) {
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var msg = result.Content.ReadAsStringAsync();
                    msg.Wait();

                    Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(msg.Result)!;
                    
                    TextBoxContainer.Children.Clear(); // Clear existing TextBoxes if any.
                        Console.WriteLine("huh");
                        for (int i = 0; i < root.num_results; i++)
                        {
                            TextBox textBox = new TextBox();
                            textBox.Text = root.results![i].title;
                            textBox.IsReadOnly = true;
                            textBox.HorizontalAlignment= new HorizontalAlignment();
                            textBox.BorderThickness= new Thickness(0);
                            textBox.TextWrapping= new TextWrapping();
                            textBox.VerticalAlignment= new VerticalAlignment();
                            textBox.Width = 600;
                            textBox.Margin = new Thickness(10,50,0,0);
                            TextBox textBox2 = new TextBox();
                            textBox2.Text = root.results![i].@abstract;
                            textBox2.IsReadOnly = true;
                            textBox2.HorizontalAlignment= new HorizontalAlignment();
                            textBox2.BorderThickness= new Thickness(0);
                            textBox2.TextWrapping= new TextWrapping();
                            textBox2.VerticalAlignment= new VerticalAlignment();
                            textBox2.Width = 600;
                            textBox2.Margin = new Thickness(10,5,0,0);
                            TextBoxContainer.Children.Add(textBox);
                            TextBoxContainer.Children.Add(textBox2);
                            Image img = new Image();
                            if (root.results[i].media!.Count > 0) {
                            if (root.results[i].media![0].mediametadata!.Count > 0) {
                                BitmapImage bitmapImage = new BitmapImage(new Uri(root.results[i].media![0].mediametadata![0].url!));
                                img.Source = bitmapImage;
                                img.Height = (double) root.results[i].media![0].mediametadata![0].height!;
                                img.Width = (double) root.results[i].media![0].mediametadata![0].width!;
                                img.Margin = new Thickness(5,10,0,0);
                                img.HorizontalAlignment = new HorizontalAlignment();
                                img.VerticalAlignment = new VerticalAlignment();
                            }
                            }
                            TextBoxContainer.Children.Add(img);

                        }
                    
                    
                }
                }
            }
            catch (FormatException) {

            }
        }
        
        
    }
    
public class MediaMetadatum
{
    public string? url { get; set; }
    public string? format { get; set; }
    public int? height { get; set; }
    public int? width { get; set; }
}

public class Medium
{
    public string? type { get; set; }
    public string? subtype { get; set; }
    public string? caption { get; set; }
    public string? copyright { get; set; }
    public int? approved_for_syndication { get; set; }

    [JsonProperty("media-metadata")]
    public List<MediaMetadatum>? mediametadata { get; set; }
}

public class Result
{
    public string? uri { get; set; }
    public string? url { get; set; }
    public object? id { get; set; }
    public object? asset_id { get; set; }
    public string? source { get; set; }
    public string? published_date { get; set; }
    public string? updated { get; set; }
    public string? section { get; set; }
    public string? subsection { get; set; }
    public string? nytdsection { get; set; }
    public string? adx_keywords { get; set; }
    public object? column { get; set; }
    public string? byline { get; set; }
    public string? type { get; set; }
    public string? title { get; set; }
    public string? @abstract { get; set; }
    public List<string>? des_facet { get; set; }
    public List<string>? org_facet { get; set; }
    public List<string>? per_facet { get; set; }
    public List<string>? geo_facet { get; set; }
    public List<Medium>? media { get; set; }
    public int? eta_id { get; set; }
}

public class Root
{
    public string? status { get; set; }
    public string? copyright { get; set; }
    public int? num_results { get; set; }
    public List<Result>? results { get; set; }
}

    
}
