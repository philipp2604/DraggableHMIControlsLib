using System.Windows;
using System.Windows.Media;
using DraggableHMIControlsLib.Models.ControlStyles;
using DraggableHMIControlsLib.Models.StyleEvents;

namespace DraggableHMIControlsLib.Models.Controls;

public abstract class HMIControl
{
    public HMIControl(string name)
    {
        Name = name;
        BaseStyle = new HMIControlStyle(0, 0, 0, Visibility.Visible, false);
        CurrentStyle = new HMIControlStyle(BaseStyle);
        StyleEvents = new List<ControlStyleEvent>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Thickness ParentMargin { get; set; }
    public HMIControlStyle CurrentStyle { get; set; }
    public HMIControlStyle BaseStyle { get; set; }
    public List<ControlStyleEvent> StyleEvents { get; set; }
}
