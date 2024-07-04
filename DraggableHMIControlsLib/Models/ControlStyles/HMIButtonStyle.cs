using System.Windows;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.ControlStyles;

public class HMIButtonStyle : HMIControlStyle
{
    public HMIButtonStyle() : base()
    {
        Height = 50;
        Width = 150;
        ForegroundBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#000000"));
        BackgroundBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#DDDDDD"));
        BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#707070"));
        CornerRadius = 0;
        FontSize = 12;
        FontFamily = new FontFamily("Segeo UI");
        Visibility = Visibility.Visible;
        Blinking = false;
    }

    public HMIButtonStyle(HMIButtonStyle style)
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
    public Brush ForegroundBrush { get; set; }
    public Brush BackgroundBrush { get; set; }
    public Brush BorderBrush { get; set; }
    public double FontSize { get; set; }
    public FontFamily FontFamily { get; set; }
}
