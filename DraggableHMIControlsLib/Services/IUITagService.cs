using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableHMIControlsLib.Services;

public interface IUITagService
{
    public event EventHandler? TagsUpdated;


    //Bit operations
    public void SetBit(int tagId);
    public void SetBitInVariable(int tagId, int bitIndex);
    public void InvertBit(int tagId);
    public void InvertBitInVariable(int tagId, int bitIndex);
    public void ResetBit(int tagId);
    public void ResetBitInVariable(int tagId, int bitIndex);

    //Var operations
    public void IncrementVariable(int tagId);
    public void DecrementVariable(int tagId);
    public void SetVariable(int tagId, object value);
}
