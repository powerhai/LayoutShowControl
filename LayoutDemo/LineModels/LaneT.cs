using System.Collections.ObjectModel;
using System.Linq;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class LaneT : LineModule, IBusDevice
    {
        private ObservableCollection<IBranchDevice> mModules;
        

        public bool IsChildrenOnTop { get; set; }
         
        public bool HasLeftChildren { get { return Modules.Count > 0 && Modules.Any(a => a.IsOnLeft == true); } }
        public bool HasRightChildren { get { return Modules.Count > 0 && Modules.Any(a => a.IsOnLeft == false); } }

        public bool HasChildren { get { return Modules.Count > 0; } }
        public ObservableCollection<IBranchDevice> Modules
        {
            get { return mModules ?? (mModules = new ObservableCollection<IBranchDevice>()); }
        }
    }
}