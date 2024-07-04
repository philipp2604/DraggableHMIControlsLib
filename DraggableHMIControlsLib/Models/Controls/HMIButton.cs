using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.ControlStyles;

namespace DraggableHMIControlsLib.Models.Controls;

public class HMIButton : HMIControl
{
    public HMIButton(string name, string text = "", HMIButtonStyle? buttonStyle = null) : base(name)
    {
        Text = text;

        Actions = new List<HMIButtonAction>();

        BaseStyle = buttonStyle ?? new HMIButtonStyle();
        CurrentStyle = new HMIButtonStyle((HMIButtonStyle)BaseStyle);
    }

    public string Text { get; set; }

    public List<HMIButtonAction> Actions { get; set; }

}
