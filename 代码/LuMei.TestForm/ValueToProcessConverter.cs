﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LuMei.TestForm
{
    class ValueToProcessConverter : IValueConverter
    {
        private const double Thickness = 8;
        private const double Padding = 1;
        private const double WarnValue = 60;
        private const int SuccessRateFontSize = 34;
        private static readonly SolidColorBrush NormalBrush;
        private static readonly SolidColorBrush WarnBrush;
        private static readonly Typeface SuccessRateTypeface;

        private string _percentString;
        private Point _centerPoint;
        private double _radius;

        static ValueToProcessConverter()
        {
            NormalBrush = new SolidColorBrush(Colors.Green);
            WarnBrush = new SolidColorBrush(Colors.Red);
            SuccessRateTypeface = new Typeface(new FontFamily("MSYH"), new FontStyle(), new FontWeight(), new FontStretch());
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double && !string.IsNullOrEmpty((string)parameter)) {
                double arg = (double)value;
                double width = double.Parse((string)parameter);
                _radius = width / 2;
                _centerPoint = new Point(_radius, _radius);

                return DrawBrush(arg, 100, _radius, _radius, Thickness, Padding);
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据角度获取坐标
        /// </summary>
        /// <param name="centerPoint"></param>
        /// <param name="r"></param>
        /// <param name="angel"></param>
        /// <returns></returns>
        private Point GetPointByAngel(Point centerPoint, double r, double angel)
        {
            Point p = new Point();
            p.X = Math.Sin(angel * Math.PI / 180) * r + centerPoint.X;
            p.Y = centerPoint.Y - Math.Cos(angel * Math.PI / 180) * r;

            return p;
        }

        /// <summary>
        /// 根据4个坐标画出扇形
        /// </summary>
        /// <param name="bigFirstPoint"></param>
        /// <param name="bigSecondPoint"></param>
        /// <param name="smallFirstPoint"></param>
        /// <param name="smallSecondPoint"></param>
        /// <param name="bigRadius"></param>
        /// <param name="smallRadius"></param>
        /// <param name="isLargeArc"></param>
        /// <returns></returns>
        private Geometry DrawingArcGeometry(Point bigFirstPoint, Point bigSecondPoint, Point smallFirstPoint, Point smallSecondPoint, double bigRadius, double smallRadius, bool isLargeArc)
        {
            PathFigure pathFigure = new PathFigure { IsClosed = true };
            pathFigure.StartPoint = bigFirstPoint;
            pathFigure.Segments.Add(
              new ArcSegment {
                  Point = bigSecondPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(bigRadius, bigRadius),
                  SweepDirection = SweepDirection.Clockwise
              });
            pathFigure.Segments.Add(new LineSegment { Point = smallSecondPoint });
            pathFigure.Segments.Add(
             new ArcSegment {
                 Point = smallFirstPoint,
                 IsLargeArc = isLargeArc,
                 Size = new Size(smallRadius, smallRadius),
                 SweepDirection = SweepDirection.Counterclockwise
             });
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        /// <summary>
        /// 根据当前值和最大值获取扇形
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private Geometry GetGeometry(double value, double maxValue, double radiusX, double radiusY, double thickness, double padding)
        {
            bool isLargeArc = false;
            double percent = value / maxValue;
            _percentString = string.Format("{0}%", Math.Round(percent * 100));
            double angel = percent * 360D;
            if (angel > 180) isLargeArc = true;
            double bigR = radiusX;
            double smallR = radiusX - thickness + padding;
            Point firstpoint = GetPointByAngel(_centerPoint, bigR, 0);
            Point secondpoint = GetPointByAngel(_centerPoint, bigR, angel);
            Point thirdpoint = GetPointByAngel(_centerPoint, smallR, 0);
            Point fourpoint = GetPointByAngel(_centerPoint, smallR, angel);
            return DrawingArcGeometry(firstpoint, secondpoint, thirdpoint, fourpoint, bigR, smallR, isLargeArc);
        }

        private void DrawingGeometry(DrawingContext drawingContext, double value, double maxValue, double radiusX, double radiusY, double thickness, double padding)
        {
            if (value != maxValue) {
                SolidColorBrush brush;
                if (value < WarnValue) {
                    brush = WarnBrush;
                } else {
                    brush = NormalBrush;
                }
                drawingContext.DrawEllipse(null, new Pen(new SolidColorBrush(Color.FromRgb(0xdd, 0xdf, 0xe1)), thickness), _centerPoint, radiusX, radiusY);
                drawingContext.DrawGeometry(brush, new Pen(), GetGeometry(value, maxValue, radiusX, radiusY, thickness, padding));
                FormattedText formatWords = new FormattedText(_percentString,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    SuccessRateTypeface,
                    SuccessRateFontSize,
                    brush);
                Point startPoint = new Point(_centerPoint.X - formatWords.Width / 2, _centerPoint.Y - formatWords.Height / 2);
                drawingContext.DrawText(formatWords, startPoint);
            } else {
                drawingContext.DrawEllipse(null, new Pen(NormalBrush, thickness), _centerPoint, radiusX, radiusY);
                FormattedText formatWords = new FormattedText("100%",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    SuccessRateTypeface,
                    SuccessRateFontSize,
                    NormalBrush);
                Point startPoint = new Point(_centerPoint.X - formatWords.Width / 2, _centerPoint.Y - formatWords.Height / 2);
                drawingContext.DrawText(formatWords, startPoint);
            }

            drawingContext.Close();
        }

        /// <summary>
        /// 根据当前值和最大值画出进度条
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private Visual DrawShape(double value, double maxValue, double radiusX, double radiusY, double thickness, double padding)
        {
            DrawingVisual drawingWordsVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingWordsVisual.RenderOpen();

            DrawingGeometry(drawingContext, value, maxValue, radiusX, radiusY, thickness, padding);

            return drawingWordsVisual;
        }

        /// <summary>
        /// 根据当前值和最大值画出进度条
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private Brush DrawBrush(double value, double maxValue, double radiusX, double radiusY, double thickness, double padding)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            DrawingContext drawingContext = drawingGroup.Open();

            DrawingGeometry(drawingContext, value, maxValue, radiusX, radiusY, thickness, padding);

            DrawingBrush brush = new DrawingBrush(drawingGroup);

            return brush;
        }

    }
}
