using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.ControlStyles;
namespace DraggableHMIControlsLib.Models.Controls;

public class HMIRectangle : HMIControl
{
    public HMIRectangle(string name, HMIRectangleStyle? rectangleStyle = null) : base(name)
    {
        Actions = new List<HMIRectangleAction>();

        BaseStyle = rectangleStyle ?? new HMIRectangleStyle();
        CurrentStyle = new HMIRectangleStyle((HMIRectangleStyle)BaseStyle);
    }

    public List<HMIRectangleAction> Actions { get; set; }
}
