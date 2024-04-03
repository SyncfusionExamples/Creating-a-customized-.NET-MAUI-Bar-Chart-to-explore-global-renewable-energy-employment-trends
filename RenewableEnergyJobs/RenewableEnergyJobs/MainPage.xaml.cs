using Microsoft.Maui.Graphics.Platform;
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

    public class CategoryAxisExt : CategoryAxis
    {
        protected override void DrawAxis(ICanvas canvas, Rect arrangeRect)
        {
            base.DrawAxis(canvas, arrangeRect);

//#if WINDOWS
//            foreach (ChartAxisLabel label in VisibleLabels)
//            {
//                string? labelText = label.Content.ToString();

//                if (this.BindingContext is JobsViewModel viewModel && labelText != null && viewModel.Streams.ContainsKey(labelText))
//                {
//                    Stream stream = viewModel.Streams[labelText];
//                    var image = PlatformImage.FromStream(stream);
//                    var top = ValueToPoint(label.Position); // Assuming positions start from 0
//                    canvas.SaveState();
//                    canvas.DrawImage(image, (float)arrangeRect.Right - 35, top - 10, 25, 25);
//                    canvas.RestoreState();
//                }
//            }
//#endif
        }
    }
}
