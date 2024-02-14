using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Status TaskStatus { get; set; } = BO.Status.All;
    /// <summary>
    /// List of task
    /// </summary>
    public IEnumerable<BO.TaskInList>? TaskList
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    /// <summary>
    /// Shows the window of task list
    /// </summary>
    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.TaskInList.ReadAll();
        TaskList = temp;
    }

    /// <summary>
    /// Select stats of task from the enum
    /// </summary>
    /// <param name="sender">The button</param>
    /// <param name="e">The event</param>
    private void CmbTaskStatus_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp;
    }

    /// <summary>
    /// Display list of tasks when the user dubbleclicks
    /// </summary>
    /// <param name="sender">The button</param>
    /// <param name="e">The event</param>
    private void LsvDisplayTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? taskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
        new TaskWindow(taskInList!.Id).ShowDialog();
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp;
    }

    /// <summary>
    /// Call the add task method from the task window
    /// </summary>
    /// <param name="sender">The button</param>
    /// <param name="e">The event</param>
    private void BtnAddTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp;
    }
}
