using ChickenShooter.Model;
using System;
using System.Windows;

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

        public MainWindow()
        {
            InitializeComponent();
            //this.game = game;
            //this.Closing += closeGameScreen;
        }

        public void closeGameScreen(object sender, EventArgs e)
        {
            game.Running = false;
        }

    }
}
