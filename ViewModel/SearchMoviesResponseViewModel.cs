using System.Collections.Generic;
using MovieZ.Model;

namespace MovieZ.ViewModel
{
    /// <summary>
    /// Viewmodel to search movies response.
    /// </summary>
    public class SearchMoviesResponseViewModel
    {
        public int Page { get; set; }

        public int Total_pages { get; set; }

        public List<Movie> Results { get; set; }
    }
}
