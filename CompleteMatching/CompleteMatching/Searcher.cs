using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace CompleteMatching
{
	public static class Searcher
	{
		public static (bool isSuccess, int[] result) SearchFullMatсhing(string input)
		{
			var content = File.ReadAllLines(input);
			var firstRow = content[0].Split().Select(int.Parse).ToArray();
			var lenX = firstRow[0];
			var lenY = firstRow[1];
			var length = int.Parse(content[1]) - 1;
			var adjacencyArray = new int[length];
			var i = 2;
			while (true)
			{
				var row = content[i].Split();
				var rowLen = row.Length == 10 ? row.Length : row.Length - 1;
				for (int j = 0; j < rowLen; j++)
				{
					var element = int.Parse(row[j]) - 1;
					adjacencyArray[(i - 2) * 10 + j] = element;
				}

				i++;
				if (rowLen != 10)
					break;
			}

			var result = Search(adjacencyArray, lenX);
			return result;

		}

		private static (bool isSuccess, int[] result) Search(int[] adjacencyArray, int nodeCount)
		{
			var visitedXNodes = new HashSet<int>();
			var visitedYNodes = new Dictionary<int, HashSet<int>>();
			for (int i = 0; i < nodeCount; i++)
			{
				if (!visitedYNodes.ContainsKey(i))
					visitedYNodes.Add(i, new HashSet<int>());
			}
			var xDouble = new int?[nodeCount];
			var yDouble = new int?[nodeCount];
			var indication = 1;
			while (indication == 1 && visitedXNodes.Count != nodeCount)
			{
				var queue = new Queue<int>();
				var startNode = GetRandomNode(nodeCount, visitedXNodes);
				queue.Enqueue(startNode);
				indication = 0;
				var res = new List<(int xi, int yi)>();
				var visitedNodes = new HashSet<int>();
				while (queue.Count > 0 && indication == 0)
				{
					var x = queue.Dequeue();
					var correct = true;
					while (correct && indication == 0)
					{
						var y = ChoiceY(adjacencyArray, x, visitedYNodes[x]);
						correct = y.HasValue;
						if (correct)
						{
							visitedYNodes[x].Add(y.Value);
							if (res.Count > 0 && res.Last().xi == x)
								res[res.Count - 1] = (x, y.Value);
							else
								res.Add((x, y.Value));

							var z = yDouble[y.Value];

							if (z.HasValue)
							{
								if (visitedNodes.Contains(z.Value))
									continue;
								queue.Enqueue(z.Value);
								visitedNodes.Add(z.Value);
							}
							else
							{
								indication = 1;
							}
						}
					}
				}

				if (indication == 1)
				{
					for (int i = 0; i < res.Count; i++)
					{
						var searchedY = res[i].yi;
						var searchedX = res[i].xi;
						visitedXNodes.Add(searchedX);
						xDouble[searchedX] = searchedY;
						yDouble[searchedY] = searchedX;
					}
				}

				if (indication == 0)
					return (false, new[] { startNode + 1 });
			}

			return (true, xDouble.Select(x => x.HasValue ? x.Value + 1 : 0).ToArray());

		}

		private static int GetRandomNode(int nodeCount, HashSet<int> visitedNodes)
		{
			var rnd = new Random();
			while (true)
			{
				var randomNode = rnd.Next(nodeCount);
				if (!visitedNodes.Contains(randomNode))
					return randomNode;
			}
		}

		private static int? ChoiceY(int[] adjacencyArray, int x, HashSet<int> visitedYNodes)
		{
			var len = adjacencyArray[x + 1] - adjacencyArray[x];
			for (int i = adjacencyArray[x]; i < adjacencyArray[x] + len; i++)
			{
				if (visitedYNodes.Contains(adjacencyArray[i]))
					continue;
				return adjacencyArray[i];
			}

			return null;
		}
	}
}