using DraggableHMIControlsLib.ViewModels;
using System.Windows;
using System.Windows.Input;
namespace DraggableHMIControlsLib.Views;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class HMIButtonView : HMIControlView
{
    public HMIButtonView()
    {
        InitializeComponent();
        button.Click += onButtonClick;
        button.PreviewMouseLeftButtonDown += onButtonPress;
        button.PreviewMouseLeftButtonUp += onButtonRelease;
    }

    protected override void startEditMode()
    {
        button.Click -= onButtonClick;
        button.PreviewMouseLeftButtonDown -= onButtonPress;
        button.PreviewMouseLeftButtonUp -= onButtonRelease;
        base.startEditMode();
    }

    protected override void stopEditMode()
    {
        base.stopEditMode();
        button.Click += onButtonClick;
        button.PreviewMouseLeftButtonDown += onButtonPress;
        button.PreviewMouseLeftButtonUp += onButtonRelease;
    }

    private void onButtonClick(object sender, RoutedEventArgs e)
    {
        ((HMIButtonViewModel)DataContext).OnClicked();
    }

    private void onButtonPress(object sender, MouseButtonEventArgs e)
    {
        ((HMIButtonViewModel)DataContext).OnPressed();
    }

    private void onButtonRelease(object sender, MouseButtonEventArgs e)
    {
        ((HMIButtonViewModel)DataContext).OnReleased();
    }
}
