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
            try
            {
                CurrentTask = s_bl!.Task!.Read(id);
            }
            catch (BO.BlDoesNotExistException) { MessageBox.Show("ERROR: '\n'There is no object with id "+id); }
        }
        else
        {
            CurrentTask = new BO.Task { Id=0,Alias="",Description="",CreatedAt=DateTime.Now,EngineerId=new EngineerInTask { Id=0} };
        }
    }

    public static bool InputIntegrityCheck(BO.Task? task)
    {
        if ( task!.Alias == "" || task.Description == "" || task.CreatedAt is null)
        {
            MessageBox.Show("ERROR: '\n'Missing data!.");
            return false;
        }
        return true;
    }

    private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        bool isOk = true;
        try
        {
            s_bl.Engineer.Read(CurrentTask!.EngineerId!.Id);
                
        }
        catch(BO.BlDoesNotExistException)
        {
            isOk = false;
          
        }
        if(!isOk)
            MessageBox.Show("ERROR: '\n'There is no engineer with this id");
        else
        {
            if (id != 0)
            {
                try
                {
                    if (InputIntegrityCheck(CurrentTask))
                    {
                        s_bl.Task.Update(CurrentTask!);
                        MessageBox.Show("Task with id " + id + " had updated successfully!");
                        this.Close();
                    }
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
                    if (InputIntegrityCheck(CurrentTask))
                    {
                        s_bl.Task.Create(CurrentTask!);
                        MessageBox.Show("Task with id " + id + " had created successfully!");
                        this.Close();
                    }
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
}

