using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinMaxSearch
{
	public static class Searcher
	{
		public static (int[] way, int weight) SearchMaxMin(string input)
		{
			var content = File.ReadAllLines(input);
			var length = int.Parse(content[0]);
			var matrix = new int[length, length];
			for (int i = 1; i < length + 1; i++)
			{
				var row = content[i].Split();
				for (int j = 0; j < row.Length; j++)
				{
					var element = int.Parse(row[j]);
					matrix[i - 1, j] = element;
				}
			}

			var start = int.Parse(content[length + 1]);
			var target = int.Parse(content[length + 2]);
			return MinMaxSearchD(matrix, start, target);
		}

		private static (int[] way, int weight) MinMaxSearchD(int[,] matrix, int start, int target)
		{
			var length = matrix.GetLength(0);
			var result = new List<int>();
			var resultWeights = new int[target][];
			var arrayWeights = new int[length];
			var visitedNodes = new HashSet<int>();
			arrayWeights[0] = int.MaxValue;
			visitedNodes.Add(start - 1);
			result.Add(start);


			for (int i = 0; i < length; i++)
			{
				if (matrix[start - 1, i] == -32768)
					arrayWeights[i] = int.MaxValue;
				else
					arrayWeights[i] = matrix[start-1, i];
			}

			CopyArray(resultWeights, 0, arrayWeights, length);

			for (int k = 1; k < target; k++)
			{
				var minV = GetIndexByMin(arrayWeights, visitedNodes);
				visitedNodes.Add(minV);
				for (int i = 0; i < length; i++)
				{
					if (matrix[minV, i] == -32768 || visitedNodes.Contains(i))
						continue;
					var newMin = Math.Max(arrayWeights[minV], matrix[minV, i]);
					if (newMin < arrayWeights[i])
					{
						arrayWeights[i] = newMin;
					}
				}
				result.Add(minV + 1);
				CopyArray(resultWeights, k, arrayWeights, length);
			}


			if (arrayWeights[target - 1] != int.MaxValue)
			{
				var minMaxWeight = arrayWeights[target - 1];
				var takeCount = 0;
				for (int i = 0; i < target; i++)
				{
					takeCount++;
					if (resultWeights[i][target - 1] == minMaxWeight)
						break;
				}

				result = result.Take(takeCount).ToList();
				result.Add(target);
				return (result.ToArray(), minMaxWeight);
			}
			return (new int[0], 0);
		}

		private static int GetIndexByMin(int[] arrayWeights, HashSet<int> visitedNodes)
		{
			var result = 0;
			var min = int.MaxValue;
			for (int i = 0; i < arrayWeights.Length; i++)
			{
				if (visitedNodes.Contains(i) || min <= arrayWeights[i])
					continue;
				min = arrayWeights[i];
				result = i;
			}

			return result;
		}

		private static void CopyArray(int[][] destArr, int index, int[] sourceArr, int length)
		{
			var copyArray = new int[length];
			Array.Copy(sourceArr, copyArray, length);
			destArr[index] = copyArray;
		}
	}
}