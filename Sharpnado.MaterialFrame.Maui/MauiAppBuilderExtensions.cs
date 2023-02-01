using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.LifecycleEvents;
#if ANDROID
using Sharpnado.MaterialFrame.Maui.Droid;
#endif
#if IOS
using Sharpnado.MaterialFrame.Maui.iOS;
#endif

namespace Sharpnado.MaterialFrame.Maui
{
    public static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder UseSharpnadoMaterialFrame(this MauiAppBuilder builder, bool loggerEnable, bool debugLogEnable =false)
        {
            InternalLogger.EnableDebug = debugLogEnable;
            InternalLogger.EnableLogging = loggerEnable;

            builder.ConfigureLifecycleEvents(events => {
#if IOS
                events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                    iOSMaterialFrameRenderer.Init();
                    return false;
                }));
#endif

#if IOS
                builder.ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(iOSMaterialFrameRenderer));
                });
#endif
#if ANDROID
                events.AddAndroid(android => android.OnCreate((activity, state) => {

            }));
#endif
            });
#if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddCompatibilityRenderer(typeof(MaterialFrame), typeof(AndroidMaterialFrameRenderer));
            });
#endif
            return builder;
        }
    }
}
