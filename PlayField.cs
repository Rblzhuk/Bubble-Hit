using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCourseProject
{
    class PlayField
    {
        private int widthPlayField;
        public int WidthPlayField
        {
            get => widthPlayField;

            set
            {
                if (widthPlayField != value) { widthPlayField = value; }
            }
        }

        private int heightPlayField;
        public int HeightPlayField
        {
            get => heightPlayField;

            set
            {
                if (heightPlayField != value) { heightPlayField = value; }
            }
        }

        private Color colorFramePlayField;
        public Color ColorPlayField
        {
            get => colorPlayField;

            set
            {
                if (colorPlayField != value) { colorPlayField = value; }
            }
        }

        private Color colorPlayField;
        public Color ColorPlayField
        {
            get => colorPlayField;

            set
            {
                if (colorPlayField != value) { colorPlayField = value; }
            }
        }
    }
}
