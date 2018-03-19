using System.Collections.Generic;
using MovieZ.Model;

namespace MovieZ.ViewModel
{
    /// <summary>
    /// View model to genres list response.
    /// </summary>
    public class GenresListResponse
    {
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
