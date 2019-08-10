using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GetApiStarWars1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            try
            { 
                loadData();
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() => {
                    DisplayAlert("No Internet Connection ", ex.Message, "Ok");
                });
            }

        }
        private async void loadData()
        {
            var qRequest = new GraphQLRequest
            {
                Query = @"
                {
                  allFilms {
                    films {
                      title
                      director
                    }
                  }
                }"
            };
            String url = "https://swapi.apis.guru/graphiql";
            var graphQLClient = new GraphQLClient(url);
            var graphQLResponse = await graphQLClient.PostAsync(qRequest);
            listView1.ItemsSource = graphQLResponse.Data.allFilms.films.ToObject<List<FilmStarwars>>();
        }

        public class FilmStarwars
        {
            public string title { get; set; }
            public string director { get; set; }
        }
    }
}
