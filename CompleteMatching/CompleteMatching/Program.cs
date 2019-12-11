using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompleteMatching;

namespace MinMaxSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			var dictPath = Directory.GetCurrentDirectory();

			var input = Path.Combine(dictPath, "in.txt");
			var output = Path.Combine(dictPath, "out.txt");
			var resultSearch = Searcher.SearchFullMatсhing(input);
			if (!resultSearch.isSuccess)
			{
				File.WriteAllText(output, "N\r\n"+string.Join(" ", resultSearch.result));
				
			}
			else
			{
				var strResult = "Y\r\n";
				strResult += String.Join(" ", resultSearch.result);
				File.WriteAllText(output, strResult);
			}
		}
	}
}
