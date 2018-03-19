using System.Collections.Generic;

namespace MovieZ.Model
{
    /// <summary>
    /// Movie model.
    /// </summary>
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public List<int> Genre_ids { get; set; } = new List<int>();

        public string Release_date { get; set; }

        public string Overview { get; set; }

        public string Poster_path { get; set; }

        public string Backdrop_path { get; set; }
    }
}
