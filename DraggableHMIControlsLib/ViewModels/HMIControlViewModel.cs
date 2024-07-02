using DraggableHMIControlsLib.Models;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.ControlAction;
using static DraggableHMIControlsLib.Models.ControlStyleEvent;

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
        get => ControlModel.Style.Height;
        set
        {
            if (value != ControlModel.Style.Height)
            {
                ControlModel.Style.Height = value;
                NotifyPropertyChanged(nameof(Height));
            }
        }
    }

    public double Width
    {
        get => ControlModel.Style.Width;
        set
        {
            if (value != ControlModel.Style.Width)
            {
                ControlModel.Style.Width = value;
                NotifyPropertyChanged(nameof(Width));
            }
        }
    }

    public Brush ForegroundBrush
    {
        get => ControlModel.Style.ForegroundBrush;
        set
        {
            if(value != ControlModel.Style.ForegroundBrush)
            {
                ControlModel.Style.ForegroundBrush = value;
                NotifyPropertyChanged(nameof(ForegroundBrush));
            }
        }
    }
    public Brush BackgroundBrush
    {
        get => ControlModel.Style.BackgroundBrush;
        set
        {
            if (value != ControlModel.Style.BackgroundBrush)
            {
                ControlModel.Style.BackgroundBrush = value;
                NotifyPropertyChanged(nameof(BackgroundBrush));
            }
        }
    }

    public Brush BorderBrush
    {
        get => ControlModel.Style.BorderBrush;
        set
        {
            if (value != ControlModel.Style.BorderBrush)
            {
                ControlModel.Style.BorderBrush = value;
                NotifyPropertyChanged(nameof(BorderBrush));
            }
        }
    }

    public double CornerRadius
    {
        get => ControlModel.Style.CornerRadius;
        set
        {
            if (value != ControlModel.Style.CornerRadius)
            {
                ControlModel.Style.CornerRadius = value;
                NotifyPropertyChanged(nameof(CornerRadius));
            }
        }
    }

    public double FontSize
    {
        get => ControlModel.Style.FontSize;
        set
        {
            if (value != ControlModel.Style.FontSize)
            {
                ControlModel.Style.FontSize = value;
                NotifyPropertyChanged(nameof(FontSize));
            }
        }
    }

    public FontFamily FontFamily
    {
        get => ControlModel.Style.FontFamily;
        set
        {
            if(value !=  ControlModel.Style.FontFamily)
            {
                ControlModel.Style.FontFamily = value;
                NotifyPropertyChanged(nameof(FontFamily));
            }
        }
    }

    public Visibility Visibility
    {
        get => ControlModel.Style.Visibility;
        set
        {
            if(value != ControlModel.Style.Visibility)
            {
                ControlModel.Style.Visibility = value;
                NotifyPropertyChanged(nameof(Visibility));
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

    public virtual void OnTagValuesUpdated()
    {
        foreach(var item in ControlModel.StyleEvents)
        {
            object? tagValue = _uiTagService.GetTagValue(item.tagId);
            if(tagValue == null) continue;

            switch(item.styleEventType)
            {
                case StyleEventType.ConditionalVisibility:
                    if(tagValue.GetType() == item.tagValue.GetType())
                    {
                        if(tagValue.Equals(item.tagValue))
                        {
                            if (item.param != null && item.param.GetType() == typeof(Visibility))
                            {
                                ControlModel.Style.Visibility = (Visibility)item.param;
                                NotifyPropertyChanged(nameof(Visibility));
                            }
                        }
                    }
                    break;
            }
        }
    }
}
