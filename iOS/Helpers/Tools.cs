using Foundation;
using UIKit;

namespace MovieZ.iOS.Helpers
{
    /// <summary>
    /// Static class to store usefull tools
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Image tools.
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// Get image from url.
            /// </summary>
            /// <returns>The UIImage.</returns>
            /// <param name="uri">Uri of image.</param>
            public static UIImage FromUrl(string uri)
            {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                    return UIImage.LoadFromData(data);
            }
        }
    }
}
