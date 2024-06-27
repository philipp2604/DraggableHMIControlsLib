using DraggableHMIControlsLib.Models;
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

    public HMIButtonViewModel(HMIButton model, IUITagService uITagService) : base(model, uITagService)
    {

    }

    public string Text
    { 
        get => ((HMIButton)ControlModel).Text;
        set
        {
            if(value != ((HMIButton)ControlModel).Text)
            {
                NotifyPropertyChanging(nameof(Text));
                ((HMIButton)ControlModel).Text = value;
                NotifyPropertyChanged(nameof(Text));
            }
        }
    }

    public RelayCommand ClickedCmd { get => _clickedCmd ??= new RelayCommand(OnClicked); }
    public RelayCommand PressedCmd { get => _pressedCmd ??= new RelayCommand(OnPressed); }
    public RelayCommand ReleasedCmd { get => _releasedCmd ??= new RelayCommand(OnReleased); }

    public void OnClicked()
    {
        foreach(var item in ((HMIButton)ControlModel).ClickedActions)
        {
            ExecuteAction(item);
        }
    }

    public void OnPressed()
    {
        foreach (var item in ((HMIButton)ControlModel).PressedActions)
        {
            ExecuteAction(item);
        }
    }

    public void OnReleased()
    {
        foreach (var item in ((HMIButton)ControlModel).ReleasedActions)
        {
            ExecuteAction(item);
        }
    }
}
