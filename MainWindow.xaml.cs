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
using System.Security.Authentication;

namespace MyWPFApp
{

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
            string period = "30";

            GetAPI($"https://api.nytimes.com/svc/mostpopular/v2/viewed/{period}.json?api-key={APIKEY}", "viewed");
            GetAPI($"https://api.nytimes.com/svc/mostpopular/v2/shared/{period}.json?api-key={APIKEY}", "shared");
        }

        List<object> ids = new List<object>();
        public void GetAPI(string URL, string section) {
            HttpClient client = new HttpClient();
            var responseTask = client.GetAsync(URL);
            responseTask.Wait();
            try {
                if (responseTask.IsCompleted) {
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) {
                    var msg = result.Content.ReadAsStringAsync();
                    msg.Wait();

                    Root root = JsonConvert.DeserializeObject<Root>(msg.Result)!;
                    
                        Console.WriteLine("huh");
                        StackPanel row = new StackPanel();
                        var bc = new BrushConverter();
                        Brush br = (Brush)bc.ConvertFrom("#181818")!;
                        TextBox Section = new TextBox();
                        Section.Background = br;
                        Section.Foreground = Brushes.Gainsboro;
                        Section.FontSize = 28;
                        Section.Text = $"Most {section} articles by the NYT in the past 30 days";
                        Section.IsReadOnly = true;
                        Section.HorizontalAlignment= new HorizontalAlignment();
                        Section.BorderThickness= new Thickness(0);
                        Section.TextWrapping= new TextWrapping();
                        Section.FontFamily = new FontFamily("Trebuchet MS");
                        Section.VerticalAlignment= new VerticalAlignment();
                        Section.Width = 800;
                        Section.Margin = new Thickness(40,20,0,0);
                        row.Children.Add(Section);
                        for (int i = 0; i < root.num_results; i++)

                        {   
                            if (!ids.Contains(root.results![i].id!)) {
                                ids.Add(root.results![i].id!);
                                StackPanel panel = new StackPanel();
                                panel.Orientation = Orientation.Horizontal;
                                TextBox textBox = new TextBox();
                                textBox.Background = br;
                                textBox.Foreground = Brushes.Gainsboro;
                                textBox.FontSize = 20;
                                textBox.Text = root.results![i].title;
                                textBox.IsReadOnly = true;
                                textBox.HorizontalAlignment= new HorizontalAlignment();
                                textBox.BorderThickness= new Thickness(0);
                                textBox.TextWrapping= new TextWrapping();
                                textBox.FontFamily = new FontFamily("Trebuchet MS");
                                textBox.VerticalAlignment= new VerticalAlignment();
                                textBox.Width = 800;
                                textBox.Margin = new Thickness(40,20,0,0);
                                TextBox textBox2 = new TextBox();
                                textBox2.FontSize = 18;
                                textBox2.Background = br;
                                textBox2.Foreground = Brushes.Gainsboro;
                                textBox2.Text = root.results![i].@abstract + Environment.NewLine + Environment.NewLine +root.results![i].url;
                                textBox2.IsReadOnly = true;
                                textBox2.HorizontalAlignment= new HorizontalAlignment();
                                textBox2.BorderThickness= new Thickness(0);
                                textBox2.TextWrapping= new TextWrapping();
                                textBox2.VerticalAlignment= new VerticalAlignment();
                                textBox2.Width = 800;
                                textBox2.Margin = new Thickness(40,5,0,5);
                                textBox2.FontFamily = new FontFamily("Trebuchet MS");
                                row.Children.Add(textBox);
                                panel.Children.Add(textBox2);
                                Image img = new Image();
                                if (root.results[i].media!.Count > 0) {
                                if (root.results[i].media![0].mediametadata!.Count > 0) {
                                    BitmapImage bitmapImage = new BitmapImage(new Uri(root.results[i].media![0].mediametadata![0].url!));
                                    img.Source = bitmapImage;
                                    img.Height = (double) root.results[i].media![0].mediametadata![0].height!;
                                    img.Width = (double) root.results[i].media![0].mediametadata![0].width!;
                                    img.Margin = new Thickness(5,0,0,0);
                                    img.HorizontalAlignment = new HorizontalAlignment();
                                    img.VerticalAlignment = new VerticalAlignment();
                                }
                                }
                                panel.Children.Add(img);
                                row.Children.Add(panel);
                            }
                            

                        }
                        TextBoxContainer.Children.Add(row);
                    
                }
                }
            }
            catch (FormatException) {
                Console.WriteLine("Error");
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
