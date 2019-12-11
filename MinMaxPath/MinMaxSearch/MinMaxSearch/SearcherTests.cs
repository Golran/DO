using System;
using System.IO;
using NUnit.Framework;

namespace MinMaxSearch
{
	public class SearcherTests
	{
		private readonly string path = @"D:\Учёба\ДО\MinMaxPath\TestMinMax";
		[TestCase("Test1.txt",9,"1 3 2 4 5")]
		[TestCase("Test2.txt",13,"1 4")]
		[TestCase("Test3.txt",10,"1 4")]
		[TestCase("Test4.txt",15,"1 3 5 4")]
		[TestCase("Test5.txt",60, "1 2 4")]
		[TestCase("Test6.txt",70, "1 2 3 5")]
		[TestCase("Test7.txt",60,"1 2 3")]
		[TestCase("Test8.txt",60,"1 2")]
		[TestCase("Test9.txt",60,"1 2 4")]
		[TestCase("Test10.txt",0,"")]
		[TestCase("Test11.txt",60,"1 2 3 6")]
		[TestCase("Test12.txt",60,"3 6")]
		[TestCase("Test13.txt",60,"3 6")]
		[TestCase("Test14.txt",50,"3 5 6")]
		[TestCase("Test15.txt",15,"3 1 2 4")]
		[TestCase("Test16.txt",8,"3 5 6 4")]
		public void Test(string input, int expectedWeight, string expectedWay)
		{
			var superPath = Path.Combine(path, input);

			var searchResult = Searcher.SearchMaxMin(superPath);

			Assert.AreEqual(expectedWeight,searchResult.weight);
			Assert.AreEqual(expectedWay, string.Join(" ",searchResult.way));

		}
	}
}