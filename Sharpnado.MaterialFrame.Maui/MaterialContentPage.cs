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

        int _pTop = 80;
        int _pBot = 50;

        public int pTop
        {
            get { return _pTop; }
            set
            {
                _pTop = value;
                onChangePadding();
                OnPropertyChanged("pTop");
            }
        }

        public int pBot
        {
            get
            {
                return _pBot;
            }
            set
            {
                _pBot = value; 
                onChangePadding();
                OnPropertyChanged("pTop");
            }
        }

        public MaterialContentPage()
        {
            onChangePadding();
        }


        void onChangePadding()
        {
            Padding = new Thickness(Padding.Left + 0, Padding.Top + pTop, Padding.Right + 0, Padding.Bottom + pBot);
        }

        public virtual void OnAppearing()
        { 
        
        }

        public virtual void OnDisappearing()
        {

        }
    }
}
