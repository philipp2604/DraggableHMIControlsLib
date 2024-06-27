using DraggableHMIControlsLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DraggableHMIControlsLib.Views;

public abstract class HMIControlView : UserControl
{
    private double _mouseX, _mouseY;
    private Line _xGuideLine, _yGuideLine;

    public HMIControlView()
    {
        _xGuideLine = new Line() { Stroke = Brushes.Black, StrokeThickness = 1 };
        _yGuideLine = new Line() { Stroke = Brushes.Black, StrokeThickness = 1 };

        GridSnapSpacing = 10;

        EnableGuideLines = true;

        DataContextChanged += DataContextChangedHandler;
        Loaded += LoadedHandler;
    }


    public int GridSnapSpacing { get; set; }
    public bool EnableGuideLines { get; set; }


    private void DataContextChangedHandler(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (!(DataContext.GetType().IsSubclassOf(typeof(HMIControlViewModel))) || (DataContext.GetType() == typeof(HMIControlViewModel))) return;

        var viewModel = (HMIControlViewModel)DataContext;
        if (viewModel != null)
        {
            viewModel.EditModeChange += (enable) => { EditMode(enable); };

            viewModel.SaveUI += () => { viewModel.ControlModel.ParentMargin = GetParentMargin(); };
        }
    }

    private void LoadedHandler(object sender, RoutedEventArgs e)
    {
        if (DataContext == null) return;
        if (!(DataContext.GetType().IsSubclassOf(typeof(HMIControlViewModel))) || (DataContext.GetType() == typeof(HMIControlViewModel))) return;

        setupEditMode();
        SetParentMargin(((HMIControlViewModel)DataContext).ControlModel.ParentMargin);
    }

    public Thickness GetParentMargin()
    {
        var parentContainer = Parent as FrameworkElement;
        if (parentContainer == null)
            return new Thickness();
        else
            return parentContainer.Margin;
    }

    public void SetParentMargin(Thickness margin)
    {
        var parentElement = Parent as FrameworkElement;
        if (parentElement == null) return;

        parentElement.Margin = margin;
    }

    public void EditMode(bool enable)
    {
        if (enable)
            startEditMode();
        else
            stopEditMode();
    }

    private void setupEditMode()
    {
        var parentElement = Parent as FrameworkElement;
        if (parentElement == null) return;

        if (!parentElement.AllowDrop)
        {
            Canvas canvas = new Canvas() { AllowDrop = true };

            if (parentElement is ContentControl)
            {
                ((ContentControl)parentElement).Content = null;
                canvas.Children.Add(this);
                ((ContentControl)parentElement).Content = canvas;
            }
            else if (parentElement is Panel)
            {
                ((Panel)parentElement).Children.Remove(this);

                canvas.Children.Add(this);

                ((Panel)parentElement).Children.Add(canvas);
            }

            //Restore previous control position
            var positionTransform = TransformToAncestor(parentElement);
            var areaPosition = positionTransform.Transform(new Point(0, 0));
            Canvas.SetLeft(this, areaPosition.X);
            Canvas.SetTop(this, areaPosition.Y);
        }

    }

    protected virtual void startEditMode()
    {
        Cursor = Cursors.SizeAll;
        PreviewMouseUp += onMouseUp;
        PreviewMouseLeftButtonDown += onMouseLeftButtonDown;
        PreviewMouseMove += onMouseMove;
    }

    protected virtual void stopEditMode()
    {
        Cursor = Cursors.Arrow;
        PreviewMouseUp -= onMouseUp;
        PreviewMouseLeftButtonDown -= onMouseLeftButtonDown;
        PreviewMouseMove -= onMouseMove;
    }

    private void onMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender != this) return;

        var mainWIndow = Window.GetWindow(this);

        _mouseX = e.GetPosition(mainWIndow).X;
        _mouseY = e.GetPosition(mainWIndow).Y;

        var parentElement = Parent as FrameworkElement;
        if (parentElement == null) return;

        if (EnableGuideLines)
        {
            if (!((Panel)parentElement).Children.Contains(_xGuideLine))
                ((Panel)parentElement).Children.Add(_xGuideLine);
            if (!((Panel)parentElement).Children.Contains(_yGuideLine))
                ((Panel)parentElement).Children.Add(_yGuideLine);
        }
    }

    private void onMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (sender != this) return;

        e.MouseDevice.Capture(null);

        var parentElement = Parent as FrameworkElement;
        if (parentElement == null) return;


        if (((Panel)parentElement).Children.Contains(_xGuideLine))
            ((Panel)parentElement).Children.Remove(_xGuideLine);
        if (((Panel)parentElement).Children.Contains(_yGuideLine))
            ((Panel)parentElement).Children.Remove(_yGuideLine);
    }

    private void onMouseMove(object sender, MouseEventArgs e)
    {
        if (sender != this) return;

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            e.MouseDevice.Capture(this);

            var mainWIndow = Window.GetWindow(this);

            int tempX = Convert.ToInt32(e.GetPosition(mainWIndow).X);
            int tempY = Convert.ToInt32(e.GetPosition(mainWIndow).Y);

            var parentElement = Parent as FrameworkElement;
            if (parentElement == null) return;


            //Guide line positioning
            var positionTransform = TransformToAncestor(parentElement);
            var areaPosition = positionTransform.Transform(new Point(0, 0));

            _xGuideLine.X1 = -5000;
            _xGuideLine.X2 = 5000;
            _xGuideLine.Y1 = areaPosition.Y + Height / 2;
            _xGuideLine.Y2 = areaPosition.Y + Height / 2;

            _yGuideLine.X1 = areaPosition.X + Width / 2;
            _yGuideLine.X2 = areaPosition.X + Width / 2;
            _yGuideLine.Y1 = -5000;
            _yGuideLine.Y2 = 5000;



            //Round new position for grid snap
            int tempXRounded = (int)Math.Ceiling(Convert.ToDecimal(tempX) / GridSnapSpacing) * GridSnapSpacing;
            int tempYRounded = (int)Math.Ceiling(Convert.ToDecimal(tempY) / GridSnapSpacing) * GridSnapSpacing;


            Thickness margin = parentElement.Margin;

            if (_mouseX > tempX)
            {
                margin.Left += (tempXRounded - _mouseX);
                margin.Right -= (tempXRounded - _mouseX);
            }
            else if (_mouseX < tempX)
            {
                margin.Left -= (_mouseX - tempXRounded);
                margin.Right -= (tempXRounded - _mouseX);
            }
            if (_mouseY > tempY)
            {
                margin.Top += (tempYRounded - _mouseY);
                margin.Bottom -= (tempYRounded - _mouseY);
            }
            else if (_mouseY < tempY)
            {
                margin.Top -= (_mouseY - tempYRounded);
                margin.Bottom -= (tempYRounded - _mouseY);
            }

            parentElement.Margin = margin;

            _mouseX = tempXRounded;
            _mouseY = tempYRounded;
        }
    }
}

