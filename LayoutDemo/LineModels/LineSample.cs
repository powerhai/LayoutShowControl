using System;
using System.Windows;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class LineSample : DependencyObject
    {
        public static readonly DependencyProperty SampleIDProperty = DependencyProperty.Register("SampleID", typeof(string), typeof(LineSample));
        public string SampleID
        {
            get
            {
                return (string) GetValue(SampleIDProperty);
            }
            set
            {
                SetValue(SampleIDProperty, value);
            }
        }


        public static readonly DependencyProperty IsSTATProperty = DependencyProperty.Register("IsSTAT", typeof(bool), typeof(LineSample));
        public bool IsSTAT
        {
            get
            {
                return (bool)GetValue(IsSTATProperty);
            }
            set
            {
                SetValue(IsSTATProperty, value);
            }
        }
    }
}
