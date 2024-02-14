﻿using System;
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
/// 

public partial class EngineerListWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.EngineerExperience EngineerExperience { get; set; } = BO.EngineerExperience.All;
    public IEnumerable<BO.EngineerInList>? EngineerList
    {
        get { return (IEnumerable<BO.EngineerInList>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));

    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.EngineerInList.ReadAll();
        EngineerList = temp;

    }

    private void CmbEngineerExperience_SelectionChange(object sender, SelectionChangedEventArgs e)
    {
        var temp = EngineerExperience == BO.EngineerExperience.All ?
        s_bl?.EngineerInList.ReadAll() :
          s_bl?.EngineerInList.ReadAll(item => item!.Level == EngineerExperience);
        EngineerList = temp;
    }

    private void BtnAddEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().ShowDialog();
        var temp = EngineerExperience == BO.EngineerExperience.All ? s_bl?.EngineerInList.ReadAll() :
             s_bl?.EngineerInList.ReadAll(item => item!.Level == EngineerExperience);
        EngineerList = temp;
    }

    private void LsvDisplayEngineers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.EngineerInList? engineerInList = (sender as ListView)?.SelectedItem as BO.EngineerInList;
        new EngineerWindow(engineerInList!.Id).ShowDialog();
        var temp = EngineerExperience == BO.EngineerExperience.All ? s_bl?.EngineerInList.ReadAll() :
        s_bl?.EngineerInList.ReadAll(item => item!.Level == EngineerExperience);
        EngineerList = temp;
    }
}
