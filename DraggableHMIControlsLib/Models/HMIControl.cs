using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models;

public abstract class HMIControl
{
    public HMIControl(string name)
    {
        Name = name;
        Style = new HMIControlStyle(0, 0, new SolidColorBrush(), new SolidColorBrush(), new SolidColorBrush());
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Thickness ParentMargin { get; set; }
    public HMIControlStyle Style { get; set; }

    public class HMIControlStyle
    {
        public HMIControlStyle(double height, double width, Brush foregroundBrush, Brush backgroundBrush, Brush borderBrush)
        {
            Height = height;
            Width = width;
            ForegroundBrush = foregroundBrush;
            BackgroundBrush = backgroundBrush;
            BorderBrush = borderBrush;
        }

        public double Height { get; set; }
        public double Width { get; set; }
        public Brush ForegroundBrush { get; set; }
        public Brush BackgroundBrush { get; set; }
        public Brush BorderBrush { get; set; }
        public double CornerRadius { get; set; }
        public double FontSize { get; set; }
    }
}
