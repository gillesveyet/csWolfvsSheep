using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WolfvsSheep
{
    struct Pos : IEquatable<Pos>, IComparable<Pos>
    {
        const int CHECKER_SIZE = 10;

        public const int MAX_POSITIONS = 50;

        // 0 --------> X
        // |
        // !
        // Y

        private byte _x;
        private byte _y;

        /// <summary>
        /// X position from 0 (left) to 9 (right).
        /// </summary>
        public int X { get { return _x; } private set { _x = (byte)value; } }

        /// <summary>
        /// Y position from 0 (top) to 9 (bottom).
        /// </summary>
        public int Y { get { return _y; } private set { _y = (byte)value; } }

        public static Pos None = new Pos(0,0);

        public bool IsNone
        {
            get { return _x == 0 && _y == 0; }
        }

        /// <summary>
        /// Byte value from position.
        /// 
        ///   49  48  47  46  45 
        /// 40  41  42  43  44
        /// .....................
        /// .....................
        /// .....................
        /// 20  21  22  23  24
        ///   19  18  17  16  15
        /// 10  11  12  13  14
        ///    9   8   7   6   5
        ///  0   1   2   3   4 
        /// 
        /// Sheep start pos are 0,1,2,3,4
        /// Wolf start pos is 47
        /// </summary>
        public byte ByteVal
        {
            get
            {
                int byVal = (9 - Y) * 5;

                if (Y % 2 == 0)
                    byVal += 4 - X / 2;
                else
                    byVal += X / 2;

                return (byte)byVal;
            }
        }

        public Pos(int x, int y)
            : this()
        {
            this._x = (byte) x;
            this._y = (byte) y;
        }

        public Pos(byte byVal)
            : this()
        {
            this.Y = 9 - byVal / 5;
            this.X = this.Y % 2 == 0 ? 9 - (byVal % 5) * 2 : (byVal % 5) * 2;
        }

        public bool IsValid
        {
            get { return X >= 0 && X < CHECKER_SIZE && Y >= 0 && Y < CHECKER_SIZE && (X + Y) % 2 != 0; }
        }

        public IEnumerable<Pos> GetWolfMoves()
        {
            int x = this.X;
            int y = this.Y;

            //Can omit check Y < 9 because it would mean that wolf has already win. 
            //if (Y < 9)
            //{

            if (y % 2 == 0)
            {
                if (x < 9)
                    yield return new Pos(x + 1, y + 1);

                if (x > 0)
                    yield return new Pos(x - 1, y + 1);
            }
            else
            {
                //same moves as above but in different order : so that wolf try to remain in center position (X = 4 or x=5)
                if (x > 0)
                    yield return new Pos(x - 1, y + 1);

                if (x < 9)
                    yield return new Pos(x + 1, y + 1);
            }

            //}

            if (y > 0)
            {
                if (x < 9)
                    yield return new Pos(x + 1, y - 1);

                if (x > 0)
                    yield return new Pos(x - 1, y - 1);
            }
        }

        public IEnumerable<Pos> GetSheepMoves()
        {
            int x = this.X;
            int y = this.Y;

            if (y > 0)
            {
                if (y % 2 == 0)
                {
                    if (x > 0)
                        yield return new Pos(x - 1, y - 1);

                    if (x < 9)
                        yield return new Pos(x + 1, y - 1);
                }
                else
                {
                    //same moves as above but in different order : so that sheep do not leave hole
                    if (x < 9)
                        yield return new Pos(x + 1, y - 1);

                    if (x > 0)
                        yield return new Pos(x - 1, y - 1);
                }
            }
        }

        public bool Equals(Pos other)
        {
            return this._x == other._x && this._y == other._y;
        }

        public int CompareTo(Pos other)
        {
            return this.ByteVal.CompareTo(other.ByteVal);
        }

        public override bool Equals(object obj)
        {
            if (obj is Pos)
                return this.Equals((Pos)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ByteVal;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.X, this.Y);
        }
    }
}
