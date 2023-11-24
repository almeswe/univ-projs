namespace Math.Tests.Matrix
{
    [TestClass]
    public sealed class Matrix_Add
    {
		[TestMethod]
		public void Test1()
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
				{6, 8},
				{10, 12},
			});
			Assert.IsTrue(m == (ml + mr));
		}

		[TestMethod]
		public void Test2()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3},
				{4, 5, 6},
				{7, 8, 9},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{10, 11, 12},
				{13, 14, 15},
				{16, 17, 18},
			});
			var m = new Math.Matrix(new float[,]
			{
				{11, 13, 15},
				{17, 19, 21},
				{23, 25, 27},
			});
			Assert.IsTrue(m == (ml + mr));
		}

		[TestMethod]
		public void Test3()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3, 4},
				{5, 6, 7, 8},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{9, 10, 11, 12},
				{13, 14, 15, 16},
			});
			var m = new Math.Matrix(new float[,]
			{
				{10, 12, 14, 16},
				{18, 20, 22, 24},
			});
			Assert.IsTrue(m == (ml + mr));
		}

		[TestMethod]
		public void Test4()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2, 3},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{4, 5, 6},
			});
			var m = new Math.Matrix(new float[,]
			{
				{5, 7, 9},
			});
			Assert.IsTrue(m == (ml + mr));
		}

		[TestMethod]
		public void Test5()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2},
				{3, 4},
				{5, 6},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{7, 8},
				{9, 10},
				{11, 12},
			}); 
			var m = new Math.Matrix(new float[,]
			{
				{8, 10},
				{12, 14},
				{16, 18},
			}); 
			Assert.IsTrue(m == (ml + mr));
		}

		[TestMethod]
		public void Test6()
		{
			var ml = new Math.Matrix(new float[,]
			{
				{1, 2},
				{3, 4},
				{5, 6},
				{5, 6},
			});
			var mr = new Math.Matrix(new float[,]
			{
				{7, 8},
				{9, 10},
				{11, 12},
			});
			Assert.ThrowsException<ArgumentException>(() => ml + mr);
		}
	}
}