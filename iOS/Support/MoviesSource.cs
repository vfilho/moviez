using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using MovieZ.iOS.Helpers;
using MovieZ.Model;
using MovieZ.Services;
using MovieZ.ViewModel;
using UIKit;

namespace MovieZ.iOS.Support
{
    /// <summary>
    /// Sourte to table view of Movies.
    /// </summary>
    public class MoviesSource : UITableViewSource
    {
        List<Movie> tableItems; //Store all items listed on tableview
        string cellIdentifier = "MoviesViewCell";

        UIViewController parent; //Parente view controller
        UITableView tableView; //Table view associated to this source

        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }

        string searchStr;
        MovieService service;
        bool isReloading;

        public MoviesSource(UIViewController parent, MovieService movieService, UITableView tableView)
        {
            tableItems = new List<Movie>();
            this.parent = parent;
            this.tableView = tableView;
            service = movieService;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            parent.NavigationController.PushViewController(new MovieDetailsViewController(tableItems[indexPath.Row]), true);

            tableView.DeselectRow(indexPath, true);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Count;
        }

        public override nint NumberOfSections(UITableView tableView)  
        {  
            return 1;  
        }  

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            MoviesViewCell cell = (MoviesViewCell)tableView.DequeueReusableCell(cellIdentifier);
            Movie item = tableItems[indexPath.Row];

            if (cell == null)
                cell = new MoviesViewCell(UITableViewCellStyle.Default, cellIdentifier);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.Id = item.Id;
            cell.TitleTxt.Text = item.Title;

            cell.GenreTxt.Text = "";
            foreach(Genre genre in item.Genres){
                cell.GenreTxt.Text += (string.IsNullOrEmpty(cell.GenreTxt.Text) ? "" : "/ ") + genre.Name;
            }

            cell.ReleaseDateTxt.Text = item.Release_date;

            if (!string.IsNullOrEmpty(item.Backdrop_path))
                cell.BackgroundImg.Image = Tools.Image.FromUrl(string.Format(Constants.ImageUrl, item.Backdrop_path));
            else
                cell.BackgroundImg.Image = new UIImage();
         
            return cell;
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            /* Control the paging of movie list.
             * - Check if the user scrolled to the bottom of list and if
             * the source isn't loading at the moment and if the current page 
             * is small then the pages number;
             * - Load the next page with the same search string used
             * before.
             */ 
            float height = (float)scrollView.Frame.Size.Height;
            float distanceFromBottom = (float)(scrollView.ContentSize.Height - scrollView.ContentOffset.Y);
            if (distanceFromBottom < height && isReloading == false && CurrentPage < MaxPage)
            {
                isReloading = true;
                InvokeOnMainThread(async () => await SearchMoviesAsync(CurrentPage + 1, searchStr));
            }
        }

        /// <summary>
        /// Searchs the movies async.
        /// </summary>
        /// <returns>The movies async.</returns>
        /// <param name="page">Page.</param>
        /// <param name="searchStr">Search string.</param>
        public async Task SearchMoviesAsync(int page, string searchStr = "")
        {
            NSIndexPath[] indexes;

            this.searchStr = searchStr;//Store the searchStr to paging logic.

            /* If page is 1 It's mean we want reload the list:
             * - Delete all current movies listed;
             */ 
            if(page == 1 && tableItems.Count > 0){
                isReloading = true;
                indexes = new NSIndexPath[tableItems.Count];

                for (int i = 0; i < tableItems.Count; i++)
                {
                    indexes[i] = NSIndexPath.FromRowSection(i, 0);
                }

                tableItems.Clear();

                tableView.BeginUpdates();
                tableView.DeleteRows(indexes, UITableViewRowAnimation.Automatic);
                tableView.EndUpdates();
            }

            //Call the Api.
            SearchMoviesResponseViewModel result = await service.SearchMoviesAsync(searchStr, page);

            //Set the paging variables
            CurrentPage = result.Page;
            MaxPage = result.Total_pages;

            //Add the new items and tell to table view to update the list.
            int currentSize = tableItems.Count;

            tableItems.AddRange(result.Results);

            indexes = new NSIndexPath[result.Results.Count];

            for (int i = 0; i < result.Results.Count; i++){
                indexes[i] = NSIndexPath.FromRowSection(i + currentSize, 0);
            }

            tableView.BeginUpdates();
            tableView.InsertRows(indexes, UITableViewRowAnimation.Automatic);
            tableView.EndUpdates();

            isReloading = false;
        }
    }
}
