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

    protected readonly IUITagService _uiTagService;
    protected readonly IUITimerService _uiTimerService;
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

    

    public void EditMode(bool enable) => EditModeChange?.Invoke(enable);
    public void SaveLayout() => SaveUI?.Invoke();

    public void ExecuteAction(HMIControlAction action)
    {
        switch(action.Type)
        {
            case HMIControlAction.ActionType.SetBit:
                _uiTagService.SetBit(action.TagId);
                break;
            case HMIControlAction.ActionType.SetBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.SetBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIControlAction.ActionType.InvertBit:
                _uiTagService.InvertBit(action.TagId);
                break;
            case HMIControlAction.ActionType.InvertBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.InvertBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIControlAction.ActionType.ResetBit:
                _uiTagService.ResetBit(action.TagId);
                break;
            case HMIControlAction.ActionType.ResetBitInVariable:
                if (action.Parameter == null) break;
                _uiTagService.ResetBitInVariable(action.TagId, (int)action.Parameter);
                break;
            case HMIControlAction.ActionType.IncrementVariable:
                _uiTagService.IncrementVariable(action.TagId);
                break;
            case HMIControlAction.ActionType.DecrementVariable:
                _uiTagService.DecrementVariable(action.TagId);
                break;
            case HMIControlAction.ActionType.SetVariable:
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
                                var cornerRadius = ((ConditionalStylingArgs)item.Parameter).CornerRadius;
                                var visibility = ((ConditionalStylingArgs)item.Parameter).Visibility;
                                var blinking = ((ConditionalStylingArgs)item.Parameter).Blinking;

                                newStyle.Height = height ?? newStyle.Height;
                                newStyle.Width = width ?? newStyle.Width;
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

    }

    public virtual void OnUITimerElapsed(object? sender, EventArgs e)
    {
        
    }
}
