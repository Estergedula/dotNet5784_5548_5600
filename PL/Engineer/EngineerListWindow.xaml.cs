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

namespace PL.Engineer;


/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.All;
    public ObservableCollection<BO.Engineer> EngineerList
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }
    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("CourseList", typeof(ObservableCollection<BO.Engineer>),
            typeof(EngineerListWindow), new PropertyMetadata(null));


    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Engineer.ReadAll();
        EngineerList = temp == null ? new() : new(temp);
    }

    private void cmbFilter_SelectedChange(object sender, SelectionChangedEventArgs e)
    {

    }
}
//נשים לב: המהנדסים לא מוצגים,  ו7א לא הלך