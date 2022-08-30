using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCourseProject
{
    class Ball
    {
        public int x;
        public int y;
        public Ball(int ballX,int ballY)
        {
            x = ballX;
            y = ballY;
        }

        public override bool Equals(object obj)
        {
            if (obj is Ball)
            {
                Ball c = (Ball)obj;
                return x == c.x && y == c.y;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
