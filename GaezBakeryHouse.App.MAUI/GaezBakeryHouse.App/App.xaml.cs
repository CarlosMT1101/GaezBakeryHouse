﻿using GaezBakeryHouse.App.Views;

namespace GaezBakeryHouse.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }
    }
}