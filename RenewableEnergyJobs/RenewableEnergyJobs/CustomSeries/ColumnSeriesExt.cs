using Syncfusion.Maui.Charts;

namespace RenewableEnergyJobs
{
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
                RectF innerRect = new RectF() { X = rect.X, Y = rect.Y, Width = viewModel.innerRectWidth, Height = rect.Height };// width mac 25

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
            //To draw female left arm
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
}
