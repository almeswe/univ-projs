namespace Math.Tests.Matrix
{
	[TestClass]
	public sealed class Matrix_Dot
	{
		[TestMethod]
		public void Test1()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3},
				{4, 5, 6},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{7, 8},
				{9, 10},
				{11, 12},
			});
			var m = new Math.Matrix(new float[,]
			{
				{58, 64},
				{139, 154}
			});
			Assert.IsTrue(m == (ml % mr));
		}

		[TestMethod]
		public void Test2()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2},
				{3, 4},
				{5, 6}
			});
			var mr = new Math.Matrix(new float[,]
			{
				{7, 8, 9, 10},
				{11, 12, 13, 14},
			});
			var m = new Math.Matrix(new float[,]
			{
				{29, 32, 35, 38},
				{65, 72, 79, 86},
				{101, 112, 123, 134}
			});
			Assert.IsTrue(m == (ml % mr));
		}

		[TestMethod]
		public void Test3()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2},
				{3, 4},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{5, 6},
				{7, 8},
			});
			var m = new Math.Matrix(new float[,]
			{
				{19, 22},
				{43, 50},
			});
			Assert.IsTrue(m == (ml % mr));
		}

		[TestMethod]
		public void Test4()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{10},
				{11},
				{12},
			});
			var m = new Math.Matrix(new float[,]
			{
				{68},
				{167},
				{266}
			});
			Assert.IsTrue(m == (ml % mr));
		}

		[TestMethod]
		public void Test5()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3, 4},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{5},
				{6},
				{7},
				{8}
			});
			var m = new Math.Matrix(new float[,]
			{
				{70},
			});
			Assert.IsTrue(m == (ml % mr));
		}

		[TestMethod]
		public void Test6()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3, 4},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{5},
				{6},
				{7},
				{8},
				{8},
			});
			Assert.ThrowsException<ArgumentException>(() => ml % mr);
		}
	}
}
