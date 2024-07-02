using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.ControlAction;
using static DraggableHMIControlsLib.Models.ControlStyleEvent;

namespace DraggableHMIControlsLib.Models;

public abstract class HMIControl
{
    public HMIControl(string name)
    {
        Name = name;
        Style = new HMIControlStyle(0, 0, new SolidColorBrush(), new SolidColorBrush(), new SolidColorBrush(), 0, 0, new FontFamily("Arial"), Visibility.Visible);
        StyleEvents = new List<(StyleEventType styleEventType, int tagId, object tagValue, object param)>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Thickness ParentMargin { get; set; }
    public HMIControlStyle Style { get; set; }

    public List<(StyleEventType styleEventType, int tagId, object tagValue, object param)> StyleEvents { get; set; }
}
