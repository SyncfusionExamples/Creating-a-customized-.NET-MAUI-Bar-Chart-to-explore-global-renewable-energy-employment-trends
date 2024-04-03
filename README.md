# Creating a customized .NET MAUI Bar Chart to explore global renewable energy employment trends
This sample demonstrates how to create a customized .NET MAUI Bar Chart to explore global renewable energy employment trends in 2022 using the [Syncfusion .NET MAUI Column Chart with a transposed (Bar Chart)](https://help.syncfusion.com/maui/cartesian-charts/barchart).

<img width="947" alt="Global renewable energy employment trends in 2022" src="https://github.com/SyncfusionExamples/Creating-a-customized-.NET-MAUI-Bar-Chart-to-explore-global-renewable-energy-employment-trends/assets/105496706/ed0f6a71-131f-4458-a850-cd2180ebe257">

 
## Step 1: Gather the renewable energy employment data.

Before proceeding, we should gather renewable energy employment data for various technologies from the [IRENA Report](https://www.irena.org/Energy-Transition/Socio-economic-impact/Energy-and-Jobs) to use in our bar chart

## Step 2: Populate the data for the chart.
To visualize the global renewable energy employment trends across various technologies in 2022, define the **JobsModel** class with the following properties:

Technology - This property represents the different technologies used in renewable energy production.
Jobs - This property represents the number of jobs available in each respective technology.

**C#**
```
public class JobsModel
{
    public string? Technology { get; set; }
    public double Jobs { get; set; }
}
```

Generate the data collection that illustrates the renewable energy employment trends using the **JobsViewModel** class, with the **EmploymentDetails** property. Please refer to the following code example.

**C#**
```
public class JobsViewModel
{
    public ObservableCollection<JobsModel> EmploymentDetails { get; set; }

    public JobsViewModel()
    {
        EmploymentDetails = new ObservableCollection<JobsModel>()
        {
            new JobsModel() { Technology = "Tide, wave and ocean energy", Jobs = 1},
            new JobsModel() { Technology = "Municipal and Industrial waste", Jobs = 27},
            new JobsModel() { Technology = "CSP", Jobs = 80},
            new JobsModel() { Technology = "Others", Jobs = 149},
            new JobsModel() { Technology = "Geothermal energy", Jobs = 152},
            new JobsModel() { Technology = "Heat pumps", Jobs = 241},
            new JobsModel() { Technology = "Biogas", Jobs = 309},
            new JobsModel() { Technology = "Solar heating/cooling", Jobs = 712},
            new JobsModel() { Technology = "Solid biomass", Jobs = 779},
            new JobsModel() { Technology = "Wind energy", Jobs = 1400},
            new JobsModel() { Technology = "Hydropower", Jobs = 2485},
            new JobsModel() { Technology = "Liquid biofuels", Jobs = 2490},
            new JobsModel() { Technology = "Solar photovoltaic", Jobs = 4902},
        };
    }
}
```

## Step 3: Configure the Syncfusion .NET MAUI Cartesian Chart.

Now, let’s configure the Syncfusion .NET MAUI SfCartesianChart control using this [documentation](https://help.syncfusion.com/maui/cartesian-charts/getting-started).

In our case, we need to update the x-axis to reflect the renewable energy technologies employed; therefore, we will use a [CategoryAxis](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.CategoryAxis.html) for the [XAxes](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_XAxes). Since we are using numerical values on the Y-axis to represent job counts, we will employ a [NumericalAxis](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.NumericalAxis.html) for the [YAxes](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_YAxes).

**XAML**
```
<chart:SfCartesianChart>

   <chart:SfCartesianChart.XAxes>
       <chart:CategoryAxis>
       </chart:CategoryAxis>
   </chart:SfCartesianChart.XAxes>

   <chart:SfCartesianChart.YAxes>
       <chart:NumericalAxis>
       </chart:NumericalAxis>
   </chart:SfCartesianChart.YAxes>

</chart:SfCartesianChart>
```

## Step 4: Bind the renewable energy employment data details to the column chart.

Create a column chart with [transposed](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.SfCartesianChart.html#Syncfusion_Maui_Charts_SfCartesianChart_IsTransposedProperty) (bar chart) to display the renewable energy employment across various technologies details. In our case, we want a customized column chart with a customized segment. Therefore, in the code-behind, initialize the column chart and segment it accordingly. Refer to the following code example and utilize a customized column instance to bind the data to the chart.

**C#**
```
public class ColumnSeriesExt : ColumnSeries
{

}

public class ColumnSegmentExt : ColumnSegment
{
        
}
```

To achieve a bar chart, we need to transpose the column chart. Use a customized column instance to bind the data to the chart. Bind the **EmploymentDetails** collection to the **ItemSource** property. The **XBindingPath** and **YBindingPath** properties should be associated with the Technology and Jobs properties, respectively.

**XAML**
```
<chart:SfCartesianChart IsTransposed="True">
…
<local:ColumnSeriesExt ItemsSource="{Binding EmploymentDetails}" XBindingPath=" Technology" YBindingPath="Jobs">

</local:ColumnSeriesExt>

</ chart:SfCartesianChart>
```

## Step 5: Customize the column chart.

Refer to the following code example. In it, the **ColumnSeriesExt** class extends the standard **ColumnSeries** and overrides the **CreateSegment** method. This method is crucial because it determines the creation of a custom **ColumnSegmentExt** instance, allowing the chart to display specialized segments.

The **ColumnSegmentExt** class, which inherits from **ColumnSegment**, contains the drawing path logic for male and female vectors in the custom column segment.

**C#**
```
public class ColumnSeriesExt : ColumnSeries
{
        protected override ChartSegment CreateSegment()
        {
            return new ColumnSegmentExt();
        }
}

public class ColumnSegmentExt : ColumnSegment
{
        int innerRectCount = 1;
        float innerRectHalfWidth = 0;
        float pathHeadRadius = 0;

        protected override void Draw(ICanvas canvas)
        {
            if (Series is ChartSeries series && series.BindingContext is JobsViewModel viewModel)
            {
                 RectF segmentRect = new RectF(Left, Top, Right - Left, Bottom - Top);

                 canvas.SaveState();
                 canvas.ClipRectangle(segmentRect);
                 RectF rect = new RectF() { X = Left, Y = Top, Width = Right - Left, Height = Bottom - Top - viewModel.bottomRectHeight };
                 Brush segmentColor = series.PaletteBrushes[Index];
                 RectF innerRect = new RectF() { X = rect.X, Y = rect.Y, Width = viewModel.innerRectWidth, Height = rect.Height };

                 for (float i = innerRect.X; i < rect.Width; i++)
                 {
                     innerRect.X = i;
                     i += innerRect.Width;
                     innerRectHalfWidth = innerRect.X + innerRect.Width / 2;
                     pathHeadRadius = innerRect.Width / 4.5f;

                     canvas.SaveState();
                     PathF path = new PathF();

                     if (innerRectCount % 2 != 0)
                     {
                        DrawFemalePath(innerRect, innerRectHalfWidth, pathHeadRadius, ref path);
                     }
                     else
                     {
                        DrawMalePath(innerRect, innerRectHalfWidth, pathHeadRadius, ref path);
                     }

                     canvas.SetFillPaint(segmentColor, innerRect);
                     canvas.FillCircle(innerRectHalfWidth, innerRect.Y + pathHeadRadius, pathHeadRadius);
                     canvas.FillPath(path);
                     canvas.RestoreState();
                     innerRectCount++;
                 }

                 RectF bottomRect = new RectF(rect.X, rect.Bottom, rect.Width, viewModel.bottomRectHeight);
                 canvas.SetFillPaint(segmentColor, bottomRect);
                 canvas.FillRectangle(bottomRect);
                 canvas.RestoreState();
            }
        }

        private void DrawFemalePath(RectF innerRect, float innerRectHalfWidth, float pathHeadRadius, ref PathF path)
        {
            //To draw female dress detail
            path.MoveTo(innerRectHalfWidth - pathHeadRadius, innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRectHalfWidth + pathHeadRadius, innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRectHalfWidth + (innerRect.Width / 3), innerRect.Bottom - (innerRect.Height / 6));
            path.LineTo(innerRectHalfWidth - (innerRect.Width / 3), innerRect.Bottom - (innerRect.Height / 6));
            path.Close();
            //To draw female right leg 
            path.MoveTo(innerRectHalfWidth - (pathHeadRadius / 4), innerRect.Bottom - (innerRect.Height / 6));
            path.LineTo(innerRectHalfWidth - (pathHeadRadius / 4), innerRect.Bottom);
            path.LineTo(innerRectHalfWidth - pathHeadRadius, innerRect.Bottom);
            path.LineTo(innerRectHalfWidth - pathHeadRadius, innerRect.Bottom - (innerRect.Height / 6));
            path.Close();
            //To draw female left leg
            path.MoveTo(innerRectHalfWidth + (pathHeadRadius / 4), innerRect.Bottom - (innerRect.Height / 6));
            path.LineTo(innerRectHalfWidth + (pathHeadRadius / 4), innerRect.Bottom);
            path.LineTo(innerRectHalfWidth + pathHeadRadius, innerRect.Bottom);
            path.LineTo(innerRectHalfWidth + pathHeadRadius, innerRect.Bottom - (innerRect.Height / 6));
            path.Close();
            //To draw female right arm
            path.MoveTo(innerRectHalfWidth - pathHeadRadius, innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRect.X, innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRect.X + (float)(innerRect.Width / 8), innerRect.Bottom - (innerRect.Height / 3));
            ////To draw female left arm
            path.MoveTo(innerRectHalfWidth + pathHeadRadius, innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRect.Right, innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRect.Right - (float)(innerRect.Width / 8), innerRect.Bottom - (innerRect.Height / 3));
        }

        private void DrawMalePath(RectF innerRect, float innerRectHalfWidth, float pathHeadRadius, ref PathF path)
        {
            //To draw male dress detail
            path.MoveTo(innerRectHalfWidth - (pathHeadRadius / 4 + pathHeadRadius), innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRectHalfWidth + (pathHeadRadius / 4 + pathHeadRadius), innerRect.Y + (innerRect.Height / 4));
            path.LineTo(innerRectHalfWidth + (pathHeadRadius / 4 + pathHeadRadius), innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRectHalfWidth - (pathHeadRadius / 4 + pathHeadRadius), innerRect.Bottom - (innerRect.Height / 3));
            path.Close();
            //To draw male right leg
            path.MoveTo(innerRectHalfWidth - (pathHeadRadius / 6), innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRectHalfWidth - (pathHeadRadius / 6), innerRect.Bottom);
            path.LineTo(innerRectHalfWidth - pathHeadRadius, innerRect.Bottom);
            path.LineTo(innerRectHalfWidth - pathHeadRadius, innerRect.Bottom - (innerRect.Height / 3));
            path.Close();
            //To draw male left leg
            path.MoveTo(innerRectHalfWidth + (pathHeadRadius / 6), innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRectHalfWidth + (pathHeadRadius / 6), innerRect.Bottom);
            path.LineTo(innerRectHalfWidth + pathHeadRadius, innerRect.Bottom);
            path.LineTo(innerRectHalfWidth + pathHeadRadius, innerRect.Bottom - (innerRect.Height / 3));
            path.Close();
            //To draw male right arm
            path.MoveTo(innerRectHalfWidth - (pathHeadRadius / 4 + pathHeadRadius), innerRect.Y + (innerRect.Height / 5));
            path.LineTo(innerRect.X, innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRect.X + (float)(innerRect.Width / 6), innerRect.Bottom - (innerRect.Height / 3));
            //To draw male left arm
            path.MoveTo(innerRectHalfWidth + (pathHeadRadius / 4 + pathHeadRadius), innerRect.Y + (innerRect.Height / 5));
            path.LineTo(innerRect.Right, innerRect.Bottom - (innerRect.Height / 3));
            path.LineTo(innerRect.Right - (float)(innerRect.Width / 6), innerRect.Bottom - (innerRect.Height / 3));
        }
}
```

## Step 6: Personalize the chart appearance.
We can customize the .NET MAUI bar chart appearance to enhance the readability of the data.

### Adding the chart title :

Incorporating a title into the chart enhances the clarity of the presented data. Please refer to the following code example for customizing the [chart title](https://help.syncfusion.com/maui/cartesian-charts/getting-started#add-a-title).

**XAML**
```
<chart:SfCartesianChart.Title>
    <HorizontalStackLayout >
        <Image Source="clip1.png" Margin="{OnPlatform Default='5,0,5,0', Android='0,0,5,0', iOS='0,0,5,0'}" 
   HeightRequest="{OnPlatform Android=30, Default=40, iOS= 30}"/>
        <Label Text="Global Employment in Renewable Energy Technologies, 2022" 
               FontFamily="OpenSansSemibold" Margin="{OnPlatform Default='0,0,0,5', Android='0', iOS='0'}" 
               HorizontalOptions="Center" HorizontalTextAlignment="Center" 
               VerticalOptions="Center" FontSize="{OnPlatform Default=18, MacCatalyst=24}" TextColor="Black"/>
    </HorizontalStackLayout>
</chart:SfCartesianChart.Title>
```

### Personalizing the chart axis : 

To customize the chart axis, you can refer the following code snippet

**XAML**
```
<chart:SfCartesianChart.XAxes>

    <chart:CategoryAxis EdgeLabelsDrawingMode="Shift"   LabelPlacement="BetweenTicks" ShowMajorGridLines="False">
                        
        <chart:CategoryAxis.MajorTickStyle>
            <chart:ChartAxisTickStyle StrokeWidth="0"/>
        </chart:CategoryAxis.MajorTickStyle>

        <chart:CategoryAxis.LabelStyle>
            <chart:ChartAxisLabelStyle Margin="{OnPlatform WinUI='0,4,4,0',Android='0',MacCatalyst='0,4,0,0',iOS='0'}" FontSize="{OnPlatform MacCatalyst=20}" FontFamily="{OnPlatform WinUI=OpenSansSemibold, iOS=OpenSansSemibold, Android=OpenSansSemibold }"
    TextColor="Black" FontAttributes="Italic"/>
        </chart:CategoryAxis.LabelStyle>

    </chart:CategoryAxis>
</chart:SfCartesianChart.XAxes>

<chart:SfCartesianChart.YAxes>

    <chart:NumericalAxis x:Name="yAxis" Minimum="0" Interval="500" Maximum="5000"
                  ShowMajorGridLines="False" EdgeLabelsDrawingMode="Center"
                  ShowMinorGridLines="False" PlotOffsetEnd="50">

        <chart:NumericalAxis.LabelStyle>
            <chart:ChartAxisLabelStyle TextColor="Black" FontSize="{OnPlatform MacCatalyst=20}" FontFamily="OpenSansSemibold" FontAttributes="Italic"/>
        </chart:NumericalAxis.LabelStyle>

    </chart:NumericalAxis>
</chart:SfCartesianChart.YAxes>
```

**C#**
```
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
```

### Personalizing the series' visuals :

We can enhance the appearance of the series by setting the [palette](https://help.syncfusion.com/maui/cartesian-charts/appearance#applying-palettebrushes-for-series) to the series, which applies predefined brushes to the segments.

**XAML**
```
<local:ColumnSeriesExt PaletteBrushes="{Binding SeriesPaletteBrushes}">

</local:ColumnSeriesExt>
```

**C#**
```
public class JobsViewModel
{
    public ObservableCollection<Brush> SeriesPaletteBrushes { get; set; }

    public JobsViewModel()
    {
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
    }
}
```

### Adding the data labels :

We can make the data easier to read by enabling the chart data labels using the [ShowDataLabels](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Charts.ChartSeries.html#Syncfusion_Maui_Charts_ChartSeries_ShowDataLabels) property.

**XAML**
```
<local:ColumnSeriesExt ShowDataLabels="True">

    <chart:ColumnSeries.DataLabelSettings>
        <chart:CartesianDataLabelSettings LabelPlacement="Outer" UseSeriesPalette="False">

            <chart:CartesianDataLabelSettings.LabelStyle>
                <chart:ChartDataLabelStyle FontSize="{OnPlatform MacCatalyst=20, Default=Medium}" FontFamily="{OnPlatform WinUI=OpenSansSemibold, iOS=OpenSansSemibold, Android=OpenSansSemibold }"></chart:ChartDataLabelStyle>
            </chart:CartesianDataLabelSettings.LabelStyle>

        </chart:CartesianDataLabelSettings>
    </chart:ColumnSeries.DataLabelSettings>
</local:ColumnSeriesExt>
```

In the code example above, the data labels show information about the renewable energy job counts in thousands.

### Incorporating a plot area background view

Finally, incorporate a [plot area background view](https://help.syncfusion.com/maui/cartesian-charts/appearance#plotting-area-customization) to make our Bar Chart more informative and attractive. With this, we can add any view to the chart plot area to include relevant data.

**XAML**
```
<chart:SfCartesianChart.PlotAreaBackgroundView>
    <AbsoluteLayout>
        <Image Source="background.jpg"
               AbsoluteLayout.LayoutBounds="1, 1,-1,-1"
               AbsoluteLayout.LayoutFlags="PositionProportional"
               HeightRequest="{OnPlatform Android=150,iOS=150, MacCatalyst=400, Default=250}"
               WidthRequest="{OnPlatform Android=150,iOS=150, MacCatalyst=300, Default=200}"/>
    </AbsoluteLayout>
</chart:SfCartesianChart.PlotAreaBackgroundView>
```

After executing the previous code examples, the output will resemble the following image.

<img width="947" alt="Global renewable energy employment trends in 2022" src="https://github.com/SyncfusionExamples/Creating-a-customized-.NET-MAUI-Bar-Chart-to-explore-global-renewable-energy-employment-trends/assets/105496706/68e9022c-5579-4987-a40c-2e6fe67fa832">

 You can also refer to the [customized Bar Chart to explore global renewable energy employment trends blog]()

