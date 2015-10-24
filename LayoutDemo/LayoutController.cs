using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels;
using Microsoft.Practices.Prism.Commands;

namespace LayoutDemo
{
    public class LayoutController
    {
        private List<LineModule> mModules = new List<LineModule>();
 
        public List<LineModule> Modules
        {
            get
            {
                return mModules;
            }
            set
            {
                mModules = value;
            }

        }
        public static Random RandomObject = new Random();
        public LayoutController ()
        {
            LoadModules();
        }

        public void LoadModules ()
        {
            Modules.Add(new Inlet(){Instrument = 11, Unit=1 , Label="Inlet"});
            Modules.Add(new ErrorLane(){Instrument=5, Unit=1, Label= "ErrorLane"});

            //add tlane 1
            {
                var tlane = new LaneT()
                {
                    Instrument = 2,
                    Unit = 1,
                    IsChildrenOnTop = true,
                    Label = "T1",
                    Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown
                };
                this.Modules.Add(tlane);

                {
                    var module = new LcInstrument() {Label = "DXI800-1" , Instrument=18, IsOnLeft=true, Status= BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Warning, ParentTLaneUnit = tlane.Unit};
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "DXI800-2", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Error, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "DXI800-3", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }
            }

            //add tlane 1
            {
                var tlane = new LaneT()
                {
                    Instrument = 2,
                    Unit = 2,
                    IsChildrenOnTop = true,
                    Label = "T2",
                    Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown
                };
                this.Modules.Add(tlane);

                {
                    var module = new LcInstrument() { Label = "DXI800-1", Instrument = 18, IsOnLeft = true, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Warning, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "DXI800-2", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Error, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "DXI800-3", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }
            }


            //add tlane 3
            {
                var tlane = new LaneT()
                {
                    Instrument = 2,
                    Unit = 3,
                    IsChildrenOnTop = false,
                    Label = "T3",
                    Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown
                };
                this.Modules.Add(tlane);

                {
                    var module = new LcInstrument() { Label = "S-1", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Warning, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "B-2", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Error, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "KKO-3", Instrument = 18, IsOnLeft = false, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }
            }

            //add tlane 3
            {
                var tlane = new LaneT()
                {
                    Instrument = 2,
                    Unit = 4,
                    IsChildrenOnTop = false,
                    Label = "T4",
                    Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown
                };
                this.Modules.Add(tlane);

                {
                    var module = new LcInstrument() { Label = "S-1", Instrument = 18, IsOnLeft = true, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Warning, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }

                {
                    var module = new LcInstrument() { Label = "B-2", Instrument = 18, IsOnLeft = true, Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Error, ParentTLaneUnit = tlane.Unit };
                    this.Modules.Add(module);
                    tlane.Modules.Add(module);
                }
 
            }
        }
        public ICommand TestCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    var num = RandomObject.Next(0, this.Modules.Count);
                    var num2 = RandomObject.Next(0, 4);
                    switch(num2)
                    {
                        case 0:
                            Modules[num].Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Error;
                            break;
                        case 1:
                            Modules[num].Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Normal;
                            break;
                        case 2: 
                            Modules[num].Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Unknown;
                            break;
                        case 3:
                            Modules[num].Status = BCI.Cabernet.Presentation.Modules.SystemManagement.LineModels.LcModuleStatus.Warning;
                            break;
                    }


                    
                }); 
            }
        }
    }
}
