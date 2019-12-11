using System.IO;
using NUnit.Framework;

namespace CompleteMatching
{
	public class SearcherTests
	{
		private readonly string sourceDir = @"D:\Учёба\ДО\CompleteMatching\Tests";

		[TestCase("in1.txt", true, ExpectedResult = "1 2 3 4")]
		[TestCase("in2.txt", true, ExpectedResult = "4 3 2 1")]
		[TestCase("in3.txt", false, ExpectedResult = "4")]
		[TestCase("in4.txt", false, ExpectedResult = "2")]
		public string Test(string fileName, bool expectedSuccess)
		{
			var path = Path.Combine(sourceDir, fileName);

			var result = Searcher.SearchFullMatсhing(path);

			//Assert.AreEqual(expectedSuccess, result.isSuccess);
			return string.Join(" ", result.result);
		}
	}
}