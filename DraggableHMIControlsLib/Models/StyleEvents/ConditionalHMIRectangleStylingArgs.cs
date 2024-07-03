using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public partial class ControlStyleEvent
{
    public class ConditionalHMIRectangleStylingArgs : ConditionalStylingArgs
    {
        public Brush? FillBrush { get; set; }
    }
}
