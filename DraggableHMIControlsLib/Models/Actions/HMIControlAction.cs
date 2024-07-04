namespace DraggableHMIControlsLib.Models.Actions;

public class HMIControlAction
{
    public ActionType Type { get; set; }
    public ActionTrigger Trigger { get; set; }
    public int TagId { get; set; }
    public object? Parameter { get; set; }

    public enum ActionTrigger
    {
        MouseClicked,
        MousePressed,
        MouseReleased
    }

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
}
