﻿using System.Windows;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>


public partial class EngineerWindow : Window
{
    private int id;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Engineer? CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(EngineerProperty); }
        set { SetValue(EngineerProperty, value); }
    }

    public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("Engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
    public EngineerWindow(int id = 0)
    {
        InitializeComponent();
        this.id = id;
        if (id!=0)
        {
            try { CurrentEngineer=s_bl!.Engineer!.Read(id); }
            catch (BO.BlDoesNotExistException) { MessageBox.Show("ERROR: '\n'There is no object with id "+id); }
        }
        else
        {
            CurrentEngineer=new BO.Engineer { Id=0 };
        }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if (id != 0)
        {
            try
            {
                s_bl.Engineer.Update(CurrentEngineer!);
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
                s_bl.Engineer.Create(CurrentEngineer!);
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
