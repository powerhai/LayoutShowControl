using System.Windows;

namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public class LcInstrument : LineModule, ITestDevice
    {
       
        public bool? IsOnLeft { get; set; }
        public int ParentTLaneUnit { get; set; }

    }
}