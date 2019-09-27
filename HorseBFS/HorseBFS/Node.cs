using System;
using System.Collections.Generic;

namespace HorseBFS
{
	public class Node : IEquatable<Node>
	{
		public int X { get; set; }
		public int Y { get; set; }
		public static Dictionary<int, char> TransformBackDictionary = new Dictionary<int, char>()
			{{1,'a'},{2,'b'},{3,'c'},{4,'d'},{5,'e'},{6,'f'},{7,'g'},{8,'h'}};
		public static Dictionary<char, int> TransformDictionary = new Dictionary<char, int>()
			{{'a',1},{'b',2},{'c',3},{'d',4},{'e',5},{'f',6},{'g',7},{'h',8},};
		public Node(int x, int y)
		{
			X = x;
			Y = y;
		}

		public bool CheckGoodPosition(Node target)
		{
			var dx = Math.Abs(target.X - X);
			var dy = Math.Abs(target.Y - Y);
			return CheckCorrectPosition() && (dx == 0 && dy == 0 || dx > 1 || dy > 1);
		}

		public bool CheckCorrectPosition()
		{
			return X < 9 && X > 0 && Y < 9 && Y > 0;
		}

		public bool Equals(Node other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return X == other.X && Y == other.Y;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Node) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}
	}
}