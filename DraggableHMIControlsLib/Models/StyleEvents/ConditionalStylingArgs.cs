using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public partial class ControlStyleEvent
{
    public class ConditionalStylingArgs
    {
        public double? Height { get; set; } = null;
        public double? Width { get; set; } = null;
        public double? CornerRadius { get; set; }
        public Visibility? Visibility { get; set; }
        public bool? Blinking { get; set; }
    }
}
