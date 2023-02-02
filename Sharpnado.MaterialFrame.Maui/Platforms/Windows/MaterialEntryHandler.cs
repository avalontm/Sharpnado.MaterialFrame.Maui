#if WINDOWS
//using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Runtime.CompilerServices;
//using Microsoft.UI.Xaml.Media;
using Windows.System;
//using Windows.UI;
using Border = Microsoft.UI.Xaml.Controls.Border;
using Color = Windows.UI.Color;
using Colors = Microsoft.UI.Colors;
using MauiColor = Microsoft.Maui.Graphics.Color;
using PlatformEntryType = Microsoft.Maui.Platform.MauiPasswordTextBox;
using PlatformView = Microsoft.UI.Xaml.Controls.Border; // Microsoft.UI.Xaml.FrameworkElement;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;
using TextChangedEventArgs = Microsoft.UI.Xaml.Controls.TextChangedEventArgs;
using Thickness = Microsoft.UI.Xaml.Thickness;

namespace Sharpnado.MaterialFrame.Maui.Platforms.Windows
{
    /// <summary>
    /// Windows platform partial of class. Based on Maui src\Core\src\Handlers\Entry\EntryHandler.Windows.cs
    /// "TPlatformView" is "Microsoft.UI.Xaml.FrameworkElement". WAS "Microsoft.UI.Xaml.Controls.TextBox".
    /// </summary>
    public partial class MaterialEntryHandler : Microsoft.Maui.Handlers.ViewHandler<IEntry, PlatformView>   //EntryHandler
    {
        static readonly bool s_shouldBeDelayed = DeviceInfo.Idiom != DeviceIdiom.Desktop;

        public MaterialEntryHandler(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {
        }

        /// <summary>
        /// Adapted from EntryHandler.CreatePlatformView.
        /// </summary>
        /// <returns></returns>
        protected override PlatformView CreatePlatformView()
        {
            var myentry = VirtualView as MaterialEntry;

            var textbox = new MauiPasswordTextBox
            {
                // From EntryHandler.
                IsObfuscationDelayed = s_shouldBeDelayed

                // TODO: pass entry properties through.
            };

            MauiColor color = myentry != null
                    ? myentry.UnderlineColor
                    : MaterialEntry.UnderlineColorProperty.DefaultValue as MauiColor;
            int thickness = myentry != null
                    ? myentry.UnderlineThickness
                    : (int)MaterialEntry.UnderlineThicknessProperty.DefaultValue;

            var border = new Border
            {
                Child = textbox,
                BorderBrush = color.ToPlatform(),
                BorderThickness = new Thickness(0, 0, 0, thickness)
            };
            return border;
        }

    }
}
#endif