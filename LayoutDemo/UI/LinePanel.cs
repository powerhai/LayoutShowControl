using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.UI
{
    public class LinePanel : Panel
    {
        private const int INSTRUMENT_WIDTH = 90;
        private const int INSTRUMENT_HEIGHT = 30;
        private const int LANE_WIDTH = INSTRUMENT_HEIGHT;
        private const int LANE_HEIGHT = INSTRUMENT_HEIGHT;
        private const int LANE_INTERVAL = INSTRUMENT_WIDTH;
        private const int BUS_THICKNESS = 9;
        private const int BRANCH_THICKNESS = 5;
        private const int ROW_INTERVAL = INSTRUMENT_HEIGHT/2;
        private const int INSTRUMENT_INTERVAL = 10;
        private readonly Brush BusColor = Brushes.DarkGray;
        private readonly Brush BranchColor = Brushes.Gainsboro;

        private readonly List<FrameworkElement> BusDevices = new List<FrameworkElement>();
        private readonly List<FrameworkElement> BranchDevices = new List<FrameworkElement>();
        private readonly Dictionary<LaneT, FrameworkElement> TLanes = new Dictionary<LaneT, FrameworkElement>();
        private int mBusY = 0;

 
        protected override Size MeasureOverride(Size availableSize)
        {
            if (base.InternalChildren.Count <= 0)
                return new Size(100, 100);
            BusDevices.Clear();
            BranchDevices.Clear();
            TLanes.Clear();

            var onTopMaxModulesCount = 0;
            var onDownMaxModulesCount = 0;

            foreach (FrameworkElement item in base.InternalChildren)
            {
                if (item.DataContext is IBusDevice)
                {
                    BusDevices.Add(item);
                }
                else if (item.DataContext is IBranchDevice)
                {
                    BranchDevices.Add(item);
                }
                if (item.DataContext is LaneT)
                {
                    var lanet = item.DataContext as LaneT;
                    TLanes.Add(lanet, item);
                    if (lanet.IsChildrenOnTop)
                        onTopMaxModulesCount = Math.Max(onTopMaxModulesCount, lanet.Modules.Count);
                    else
                        onDownMaxModulesCount = Math.Max(onDownMaxModulesCount, lanet.Modules.Count);
                }
                item.Measure(availableSize);
            }
            mBusY = onTopMaxModulesCount * (ROW_INTERVAL + INSTRUMENT_HEIGHT);


            double left = 0;
            bool prvHasRightModules = false;
            bool prvIsOnTop = false;
            for (var i = 0;i < BusDevices.Count ;i++ )
            {
                var module = BusDevices[i];
                var tlane = BusDevices[i].DataContext as LaneT;
                if (tlane != null)
                {
                    if (prvIsOnTop == tlane.IsChildrenOnTop && prvHasRightModules && tlane.HasLeftChildren)
                        left += INSTRUMENT_WIDTH + INSTRUMENT_INTERVAL;
                }
                module.Arrange(new Rect(left, mBusY, module.DesiredSize.Width, module.DesiredSize.Height));
               
                if (tlane != null)
                {
                    prvIsOnTop = tlane.IsChildrenOnTop;
                    if (tlane.HasRightChildren)
                    {
                        left += module.ActualWidth / 2 + INSTRUMENT_WIDTH + INSTRUMENT_INTERVAL;
                        prvHasRightModules = true;
                    }
                    else
                    {
                        prvHasRightModules = false;
                        left += module.ActualWidth + LANE_INTERVAL;
                    }
                    if (i == BusDevices.Count - 1 && tlane.HasRightChildren )
                    {
                        left += module.ActualWidth / 2 + INSTRUMENT_WIDTH + INSTRUMENT_INTERVAL;
                    }
                }
                else
                {
                    left += module.ActualWidth + LANE_INTERVAL;
                }
            }
            left -= LANE_INTERVAL;

            return new Size(left, (onDownMaxModulesCount + onTopMaxModulesCount) * (ROW_INTERVAL + INSTRUMENT_HEIGHT) + LANE_HEIGHT);

        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double left = 0;
            bool prvHasRightModules = false;
            bool prvIsOnTop = false;
            foreach (var item in BusDevices)
            {
                var tlane = item.DataContext as LaneT;
                if (tlane != null)
                {
                    if (prvIsOnTop == tlane.IsChildrenOnTop && prvHasRightModules && tlane.HasLeftChildren)
                        left += INSTRUMENT_WIDTH + INSTRUMENT_INTERVAL;
                }
                item.Arrange(new Rect(left, mBusY, item.DesiredSize.Width, item.DesiredSize.Height));
                if (tlane != null)
                {
                    prvIsOnTop = tlane.IsChildrenOnTop;
                    if (tlane.HasRightChildren)
                    {
                        left += item.ActualWidth / 2 + INSTRUMENT_WIDTH + INSTRUMENT_INTERVAL;
                        prvHasRightModules = true;
                    }
                    else
                    {
                        prvHasRightModules = false;
                        left += item.ActualWidth + LANE_INTERVAL;
                    }
                }
                else
                {
                    left += item.ActualWidth + LANE_INTERVAL;
                }
            }

            foreach (var item in BranchDevices)
            {
                var dev = item.DataContext as IBranchDevice;
                var tlane = TLanes.First(a => a.Key.Unit == dev.ParentTLaneUnit);
                var tlanePoint = tlane.Value.TranslatePoint(new Point(LANE_WIDTH / 2, LANE_HEIGHT / 2), this);
                var index = tlane.Key.Modules.IndexOf(dev);
                var devLeft = dev.IsOnLeft.HasValue
                    ? (dev.IsOnLeft.Value
                        ? tlanePoint.X - item.DesiredSize.Width - INSTRUMENT_INTERVAL
                        : tlanePoint.X + INSTRUMENT_INTERVAL)
                    : tlanePoint.X - (item.DesiredSize.Width / 2);

                var devTop = tlane.Key.IsChildrenOnTop ? tlanePoint.Y - index * (INSTRUMENT_HEIGHT + ROW_INTERVAL) - ROW_INTERVAL - LANE_HEIGHT - (LANE_HEIGHT / 2) : tlanePoint.Y + index * (INSTRUMENT_HEIGHT + ROW_INTERVAL) + (LANE_HEIGHT / 2) + ROW_INTERVAL;
                item.Arrange(new Rect(new Point(devLeft, devTop), item.DesiredSize));
                
            }

            this.InvalidateVisual();
            return finalSize;
        }


        protected override void OnRender(DrawingContext dc)
        {
            if (BusDevices.Count <= 0)
                return;

            if (BusDevices.Count >= 2)
            {
                var busPoint1 = BusDevices[0].TranslatePoint(new Point(BusDevices[0].ActualWidth / 2, BusDevices[0].ActualHeight / 2), this);
                var busPoint2 = BusDevices[BusDevices.Count - 1].TranslatePoint(new Point(BusDevices[BusDevices.Count - 1].ActualWidth / 2, BusDevices[BusDevices.Count - 1].ActualHeight / 2), this);
                dc.DrawLine(new Pen(BusColor, BUS_THICKNESS), busPoint1, busPoint2);
            }

            foreach (var item in TLanes)
            {
                if (item.Key.HasChildren)
                {
                    var lanePoint = item.Value.TranslatePoint(new Point(item.Value.ActualWidth / 2, item.Value.ActualHeight / 2), this);
                    var endPoint = new Point(lanePoint.X, item.Key.IsChildrenOnTop ? lanePoint.Y - ((INSTRUMENT_HEIGHT + ROW_INTERVAL) * item.Key.Modules.Count) : lanePoint.Y + ((INSTRUMENT_HEIGHT + ROW_INTERVAL) * item.Key.Modules.Count));
                    dc.DrawLine(new Pen(BranchColor, BRANCH_THICKNESS), lanePoint, endPoint);

                    for (int i = 0; i < item.Key.Modules.Count; i++)
                    {
                        var inleft = item.Key.Modules[i].IsOnLeft;
                        var p1 = new Point(lanePoint.X, item.Key.IsChildrenOnTop ? lanePoint.Y - ((INSTRUMENT_HEIGHT + ROW_INTERVAL) * i) - (INSTRUMENT_HEIGHT + ROW_INTERVAL) : lanePoint.Y + ((INSTRUMENT_HEIGHT + ROW_INTERVAL) * i) + (INSTRUMENT_HEIGHT + ROW_INTERVAL));
                        var p2 = new Point(inleft.HasValue ? (inleft.Value == true ? p1.X - (INSTRUMENT_HEIGHT + ROW_INTERVAL) : p1.X + (INSTRUMENT_HEIGHT + ROW_INTERVAL)) : p1.X, p1.Y);
                        if (inleft.HasValue && inleft.Value == true)
                        {
                            p1.X = p1.X + (BRANCH_THICKNESS / 2) + 0.5;
                        }
                        else
                        {
                            p1.X = p1.X - (BRANCH_THICKNESS / 2) - 0.5;
                        }

                        dc.DrawLine(new Pen(BranchColor, BRANCH_THICKNESS), p1, p2);

                    }

                }
            }
            base.OnRender(dc);
        }
    }
}
