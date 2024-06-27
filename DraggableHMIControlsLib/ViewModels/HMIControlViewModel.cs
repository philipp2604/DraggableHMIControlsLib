using DraggableHMIControlsLib.Models;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DraggableHMIControlsLib.Models.ControlActions;

namespace DraggableHMIControlsLib.ViewModels;

public abstract class HMIControlViewModel : ObservableObject
{
    public event Action<bool>? EditModeChange;
    public event Action? SaveUI;

    private readonly IUITagService _uiTagService;

    public HMIControlViewModel(HMIControl controlModel, IUITagService uiTagService)
    {
        ControlModel = controlModel;
        _uiTagService = uiTagService;
    }

    public HMIControl ControlModel { get; set; }
    public double Height
    {
        get => ControlModel.Height;
        set
        {
            if (value != ControlModel.Height)
            {
                ControlModel.Height = value;
                NotifyPropertyChanged(nameof(Height));
            }
        }
    }

    public double Width
    {
        get => ControlModel.Width;
        set
        {
            if (value != ControlModel.Width)
            {
                ControlModel.Width = value;
                NotifyPropertyChanged(nameof(Width));
            }
        }
    }

    public void EditMode(bool enable) => EditModeChange?.Invoke(enable);
    public void SaveLayout() => SaveUI?.Invoke();

    public void ExecuteAction((ActionType actionType, int tagId, object param) payload)
    {
        switch(payload.actionType)
        {
            case ActionType.SetBit:
                _uiTagService.SetBit(payload.tagId);
                break;
            case ActionType.SetBitInVariable:
                _uiTagService.SetBitInVariable(payload.tagId, (int)payload.param);
                break;
            case ActionType.InvertBit:
                _uiTagService.InvertBit(payload.tagId);
                break;
            case ActionType.InvertBitInVariable:
                _uiTagService.InvertBitInVariable(payload.tagId, (int)payload.param);
                break;
            case ActionType.ResetBit:
                _uiTagService.ResetBit(payload.tagId);
                break;
            case ActionType.ResetBitInVariable:
                _uiTagService.ResetBitInVariable(payload.tagId, (int)payload.param);
                break;
            case ActionType.IncrementVariable:
                _uiTagService.IncrementVariable(payload.tagId);
                break;
            case ActionType.DecrementVariable:
                _uiTagService.DecrementVariable(payload.tagId);
                break;
            case ActionType.SetVariable:
                _uiTagService.SetVariable(payload.tagId, payload.param);
                break;
        }
    }
}
