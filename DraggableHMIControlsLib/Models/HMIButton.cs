using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DraggableHMIControlsLib.Models.ControlActions;

namespace DraggableHMIControlsLib.Models;

public class HMIButton : HMIControl
{
    public HMIButton(string name, string text = "", double width = 150, double height = 50) : base(name)
    {
        Text = text;
        Width = width;
        Height = height;

        ClickedActions = new List<(ActionType actionType, int tagId, object param)>();
        PressedActions = new List<(ActionType actionType, int tagId, object param)>();
        ReleasedActions = new List<(ActionType actionType, int tagId, object param)>();
    }

    public string Text { get; set; }

    public static List<ActionType> AvailableActions = new List<ActionType>()
    {
        ActionType.SetBit,
        ActionType.SetBitInVariable,
        ActionType.InvertBit,
        ActionType.InvertBitInVariable,
        ActionType.ResetBit,
        ActionType.ResetBitInVariable,
        ActionType.IncrementVariable,
        ActionType.DecrementVariable,
        ActionType.SetVariable
    };

    public List<(ActionType actionType, int tagId, object param)> ClickedActions { get; set; }
    public List<(ActionType actionType, int tagId, object param)> PressedActions { get; set; }
    public List<(ActionType actionType, int tagId, object param)> ReleasedActions { get; set; }
}
