using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;


using System.Reflection;
#if ANDROID
using Sharpnado.MaterialFrame.Maui.Droid;
#endif
#if IOS
using Sharpnado.MaterialFrame.Maui.iOS;
#endif
#if WINDOWS
using Microsoft.UI.Xaml;
using Sharpnado.MaterialFrame.Maui.UWP;
#endif
namespace Sharpnado.MaterialFrame.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseSharpnadoMaterialFrame(this MauiAppBuilder builder, bool loggerEnable, bool debugLogEnable = false)
        {
            InternalLogger.EnableDebug = debugLogEnable;
            InternalLogger.EnableLogging = loggerEnable;

            builder.UseMauiCompatibility();

            builder.ConfigureLifecycleEvents(events =>
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
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(AndroidMaterialFrameRenderer));
            });
#endif

#if IOS
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(iOSMaterialFrameRenderer));
            });
#endif

#if WINDOWS
            builder.ConfigureMauiHandlers(handlers =>
            {
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
