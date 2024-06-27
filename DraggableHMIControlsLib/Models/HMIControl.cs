using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DraggableHMIControlsLib.Models;

public abstract class HMIControl
{
    public HMIControl(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Thickness ParentMargin { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
}
