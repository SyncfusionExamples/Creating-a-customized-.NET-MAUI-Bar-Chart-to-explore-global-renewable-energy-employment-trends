using System.Collections.ObjectModel;

namespace RenewableEnergyJobs
{
    public class JobsViewModel
    {
        public ObservableCollection<JobsModel> EmploymentDetails { get; set; }
        public ObservableCollection<Brush> SeriesPaletteBrushes { get; set; }

        public float bottomRectHeight;

        public float innerRectWidth;

        public JobsViewModel()
        {
            EmploymentDetails = new ObservableCollection<JobsModel>()
            {
                new JobsModel() { Technology = "Tide, wave and ocean energy", Jobs = 1},
                new JobsModel() { Technology = "Municipal and Industrial waste", Jobs = 27},
                new JobsModel() { Technology = "CSP", Jobs = 80 },
                new JobsModel() { Technology = "Others", Jobs = 149},
                new JobsModel() { Technology = "Geothermal energy", Jobs = 152 },
                new JobsModel() { Technology = "Heat pumps", Jobs = 241},
                new JobsModel() { Technology = "Biogas", Jobs = 309 },
                new JobsModel() { Technology = "Solar heating/cooling", Jobs = 712},
                new JobsModel() { Technology = "Solid biomass", Jobs = 779},
                new JobsModel() { Technology = "Wind energy", Jobs = 1400},
                new JobsModel() { Technology = "Hydropower", Jobs = 2485},
                new JobsModel() { Technology = "Liquid biofuels", Jobs = 2490},
                new JobsModel() { Technology = "Solar photovoltaic", Jobs = 4902},
            };

            SeriesPaletteBrushes = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#3a6baa")),
                new SolidColorBrush(Color.FromArgb("#cb9b55")),
                new SolidColorBrush(Color.FromArgb("#b3b300")),
                new SolidColorBrush(Color.FromArgb("#520277")),
                new SolidColorBrush(Color.FromArgb("#ab7b43")),
                new SolidColorBrush(Color.FromArgb("#c0c0c0")),
                new SolidColorBrush(Color.FromArgb("#03a003")),
                new SolidColorBrush(Color.FromArgb("#ffcc00")),
                new SolidColorBrush(Color.FromArgb("#a52a2a")),
                new SolidColorBrush(Color.FromArgb("#808080")),
                new SolidColorBrush(Color.FromArgb("#2355ea")),
                new SolidColorBrush(Color.FromArgb("#4ad54a")),
                new SolidColorBrush(Color.FromArgb("#ffa600")),
            };

            SetPlatformValues();
        }

        private void SetPlatformValues()
        {
            if (Microsoft.Maui.Devices.DeviceInfo.Platform == Microsoft.Maui.Devices.DevicePlatform.MacCatalyst)
            {
                bottomRectHeight = 5;
                innerRectWidth = 25;
            }
            else
            {
                bottomRectHeight = 3;
                innerRectWidth = 15;
            }
        }
    }
}
