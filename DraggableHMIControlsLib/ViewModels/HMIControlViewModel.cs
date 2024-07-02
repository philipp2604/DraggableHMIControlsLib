using DraggableHMIControlsLib.Models.Actions;
using DraggableHMIControlsLib.Models.Controls;
using DraggableHMIControlsLib.Models.ControlStyles;
using DraggableHMIControlsLib.Models.StyleEvents;
using DraggableHMIControlsLib.Services;
using MvvmHelpers;
using System.Windows;
using System.Windows.Media;
using static DraggableHMIControlsLib.Models.StyleEvents.ControlStyleEvent;

namespace DraggableHMIControlsLib.ViewModels;

public abstract class HMIControlViewModel : ObservableObject
{
    public event Action<bool>? EditModeChange;
    public event Action? SaveUI;

    private readonly IUITagService _uiTagService;
    private readonly IUITimerService _uiTimerService;
    protected readonly HMIControl _controlModel;

    public HMIControlViewModel(HMIControl controlModel, IUITagService uiTagService, IUITimerService uiTimerService)
    {
        _controlModel = controlModel;
        _uiTagService = uiTagService;
        _uiTimerService = uiTimerService;

        _uiTagService.TagsUpdated += OnTagValuesUpdated;
    }

    public Thickness ParentMargin { get => _controlModel.ParentMargin; set => _controlModel.ParentMargin = value; }
    public List<ControlStyleEvent> StyleEvents { get => _controlModel.StyleEvents; set => _controlModel.StyleEvents = value; }

    public double Height
    {
        get => _controlModel.CurrentStyle.Height;
        set
        {
            if (value != _controlModel.CurrentStyle.Height)
            {
                _controlModel.CurrentStyle.Height = value;
                NotifyPropertyChanged(nameof(Height));
            }
        }
    }

    public double Width
    {
        get => _controlModel.CurrentStyle.Width;
        set
        {
            if (value != _controlModel.CurrentStyle.Width)
            {
                _controlModel.CurrentStyle.Width = value;
                NotifyPropertyChanged(nameof(Width));
            }
        }
    }

    public Brush ForegroundBrush
    {
        get => _controlModel.CurrentStyle.ForegroundBrush;
        set
        {
            if(value != _controlModel.CurrentStyle.ForegroundBrush)
            {
                _controlModel.CurrentStyle.ForegroundBrush = value;
                NotifyPropertyChanged(nameof(ForegroundBrush));
            }
        }
    }
    public Brush BackgroundBrush
    {
        get => _controlModel.CurrentStyle.BackgroundBrush;
        set
        {
            if (value != _controlModel.CurrentStyle.BackgroundBrush)
            {
                _controlModel.CurrentStyle.BackgroundBrush = value;
                NotifyPropertyChanged(nameof(BackgroundBrush));
            }
        }
    }

    public Brush BorderBrush
    {
        get => _controlModel.CurrentStyle.BorderBrush;
        set
        {
            if (value != _controlModel.CurrentStyle.BorderBrush)
            {
                _controlModel.CurrentStyle.BorderBrush = value;
                NotifyPropertyChanged(nameof(BorderBrush));
            }
        }
    }

    public double CornerRadius
    {
        get => _controlModel.CurrentStyle.CornerRadius;
        set
        {
            if (value != _controlModel.CurrentStyle.CornerRadius)
            {
                _controlModel.CurrentStyle.CornerRadius = value;
                NotifyPropertyChanged(nameof(CornerRadius));
            }
        }
    }

    public double FontSize
    {
        get => _controlModel.CurrentStyle.FontSize;
        set
        {
            if (value != _controlModel.CurrentStyle.FontSize)
            {
                _controlModel.CurrentStyle.FontSize = value;
                NotifyPropertyChanged(nameof(FontSize));
            }
        }
    }

    public FontFamily FontFamily
    {
        get => _controlModel.CurrentStyle.FontFamily;
        set
        {
            if(value !=  _controlModel.CurrentStyle.FontFamily)
            {
                _controlModel.CurrentStyle.FontFamily = value;
                NotifyPropertyChanged(nameof(FontFamily));
            }
        }
    }

    public Visibility Visibility
    {
        get => _controlModel.CurrentStyle.Visibility;
        set
        {
            if(value != _controlModel.CurrentStyle.Visibility)
            {
                _controlModel.CurrentStyle.Visibility = value;
                NotifyPropertyChanged(nameof(Visibility));
            }
        }
    }

