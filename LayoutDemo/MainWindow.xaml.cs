using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels;

namespace LayoutDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private double mScale;
        private LayoutController mController;
        public MainWindow()
        {
            mController= new LayoutController();
            InitializeComponent();
           
            this.Loaded += MainWindowLoaded;
            LV_Line.Loaded += LvLineLoaded;
        }
        private void LvLineLoaded (object sender, RoutedEventArgs e)
        {
             ZoomToShowAll(mController.Modules);
        }

        void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = mController;
        }

        private void ZoomToShowAll(List<LineModule> sampleTrackingModules)
        {
            int countTlane = sampleTrackingModules.Count(a => a is LaneT);
            mScale = 1;
            if(countTlane >= 7)
            {
                mScale = 6;
            }
            if (countTlane >= 8)
            {
                mScale = 0.8;
            }
            if (countTlane >= 10)
            {
                mScale = 0.7;
            }
            if (countTlane >= 12)
            {
                mScale = 0.6;
            }
            if (countTlane >= 14)
            {
                mScale = 0.5;
            }
            viewbox.LayoutTransform = new ScaleTransform(mScale, mScale);
 
        }
    }
}
