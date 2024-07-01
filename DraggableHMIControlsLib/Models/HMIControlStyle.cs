using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models
{
    public class HMIControlStyle
    {
        public HMIControlStyle(double height, double width, Brush foregroundBrush, Brush backgroundBrush, Brush borderBrush, double cornerRadius, double fontSize, FontFamily fontFamily)
        {
            Height = height;
            Width = width;
            ForegroundBrush = foregroundBrush;
            BackgroundBrush = backgroundBrush;
            BorderBrush = borderBrush;
            CornerRadius = cornerRadius;
            FontSize = fontSize;
            FontFamily = fontFamily;
        }

        public double Height { get; set; }
        public double Width { get; set; }
        public Brush ForegroundBrush { get; set; }
        public Brush BackgroundBrush { get; set; }
        public Brush BorderBrush { get; set; }
        public double CornerRadius { get; set; }
        public double FontSize { get; set; }
        public FontFamily FontFamily { get; set; }
    }
}
