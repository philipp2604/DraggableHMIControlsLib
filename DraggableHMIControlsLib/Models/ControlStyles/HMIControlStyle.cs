using System.Windows;

namespace DraggableHMIControlsLib.Models.ControlStyles;

public class HMIControlStyle
{
    public HMIControlStyle(double height = 0, double width = 0, double cornerRadius = 0, Visibility? visibility = null, bool blinking = false)
    {
        Height = height;
        Width = width;
        CornerRadius = cornerRadius;
        Visibility = visibility ?? Visibility.Visible;
        Blinking = blinking;
    }

    public HMIControlStyle(HMIControlStyle style)
    {
        Height = style.Height;
        Width = style.Width;
        CornerRadius = style.CornerRadius;
        Visibility = style.Visibility;
        Blinking = style.Blinking;
    }

    public double Height { get; set; }
    public double Width { get; set; }
    public double CornerRadius { get; set; }
    public Visibility Visibility { get; set; }
    public bool Blinking { get; set; }
}
