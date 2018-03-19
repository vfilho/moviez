using System;
using Foundation;
using MovieZ.iOS.Support;
using MovieZ.Services;
using UIKit;

namespace MovieZ.iOS
{
    /// <summary>
    /// List of upcoming movies.
    /// </summary>
    public partial class MoviesViewController : UIViewController
    {
        UITableView tableView;
        UIRefreshControl refresher;
        UIActivityIndicatorView spinner;

        MoviesSource movieSource;

        MovieService service;

        public MoviesViewController (IntPtr handle) : base (handle)
        {
            service = new MovieService();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Configure the view

            Title = "Movies";

            //Add the search button at navigation bar, that will show a screen that 
            //allow the user search for any movie.
            this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(
                    UIImage.FromBundle("mag.png").ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal),
                    UIBarButtonItemStyle.Plain, (sender, e) =>
                    {
                        NavigationController.PushViewController(new SearchMoviesController(), true);
                    }
                ), true);
            
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

            spinner.StartAnimating();

            //Refresher
            refresher = new UIRefreshControl();
            tableView.AddSubview(refresher);
            refresher.AttributedTitle = new NSAttributedString("Pull to refresh");
            refresher.AddTarget((sender, e) => {
                InvokeOnMainThread(async () => await movieSource.SearchMoviesAsync(1));
                refresher.EndRefreshing();
            }, UIControlEvent.ValueChanged);

            movieSource = new MoviesSource(this, service, tableView);
            tableView.Source = movieSource;

            //Load the first page
            InvokeOnMainThread(async () => { 
                await movieSource.SearchMoviesAsync(1); 
                spinner.StopAnimating();
                spinner.Hidden = true;
            });
        }
    }
}