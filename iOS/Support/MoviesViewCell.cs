using System;
using UIKit;

namespace MovieZ.iOS.Support
{
    /// <summary>
    /// Movie cell used on lists
    /// </summary>
    public class MoviesViewCell : UITableViewCell
    {
        public UIImageView BackgroundImg;
        public UILabel TitleTxt;
        public UILabel GenreTxt;
        public UILabel ReleaseDateTxt;
        public int Id { get; set; }

        protected MoviesViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        /// <summary>
        /// Create and configure the new row.
        /// </summary>
        /// <param name="style">Style.</param>
        /// <param name="reuseIdentifier">Reuse identifier.</param>
        public MoviesViewCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            //Background
            BackgroundImg = new UIImageView()
            {
                ContentMode = UIViewContentMode.ScaleAspectFill,
                TranslatesAutoresizingMaskIntoConstraints = false,
                ClipsToBounds = true
            };

            BackgroundImg.Layer.CornerRadius = 6f;

            this.AddSubview(BackgroundImg);

            this.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Left, 1f, 10f));
            this.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Right, 1f, -10f));
            this.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Top, 1f, 5f));
            this.AddConstraint(NSLayoutConstraint.Create(BackgroundImg, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Bottom, 1f, 0f));

            //Dark upper
            UIImageView UpperImg = new UIImageView()
            {
                ContentMode = UIViewContentMode.Center,
                TranslatesAutoresizingMaskIntoConstraints = false,
                ClipsToBounds = true,
                BackgroundColor = UIColor.FromRGBA(0f, 0f, 0f, 0.5f)
            };

            UpperImg.Layer.CornerRadius = 6f;

            this.AddSubview(UpperImg);

            this.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Left, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Left, 1f, 10f));
            this.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Right, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Right, 1f, -10f));
            this.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Top, 1f, 5f));
            this.AddConstraint(NSLayoutConstraint.Create(UpperImg, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this,
                                                    NSLayoutAttribute.Bottom, 1f, 0f));

            //Title text
            TitleTxt = new UILabel()
            {
                Font = UIFont.FromName("Lato-Bold", 16f),
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 2,
                LineBreakMode = UILineBreakMode.TailTruncation
            };

            this.AddSubview(TitleTxt);
            this.AddConstraint(NSLayoutConstraint.Create(TitleTxt, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Left, 1f, 20f));
            this.AddConstraint(NSLayoutConstraint.Create(TitleTxt, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Right, 1f, -20f));
            this.AddConstraint(NSLayoutConstraint.Create(TitleTxt, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                    null, NSLayoutAttribute.NoAttribute, 1f, 25f));
            this.AddConstraint(NSLayoutConstraint.Create(TitleTxt, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Top, 1f, 15f));

            //Genre text
            GenreTxt = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 12f),
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 3,
                LineBreakMode = UILineBreakMode.TailTruncation

            };

            this.AddSubview(GenreTxt);
            this.AddConstraint(NSLayoutConstraint.Create(GenreTxt, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Left, 1f, 20f));
            this.AddConstraint(NSLayoutConstraint.Create(GenreTxt, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Right, 1f, -20f));
            this.AddConstraint(NSLayoutConstraint.Create(GenreTxt, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                    null, NSLayoutAttribute.NoAttribute, 1f, 22f));
            this.AddConstraint(NSLayoutConstraint.Create(GenreTxt, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                    TitleTxt, NSLayoutAttribute.Bottom, 1f, 0f));

            //Release date text
            ReleaseDateTxt = new UILabel()
            {
                Font = UIFont.FromName("Lato-Regular", 14f),
                TextColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 2,
                LineBreakMode = UILineBreakMode.TailTruncation,
                TextAlignment = UITextAlignment.Right
            };

            this.AddSubview(ReleaseDateTxt);
            this.AddConstraint(NSLayoutConstraint.Create(ReleaseDateTxt, NSLayoutAttribute.Left, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Left, 1f, 20f));
            this.AddConstraint(NSLayoutConstraint.Create(ReleaseDateTxt, NSLayoutAttribute.Right, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Right, 1f, -20f));
            this.AddConstraint(NSLayoutConstraint.Create(ReleaseDateTxt, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                                                    null, NSLayoutAttribute.NoAttribute, 1f, 22f));
            this.AddConstraint(NSLayoutConstraint.Create(ReleaseDateTxt, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal,
                                                    BackgroundImg, NSLayoutAttribute.Bottom, 1f, -5f));
        }
    }
}
