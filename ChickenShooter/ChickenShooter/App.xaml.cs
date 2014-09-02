using ChickenShooter.controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using ChickenShooter.helper;
using ChickenShooter.model;
using System.Threading;

namespace ChickenShooter
{
    public partial class App : Application
    {
        private GameController gameController;

        public App()
        {
            gameController = new GameController();
        }
    }
}
