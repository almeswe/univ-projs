namespace Math.Tests.Matrix
{
	[TestClass]
	public sealed class Matrix_Transpose
	{
		[TestMethod]
		public void TestMethod1()
		{
			var m = new Math.Matrix(new float[,]
			{
				{1, 2, 3},
				{4, 5, 6}
			});
			var t = new Math.Matrix(new float[,]
			{
				{1,4 },
				{2,5 },
				{3,6},
			});
			Assert.IsTrue(~m == t);
		}

		[TestMethod]
		public void TestMethod2()
		{
			var m = new Math.Matrix(new float[,]
			{
				{7, 8},
				{9, 10},
				{11, 12},
			});
			var t = new Math.Matrix(new float[,]
			{
				{7, 9, 11},
				{8, 10, 12},
			});
			Assert.IsTrue(~m == t);
		}

		[TestMethod]
		public void TestMethod3()
		{
			var m = new Math.Matrix(new float[,]
			{
				{1, 2},
				{3, 4},
			});
			var t = new Math.Matrix(new float[,]
			{
				{1, 3},
				{2, 4},
			});
			Assert.IsTrue(~m == t);
		}

		[TestMethod]
		public void TestMethod4()
		{
			var m = new Math.Matrix(new float[,]
			{
				{5, 6, 7},
				{8, 9, 10},
				{11, 12, 13},
			});
			var t = new Math.Matrix(new float[,]
			{
				{5,8,11 },
				{6,9,12 },
				{7,10,13},
			});
			Assert.IsTrue(~m == t);
		}

		[TestMethod]
		public void TestMethod5()
		{
			var m = new Math.Matrix(new float[,]
			{
				{1,2,3,4},
			});
			var t = new Math.Matrix(new float[,]
			{
				{1},
				{2},
				{3},
				{4},
			});
			Assert.IsTrue(~m == t);
		}
	}
}
