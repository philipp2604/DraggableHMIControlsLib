using DraggableHMIControlsLib.ViewModels;
using System.Windows.Input;

namespace DraggableHMIControlsLib.Views
{
    /// <summary>
    /// Interaktionslogik für HMIRectangleView.xaml
    /// </summary>
    public partial class HMIRectangleView : HMIControlView
    {
        private MouseButtonState _lastMouseButtonState;
        private event EventHandler? Click;

        public HMIRectangleView()
        {
            InitializeComponent();
            _lastMouseButtonState = MouseButtonState.Released;
            Click += onButtonClick;
            rectangle.PreviewMouseLeftButtonDown += onButtonPress;
            rectangle.PreviewMouseLeftButtonUp += onButtonRelease;
        }

        
        protected override void startEditMode()
        {
            Click -= onButtonClick;
            rectangle.PreviewMouseLeftButtonDown -= onButtonPress;
            rectangle.PreviewMouseLeftButtonUp -= onButtonRelease;
            base.startEditMode();
        }

        protected override void stopEditMode()
        {
            base.stopEditMode();
            Click += onButtonClick;
            rectangle.PreviewMouseLeftButtonDown += onButtonPress;
            rectangle.PreviewMouseLeftButtonUp += onButtonRelease;
        }

        private void onButtonClick(object? sender, EventArgs e)
        {
            ((HMIRectangleViewModel)DataContext).OnClicked();
        }

        private void onButtonPress(object sender, MouseButtonEventArgs e)
        {
            ((HMIRectangleViewModel)DataContext).OnPressed();
            _lastMouseButtonState = MouseButtonState.Pressed;
        }

        private void onButtonRelease(object sender, MouseButtonEventArgs e)
        {
            ((HMIRectangleViewModel)DataContext).OnReleased();

            if(_lastMouseButtonState == MouseButtonState.Pressed)
                Click?.Invoke(this, new EventArgs());

            _lastMouseButtonState = MouseButtonState.Released;
        }
    }
}
