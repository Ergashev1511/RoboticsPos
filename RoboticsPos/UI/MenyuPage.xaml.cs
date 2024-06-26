﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RoboticsPos.Variablies;

namespace RoboticsPos.UI
{
    /// <summary>
    /// Interaction logic for MenyuPage.xaml
    /// </summary>
    public partial class MenyuPage : UserControl
    {

        MainWindow mainWindow { get; set; }


        public void SetMainWinndow(MainWindow mainWindow)
        { this.mainWindow = mainWindow; }
        public MenyuPage()
        {
            InitializeComponent();
        }


        private void Button_Sozlama(object sender, RoutedEventArgs e)
        {
            mainWindow.SettingsViewBox.Visibility = Visibility.Visible;
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
        }


        public void UserNameId()
        {
            if (mainWindow.MenyuViewBox.Visibility == Visibility.Visible)
            {
                
            }
        }

        private void Button_Kassa(object sender, RoutedEventArgs e)
        {
            mainWindow.KassaViewBox.Visibility = Visibility.Visible;
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
        }

        private void Button_ShaxsiyMal(object sender, RoutedEventArgs e)
        {
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
            mainWindow.Shaxsiy_kabinetViewBox.Visibility = Visibility.Visible;
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
            mainWindow.KirishViewBox.Visibility = Visibility.Visible;
            ClearForm();
        }

        private void Store_btn_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Store_doc.Visibility = Visibility.Visible;
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
        }

       
        public void ClearForm()
        {
            usernametxt.Text = "";
            txt_parol.Text = "";
        }

        // private void MenyuPage_OnLoaded(object sender, RoutedEventArgs e)
        // {
        //     usernametxt.Text = StaticVariablies.CurrentUsername;
        // }
        private void Report_btn_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.MenyuViewBox.Visibility = Visibility.Collapsed;
            mainWindow.hisobot_doc.Visibility = Visibility.Visible;
        }

        public void GetUserName(string username, string parol)
        {
            usernametxt.Text = username;
            txt_parol.Text = parol;
        }
    }
}
