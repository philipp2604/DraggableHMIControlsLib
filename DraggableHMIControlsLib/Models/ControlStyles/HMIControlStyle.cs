using System.Windows;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.ControlStyles;

public class HMIControlStyle
{
    public HMIControlStyle(double height = 0, double width = 0, Brush? foregroundBrush = null, Brush? backgroundBrush = null, Brush? borderBrush = null, double cornerRadius = 0, double fontSize = 0, FontFamily? fontFamily = null, Visibility? visibility = null, bool blinking = false)
    {
        Height = height;
        Width = width;
        ForegroundBrush = foregroundBrush ?? new SolidColorBrush();
        BackgroundBrush = backgroundBrush ?? new SolidColorBrush();
        BorderBrush = borderBrush ?? new SolidColorBrush();
        CornerRadius = cornerRadius;
        FontSize = fontSize;
        FontFamily = fontFamily ?? new FontFamily("Arial");
        Visibility = visibility ?? Visibility.Visible;
        Blinking = blinking;
    }

    public HMIControlStyle(HMIControlStyle style)
    {
        Height = style.Height;
        Width = style.Width;
        ForegroundBrush = style.ForegroundBrush;
        BackgroundBrush = style.BackgroundBrush;
        BorderBrush = style.BorderBrush;
        CornerRadius = style.CornerRadius;
        FontSize = style.FontSize;
        FontFamily = style.FontFamily;
        Visibility = style.Visibility;
        Blinking = style.Blinking;
    }

    public double Height { get; set; }
    public double Width { get; set; }
    public Brush ForegroundBrush { get; set; }
    public Brush BackgroundBrush { get; set; }
    public Brush BorderBrush { get; set; }
    public double CornerRadius { get; set; }
    public double FontSize { get; set; }
    public FontFamily FontFamily { get; set; }
    public Visibility Visibility { get; set; }
    public bool Blinking { get; set; }
}
