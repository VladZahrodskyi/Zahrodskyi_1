using System;
using System.Runtime.Serialization;

namespace MatrixBuild
{
	public class OutOfRangOfMAtrixException : ApplicationException
	{
		public OutOfRangOfMAtrixException():base()
		{ }
		
		public OutOfRangOfMAtrixException(string message) : base(message)
		{ }

		public OutOfRangOfMAtrixException(string message, Exception innerException) : base(message, innerException)
		{ }
		protected OutOfRangOfMAtrixException(SerializationInfo info, StreamingContext context):base(info, context)
		{ }
	}
	public class NotSquaredMatrix : ApplicationException
	{
		public NotSquaredMatrix() : base()
		{ }

		public NotSquaredMatrix(string message) : base(message)
		{ }

		public NotSquaredMatrix(string message, Exception innerException) : base(message, innerException)
		{ }
		protected NotSquaredMatrix(SerializationInfo info, StreamingContext context) : base(info, context)
		{ }
	}

		public class NotTheSameDimensionsMatrixs : ApplicationException
	{
		public NotTheSameDimensionsMatrixs() : base()
		{ }

		public NotTheSameDimensionsMatrixs(string message) : base(message)
		{ }

		public NotTheSameDimensionsMatrixs(string message, Exception innerException) : base(message, innerException)
		{ }
		protected NotTheSameDimensionsMatrixs(SerializationInfo info, StreamingContext context) : base(info, context)
		{ }
	}
	public class ColumnsNotEquelRows : ApplicationException
	{
		public ColumnsNotEquelRows() : base()
		{ }

		public ColumnsNotEquelRows(string message) : base(message)
		{ }

		public ColumnsNotEquelRows(string message, Exception innerException) : base(message, innerException)
		{ }
		protected ColumnsNotEquelRows(SerializationInfo info, StreamingContext context) : base(info, context)
		{ }
	}
	
	public class Matrix
	{
		private double[,] _arrayForMatrix;

		public int Rows { get; private set; }//definition only in ctor
		public int Colums { get; private set; }//definition only in ctor

		public Matrix() : this(5, 5)//goto main matrix ctor
		{

		}

		public Matrix(int rows) : this(rows, 0)//vector matrix ctor
		{

		}

		public Matrix(int rows, int colums)//main matrix ctor
		{
			Colums = colums;
			Rows = rows;
			_arrayForMatrix = new double[rows, colums];
		}

		public Matrix(Matrix matrix)
		{
			this.Colums = matrix.Colums;
			this.Rows = matrix.Rows;
			this._arrayForMatrix = matrix._arrayForMatrix;
		}

