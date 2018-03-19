using System.Threading.Tasks;
using MovieZ.ViewModel;

namespace MovieZ.Services
{
    /// <summary>
    /// Movies service interface.
    /// </summary>
    public interface IMoviesService
    {
        Task<SearchMoviesResponseViewModel> SearchMoviesAsync(string name, int page);
    }
}
