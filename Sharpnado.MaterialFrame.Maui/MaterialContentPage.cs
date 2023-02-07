using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpnado.MaterialFrame.Maui
{
    public class MaterialContentPage : ContentView
    {
        public Page Current { get { return Application.Current.MainPage; } }
        public int pTop = 80;
        public int pBot = 50;

        public MaterialContentPage()
        {
            Padding = new Thickness(Padding.Left + 0, Padding.Top +  pTop, Padding.Right + 0, Padding.Bottom + pBot);
        }

        public virtual void OnAppearing()
        { 
        
        }

        public virtual void OnDisappearing()
        {

        }
    }
}
