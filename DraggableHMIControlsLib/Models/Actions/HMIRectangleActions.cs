
namespace DraggableHMIControlsLib.Models.Actions;

public class HMIRectangleAction : HMIControlAction
{
    public HMIRectangleAction(HMIRectangleActionType action, HMIRectangleActionTrigger trigger)
    {
        Type = (ActionType)action;
        Trigger = (ActionTrigger)trigger;
    }

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
