using System;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class ModuleSubStatusChange
    {
        public bool StatusStopped
        {
            get;
            set;
        }

        public bool StatusDisable
        {
            get;
            set;
        }

        public bool StatusLaneFull
        {
            get;
            set;
        }

        public bool StatusBypass
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