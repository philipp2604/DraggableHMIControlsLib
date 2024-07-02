using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DraggableHMIControlsLib.Models.ControlStyles
{
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
    }
}
