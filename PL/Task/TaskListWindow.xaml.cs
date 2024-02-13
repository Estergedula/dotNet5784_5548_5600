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
    public IEnumerable<BO.TaskInList>? TaskList
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.TaskInList.ReadAll();
        TaskList = temp;
    }
    private void CmbTaskStatus_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp;// == null ? new() : new(temp);
    }


    private void LsvDisplayTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? taskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
        new TaskWindow(taskInList!.Id).ShowDialog();
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp; //== null ? new() : new(temp);
    }

    private void BtnAddTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp;//== null ? new() : new(temp);
    }
}
