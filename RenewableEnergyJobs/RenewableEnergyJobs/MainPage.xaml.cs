using Syncfusion.Maui.Charts;

namespace RenewableEnergyJobs
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

#if !ANDROID && !IOS
            ChartAxisTitle title = new ChartAxisTitle()
            {
                Text = "Jobs (Thousands)",
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                FontSize = labelStyle.FontSize,
            };
            yAxis.Title = title;
#endif
        }
    }
}
