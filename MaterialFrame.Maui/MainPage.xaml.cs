using Sharpnado.MaterialFrame.Maui;

namespace MaterialFrame.Maui
{
    public partial class MainPage : MaterialContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine($"[OnAppearing] MainPage");
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}