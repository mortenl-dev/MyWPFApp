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
        private string? _NewsTitle = "N/A";
        public string NewsTitle
        { 
            get { return _NewsTitle!; }
            set {
                _NewsTitle = value;
            }
        }
        private string? _Description = "N/A";
        public string Description
        { 
            get { return _Description!; }
            set {
                _Description = value;
            }
        }
        private string? _ImageSource = "N/A";
        public string ImageSource
        { 
            get { return _ImageSource!; }
            set {
                _ImageSource = value;
            }
        }
        private string? _ImageHeight = "N/A";
        public string ImageHeight
        { 
            get { return _ImageHeight!; }
            set {
                _ImageHeight = value;
            }
        }
        private string? _ImageWidth = "N/A";
        public string ImageWidth
        { 
            get { return _ImageWidth!; }
            set {
                _ImageHeight = value;
            }
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
                    _NewsTitle = root.results![0].title!;
                    _Description = root.results![0].@abstract!;
                    if (root.results[0].media!.Count > 0) {
                        if (root.results[0].media![0].mediametadata!.Count > 0) {
                            _ImageSource = root.results[0].media![0].mediametadata![0].url!;
                            _ImageHeight = root.results[0].media![0].mediametadata![0].height.ToString();
                            _ImageWidth = root.results[0].media![0].mediametadata![0].width.ToString();
                        }
                    }
                    
                    


                    foreach (var res in root.results!)
                        {
                            

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
