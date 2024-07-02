using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.Controls;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraggableHMIControlsLib.ViewModels;

public class HMIButtonViewModel : HMIControlViewModel
{

    private RelayCommand? _clickedCmd;
    private RelayCommand? _pressedCmd;
    private RelayCommand? _releasedCmd;

    public HMIButtonViewModel(HMIButton model, IUITagService uiTagService, IUITimerService uiTimerService) : base(model, uiTagService, uiTimerService)
    {
        Blinking(model.CurrentStyle.Blinking);
    }

    public string Text
    { 
        get => ((HMIButton)_controlModel).Text;
        set
        {
            if(value != ((HMIButton)_controlModel).Text)
            {
                NotifyPropertyChanging(nameof(Text));
                ((HMIButton)_controlModel).Text = value;
                NotifyPropertyChanged(nameof(Text));
            }
        }
    }

    public void RegisterAction(HMIButtonAction action)
    {
        ((HMIButton)_controlModel).Actions.Add(action);
    }

    public List<HMIButtonAction> GetActionsByTrigger(HMIButtonAction.ActionTrigger trigger)
    {
        List<HMIButtonAction> actions = new List<HMIButtonAction>();
        
        foreach(var action in ((HMIButton)_controlModel).Actions)
        {
            if(action.Trigger == trigger)
                actions.Add(action);
        }

        return actions;
    }

    public RelayCommand ClickedCmd { get => _clickedCmd ??= new RelayCommand(OnClicked); }
    public RelayCommand PressedCmd { get => _pressedCmd ??= new RelayCommand(OnPressed); }
    public RelayCommand ReleasedCmd { get => _releasedCmd ??= new RelayCommand(OnReleased); }

    public void OnClicked()
    {
        foreach(var item in GetActionsByTrigger(HMIButtonAction.ActionTrigger.MouseClicked))
        {
            ExecuteAction(item);
        }
    }

    public void OnPressed()
    {
        foreach (var item in GetActionsByTrigger(HMIButtonAction.ActionTrigger.MousePressed))
        {
            ExecuteAction(item);
        }
    }

    public void OnReleased()
    {
        foreach (var item in GetActionsByTrigger(HMIButtonAction.ActionTrigger.MouseReleased))
        {
            ExecuteAction(item);
        }
    }
}
