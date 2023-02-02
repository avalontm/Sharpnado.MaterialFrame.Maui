using Microsoft.Maui.Controls.Handlers.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace Sharpnado.MaterialFrame.Maui.Platforms.iOS
{
    public  class MaterialNavigationPageHandler : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationBar.BackgroundColor = UIColor.Red;
            System.Diagnostics.Debug.WriteLine($"[ViewDidLoad] MaterialNavigationPageHandler");
        }
    }
}
