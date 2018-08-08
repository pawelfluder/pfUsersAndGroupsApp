﻿using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace pfUsersAndGroupsApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnityContainer _container;
        IRegionManager _regionManager;
        IRegion _region;

        Users _viewA;
        Groups _viewB;

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewA = _container.Resolve<Users>();
            _viewB = _container.Resolve<Groups>();

            _region = _regionManager.Regions["ContentRegion"];

            _region.Add(_viewB);
            _region.Add(_viewA);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //activate view a
            _region.Activate(_viewA);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //deactivate view a
            _region.Deactivate(_viewA);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //activate view b
            _region.Activate(_viewB);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //deactivate view b
            _region.Deactivate(_viewB);
        }
    }
}