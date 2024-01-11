using System.Windows;

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>


public partial class EngineerWindow : Window
{
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
        if (id!=0)
        {
            try { CurrentEngineer=s_bl!.Engineer!.Read(id); }
            catch (BO.BlDoesNotExistException) { MessageBox.Show("ERROR: '\n'There is no object with id "+id); }
        }
        else
        {
            CurrentEngineer=new BO.Engineer { Id=0,Name="fghjkl"};
        }
    }
}
