namespace Data.Model
{
    public class MarkModel
    {
        private int _x;
        private int _y;

        public int X
        {
            get { return _x; }
            set
            {
                if (value > 5) _x = 5;
                else if (value < 1) _x = 1;
                else _x = value;
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value > 5) _y = 5;
                else if (value < 1) _y = 1;
                else _y = value;
            }
        }
    }
}
