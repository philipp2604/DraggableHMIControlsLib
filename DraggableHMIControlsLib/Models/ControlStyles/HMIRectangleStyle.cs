using System.Windows;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.ControlStyles;

public class HMIRectangleStyle : HMIControlStyle
{
    public HMIRectangleStyle() : base()
    {
        Height = 50;
        Width = 150;
        CornerRadius = 0;
        Visibility = Visibility.Visible;
        Blinking = false;
        FillBrush = new SolidColorBrush(Colors.Black);
    }

    public HMIRectangleStyle(HMIRectangleStyle style)
    {
        Height = style.Height;
        Width = style.Width;
        CornerRadius = style.CornerRadius;
        Visibility = style.Visibility;
        Blinking = style.Blinking;
        FillBrush = style.FillBrush;
    }

    public Brush FillBrush { get; set; }
}
