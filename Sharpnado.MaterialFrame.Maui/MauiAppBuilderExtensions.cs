using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
using Sharpnado.CollectionView;
using CommunityToolkit.Maui;

#if ANDROID
using Sharpnado.MaterialFrame.Maui.Droid;
using Sharpnado.MaterialFrame.Maui.Platforms.Android;
#endif
#if IOS
using Sharpnado.MaterialFrame.Maui.iOS;
using Sharpnado.MaterialFrame.Maui.Platforms.iOS;
using UIKit;
#endif
#if WINDOWS
using Microsoft.UI.Xaml;
using Sharpnado.MaterialFrame.Maui.UWP;
using Sharpnado.MaterialFrame.Maui.Platforms.Windows;
#endif

namespace Sharpnado.MaterialFrame.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseSharpnadoMaterialFrame(this MauiAppBuilder builder, bool loggerEnable, bool debugLogEnable = false)
        {
            InternalLogger.EnableDebug = debugLogEnable;
            InternalLogger.EnableLogging = loggerEnable;
           
            builder
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .UseSharpnadoCollectionView(loggerEnable, debugLogEnable)
            .ConfigureLifecycleEvents(events =>
            {
#if IOS
                events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                    iOSMaterialFrameRenderer.Init();

                    return false;
                }));
#endif

#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, state) => {

                }));
#endif
#if WINDOWS
                events.AddWindows(windows => windows
                        .OnWindowCreated(window =>
                        {
                            window.SizeChanged += OnSizeChanged;
                        }));
#endif
            });

#if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(MaterialEntry), typeof(MaterialEntryHandler));
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(AndroidMaterialFrameRenderer));
            });
#endif


#if IOS
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(MaterialNavigationPage), typeof(MaterialNavigationPageHandler));
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(iOSMaterialFrameRenderer));
            });
#endif

#if WINDOWS
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(MaterialEntry), typeof(MaterialEntryHandler));
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(UWPMaterialFrameRenderer));
                
            });


#endif

            return builder;
        }

#if WINDOWS
        static void OnSizeChanged(object sender, Microsoft.UI.Xaml.WindowSizeChangedEventArgs args)
        {
            ILifecycleEventService service = MauiWinUIApplication.Current.Services.GetRequiredService<ILifecycleEventService>();
            service.InvokeEvents(nameof(Microsoft.UI.Xaml.Window.SizeChanged));
        }
#endif
    }
}
