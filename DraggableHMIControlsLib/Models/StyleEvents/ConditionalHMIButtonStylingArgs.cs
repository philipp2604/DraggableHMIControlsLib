using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.StyleEvents;

public partial class ControlStyleEvent
{
    public class ConditionalHMIButtonStylingArgs : ConditionalStylingArgs
    {
        public Brush? ForegroundBrush { get; set; }
        public Brush? BackgroundBrush { get; set; }
        public Brush? BorderBrush { get; set; }
        public double? FontSize { get; set; }
        public FontFamily? FontFamily { get; set; }
    }
}
