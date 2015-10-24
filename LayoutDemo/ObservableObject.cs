using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using LayoutDemo.Properties;

namespace LayoutDemo
{

    public abstract class ObservableObject :  DependencyObject, INotifyPropertyChanged
    {
        #region Constructor
        protected ObservableObject()
        {

        }

        #endregion // Constructor

        #region RaisePropertyChanged

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }



        #endregion // RaisePropertyChanged

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // INotifyPropertyChanged Members
    }



}