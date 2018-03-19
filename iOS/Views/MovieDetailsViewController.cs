using System;
using UIKit;
using MovieZ.Model;
using MovieZ.iOS.Helpers;
using CoreGraphics;

namespace MovieZ.iOS
{
    /// <summary>
    /// Movie details view controller.
    /// </summary>
    public partial class MovieDetailsViewController : UIViewController
    {
        Movie movie;

        UIScrollView scrollView;
        UILabel overviewValueLbl;

        public MovieDetailsViewController (IntPtr handle) : base (handle)
        {
        }

        public MovieDetailsViewController (Movie movie){
            this.movie = movie;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            //Maintain the scrollview contentsize.
            float height = (float)(overviewValueLbl.Frame.Y + overviewValueLbl.Bounds.Height);

            scrollView.ContentSize = new CGSize(View.Bounds.Width, height);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Convigure the View 

            //Background
            UIImageView BackgroundImg = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                ClipsToBounds = true
            };

            if (!string.IsNullOrEmpty(movie.Poster_path))
                BackgroundImg.Image = Tools.Image.FromUrl(string.Format(Constants.ImageUrl, movie.Poster_path));
            else
            {
                BackgroundImg.Image = new UIImage();
                BackgroundImg.BackgroundColor = UIColor.White;
            }

            View.AddSubview(BackgroundImg);

            View.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Left, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Right, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Top, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Bottom, 1f, 0f));

            //Dark upper
            UIImageView UpperImg = new UIImageView()
            {
                ContentMode = UIViewContentMode.Center,
                TranslatesAutoresizingMaskIntoConstraints = false,
                ClipsToBounds = true,
                BackgroundColor = UIColor.FromRGBA(0f, 0f, 0f, 0.5f)
            };

            View.AddSubview(UpperImg);

            View.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Left, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Right, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Top, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View,
                                                    NSLayoutAttribute.Bottom, 1f, 0f));

            // Scroll view
            scrollView = new UIScrollView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.AddSubview(scrollView);
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Left, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Width, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Top, 1f, 0f));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                         View, NSLayoutAttribute.Height, 1f, 0f));

            //Title lbl
            UILabel titleLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 13f),
                TextColor = UIColor.LightGray,
                Text = "Title",
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            scrollView.AddSubview(titleLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Top, 1f, 80f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleLbl, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 15f));

            //Title Value Lbl
            UILabel titleValueLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 16f),
                TextColor = UIColor.White,
                Text = movie.Title,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            scrollView.AddSubview(titleValueLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleValueLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleValueLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleValueLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               titleLbl, NSLayoutAttribute.Bottom, 1f, 0f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(titleValueLbl, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 20f));

            //Genre lbl
            UILabel genreLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 13f),
                TextColor = UIColor.LightGray,
                Text = "Genre(s)",
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            scrollView.AddSubview(genreLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               titleValueLbl, NSLayoutAttribute.Bottom, 1f, 15f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreLbl, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 15f));

            //Genre Value Lbl
            UILabel genreValueLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 16f),
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0
            };

            foreach (Genre genre in movie.Genres)
            {
                genreValueLbl.Text += (string.IsNullOrEmpty(genreValueLbl.Text) ? "" : "/ ") + genre.Name;
            }

            scrollView.AddSubview(genreValueLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreValueLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreValueLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreValueLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               genreLbl, NSLayoutAttribute.Bottom, 1f, 0f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(genreValueLbl, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 20f));

            //Release date lbl
            UILabel releaseLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 13f),
                TextColor = UIColor.LightGray,
                Text = "Release date",
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            scrollView.AddSubview(releaseLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               genreValueLbl, NSLayoutAttribute.Bottom, 1f, 15f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseLbl, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 15f));

            //Release date value Lbl
            UILabel releaseValueLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 16f),
                TextColor = UIColor.White,
                Text = movie.Release_date,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0
            };

            scrollView.AddSubview(releaseValueLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseValueLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseValueLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseValueLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               releaseLbl, NSLayoutAttribute.Bottom, 1f, 0f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(releaseValueLbl, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 20f));

            //Overview lbl
            UILabel overviewLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 13f),
                TextColor = UIColor.LightGray,
                Text = "Overview",
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0
            };

            scrollView.AddSubview(overviewLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewLbl, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Right, 1f, -20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               releaseValueLbl, NSLayoutAttribute.Bottom, 1f, 15f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewLbl, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 15f));

            //Overview value Lbl
            overviewValueLbl = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 16f),
                TextColor = UIColor.White,
                Text = movie.Overview,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0
            };

            scrollView.AddSubview(overviewValueLbl);
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewValueLbl, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Left, 1f, 20f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewValueLbl, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                                                               scrollView, NSLayoutAttribute.Width, 1f, -40f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewValueLbl, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                               overviewLbl, NSLayoutAttribute.Bottom, 1f, 0f));
            scrollView.AddConstraint(NSLayoutConstraint.Create(overviewValueLbl, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                               null, NSLayoutAttribute.NoAttribute, 1f, 20f));
        }
    }
}