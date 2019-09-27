using System;
using System.Collections.Generic;
using System.IO;

namespace HorseBFS
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictPath = Directory.GetCurrentDirectory();

			var input = Path.Combine(dictPath, "in.txt");
			var output = Path.Combine(dictPath, "out.txt");
			var content = File.ReadAllLines(input);
			var start = content[0].ToCharArray();
			var target = content[1];
			var startNode = new Node(Node.TransformDictionary[start[0]], int.Parse("" + start[1]));
			var targetNode = new Node(Node.TransformDictionary[target[0]], int.Parse("" + target[1]));
			var result = BFS(startNode, targetNode);
			var str = "";
			for (int i = 0; i < result.Length; i++)
			{
				var node = result[i];
				str += Node.TransformBackDictionary[node.X] + node.Y.ToString();
				if (i < result.Length - 1)
					str += "\r\n";
			}
			File.WriteAllText(output, str);
		}

		public static Node[] BFS(Node start, Node target)
		{
			if (start.Equals(target))
				return new Node[0];
			if (!start.CheckCorrectPosition() || !target.CheckCorrectPosition())
				return new Node[0];

			var queue = new Queue<Node>();
			var visited = new HashSet<Node>();
			var dictOfWays = new Dictionary<Node, Node[]>();
			queue.Enqueue(start);
			visited.Add(start);
			dictOfWays[start] = new[] { start };
			while (queue.Count > 0)
			{
				var node = queue.Dequeue();
				for (int dx = 1; dx > -2; dx -= 2)
					for (int dy = 2; dy > -3; dy -= 4)
					{
						var nextNode = new Node(node.X + dx, node.Y + dy);
						var nextNode2 = new Node(node.X + dy, node.Y + dx);
						NewMethod(target, visited, nextNode, queue, dictOfWays, node);
						NewMethod(target, visited, nextNode2, queue, dictOfWays, node);
					}
			}

			return dictOfWays.ContainsKey(target) ? dictOfWays[target] : new Node[0];
		}

		private static void NewMethod(Node target, HashSet<Node> visited, Node nextNode, Queue<Node> queue,
			Dictionary<Node, Node[]> dictOfWays, Node node)
		{
			if (!nextNode.CheckGoodPosition(target) || visited.Contains(nextNode)) return;
			visited.Add(nextNode);
			queue.Enqueue(nextNode);
			var way = dictOfWays[node];
			var newWay = new Node[way.Length + 1];
			Array.Copy(way, 0, newWay, 0, way.Length);
			newWay[way.Length] = nextNode;
			dictOfWays.Add(nextNode, newWay);
		}
	}
}
