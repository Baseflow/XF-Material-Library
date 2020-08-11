using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace XF.Material.Droid
{
    public static class ImageSourceExtensions
    {
        public static IImageSourceHandler GetImageSourceHandler(this ImageSource source)
        {
            return source switch
            {
                FileImageSource _ => new FileImageSourceHandler(),
                StreamImageSource _ => new StreamImagesourceHandler(),
                FontImageSource _ => new FontImageSourceHandler(),
                _ => new ImageLoaderSourceHandler(),
            };
        }
    }
}
