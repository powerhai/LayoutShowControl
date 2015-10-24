namespace BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels
{
    public interface IBranchDevice
    {

        bool? IsOnLeft { get; set; }
        int ParentTLaneUnit { get; set; }
    }
}