		public Matrix(int[] array) : this( 0 , array.Length)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._arrayForMatrix[0,i] = array[i];
			}
		}

		public Matrix(int[,] array) : this((int)(array.LongLength/array.Length), array.Length)
		{ 
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < (int)(array.LongLength / array.Length); j++)
				{
					this._arrayForMatrix[j, i] = array[j,i];
				}
				
			}
		}

		public double this[int rows, int colums]
		{
			get
			{
				if (rows < 0 || rows > Rows)
				{
					throw new OutOfRangOfMAtrixException("The Row is out of range!");
				}
				if (colums < 0 || colums > Colums)
				{
					throw new OutOfRangOfMAtrixException("The Colum is out of range!");
				}
				return _arrayForMatrix[rows, colums];
			}
			set { _arrayForMatrix[rows, colums] = value; }
		}

		public double[,] GetDoubleArray()
		{
			return _arrayForMatrix;
		}

		public override string ToString()
		{
			string allMatrix = "(";
			for (int i = 0; i < Rows; i++)
			{
				string strokaForMatrix = "";
				for (int j = 0; j < Colums-1; j++)
				{
					strokaForMatrix += _arrayForMatrix[i, j].ToString() + ", ";
				}
				strokaForMatrix += _arrayForMatrix[i, Colums - 1].ToString();
				if (i != Rows - 1 && i == 0)
				{
					allMatrix += strokaForMatrix + "\n";
				}
				else if (i != Rows - 1 && i != 0)
				{
					allMatrix += " " + strokaForMatrix + "\n";
				}
				else
				{
					allMatrix += " " + strokaForMatrix + ")";
				}//end of If-case
			}
			return allMatrix;
		}

		public override bool Equals(object obj)
		{
			return (obj is Matrix)&&this.Equals((Matrix)obj);//call another bool Equals after check if it is comparable type
		}

		public bool Equals(Matrix matrix)//main Equal method
		{
			bool answerEqualOrNot = true;
			if (Rows == matrix.Rows && Colums == matrix.Colums)
			{
				for (int i = 0; i < Rows-1; i++)
				{
					for (int j = 0; j < Colums-1; j++)
					{
						if (answerEqualOrNot == true)
						{
							answerEqualOrNot = _arrayForMatrix[i, j] == matrix._arrayForMatrix[i, j];
						}
						else
						{
							i = Rows - 1;
							j = Colums - 1;
						}
					}
				}
			}
			return answerEqualOrNot;
		}

		public override int GetHashCode()
		{
			return _arrayForMatrix.GetHashCode();
		}

		public bool IsSquared()
		{
			if (Rows == Colums)
				return true;
			else
				return false;
		}

		public static bool CompareDimension(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.Rows == matrix2.Rows && matrix1.Colums == matrix2.Colums)
				return true;
			else
				return false;
		}

		public static double Determinant(Matrix matrix)
		{
			double result = 0.0d;
			if (!matrix.IsSquared())
			{
				throw new NotSquaredMatrix("The matrix must be squared!");
			}
			if (matrix.Rows == 1)
				result = matrix[0, 0];
			else
			{
				for (int i = 0; i < matrix.Rows; i++)
				{
					result += Math.Pow(-1, i) * matrix[0, i] * Determinant(Matrix.Minor(matrix, 0, i));
				}
			}
			return result;
		}

		public static Matrix Minor(Matrix matrix, int row, int colums)
		{
			Matrix _newmatrix = new Matrix(matrix.Rows - 1, matrix.Colums - 1);
			int ii = 0, jj = 0;
			for (int i = 0; i < matrix.Rows; i++)
			{
				if (i == row)
					continue;
				jj = 0;
				for (int j = 0; j < matrix.Colums; j++)
				{
					if (j == colums)
						continue;
					_newmatrix[ii, jj] = matrix[i, j];
					jj++;
				}
				ii++;
			}
			return _newmatrix;
		}

		public static Matrix operator +(Matrix matrix1, Matrix matrix2)
		{
			if (!Matrix.CompareDimension(matrix1, matrix2))
			{
				throw new NotTheSameDimensionsMatrixs("The dimensions of two matrices must be the same!");
			}
			Matrix result = new Matrix(matrix1.Rows, matrix1.Colums);
			for (int i = 0; i < matrix1.Rows; i++)
			{
				for (int j = 0; j < matrix1.Colums; j++)
				{
					result[i, j] = matrix1[i, j] + matrix2[i, j];
				}
			}
			return result;
		}

		public static Matrix operator -(Matrix matrix)
		{
			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Colums; j++)
				{
					matrix[i, j] = -matrix[i, j];
				}
			}
			return matrix;
		}

		public static Matrix operator -(Matrix matrix1, Matrix matrix2)
		{
			if (!Matrix.CompareDimension(matrix1, matrix2))
			{
				throw new NotTheSameDimensionsMatrixs("The dimensions of two matrices must be the same!");
			}
			Matrix result = new Matrix(matrix1.Rows, matrix1.Colums);
			for (int i = 0; i < matrix1.Rows; i++)
			{
				for (int j = 0; j < matrix1.Colums; j++)
				{
					result[i, j] = matrix1[i, j] - matrix2[i, j];
				}
			}
			return result;
		}

		public static Matrix operator *(Matrix matrix, double multiplier)
		{
			Matrix result = new Matrix(matrix.Rows, matrix.Colums);
			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Colums; j++)
				{
					result[i, j] = matrix[i, j] * multiplier;
				}
			}
			return result;
		}
		
		public static Matrix operator *(double multiplier, Matrix matrix)
		{
			Matrix result = new Matrix(matrix.Rows, matrix.Colums);
			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Colums; j++)
				{
					result[i, j] = matrix[i, j] * multiplier;
				}
			}
			return result;
		}

		public static Matrix operator /(Matrix matrix, double divider)
		{
			Matrix result = new Matrix(matrix.Rows, matrix.Colums);
			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Colums; j++)
				{
					result[i, j] = matrix[i, j] / divider;
				}
			}
			return result;
		}

		public static Matrix operator /(double divider, Matrix matrix2)
		{
			Matrix result = new Matrix(matrix2.Rows, matrix2.Colums);
			for (int i = 0; i < matrix2.Rows; i++)
			{
				for (int j = 0; j < matrix2.Colums; j++)
				{
					result[i, j] = matrix2[i, j] / divider;
				}
			}
			return result;
		}

		public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.Colums != matrix2.Rows)
			{
				throw new ColumnsNotEquelRows("The numbers of columns of the" +
				 " first matrix must be equal to the number of " +
				 " rows of the second matrix!");
			}
			double tmp;
			Matrix result = new Matrix(matrix1.Rows, matrix2.Colums);
			for (int i = 0; i < matrix1.Rows; i++)
			{
				for (int j = 0; j < matrix2.Colums; j++)
				{
					tmp = result[i, j];
					for (int k = 0; k < result.Rows; k++)
					{
						tmp += matrix1[i, k] * matrix2[k, j];
					}
					result[i, j] = tmp;
				}
			}
			return result;
		}



	}
}
