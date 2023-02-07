using System.Collections.ObjectModel;

namespace Sharpnado.MaterialFrame.Maui.Controls;

public partial class MaterialContent : ContentView
{
    public static readonly BindableProperty TitleHeaderProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MaterialContent), string.Empty);

    public static readonly BindableProperty TabIconColorProperty = BindableProperty.Create(nameof(TabIconColor), typeof(Color), typeof(MaterialContent), Colors.Red);

    public static readonly BindableProperty TabSourceProperty = BindableProperty.Create(nameof(TabSource), typeof(ObservableCollection<MaterialTabItem>), typeof(MaterialContent), new ObservableCollection<MaterialTabItem>(), propertyChanged: TabSourceChanged);

    public static readonly BindableProperty CurrentIndexProperty = BindableProperty.Create(nameof(CurrentIndex), typeof(int), typeof(MaterialContent), 0);

    public static readonly BindableProperty IsDisplayVisibleProperty = BindableProperty.Create(nameof(IsDisplayVisible), typeof(bool), typeof(MaterialContent), false);

    public static readonly BindableProperty DisplayContentProperty = BindableProperty.Create(nameof(DisplayContent), typeof(MaterialContentPage), typeof(MaterialContent), propertyChanged: onExtraDataTempalte);

    public string Title
    {
        get => (string)GetValue(TitleHeaderProperty);
        set => SetValue(TitleHeaderProperty, value);
    }

    public Color TabIconColor
    {
        get => (Color)GetValue(TabIconColorProperty);
        set => SetValue(TabIconColorProperty, value);
    }

    public bool IsDisplayVisible
    {
        get => (bool)GetValue(IsDisplayVisibleProperty);
        set => SetValue(IsDisplayVisibleProperty, value);
    }

    public MaterialContentPage DisplayContent
    {
        get => (MaterialContentPage)GetValue(DisplayContentProperty);
        set => SetValue(DisplayContentProperty, value);
    }

    public ObservableCollection<MaterialTabItem> TabSource
    {
        get => (ObservableCollection<MaterialTabItem>)GetValue(TabSourceProperty);
        set => SetValue(TabSourceProperty, value);
    }

    ObservableCollection<MaterialTabItem> _pages;

    public ObservableCollection<MaterialTabItem> Pages
    {
        get { return _pages; }
        set
        {
            _pages = value; OnPropertyChanged("Pages");
        }
    }

    public int CurrentIndex
    {
        get => (int)GetValue(CurrentIndexProperty);
        set => SetValue(CurrentIndexProperty, value);
    }

    private static void onExtraDataTempalte(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MaterialContent contentPresenter && newValue is MaterialContentPage page)
        {
            contentPresenter.ExtraGrid.Content = page;
            page.OnAppearing();
            System.Diagnostics.Debug.WriteLine($"[Extra] {page}");
        }
    }


    private static void TabSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MaterialContent contentPresenter && newValue is ObservableCollection<MaterialTabItem> items)
        {
            contentPresenter.Pages.Clear();

            foreach (var tab in contentPresenter.TabSource)
            {
                contentPresenter.Pages.Add(tab);
            }

            var page = contentPresenter.Pages.FirstOrDefault();

            if (page != null)
            {
                contentPresenter.ToPage(page);
            }
        }
    }

    public void ToPage(MaterialTabItem item)
    {
        HostGrid.Content = item.Content;
        item.Content.OnAppearing();
        Title = item.Title;
    }

    public static MaterialContent Instance { private set; get; }

    public MaterialContent()
    {
        InitializeComponent();
        Pages = new ObservableCollection<MaterialTabItem>();
        Instance = this;
    }

    void OnRootScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        var position = _headerView.Height + Math.Max(0, RootScrollView.ScrollY - _headerView.Height);
        // AbsoluteLayout.SetLayoutBounds(TabsLayout, new Rectangle(0, position, 1, TabsLayout.Height));

        var opacity = Math.Max(0, Math.Min(RootScrollView.ScrollY, _headerView.Height)) / _headerView.Height - 0.1;
        _headerView.Opacity = Math.Min(1, Math.Max(0, opacity));

        BarStaticLayout.Opacity = Math.Min(1, Math.Max(0, opacity * -1)) * 10;

        //BarStaticLayout.Scale = RootScrollView.ScrollY < 0 ? 1 - RootScrollView.ScrollY / BarStaticLayout.Height * 2 : 1;

        //_headerView.TranslationY = (ImageOverlay.Opacity * 100);

    }

    private void onTabButton1(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"[TAB1]");
    }

    void onTabIconTapped(object sender, EventArgs e)
    {
        Grid grid = (sender as Grid);
        MaterialTabItem item = grid.BindingContext as MaterialTabItem;

        if (item != null)
        {
            foreach (var tabItem in TabSource)
            {
                tabItem.Selected = false;
            }

            item.Selected = true;
            toAnimation(grid);
            var _item = Pages.Where(x => x.Content.Id == item.Content.Id).FirstOrDefault();
            ToPage(_item);
        }
    }

    async Task toAnimation(Grid grid)
    {
        await grid.ScaleTo(1.2, 100);
        await grid.ScaleTo(1,100);
    }
}