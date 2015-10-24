using System;
using System.Windows;
using LayoutDemo;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class LineModule : ObservableObject
    {

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(LcModuleStatus), typeof(LineModule));

        public LcModuleStatus Status
        {
            get { return (LcModuleStatus)GetValue(StatusProperty); }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

     

   
 
 
  
       

        private string mLabel;
 

        public string Label
        {
            get { return mLabel; }
            set
            {
                mLabel = value;
                RaisePropertyChanged();
            }
        }
        public int Instrument
        {
            get;
            set;
        }
        public int Unit
        {
            get;
            set;
        }

 
    }
}