    public void Blinking(bool enable)
    {
        _controlModel.CurrentStyle.Blinking = enable;
        _uiTimerService.TimerElapsed -= OnUITimerElapsed;

        if (enable)
        {
            _uiTimerService.TimerElapsed += OnUITimerElapsed;
        }
        else
        {
            _controlModel.CurrentStyle.ForegroundBrush = _controlModel.BaseStyle.ForegroundBrush;
            _controlModel.CurrentStyle.BackgroundBrush = _controlModel.BaseStyle.BackgroundBrush;
            NotifyPropertyChanged(nameof(ForegroundBrush));
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
    }

    public void EditMode(bool enable) => EditModeChange?.Invoke(enable);
    public void SaveLayout() => SaveUI?.Invoke();

    public void ExecuteAction(HMIButtonAction action)
    {
        switch(action.Type)
        {
            case HMIButtonAction.ActionType.SetBit:
                _uiTagService.SetBit(action.TagId);
                break;
            case HMIButtonAction.ActionType.SetBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.SetBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIButtonAction.ActionType.InvertBit:
                _uiTagService.InvertBit(action.TagId);
                break;
            case HMIButtonAction.ActionType.InvertBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.InvertBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIButtonAction.ActionType.ResetBit:
                _uiTagService.ResetBit(action.TagId);
                break;
            case HMIButtonAction.ActionType.ResetBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.ResetBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIButtonAction.ActionType.IncrementVariable:
                _uiTagService.IncrementVariable(action.TagId);
                break;
            case HMIButtonAction.ActionType.DecrementVariable:
                _uiTagService.DecrementVariable(action.TagId);
                break;
            case HMIButtonAction.ActionType.SetVariable:
                if (action.Parameter == null) break;
                _uiTagService.SetVariable(action.TagId, action.Parameter);
                break;
        }
    }

    public virtual void OnTagValuesUpdated(object? sender, EventArgs e)
    {
        
        var currentStyle = new HMIControlStyle(_controlModel.CurrentStyle);
        var newStyle = new HMIControlStyle(currentStyle);

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

                            if (item.Parameter != null && item.Parameter.GetType() == typeof(ConditionalStylingArgs))
                            {
                                var height = ((ConditionalStylingArgs)item.Parameter).Height;
                                var width = ((ConditionalStylingArgs)item.Parameter).Width;
                                var foregroundBrush = ((ConditionalStylingArgs)item.Parameter).ForegroundBrush;
                                var backgroundBrush = ((ConditionalStylingArgs)item.Parameter).BackgroundBrush;
                                var borderBrush = ((ConditionalStylingArgs)item.Parameter).BorderBrush;
                                var cornerRadius = ((ConditionalStylingArgs)item.Parameter).CornerRadius;
                                var fontSize = ((ConditionalStylingArgs)item.Parameter).FontSize;
                                var fontFamily = ((ConditionalStylingArgs)item.Parameter).FontFamily;
                                var visibility = ((ConditionalStylingArgs)item.Parameter).Visibility;
                                var blinking = ((ConditionalStylingArgs)item.Parameter).Blinking;

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
                                newStyle.ForegroundBrush = _controlModel.BaseStyle.ForegroundBrush;
                                newStyle.BackgroundBrush = _controlModel.BaseStyle.BackgroundBrush;
                                newStyle.CornerRadius = _controlModel.BaseStyle.CornerRadius;
                                newStyle.FontSize = _controlModel.BaseStyle.FontSize;
                                newStyle.FontFamily = _controlModel.BaseStyle.FontFamily;
                                newStyle.Visibility = _controlModel.BaseStyle.Visibility;
                                newStyle.Blinking = _controlModel.BaseStyle.Blinking;
                            }
                        }
                    }
                    break;
                }
            }
        }

        if(currentStyle.Height !=  newStyle.Height)
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
            _controlModel.CurrentStyle.ForegroundBrush = newStyle.ForegroundBrush;
            NotifyPropertyChanged(nameof(ForegroundBrush));
        }
        if (currentStyle.BackgroundBrush != newStyle.BackgroundBrush)
        {
            _controlModel.CurrentStyle.BackgroundBrush = newStyle.BackgroundBrush;
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
        if (currentStyle.BorderBrush != newStyle.BorderBrush)
        {
            _controlModel.CurrentStyle.BorderBrush = newStyle.BorderBrush;
            NotifyPropertyChanged(nameof(BorderBrush));
        }
        if (currentStyle.CornerRadius != newStyle.CornerRadius)
        {
            _controlModel.CurrentStyle.CornerRadius = newStyle.CornerRadius;
            NotifyPropertyChanged(nameof(CornerRadius));
        }
        if (currentStyle.FontSize != newStyle.FontSize)
        {
            _controlModel.CurrentStyle.FontSize = newStyle.FontSize;
            NotifyPropertyChanged(nameof(FontSize));
        }
        if (currentStyle.FontFamily != newStyle.FontFamily)
        {
            _controlModel.CurrentStyle.FontFamily = newStyle.FontFamily;
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

    public virtual void OnUITimerElapsed(object? sender, EventArgs e)
    {
        if(_controlModel.CurrentStyle.Blinking)
        {
            var fgBrush = _controlModel.CurrentStyle.ForegroundBrush;
            var bgBrush = _controlModel.CurrentStyle.BackgroundBrush;

            _controlModel.CurrentStyle.ForegroundBrush = bgBrush;
            _controlModel.CurrentStyle.BackgroundBrush = fgBrush;

            NotifyPropertyChanged(nameof(ForegroundBrush));
            NotifyPropertyChanged(nameof(BackgroundBrush));
        }
    }
}
