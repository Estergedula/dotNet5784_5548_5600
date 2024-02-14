using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>


public partial class EngineerWindow : Window
{
    private readonly int id;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public IEnumerable<int> AllTasksId
    {
        get { return (IEnumerable<int>)GetValue(TasksProperty); }
        set { SetValue(TasksProperty, value); }
    }

    public static readonly DependencyProperty TasksProperty =
        DependencyProperty.Register("AllTasksId", typeof(IEnumerable<int>), typeof(EngineerWindow), new PropertyMetadata(null));
    public BO.Engineer? CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    public EngineerWindow(int id = 0)
    {
        InitializeComponent();
        this.id = id;
        if (id != 0)
        {
            try { 
                CurrentEngineer = s_bl!.Engineer!.Read(id);
                if(CurrentEngineer!.CurrentTask is null) {
                    CurrentEngineer.CurrentTask = new BO.TaskInEngineer {Id=0,Alias=" " };
                }
         
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show("ERROR: '\n"+ex.Message); }
        }
        else
        {
            CurrentEngineer = new BO.Engineer { Id = 0, CurrentTask = new BO.TaskInEngineer { Id = 0, Alias = " " } };
        }
        AllTasksId = s_bl.Task.ReadAll().Select(t => t.Id).ToList();
    }

    public static bool IsValidEmailAddress(string? s)
    {
        Regex regex = new (@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(s!);
    }

    public static bool InputIntegrityCheck(BO.Engineer? engineer)
    {
       return !(engineer?.Id <= 0) && engineer?.Level != BO.EngineerExperience.All&&engineer!.Name != "" && !(engineer.Cost <= 0) && IsValidEmailAddress(engineer.Email);            
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        bool isOk= CurrentEngineer!.CurrentTask is not null;
        if (isOk)
        {
            if (id != 0)
            {
                try
                {
                    if (InputIntegrityCheck(CurrentEngineer))
                    {
                        s_bl.Engineer.Update(CurrentEngineer!);
                        MessageBox.Show("Engineer with id " + id + " had updated successfully!");
                        this.Close();
                    }
                    else MessageBox.Show("ERROR: '\n'The data you entered is incorrect.");
                }
                catch (BO.BlInvalidDataException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message);
                }
                catch (BO.BlDoesNotExistException ex)
                {
                    MessageBox.Show("ERROR: '\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    if (InputIntegrityCheck(CurrentEngineer))
                    {
                        s_bl.Engineer.Create(CurrentEngineer!);
                        MessageBox.Show("Engineer with id " + CurrentEngineer!.Id + " had created successfully!");
                        this.Close();
                    }
                    else MessageBox.Show("ERROR: '\n'The data you entered is incorrect.");
                }
                catch (BO.BlInvalidDataException ex)
                {
                    MessageBox.Show("ERROR: '\n"+ex.Message);
                }
                catch (BO.BlAlreadyExistsException ex)
                {
                    MessageBox.Show("ERROR: '\n"+ex.Message);
                }

            }
        }
    }

}
