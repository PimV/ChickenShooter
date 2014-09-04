using ChickenShooter.helper;
using ChickenShooter.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChickenShooter.controller
{
    public class GameController
    {
        public MainWindow mainView;
        public List<Chicken> chickens;
        private Boolean running;
        private ChickenShooter.helper.Timer hqTimer;
        private int fps;
        private int interval;
        private StatTracker statTracker;
        private Thread gameThread;

        public GameController()
        {
        }


       

    }
}
