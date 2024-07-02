using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableHMIControlsLib.Models;

public static class ControlAction
{
    public enum ActionType
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

    public static List<string> ActionNames = new List<string>()
    {
        "SetBit",
        "SetBitInVariable",
        "InvertBit",
        "InvertBitInVariable",
        "ResetBit",
        "ResetBitInVariable",
        "IncrementBitInVariable",
        "IncrementVariable",
        "DecrementVariable",
        "SetVariable"
    };
}
