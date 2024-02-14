using BO;
using System;
using System.Windows;

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
    public TaskWindow(int id = 0)
    {
        InitializeComponent();
        this.id = id;
        if (id != 0)
        {
            try
            {
                CurrentTask = s_bl!.Task!.Read(id);
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show("ERROR: '\n" + ex.Message); }
        }
        else
        {
            CurrentTask = new BO.Task { Id = 0, Alias = "", Description = "", CreatedAt = DateTime.Now, Engineer = new EngineerInTask { Id = 0 } };
        }
    }

    public static bool InputIntegrityCheck(BO.Task? task)
    {
        if (task!.Alias == "" || task.Description == "" || task.CreatedAt is null)
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
            s_bl.Engineer.Read(CurrentTask!.Engineer!.Id);

        }
        catch (BO.BlDoesNotExistException ex)
        {
            isOk = false;
            MessageBox.Show("ERROR: '\n" + ex.Message);

        }
        
        if (isOk)
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
                catch (BO.BlInvalidDataException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message);
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message );
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
                catch (BO.BlInvalidDataException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message);
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message);
                }

            }
        }
    }
}