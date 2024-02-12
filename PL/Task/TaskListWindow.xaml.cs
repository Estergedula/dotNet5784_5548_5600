using PL.Task;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.EngineerExperience TaskExperience { get; set; } = BO.EngineerExperience.All;
    public ObservableCollection<BO.TaskInList> TaskList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Task.ReadAll();
        TaskList = temp == null ? new() : new(temp);

    }

    private void cmbTaskExperience_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        var temp = TaskExperience == BO.EngineerExperience.All ? s_bl?.Task.ReadAll() :
        s_bl?.Task.ReadAll(item => item!.ComplexilyLevel == TaskExperience);
        TaskList = temp == null ? new() : new(temp);

    }

    private void btnAddTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
    }

    private void lsvDisplayTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.Task? taskInList = (sender as ListView)?.SelectedItem as BO.Task;
        new TaskWindow(taskInList!.Id).ShowDialog();
    }
}
