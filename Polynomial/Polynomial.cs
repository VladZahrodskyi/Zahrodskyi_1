using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForPolynomial
{
	public class DegreeNotEqueltoLenghtException : ApplicationException
	{
		public DegreeNotEqueltoLenghtException() : base()
		{ }

		public DegreeNotEqueltoLenghtException(string message) : base(message)
		{ }

		public DegreeNotEqueltoLenghtException(string message, Exception innerException) : base(message, innerException)
		{ }
		
	}
	public class CanNotComparePolynomialException : ApplicationException
	{
		public CanNotComparePolynomialException() : base()
		{ }

		public CanNotComparePolynomialException(string message) : base(message)
		{ }

		public CanNotComparePolynomialException(string message, Exception innerException) : base(message, innerException)
		{ }

	}
	public class InvalidCastException : ApplicationException
	{
		public InvalidCastException() : base()
		{ }

		public InvalidCastException(string message) : base(message)
		{ }

		public InvalidCastException(string message, Exception innerException) : base(message, innerException)
		{ }

	}
	public class ZeroDegreeException : ApplicationException
	{
		public ZeroDegreeException() : base()
		{ }

		public ZeroDegreeException(string message) : base(message)
		{ }

		public ZeroDegreeException(string message, Exception innerException) : base(message, innerException)
		{ }

	}
	public class DegreeIsChangedException : ApplicationException
	{
		public DegreeIsChangedException() : base()
		{ }

		public DegreeIsChangedException(string message) : base(message)
		{ }

		public DegreeIsChangedException(string message, Exception innerException) : base(message, innerException)
		{ }

	}
	public class Polynomial : IComparable<Polynomial>
	{
		//bool minusDegree = false;
		private double[] _coefficientsOfPoliomial;
		int _hideDegree;
		bool isDegreeZero = false;

		public Polynomial()
		{ }

		public Polynomial(double[] coef)
		{
				DegreeOfPolinomial = coef.Length;
				this._coefficientsOfPoliomial = coef;
		}

		public double[] CoefficientOfPoliomial
		{
			get
			{
				return _coefficientsOfPoliomial;
			}
			protected set
			{
				_coefficientsOfPoliomial = value;
			}
		}

		public int DegreeOfPolinomial
		{
			get
			{
				return _hideDegree;
			}
			protected set
			{
				if (value == 0)
				{
					isDegreeZero = true;

					
				}

				_hideDegree = value;
			}
		}

		public double this[int index]//indexer
		{
			get { return _coefficientsOfPoliomial[index]; }
			protected set
			{
				_coefficientsOfPoliomial[index] = value;
			}

		}

		public void SetNewDegree(int degree, int coef)
		{
			if (degree == 0)
			{
				this[_coefficientsOfPoliomial.Length] = coef;
			}
			if (degree >= DegreeOfPolinomial)
			{
				Polynomial _newPolynomial = new Polynomial(new double[degree]);
				for (int i = 0; i < DegreeOfPolinomial; i++)
				{
					_newPolynomial[i] = this[i];
				}
				_newPolynomial[degree] = coef;
				DegreeOfPolinomial = degree;
				_coefficientsOfPoliomial = _newPolynomial._coefficientsOfPoliomial;
			}
			else { throw new DegreeIsChangedException("SetDegree method trying change value on lower"); }
		}
		
		public int CompareTo(Polynomial polynomial)
		{

			if (polynomial == null) { return 1; }
			if (DegreeOfPolinomial == polynomial.DegreeOfPolinomial)
			{
				if (CoefficientOfPoliomial != polynomial.CoefficientOfPoliomial)
				{
					bool comparator = false;//predict safaty of this part of code
					for (int i = 0; i < CoefficientOfPoliomial.Length; i++)
					{

						if (comparator == false)//safety
						{
							if (this[i] != polynomial[i])
							{
								comparator = true;//safety
								return 1;
							}
							else { }
						}
					}
				}
				else if (CoefficientOfPoliomial == polynomial.CoefficientOfPoliomial)
				{
					return 0;
				}

			}
			else if (DegreeOfPolinomial != polynomial.DegreeOfPolinomial)
			{
				return DegreeOfPolinomial.CompareTo(polynomial.DegreeOfPolinomial);
			}
			else
			{
				throw new CanNotComparePolynomialException();
			}
			return 1;
		}

		public int CompareTo(Object obj)
		{
			Polynomial polynomial;
			if (obj is Polynomial)
			{
				polynomial = obj as Polynomial;
			}
			else
			{
				throw new InvalidCastException("You have sent a member to Polynomial that cannot be cast.");
			}
			if (polynomial != null)
			{
				return CompareTo(polynomial);
			}
			else { return 1; }
		}
		
		public void ChangeCoefficient(int indexForChange, double substituteValueOfCoefficient)
		{
			this[indexForChange] = substituteValueOfCoefficient;
		}

		public override string ToString()
		{
			string stringPolynomial = "";
			string sign = "+";
			byte cheak = 0;
			for (int i = 0; i < CoefficientOfPoliomial.Length; i++)
			{
				if (this.DegreeOfPolinomial >= 0)
				{
					if (this[i] < 0)
					{
						stringPolynomial += $"{this[i].ToString()}x^{this.DegreeOfPolinomial - i}";
					}
					else if (i < CoefficientOfPoliomial.Length - 1 && this[i] != 0)
					{
						stringPolynomial += $"{sign}{this[i].ToString()}x^{this.DegreeOfPolinomial - i}";
					}
					else if (i == CoefficientOfPoliomial.Length - 1 && this[i] != 0)
					{
						stringPolynomial += $"{sign}{this[i].ToString()}x";
					}
				}
				else
				{
					if (isDegreeZero == true)
					{
						if (this[i] > 0)
						{
							stringPolynomial += $"{sign}{this[i].ToString()}";
						}
						else if (this[i] < 0)
						{
							sign = "-";
							stringPolynomial += $"{sign}{this[i].ToString()}";
						}
						else { stringPolynomial = "0"; }

					}
					else
					{
						i--;
						cheak++;
						if (cheak > 2)
						{
							throw new ZeroDegreeException("It is invalid  convert to string, Because Degree could not be recognized ");
						}
					}
				}

				cheak = 0;
				sign = "+";
			}
			return stringPolynomial;
		}
		public static bool CompareDegreeAndCoef(Polynomial polynom1, Polynomial polynom2)
		{
			if (polynom1.CoefficientOfPoliomial == polynom2.CoefficientOfPoliomial && polynom1.DegreeOfPolinomial == polynom2.DegreeOfPolinomial)
				return true;
			else
				return false;
		}
		public static Polynomial operator +(Polynomial polyn1, Polynomial polyn2)
		{
			Polynomial _newPolynomial = default(Polynomial);
			if (polyn1.CoefficientOfPoliomial.Length == polyn2.CoefficientOfPoliomial.Length)
			{
				_newPolynomial = new Polynomial(new double[polyn1.CoefficientOfPoliomial.Length]);
				for (int i = 0; i < polyn1.CoefficientOfPoliomial.Length; i++)
				{
					_newPolynomial[i] = polyn1[i] + polyn2[i];
				}
				return _newPolynomial;
			}
			else if (polyn1.CoefficientOfPoliomial != polyn2.CoefficientOfPoliomial)
			{

				if (polyn1.DegreeOfPolinomial > polyn2.DegreeOfPolinomial)
				{
					return GetPolynomial(polyn1, polyn2);//polyn1>polyn2
				}
				else if (polyn1.DegreeOfPolinomial < polyn2.DegreeOfPolinomial)
				{
					return GetPolynomial(polyn2, polyn1);//polyn1<polyn2
				}
				else { throw new DegreeIsChangedException(""); }


				Polynomial GetPolynomial(Polynomial inst1, Polynomial inst2)//First argument always must have bigger Coefficient and Degree
				{
					_newPolynomial = new Polynomial(new double[inst1.CoefficientOfPoliomial.Length]);
					for (int i = 0; i < inst1.CoefficientOfPoliomial.Length; i++)
					{
						if (i > inst1.DegreeOfPolinomial - 1)
						{
							_newPolynomial[i] = inst1[i] + inst2[i];
						}
						else
						{
							_newPolynomial[i] = inst1[i];
						}

					}
					return _newPolynomial;
				}
			}
			else
			{
				throw new DegreeNotEqueltoLenghtException("corrupted operator + cause Degree is different, but coefficients is the same length");
			}
		}
		public static Polynomial operator *(Polynomial polyn1, Polynomial polyn2)
		{
			Polynomial _newPolynomial = default(Polynomial);
			if (CompareDegreeAndCoef(polyn1, polyn2)|| polyn1.DegreeOfPolinomial != polyn2.DegreeOfPolinomial && polyn1.CoefficientOfPoliomial != polyn2.CoefficientOfPoliomial)
			{
				_newPolynomial = new Polynomial(new double[(polyn1.DegreeOfPolinomial + polyn2.DegreeOfPolinomial)-2]);
				for (int i = 0; i < polyn1.DegreeOfPolinomial; i++)
				{
					for (int index = 0; index < polyn2.DegreeOfPolinomial; index++)
					{
							_newPolynomial[i+index] += polyn1[i] * polyn2[index];
					}
					
				}
				return _newPolynomial;
			}
			else
			{
				throw new DegreeNotEqueltoLenghtException("corrupted operator * cause Degree is different, but coefficients is the same length");
			}
		}
		public static Polynomial operator -(Polynomial polyn1, Polynomial polyn2)
		{
			Polynomial _newPolynomial = default(Polynomial);
			if ( polyn1.CoefficientOfPoliomial.Length == polyn2.CoefficientOfPoliomial.Length)
			{
				_newPolynomial = new Polynomial(new double[polyn1.CoefficientOfPoliomial.Length]);
				for (int i = 0; i < polyn1.CoefficientOfPoliomial.Length; i++)
				{
					_newPolynomial[i] = polyn1[i] - polyn2[i];
				}
				return _newPolynomial;
			}
			else if (polyn1.CoefficientOfPoliomial != polyn2.CoefficientOfPoliomial)
			{
				int _lenghtForPolynomial, _degreeForPolynomial;
				
				if (polyn1.DegreeOfPolinomial > polyn2.DegreeOfPolinomial)
				{
					_lenghtForPolynomial = polyn1.CoefficientOfPoliomial.Length;//polyn1>polyn2
					_degreeForPolynomial = polyn1.DegreeOfPolinomial;


				}
				else if (polyn1.DegreeOfPolinomial < polyn2.DegreeOfPolinomial)
				{
					_lenghtForPolynomial = polyn2.CoefficientOfPoliomial.Length;//polyn1<polyn2
					_degreeForPolynomial = polyn2.DegreeOfPolinomial;

				}
				else { throw new ApplicationException(""); }
				return GetPolynomial(polyn1, polyn2, _lenghtForPolynomial, _degreeForPolynomial);

				Polynomial GetPolynomial(Polynomial inst1, Polynomial inst2, int _lenth, int _degree)//First argument NOT always must have bigger Coefficient and Degree
				{
					_newPolynomial = new Polynomial( new double[_lenth]);
					for (int i = 0; i < _lenth; i++)
					{
						if (i > inst1.DegreeOfPolinomial - 1)
						{
							_newPolynomial[i] = inst1[i] - inst2[i];
						}
						else
						{
							_newPolynomial[i] = inst1[i];
						}

					}
					return _newPolynomial;
				}

			}
			else
			{
				throw new ApplicationException("corrupted operator - cause Degree is different, but coefficients is the same length");
			}



		}
		
	}
}
