using System;
using System.Collections.Generic;

namespace MinOstov
{
	public class PlanePoint
	{
		public PlanePoint(int x, int y, int number)
		{
			X = x;
			Y = y;
			Number = number;
			NeighboringPoints = new List<PlanePoint>();
			Visited = false;
		}

		public PlanePoint()
		{ }

		public List<PlanePoint> NeighboringPoints;
		public int X { get; set; }
		public int Y { get; set; }
		public int Number { get; set; }
		public bool Visited { get; set; }

		public int DistanceToOtherPoint(PlanePoint otherPoint)
		{
			return Math.Abs(X - otherPoint.X) + Math.Abs(Y - otherPoint.Y);
		}
	}
}