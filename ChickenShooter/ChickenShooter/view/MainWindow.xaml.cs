using ChickenShooter.controller;
using ChickenShooter.helper;
using ChickenShooter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ChickenShooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Game game;

        public MainWindow(Game game)
            : base()
        {
            InitializeComponent();
            this.game = game;
            this.Closing += closeGameScreen;
        }

        public void closeGameScreen(object sender, EventArgs e)
        {
            game.Running = false;
        }

    }
}
