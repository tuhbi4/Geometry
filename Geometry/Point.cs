using System;

namespace Geometry
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public Point()
        {
            X = 0;
            Y = 0;
        }
        public Point(int coordinateX, int coordinateY)
        {
            X = coordinateX;
            Y = coordinateY;
        }
        public Point(int startOn, int xEndOn, int yEndOn)
        {
            Random randomCoordinate = new();
            X = randomCoordinate.Next(startOn, xEndOn);
            Y = randomCoordinate.Next(startOn, yEndOn);
        }
    }
}