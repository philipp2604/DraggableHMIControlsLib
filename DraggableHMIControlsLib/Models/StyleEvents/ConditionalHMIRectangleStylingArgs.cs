using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public partial class ControlStyleEvent
{
    public class ConditionalHMIRectangleStylingArgs : ConditionalStylingArgs
    {
        public Brush? FillBrush { get; set; }
    }
}
