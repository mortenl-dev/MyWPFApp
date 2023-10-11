using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    class FetchAPI
    {
        static void Main(string[] args)
        {
            FetchAPI fetch = new FetchAPI();
            //fetch.GetAPI("https://api.coindesk.com/v1/bpi/currentprice.json");
            string APIKEY = "Hul4AwZ6iwHtCaUvAKgm1goi8CuGrYTz";
            string period = "1";
            fetch.GetAPI($"https://api.nytimes.com/svc/mostpopular/v2/viewed/{period}.json?api-key={APIKEY}");
        }
        public void News(string URL) {

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

                    var root = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(msg.Result)!;
                    Console.WriteLine(root);
                    foreach (Result res in root.results!)
                        {
                            string resultTitle = res.title!;

                            Console.WriteLine(resultTitle);
                            Console.WriteLine();

                            string resultAbs = res.@abstract!;

                            Console.WriteLine(resultAbs);
                            Console.WriteLine();
                            Console.WriteLine();

                        }
                        

                }
            }
            }
            catch (FormatException) {

            }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);


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
}