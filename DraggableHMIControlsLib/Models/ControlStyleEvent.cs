using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableHMIControlsLib.Models;

public static class ControlStyleEvent
{
    public enum StyleEventType
    {
        ConditionalVisibility,
        ConditionalStyling,
        ConditionalBlinking
    }

    public static List<string> StyleEventNames = new List<string>()
    {
        "ConditionalVisibility",
        "ConditionalStyling",
        "ConditionalBlinking"
    };
}
