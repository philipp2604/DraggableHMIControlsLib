namespace DraggableHMIControlsLib.Models.Actions;

public class HMIButtonAction : HMIControlAction
{
    public HMIButtonAction(HMIButtonActionType action, HMIButtonActionTrigger trigger)
    {
        Type = (ActionType)action;
        Trigger = (ActionTrigger)trigger;
    }

    public enum HMIButtonActionTrigger
    {
        MouseClicked,
        MousePressed,
        MouseReleased
    }

    public enum HMIButtonActionType
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
