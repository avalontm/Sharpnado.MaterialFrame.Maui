
namespace Sharpnado.MaterialFrame.Maui.Controls;

public partial class MaterialContent : ContentView
{
    public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(MaterialContent), string.Empty);
    public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create(nameof(ContentTemplate), typeof(DataTemplate), typeof(MaterialContent), null, propertyChanged: onContentTemplateChanged);

    public string Title
    {
        get => (string)GetValue(CardTitleProperty);
        set => SetValue(CardTitleProperty, value);
    }

    public DataTemplate ContentTemplate
    {
        get => (DataTemplate)GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    static void onContentTemplateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ContentPage content = (ContentPage)((DataTemplate)newValue).CreateContent();
        ((MaterialContent)bindable).gridMain.Children.Clear();
        ((MaterialContent)bindable).gridMain.Children.Add(content);
    }


    public MaterialContent()
	{
		InitializeComponent();
	}

}