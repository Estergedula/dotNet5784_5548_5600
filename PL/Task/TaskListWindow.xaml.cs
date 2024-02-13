using BlApi;
using PL.Engineer;
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

    public static readonly DependencyProperty CourseListProperty =
        DependencyProperty.Register("CourseList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Status TaskStatus { get; set; } = BO.Status.All;
    public ObservableCollection<BO.TaskInList> TaskList
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));

    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.TaskInList.ReadAll();
        TaskList = temp == null ? new() : new(temp);
    }

    private void CmbTaskStatus_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        var temp = TaskStatus == BO.Status.All ? s_bl?.TaskInList.ReadAll() :
        s_bl?.TaskInList.ReadAll(item => item!.Status == TaskStatus);
        TaskList = temp == null ? new() : new(temp);

    }


    private void LsvDisplayEngineers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? taskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
        new TaskWindow(taskInList!.Id).ShowDialog();
    }

    private void BtnAddTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskWindow().ShowDialog();
    }
}
