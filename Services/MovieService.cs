using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MovieZ.Model;
using MovieZ.ViewModel;
using Newtonsoft.Json;

namespace MovieZ.Services
{
    /// <summary>
    /// Movie service to communicate with TMDB Api.
    /// </summary>
    public class MovieService : IMoviesService
    {
        HttpClient client;

        //List to sotore all Genres of movies
        static List<Genre> genres;

        public MovieService(){
            client = new HttpClient();
            genres = new List<Genre>();
        }

        /// <summary>
        /// Searchs the movies.
        /// </summary>
        /// <returns>The movies async.</returns>
        /// <param name="name">Partial name of movie.</param>
        /// <param name="page">Page.</param>
        public async Task<SearchMoviesResponseViewModel> SearchMoviesAsync(string name, int page)
        {
            SearchMoviesResponseViewModel searchResponse = new SearchMoviesResponseViewModel();
            GenresListResponse genresResponse;

            string searchRoute = "";
            string genresRoute = "";

            //If name is filled will call the search route or will call the upcoming route
            if(!string.IsNullOrEmpty(name))
                searchRoute = string.Format("search/movie?api_key={0}&query={1}&page={2}", Constants.ApiKey, name, page);
            else
                searchRoute = string.Format("movie/upcoming?api_key={0}&page={1}", Constants.ApiKey, page);

            var uri = new Uri(string.Format(Constants.ApiUrl, searchRoute));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    searchResponse = JsonConvert.DeserializeObject<SearchMoviesResponseViewModel>(content);

                    //If genres list isn't filled will request the list of all genres.
                    if(genres.Count == 0){
                        genresRoute = string.Format("genre/movie/list?api_key={0}", Constants.ApiKey);
                        var responseDetail = await client.GetAsync(string.Format(Constants.ApiUrl, genresRoute));
                        if (response.IsSuccessStatusCode)
                        {
                            var contentDetail = await responseDetail.Content.ReadAsStringAsync();

                            genresResponse = JsonConvert.DeserializeObject<GenresListResponse>(contentDetail);

                            genres = genresResponse.Genres;
                        }
                    }

                    //Then will fill the Genres of each movie returned.
                    foreach (Movie movie in searchResponse.Results){
                        movie.Genres = genres.Where(g => movie.Genre_ids.Contains(g.Id)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return searchResponse;
        }
    }
}
