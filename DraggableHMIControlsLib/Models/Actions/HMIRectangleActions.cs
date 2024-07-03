using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableHMIControlsLib.Models.Actions;

public class HMIRectangleAction : HMIControlAction
{
    public enum HMIRectangleActionTrigger
    {
        MouseClicked,
        MousePressed,
        MouseReleased
    }

    public enum HMIRectangleActionType
    {
        SetBit,
        SetBitInVariable,
        InvertBit,
        InvertBitInVariable,
        ResetBit,
        ResetBitInVariable,
        IncrementVariable,
        DecrementVariable,
        SetVariable
    }
}
