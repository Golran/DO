using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinOstov
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictPath = Directory.GetCurrentDirectory();

			var input = Path.Combine(dictPath, "in.txt");
			var output = Path.Combine(dictPath, "out.txt");
			var content = File.ReadAllLines(input);
			var length = int.Parse(content[0]);
			var points = new PlanePoint[length];
			for (int i = 1; i < length + 1; i++)
			{
				var row = content[i].Split(' ');
				var x = int.Parse(row[0]);
				var y = int.Parse(row[1]);
				points[i - 1] = new PlanePoint(x, y, i);
			}
			var result = FindMinOstov(points);
			var outContent = points.Select(point => point.NeighboringPoints).ToList();
			var str = "";
			for (int i = 0; i < outContent.Count; i++)
			{
				var listPoints = outContent[i].Select(point => point.Number).OrderBy(key => key).ToList();
				for (var j = 0; j < listPoints.Count; j++)
				{
					var numberPoint = listPoints[j];
					str += numberPoint + " ";
				}
				str += "0\r\n";
			}

			str += result;
			File.WriteAllText(output, str);
		}

		public static int FindMinOstov(PlanePoint[] points)
		{
			if (points.Length == 0)
				return 0;
			var visitedPoints = new List<PlanePoint>();
			var startPoint = points[0];
			startPoint.Visited = true;
			visitedPoints.Add(startPoint);
			var sumDistnces = 0;
			while (visitedPoints.Count < points.Length)
			{
				int minDistance = int.MaxValue;
				PlanePoint nearestPoint = new PlanePoint();
				PlanePoint actualPoint = new PlanePoint();
				for (int i = 0; i < visitedPoints.Count; i++)
				{
					var point = visitedPoints[i];
					for (int j = 0; j < points.Length; j++)
					{
						var otherPoint = points[j];
						if (otherPoint.Visited) continue;
						var distance = point.DistanceToOtherPoint(otherPoint);
						if (distance >= minDistance) continue;
						minDistance = distance;
						actualPoint = point;
						nearestPoint = otherPoint;
					}
				}

				actualPoint.NeighboringPoints.Add(nearestPoint);
				nearestPoint.NeighboringPoints.Add(actualPoint);
				nearestPoint.Visited = true;
				visitedPoints.Add(nearestPoint);
				sumDistnces += minDistance;
			}

			return sumDistnces;
		}
	}
}
