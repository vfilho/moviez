using Foundation;
using System;
using UIKit;
using MovieZ.iOS.Support;
using MovieZ.Services;

namespace MovieZ.iOS
{
    /// <summary>
    /// Search for any movies controller.
    /// </summary>
    public partial class SearchMoviesController : UIViewController
    {
        UITableView tableView;
        UIRefreshControl refresher;
        UIActivityIndicatorView spinner;
        UISearchController searchController;

        MoviesSource movieSource;

        MovieService service;

        //Store the last searched term to use on refresh logic.
        string searchStr;

        public SearchMoviesController (IntPtr handle) : base (handle)
        {
        }

        public SearchMoviesController(){
            service = new MovieService();
            searchStr = "*";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Configure view

            //Table View
            tableView = new UITableView()
            {
                BackgroundColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
                RowHeight = 160,
                SeparatorStyle = UITableViewCellSeparatorStyle.None
            };

            View.AddSubview(tableView);
            View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Right, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Left, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Top, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Bottom, 1f, 0f));

            movieSource = new MoviesSource(this, service, tableView);
            tableView.Source = movieSource;

            //Search Controller
            searchController = new UISearchController((UIViewController)null)
            {
                HidesNavigationBarDuringPresentation = false
            };

            searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Prominent;

            NavigationItem.TitleView = searchController.SearchBar;

            searchController.SearchBar.SearchButtonClicked += SearchClickedHandler;

            //Spinner
            spinner = new UIActivityIndicatorView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray
            };

            spinner.Center = View.Center;

            View.AddSubview(spinner);
            View.AddConstraint(NSLayoutConstraint.Create(spinner, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.CenterX, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(spinner, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.CenterY, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(spinner, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                         null, NSLayoutAttribute.Height, 1f, 300f));
            View.AddConstraint(NSLayoutConstraint.Create(spinner, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Width, 1f, 300f));

            spinner.Hidden = true;

            //Refresher
            refresher = new UIRefreshControl();
            tableView.AddSubview(refresher);
            refresher.AttributedTitle = new NSAttributedString("Pull to refresh");
            refresher.AddTarget((sender, e) => {
                InvokeOnMainThread(async () => await movieSource.SearchMoviesAsync(1, searchStr));
                refresher.EndRefreshing();
            }, UIControlEvent.ValueChanged);
        }

        /// <summary>
        /// After user type any text and click on search.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        void SearchClickedHandler(object sender, EventArgs e)
        {
            //Show the spinner, call the Api with the text typed by user and after
            //received the response hide the spinner again.
            spinner.Hidden = false;
            spinner.StartAnimating();
            searchStr = string.IsNullOrEmpty(searchController.SearchBar.Text) ? "*" : 
                              searchController.SearchBar.Text;
            InvokeOnMainThread(async () => await movieSource.SearchMoviesAsync(1, searchStr));
            spinner.StopAnimating();
            spinner.Hidden = true;
        }
    }
}