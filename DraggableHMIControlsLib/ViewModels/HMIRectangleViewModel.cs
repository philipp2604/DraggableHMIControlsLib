using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.Controls;
using DraggableHMIControlsLib.Models.ControlStyles;
using DraggableHMIControlsLib.Models.StyleEvents;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.StyleEvents.ControlStyleEvent;

namespace DraggableHMIControlsLib.ViewModels;

public class HMIRectangleViewModel : HMIControlViewModel
{
    private RelayCommand? _clickedCmd;
    private RelayCommand? _pressedCmd;
    private RelayCommand? _releasedCmd;

    public HMIRectangleViewModel(HMIRectangle model, IUITagService uiTagService, IUITimerService uiTimerService) : base(model, uiTagService, uiTimerService)
    {
        //Blinking(model.CurrentStyle.Blinking);
    }

    public Brush FillBrush
    {
        get => ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush;
        set
        {
            if (value != ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush)
            {
                ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush = value;
                NotifyPropertyChanged(nameof(FillBrush));
            }
        }
    }

    public RelayCommand ClickedCmd { get => _clickedCmd ??= new RelayCommand(OnClicked); }
    public RelayCommand PressedCmd { get => _pressedCmd ??= new RelayCommand(OnPressed); }
    public RelayCommand ReleasedCmd { get => _releasedCmd ??= new RelayCommand(OnReleased); }

    public void RegisterAction(HMIRectangleAction action)
    {
        ((HMIRectangle)_controlModel).Actions.Add(action);
    }

    public List<HMIRectangleAction> GetActionsByTrigger(HMIRectangleAction.ActionTrigger trigger)
    {
        List<HMIRectangleAction> actions = new List<HMIRectangleAction>();

        foreach (var action in ((HMIRectangle)_controlModel).Actions)
        {
            if (action.Trigger == trigger)
                actions.Add(action);
        }

        return actions;
    }

    public void Blinking(bool enable)
    {
        ((HMIRectangleStyle)_controlModel.CurrentStyle).Blinking = enable;
        _uiTimerService.TimerElapsed -= OnUITimerElapsed;

        if (enable)
        {
            _uiTimerService.TimerElapsed += OnUITimerElapsed;
        }
        else
        {
            ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush = ((HMIRectangleStyle)_controlModel.BaseStyle).FillBrush;
            NotifyPropertyChanged(nameof(FillBrush));
        }
    }

    public override void OnTagValuesUpdated(object? sender, EventArgs e)
    {

        var currentStyle = new HMIRectangleStyle(((HMIRectangleStyle)_controlModel.CurrentStyle));
        var newStyle = new HMIRectangleStyle(currentStyle);

        foreach (var item in _controlModel.StyleEvents)
        {
            object? tagValue = _uiTagService.GetTagValue(item.TagId);
            if (tagValue == null) continue;

            switch (item.EventType)
            {
                case StyleEventType.ConditionalStyling:
                {
                    if (tagValue.GetType() == item.TagValue.GetType())
                    {
                        if (tagValue.Equals(item.TagValue))
                        {
                            item.IsActive = true;

                            if (item.Parameter != null && item.Parameter.GetType() == typeof(ConditionalHMIRectangleStylingArgs))
                            {
                                var height = ((ConditionalHMIRectangleStylingArgs)item.Parameter).Height;
                                var width = ((ConditionalHMIRectangleStylingArgs)item.Parameter).Width;
                                var fillBrush = ((ConditionalHMIRectangleStylingArgs)item.Parameter).FillBrush;
                                var cornerRadius = ((ConditionalHMIRectangleStylingArgs)item.Parameter).CornerRadius;
                                var visibility = ((ConditionalHMIRectangleStylingArgs)item.Parameter).Visibility;
                                var blinking = ((ConditionalHMIRectangleStylingArgs)item.Parameter).Blinking;

                                newStyle.Height = height ?? newStyle.Height;
                                newStyle.Width = width ?? newStyle.Width;
                                newStyle.FillBrush = fillBrush ?? newStyle.FillBrush;
                                newStyle.CornerRadius = cornerRadius ?? newStyle.CornerRadius;
                                newStyle.Visibility = visibility ?? newStyle.Visibility;
                                newStyle.Blinking = blinking ?? newStyle.Blinking;
                            }
                        }
                        else
                        {
                            if (item.IsActive)
                            {
                                item.IsActive = false;
                                newStyle.Height = _controlModel.BaseStyle.Height;
                                newStyle.Width = _controlModel.BaseStyle.Width;
                                newStyle.FillBrush = ((HMIRectangleStyle)_controlModel.BaseStyle).FillBrush;
                                newStyle.CornerRadius = _controlModel.BaseStyle.CornerRadius;
                                newStyle.Visibility = _controlModel.BaseStyle.Visibility;
                                newStyle.Blinking = _controlModel.BaseStyle.Blinking;
                            }
                        }
                    }
                    break;
                }
            }
        }

        if (currentStyle.Height != newStyle.Height)
        {
            _controlModel.CurrentStyle.Height = newStyle.Height;
            NotifyPropertyChanged(nameof(Height));
        }
        if (currentStyle.Width != newStyle.Width)
        {
            _controlModel.CurrentStyle.Width = newStyle.Width;
            NotifyPropertyChanged(nameof(Width));
        }
        if (currentStyle.FillBrush != newStyle.FillBrush)
        {
            ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush = newStyle.FillBrush;
            NotifyPropertyChanged(nameof(FillBrush));
        }
        if (currentStyle.CornerRadius != newStyle.CornerRadius)
        {
            _controlModel.CurrentStyle.CornerRadius = newStyle.CornerRadius;
            NotifyPropertyChanged(nameof(CornerRadius));
        }
        if (currentStyle.Visibility != newStyle.Visibility)
        {
            _controlModel.CurrentStyle.Visibility = newStyle.Visibility;
            NotifyPropertyChanged(nameof(Visibility));
        }
        if (currentStyle.Blinking != newStyle.Blinking)
        {
            Blinking(newStyle.Blinking);
        }

    }

    public override void OnUITimerElapsed(object? sender, EventArgs e)
    {
        /*
        if (_controlModel.CurrentStyle.Blinking)
        {
            var fgBrush = ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush;
            var bgBrush = ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush;

            ((HMIRectangleStyle)_controlModel.CurrentStyle).Visibility = !((HMIRectangleStyle)_controlModel.CurrentStyle).Visibility;
            ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush = fgBrush;

            NotifyPropertyChanged(nameof(ForegroundBrush));
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }*/
        if(_controlModel.CurrentStyle.Blinking)
        {
            ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush.Opacity = ((HMIRectangleStyle)_controlModel.CurrentStyle).FillBrush.Opacity != 0.5 ? 0.5 : 1;
            NotifyPropertyChanged(nameof(FillBrush));
        }
    }

    public void OnClicked()
    {
        foreach (var item in GetActionsByTrigger(HMIRectangleAction.ActionTrigger.MouseClicked))
        {
            ExecuteAction(item);
        }
    }

    public void OnPressed()
    {
        foreach (var item in GetActionsByTrigger(HMIRectangleAction.ActionTrigger.MousePressed))
        {
            ExecuteAction(item);
        }
    }

    public void OnReleased()
    {
        foreach (var item in GetActionsByTrigger(HMIRectangleAction.ActionTrigger.MouseReleased))
        {
            ExecuteAction(item);
        }
    }
}
