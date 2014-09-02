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

        public int Score
        {
            get { return score; }
            set { score = value; }
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
        }

        public void increaseScore()
        {
            this.Score++;
        }

        public void increaseTime(long time)
        {
            this.GameTime += time;
        }



    }
}
