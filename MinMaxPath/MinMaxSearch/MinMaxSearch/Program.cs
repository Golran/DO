using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictPath = Directory.GetCurrentDirectory();

			var input = Path.Combine(dictPath, "in.txt");
			var output = Path.Combine(dictPath, "out.txt");
			var resultSearch = Searcher.SearchMaxMin(input);
			var wayLength = resultSearch.way.Length;
			if(wayLength == 0)
				File.WriteAllText(output, "N");
			else
			{
				var strResult = "Y\r\n";
				strResult += String.Join(" ", resultSearch.way) + "\r\n";
				strResult += resultSearch.weight;
				File.WriteAllText(output, strResult);
			}
		}
	}
}
