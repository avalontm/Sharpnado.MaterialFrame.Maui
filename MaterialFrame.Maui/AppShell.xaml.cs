using Microsoft.Maui.Controls;
using Sharpnado.MaterialFrame.Maui;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MaterialFrame.Maui
{
    public partial class AppShell : ContentPage
    {
        ObservableCollection<MaterialTabItem> _tabicons;
        public ObservableCollection<MaterialTabItem> TabIcons
        {
            get { return _tabicons; }
            set
            {
                _tabicons = value;
                OnPropertyChanged(nameof(TabIcons));
            }
        }

        public AppShell()
        {
            InitializeComponent();
            onLoadTab();
            BindingContext = this;
        }

        void onLoadTab()
        {
            TabIcons = new ObservableCollection<MaterialTabItem>() {
               new MaterialTabItem() { Icon = "tab_home.png", Title = "Home" , Content =  new MainPage(), Command = new Command(OnHomeCommand) },
               new MaterialTabItem() { Icon = "dotnet_bot.png", Title = "Tab 2" , Content = new MainPage(), Command = new Command(OnTab2Command)},
               new MaterialTabItem() { Icon = "dotnet_bot.png", Title = "Tab 3" , Content = new MainPage(), Command = new Command(OnTab3Command)},
               new MaterialTabItem() { Icon = "dotnet_bot.png", Title = "Tab 4" , Content = new MainPage(), Command = new Command(OnTab4Command)}
            };
        }

        private void OnTab4Command(object obj)
        {
            Debug.WriteLine($"[TAB4]");
        }

        private void OnTab3Command(object obj)
        {
            Debug.WriteLine($"[TAB3]");
        }

        private void OnTab2Command(object obj)
        {
            Debug.WriteLine($"[TAB2]");
        }

        void OnHomeCommand(object obj)
        {
            Debug.WriteLine($"[HOME]");
        }

    }
}