using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenShooter.model
{
    class StatTracker
    {

        private long gameTime;
        private int score;
        private int bullets;


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

        public long GameTime
        {
            get { return gameTime; }
            set { gameTime = value; }
        }


        public StatTracker()
        {
            this.gameTime = 0;
            this.score = 0;
            this.bullets = 10;
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
