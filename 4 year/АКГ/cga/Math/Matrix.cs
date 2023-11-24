namespace Math
{
	public sealed class Matrix
	{
		public float[,] Rows { get; private set; }
		public int NumOfRows { get; private set; }
		public int NumOfCols { get; private set; }

		public Matrix(float[,] rows)
		{
			NumOfRows = rows.GetLength(0);
			if (NumOfRows <= 0)
				throw new ArgumentException();
			NumOfCols = rows.GetLength(1);
			for (var i = 1; i < NumOfRows; i++)
				if (rows.GetLength(1) != NumOfCols)
					throw new ArgumentException();
			Rows = rows;
		}

		public Matrix Transpose()
		{
			var c = NumOfRows;
			var t = new float[NumOfCols, NumOfRows];
			for (var j = 0; j < NumOfCols; j++)
				for (var i = 0; i < NumOfRows; i++)
					t[j, i] = Rows[i, j];
			Rows = t;
			NumOfRows = NumOfCols;
			NumOfCols = c;
			return this;
		}

		public Matrix Dot(Matrix m)
		{
			if (NumOfCols != m.NumOfRows)
				throw new ArgumentException();
			var dotProduct = new float[NumOfRows, m.NumOfCols];
			for (var crow = 0; crow < NumOfRows; crow++)
				for (var ocol = 0; ocol < m.NumOfCols; ocol++)
					for (var ccol = 0; ccol < NumOfCols; ccol++)
						dotProduct[crow, ocol] += Rows[crow, ccol] * m.Rows[ccol, ocol];
			return new Matrix(dotProduct);
		}

		public Matrix Add(Matrix m)
		{
			if (NumOfCols != m.NumOfCols ||
				NumOfRows != m.NumOfRows)
				throw new ArgumentException();
			for (var row = 0; row < NumOfRows; row++)
				for (var col = 0; col < NumOfCols; col++)
					Rows[row, col] += m.Rows[row, col];
			return this;
		}

		public Matrix Substract(Matrix m)
		{
			if (NumOfCols != m.NumOfCols ||
				NumOfRows != m.NumOfRows)
				throw new ArgumentException();
			for (var row = 0; row < NumOfRows; row++)
				for (var col = 0; col < NumOfCols; col++)
					Rows[row, col] -= m.Rows[row, col];
			return this;
		}

		public Matrix Multiply(float scalar)
		{
			for (var row = 0; row < NumOfRows; row++)
				for (var col = 0; col < NumOfCols; col++)
					Rows[row, col] *= scalar;
			return this;
		}

		public Matrix Multiply(Matrix m)
		{
			if (NumOfCols != m.NumOfCols ||
				NumOfRows != m.NumOfRows)
				throw new ArgumentException();
			for (var row = 0; row < NumOfRows; row++)
				for (var col = 0; col < NumOfCols; col++)
					Rows[row, col] *= m.Rows[row, col];
			return this;
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is Matrix) || obj == null)
				return false;
			var m = obj as Matrix;
			if (NumOfCols != m!.NumOfCols ||
				NumOfRows != m!.NumOfRows)
				return false;
			for (var row = 0; row < NumOfRows; row++)
				for (var col = 0; col < NumOfCols; col++)
					if (Rows[row, col] != m.Rows[row, col])
						return false;
			return true;
		}

		public static Matrix operator ~(Matrix m) => m.Transpose();
		public static Matrix operator %(Matrix ml, Matrix mr) => ml.Dot(mr);
		public static Matrix operator +(Matrix ml, Matrix mr) => ml.Add(mr);
		public static Matrix operator -(Matrix ml, Matrix mr) => ml.Substract(mr);
		public static Matrix operator *(Matrix ml, Matrix mr) => ml.Multiply(mr);
		public static Matrix operator *(int scalar, Matrix m) => m.Multiply((float)scalar);
		public static Matrix operator *(float scalar, Matrix m) => m.Multiply(scalar);
		public static bool operator ==(Matrix ml, Matrix mr) => ml.Equals(mr);
		public static bool operator !=(Matrix ml, Matrix mr) => !ml.Equals(mr);
	}
}