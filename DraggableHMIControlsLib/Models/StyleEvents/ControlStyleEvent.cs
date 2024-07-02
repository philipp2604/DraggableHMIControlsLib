using System.Windows.Media;
using System.Windows;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public class ControlStyleEvent
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


    public class ConditionalStylingArgs
    {
        public double? Height { get; set; } = null;
        public double? Width { get; set; } = null;
        public Brush? ForegroundBrush { get; set; }
        public Brush? BackgroundBrush { get; set; }
        public Brush? BorderBrush { get; set; }
        public double? CornerRadius { get; set; }
        public double? FontSize { get; set; }
        public FontFamily? FontFamily { get; set; }
        public Visibility? Visibility { get; set; }
        public bool? Blinking { get; set; }
    }
}
