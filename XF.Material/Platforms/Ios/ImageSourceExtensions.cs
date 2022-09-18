using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;

namespace XF.Material.iOS
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
