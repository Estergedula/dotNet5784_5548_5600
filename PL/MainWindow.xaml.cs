using PL.Engineer;
using PL.Task;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void BtnCreateData_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Do you want to create new data?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            DalApi.IDal dal = DalApi.Factory.Get;
            DalTest.Initialization.Do(dal);
        }
    }

    private void BtnTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
    private void BtnEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
}

