using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.Controls;
using DraggableHMIControlsLib.Models.ControlStyles;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.StyleEvents.ControlStyleEvent;

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
    public Brush ForegroundBrush
    {
        get => ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush;
        set
        {
            if (value != ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush)
            {
                ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush = value;
                NotifyPropertyChanged(nameof(ForegroundBrush));
            }
        }
    }
    public Brush BackgroundBrush
    {
        get => ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush;
        set
        {
            if (value != ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush)
            {
                ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush = value;
                NotifyPropertyChanged(nameof(BackgroundBrush));
            }
        }
    }

    public Brush BorderBrush
    {
        get => ((HMIButtonStyle)_controlModel.CurrentStyle).BorderBrush;
        set
        {
            if (value != ((HMIButtonStyle)_controlModel.CurrentStyle).BorderBrush)
            {
                ((HMIButtonStyle)_controlModel.CurrentStyle).BorderBrush = value;
                NotifyPropertyChanged(nameof(BorderBrush));
            }
        }
    }

    public double FontSize
    {
        get => ((HMIButtonStyle)_controlModel.CurrentStyle).FontSize;
        set
        {
            if (value != ((HMIButtonStyle)_controlModel.CurrentStyle).FontSize)
            {
                ((HMIButtonStyle)_controlModel.CurrentStyle).FontSize = value;
                NotifyPropertyChanged(nameof(FontSize));
            }
        }
    }

    public FontFamily FontFamily
    {
        get => ((HMIButtonStyle)_controlModel.CurrentStyle).FontFamily;
        set
        {
            if (value != ((HMIButtonStyle)_controlModel.CurrentStyle).FontFamily)
            {
                ((HMIButtonStyle)_controlModel.CurrentStyle).FontFamily = value;
                NotifyPropertyChanged(nameof(FontFamily));
            }
        }
    }

    public RelayCommand ClickedCmd { get => _clickedCmd ??= new RelayCommand(OnClicked); }
    public RelayCommand PressedCmd { get => _pressedCmd ??= new RelayCommand(OnPressed); }
    public RelayCommand ReleasedCmd { get => _releasedCmd ??= new RelayCommand(OnReleased); }

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

    public void Blinking(bool enable)
    {
        ((HMIButtonStyle)_controlModel.CurrentStyle).Blinking = enable;
        _uiTimerService.TimerElapsed -= OnUITimerElapsed;

        if (enable)
        {
            _uiTimerService.TimerElapsed += OnUITimerElapsed;
        }
        else
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush = ((HMIButtonStyle)_controlModel.BaseStyle).ForegroundBrush;
            ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush = ((HMIButtonStyle)_controlModel.BaseStyle).BackgroundBrush;
            NotifyPropertyChanged(nameof(ForegroundBrush));
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
    }

    public override void OnTagValuesUpdated(object? sender, EventArgs e)
    {

        var currentStyle = new HMIButtonStyle(((HMIButtonStyle)_controlModel.CurrentStyle));
        var newStyle = new HMIButtonStyle(currentStyle);

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

                            if (item.Parameter != null && item.Parameter.GetType() == typeof(ConditionalHMIButtonStylingArgs))
                            {
                                var height = ((ConditionalHMIButtonStylingArgs)item.Parameter).Height;
                                var width = ((ConditionalHMIButtonStylingArgs)item.Parameter).Width;
                                var foregroundBrush = ((ConditionalHMIButtonStylingArgs)item.Parameter).ForegroundBrush;
                                var backgroundBrush = ((ConditionalHMIButtonStylingArgs)item.Parameter).BackgroundBrush;
                                var borderBrush = ((ConditionalHMIButtonStylingArgs)item.Parameter).BorderBrush;
                                var cornerRadius = ((ConditionalHMIButtonStylingArgs)item.Parameter).CornerRadius;
                                var fontSize = ((ConditionalHMIButtonStylingArgs)item.Parameter).FontSize;
                                var fontFamily = ((ConditionalHMIButtonStylingArgs)item.Parameter).FontFamily;
                                var visibility = ((ConditionalHMIButtonStylingArgs)item.Parameter).Visibility;
                                var blinking = ((ConditionalHMIButtonStylingArgs)item.Parameter).Blinking;

                                newStyle.Height = height ?? newStyle.Height;
                                newStyle.Width = width ?? newStyle.Width;
                                newStyle.ForegroundBrush = foregroundBrush ?? newStyle.ForegroundBrush;
                                newStyle.BackgroundBrush = backgroundBrush ?? newStyle.BackgroundBrush;
                                newStyle.BorderBrush = borderBrush ?? newStyle.BorderBrush;
                                newStyle.CornerRadius = cornerRadius ?? newStyle.CornerRadius;
                                newStyle.FontSize = fontSize ?? newStyle.FontSize;
                                newStyle.FontFamily = fontFamily ?? newStyle.FontFamily;
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
                                newStyle.ForegroundBrush = ((HMIButtonStyle)_controlModel.BaseStyle).ForegroundBrush;
                                newStyle.BackgroundBrush = ((HMIButtonStyle)_controlModel.BaseStyle).BackgroundBrush;
                                newStyle.BorderBrush = ((HMIButtonStyle)_controlModel.BaseStyle).BorderBrush;
                                newStyle.CornerRadius = _controlModel.BaseStyle.CornerRadius;
                                newStyle.FontSize = ((HMIButtonStyle)_controlModel.BaseStyle).FontSize;
                                newStyle.FontFamily = ((HMIButtonStyle)_controlModel.BaseStyle).FontFamily;
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
        if (currentStyle.ForegroundBrush != newStyle.ForegroundBrush)
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush = newStyle.ForegroundBrush;
            NotifyPropertyChanged(nameof(ForegroundBrush));
        }
        if (currentStyle.BackgroundBrush != newStyle.BackgroundBrush)
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush = newStyle.BackgroundBrush;
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
        if (currentStyle.BorderBrush != newStyle.BorderBrush)
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).BorderBrush = newStyle.BorderBrush;
            NotifyPropertyChanged(nameof(BorderBrush));
        }
        if (currentStyle.CornerRadius != newStyle.CornerRadius)
        {
            _controlModel.CurrentStyle.CornerRadius = newStyle.CornerRadius;
            NotifyPropertyChanged(nameof(CornerRadius));
        }
        if (currentStyle.FontSize != newStyle.FontSize)
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).FontSize = newStyle.FontSize;
            NotifyPropertyChanged(nameof(FontSize));
        }
        if (currentStyle.FontFamily != newStyle.FontFamily)
        {
            ((HMIButtonStyle)_controlModel.CurrentStyle).FontFamily = newStyle.FontFamily;
            NotifyPropertyChanged(nameof(FontFamily));
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
        if (_controlModel.CurrentStyle.Blinking)
        {
            var fgBrush = ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush;
            var bgBrush = ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush;

            ((HMIButtonStyle)_controlModel.CurrentStyle).ForegroundBrush = bgBrush;
            ((HMIButtonStyle)_controlModel.CurrentStyle).BackgroundBrush = fgBrush;

            NotifyPropertyChanged(nameof(ForegroundBrush));
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
    }

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
