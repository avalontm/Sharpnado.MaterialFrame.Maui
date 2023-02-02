#if ANDROID
using Android.Graphics;
using Microsoft.Maui.Handlers;
using PlatformView = AndroidX.AppCompat.Widget.AppCompatEditText;

namespace Sharpnado.MaterialFrame.Maui.Platforms.Android
{

    public partial class MaterialEntryHandler : EntryHandler
    {
        protected override PlatformView CreatePlatformView()
        {
            var nativeEntry = new PlatformView(Context);
            var myentry = VirtualView as MaterialEntry;

            if (myentry.UnderlineThickness == 0)
            {   // Hide Underline.
                nativeEntry.PaintFlags &= ~PaintFlags.UnderlineText;
                nativeEntry.Background = null;
                nativeEntry.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
            else
            {   // Show Underline. (Is only under the typed text, not the whole control.)
                nativeEntry.PaintFlags |= PaintFlags.UnderlineText;
            }

            return nativeEntry;
        }
    }
}
#endif