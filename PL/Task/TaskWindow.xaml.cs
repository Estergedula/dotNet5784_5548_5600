using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
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
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    private int id;
    public BO.Task? CurrentTask
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));
    public TaskWindow(int id=0)
    {
        InitializeComponent();
        this.id = id;
        if (id!=0)
        {
            try { CurrentTask=s_bl!.Task!.Read(id); }
            catch (BO.BlDoesNotExistException) { MessageBox.Show("ERROR: '\n'There is no object with id "+id); }
        }
        else
        {
            CurrentTask=new BO.Task { Id=0,Alias="",Description="",CreatedAt=DateTime.Now };
        }
    }

    private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (id != 0)
        {
            try
            {
                s_bl.Task.Update(CurrentTask!);
                MessageBox.Show("Object with id " + id + "had updated successfully!");
                this.Close();
            }
            catch (BO.BlInvalidDataException)
            {
                MessageBox.Show("ERROR: '\n'There is an invalid input in the object with id " + id);
            }
            catch (BO.BlDoesNotExistException)
            {
                MessageBox.Show("ERROR: '\n'There is no object with id " + id);
            }
        }
        else
        {
            try
            {
                s_bl.Task.Create(CurrentTask!);
                MessageBox.Show("Object with id " + id + "had created successfully!");
                this.Close();
            }
            catch (BO.BlInvalidDataException)
            {
                MessageBox.Show("ERROR: '\n'There is an invalid input in the object with id " + id);
            }
            catch (BO.BlAlreadyExistsException)
            {
                MessageBox.Show("ERROR: '\n'There is already an object with id " + id);
            }

        }
    }
}

//public required int Id { get; init; }
//public required string Description { get; set; }
//public required string? Alias { get; set; }
//public MillestoneInTask? Milestone { get; set; }
//public Status Status { get; set; }
//public IEnumerable<TaskInList>? DependenciesList { get; set; }?????????????
//public required DateTime CreatedAt { get; set; }//תאריך יצירה
//public DateTime ScheduleDate { get; set; }//תאריך התחלה משוער
//public DateTime Start { get; set; }//תאריך התחלה בפועל
//public DateTime ForecastDate { get; set; }//תאריך משוער לסיום
//public DateTime DeadLine { get; set; }//תאריך אחרון לסיום
//public DateTime Complete { get; set; }//תאריך סיום בפועל
//public string? Deliverables { get; set; }
//public string? Remarks { get; set; }
//public EngineerInTask? Engineer { get; set; }
//public EngineerExperience ComplexilyLevel { get; set; }