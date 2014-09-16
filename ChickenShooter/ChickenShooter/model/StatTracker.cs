using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.Model
{
    public class StatTracker
    {

        private double gameTime;
        private int score;
        private int bullets;
        private int maxScore;
        private double realTimeFps;

        private string statusMsg;
        public string StatusMsg { get { return statusMsg; } set { statusMsg = value; } }

        private Boolean gameRunning;
        public Boolean GameRunning { get { return gameRunning; } set { gameRunning = value; } }

        public int MAX_SCORE { get { return maxScore; } set { maxScore = value; } }

        public double RealTimeFps { get { return realTimeFps; } set { realTimeFps = value; } }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        public double GameTime
        {
            get { return gameTime; }
            set { gameTime = value; }
        }


        public StatTracker()
        {
            this.gameTime = 0;
            this.score = 0;
            this.bullets = 10;
            MAX_SCORE = 5;
        }

        public void increaseScore()
        {
            this.Score++;
        }

        public void increaseTime(long time)
        {
            this.GameTime += time;
        }

        public void setGameTime(long time)
        {
            this.GameTime = time;
        }

        public void decreaseBullets()
        {
            this.Bullets--;
        }

    }
}
