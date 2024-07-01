using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.ControlActions;

namespace DraggableHMIControlsLib.Models;

public class HMIButton : HMIControl
{
    public HMIButton(string name, string text = "", HMIControlStyle? buttonStyle = null) : base(name)
    {
        Text = text;

        ClickedActions = new List<(ActionType actionType, int tagId, object param)>();
        PressedActions = new List<(ActionType actionType, int tagId, object param)>();
        ReleasedActions = new List<(ActionType actionType, int tagId, object param)>();

        Style = buttonStyle ?? new HMIControlStyle(
            50,                                                                                                                     //Height
            150,                                                                                                                    //Width
            new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#000000")),      //Foreground brush
            new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#DDDDDD")),      //Background brush
            new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#707070")),      //Border brush
            0,                                                                                                                      //Corner radius
            12,                                                                                                                     //Font size
            new FontFamily("Segeo UI"),                                                                                             //Font family
            Visibility.Visible                                                                                                      //Visibility
            );                                                                                                    
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
