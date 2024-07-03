using System.Windows.Media;
using System.Windows;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public partial class ControlStyleEvent
{

    public ControlStyleEvent()
    {
        TagValue = 0;
    }

    public StyleEventType EventType { get; set; }
    public int TagId { get; set; }
    public object TagValue { get; set; }
    public object? Parameter { get; set; }
    public bool IsActive { get; set; }


    public enum StyleEventType
    {
        ConditionalStyling
    }


    public static List<string> StyleEventNames = new List<string>()
    {
        "ConditionalStyling"
    };
}
