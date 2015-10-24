using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class ModuleStatusChange
    {
        public LcModuleStatus Status
        {
            get;
            set;
        }

        public LineModule Module
        {
            get;
            set;
        }
        public DateTime ChangedTime
        {
            get;
            set;
        }
    }
}
