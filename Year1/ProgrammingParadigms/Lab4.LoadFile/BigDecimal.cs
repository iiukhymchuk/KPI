/*
 * Is taken from
 * https://github.com/Limeoats/BigDecimal/blob/master/BigDecimal/BigDecimal.cs
 */

using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Lab4.LoadFile
{
    public class BigDecimal : IComparable, IComparable<BigDecimal>, IEquatable<BigDecimal>
    {
        public BigInteger Numerator { get; private set; }
        public BigInteger Denominator { get; private set; }

        public string StringValue => ToString(10);

        public static readonly BigDecimal Zero = new BigDecimal(0);
        public static readonly BigDecimal One = new BigDecimal(1);
        public static readonly BigDecimal Two = new BigDecimal(2);
        public static readonly BigDecimal Three = new BigDecimal(3);
        public static readonly BigDecimal Epsilon =
            new BigDecimal("0.000000000000000000000000000000000000000000000000000" +
                           "00000000000000000000000000000000000000000000000001");

        public bool IsPositive()
        {
            if (Numerator.Sign == 0 && Denominator.Sign == 0) return false;
            if (Numerator.Sign == 1 && Denominator.Sign == 1) return true;
            if (Numerator.Sign == -1 && Denominator.Sign == -1) return true;
            if (Numerator.Sign == 1 && Denominator.Sign == -1) return false;
            return !(Numerator.Sign == -1 && Denominator.Sign == 1);
        }

        public bool IsNegative()
        {
            return !IsPositive();
        }

        public BigDecimal()
        {
            Numerator = 0;
            Denominator = 0;
        }
        public BigDecimal(BigDecimal bd)
        {
            Numerator = bd.Numerator;
            Denominator = bd.Denominator;
        }
        public BigDecimal(string value)
        {
            var bd = Parse(value);
            Numerator = bd.Numerator;
            Denominator = bd.Denominator;
        }
        public BigDecimal(BigInteger value)
        {
            Numerator = value;
            Denominator = 1;
        }
        public BigDecimal(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0) throw new ArgumentException("Denominator cannot be 0", nameof(denominator));
            Numerator = numerator;
            Denominator = denominator;
        }
        public BigDecimal(int value)
        {
            Numerator = value;
            Denominator = 1;
        }
        public BigDecimal(long value)
        {
            Numerator = value;
            Denominator = 1;
        }
        public BigDecimal(double value)
        {
            var bd = new BigDecimal(value.ToString());
            Numerator = bd.Numerator;
            Denominator = bd.Denominator;
        }
        public BigDecimal(float value)
        {
            var bd = new BigDecimal(value.ToString());
            Numerator = bd.Numerator;
            Denominator = bd.Denominator;
        }
        public BigDecimal(decimal value)
        {
            var bd = new BigDecimal(value.ToString());
            Numerator = bd.Numerator;
            Denominator = bd.Denominator;
        }

        public static BigDecimal Add(BigDecimal b1, BigDecimal b2)
        {
            if (Equals(b2, null))
                throw new ArgumentNullException(nameof(b2));
            var n1 = b1.Numerator;
            var n2 = b2.Numerator;
            var d1 = b1.Denominator;
            var d2 = b2.Denominator;

            var lcm = (d1 * d2) /
                      BigInteger.GreatestCommonDivisor(d1, d2);
            n1 *= lcm / d1;
            n2 *= lcm / d2;
            d2 = lcm;
            n1 += n2;
            d1 = lcm;
            return new BigDecimal(n1, d1);
        }

        public static BigDecimal Subtract(BigDecimal b1, BigDecimal b2)
        {
            if (BigDecimal.Equals(b2, null))
                throw new ArgumentNullException(nameof(b2));
            var bd1 = new BigDecimal(b1);
            var bd2 = new BigDecimal(b2);
            var tmp = BigDecimal.Negate(bd2);
            return Add(bd1, tmp);
        }

        public static BigDecimal Multiply(BigDecimal b1, BigDecimal b2)
        {
            if (Equals(b2, null))
                throw new ArgumentNullException(nameof(b2));
            var bd1 = new BigDecimal(b1);
            var bd2 = new BigDecimal(b2);
            bd1.Numerator *= bd2.Numerator;
            bd1.Denominator *= bd2.Denominator;
            return new BigDecimal(bd1);
        }

        public static BigDecimal Divide(BigDecimal b1, BigDecimal b2)
        {
            if (Equals(b2, null))
                throw new ArgumentNullException(nameof(b2));
            var bd1 = new BigDecimal(b1);
            var bd2 = new BigDecimal(b2);
            var tmp1 = bd2.Numerator;
            var tmp2 = bd2.Denominator;
            bd2.Numerator = tmp2;
            bd2.Denominator = tmp1;
            var x = bd1 * bd2;
            return new BigDecimal(x);
        }

        public static BigDecimal Abs(BigDecimal n)
        {
            var bd1 = new BigDecimal(n);
            bd1.Numerator = BigInteger.Abs(bd1.Numerator);
            bd1.Denominator = BigInteger.Abs(bd1.Denominator);
            return new BigDecimal(bd1);
        }

        public static BigDecimal Sqrt(BigDecimal n)
        {
            var sqrt = n / BigDecimal.Two;
            BigDecimal last;
            do
            {
                last = sqrt;
                sqrt = (last + n / last) / BigDecimal.Two;
            } while (!CloseTo(last - sqrt, Epsilon));
            return sqrt;
        }

        public static BigDecimal Negate(BigDecimal n)
        {
            var bd1 = n;
            bd1.Numerator *= -1;
            return new BigDecimal(bd1);
        }

        public static bool CloseTo(BigDecimal a, BigDecimal b)
        {
            return Abs(a - b) <= Epsilon;
        }

        private static BigDecimal Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            value = value.Trim();
            var decimalPos = value.IndexOf('.');
            value = value.Replace(".", "");
            if (decimalPos == -1)
            {
                var n = BigInteger.Parse(value);
                return new BigDecimal(n);
            }
            var numerator = BigInteger.Parse(value);
            var denominator = BigInteger.Pow(10, value.Length - decimalPos);
            return new BigDecimal(numerator, denominator).Reduce();
        }

        public static bool TryParse(string value, out BigDecimal result)
        {
            result = null;
            try
            {
                result = Parse(value);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private BigDecimal Reduce()
        {
            if (Denominator == 1) return this;
            var factor = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
            Numerator /= factor;
            Denominator /= factor;
            return this;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (!(obj is BigDecimal))
                throw new ArgumentException(nameof(obj) + " is not a BigDecimal");
            return CompareTo((BigDecimal)obj);
        }

        public int CompareTo(BigDecimal other)
        {
            if (BigDecimal.Equals(other, null))
                throw new ArgumentNullException(nameof(other));
            var num = Numerator;
            var den = Denominator;

            //Cross multiply
            num *= other.Denominator;
            den *= other.Numerator;

            return BigInteger.Compare(num, den);
        }

        public bool Equals(BigDecimal other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            return (other.Numerator == Numerator && other.Denominator == Denominator);
        }

        public override bool Equals(object other)
        {
            if (other == null || GetType() != other.GetType())
                return false;
            return (((BigDecimal)other).Numerator) == Numerator && ((BigDecimal)other).Denominator == Denominator;
        }

        public string ToString(int precision, bool trailingZeros = false)
        {
            Reduce();
            BigInteger remainder;
            var result = BigInteger.DivRem(Numerator, Denominator, out remainder);
            if (remainder == 0 && trailingZeros)
            {
                var x = result.ToString();
                x += '.';
                for (var a = 0; a < precision; ++a)
                    x += "0";
                return x;
            }
            else if (remainder == 0)
                return result.ToString();

            var decimals = (Numerator * BigInteger.Pow(10, precision)) / Denominator;
            if (decimals == 0 && trailingZeros)
            {
                return result + ".0";
            }
            else if (decimals == 0)
                return result.ToString();

            var sb = new StringBuilder();
            while (precision-- > 0 && decimals > 0)
            {
                sb.Append(decimals % 10);
                decimals /= 10;
            }
            return string.Format("{0}.{1}", result, trailingZeros ?
                new string(sb.ToString().Reverse().ToArray()) :
                new string(sb.ToString().Reverse().ToArray()).TrimEnd('0'));
        }

        public new static bool Equals(object left, object right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;
            return left.GetType() == right.GetType() && (((BigDecimal)left).Equals((BigDecimal)right));
        }

        public static int Compare(BigDecimal left, BigDecimal right)
        {
            if (BigDecimal.Equals(left, null))
                throw new ArgumentNullException(nameof(left));
            if (BigDecimal.Equals(right, null))
                throw new ArgumentNullException(nameof(right));
            return left.CompareTo(right);
        }

        public static string ToString(BigDecimal value)
        {
            return value.ToString();
        }

        //Operators
        public static bool operator ==(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) == 0;
        }
        public static bool operator ==(BigDecimal left, dynamic right)
        {
            return Compare(left, new BigDecimal(right)) == 0;
        }
        public static bool operator !=(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) != 0;
        }

        public static bool operator !=(BigDecimal left, dynamic right)
        {
            return Compare(left, new BigDecimal(right)) != 0;
        }
        public static bool operator >(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) > 0;
        }
        public static bool operator <(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) < 0;
        }
        public static bool operator >=(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) >= 0;
        }
        public static bool operator <=(BigDecimal left, BigDecimal right)
        {
            return Compare(left, right) <= 0;
        }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            return Add(new BigDecimal(left), right);
        }
        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            return Subtract(new BigDecimal(left), right);
        }
        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            return Multiply(new BigDecimal(left), right);
        }
        public static BigDecimal operator /(BigDecimal left, BigDecimal right)
        {
            return Divide(new BigDecimal(left), right);
        }
        public static BigDecimal operator -(BigDecimal value)
        {
            return BigDecimal.Negate(new BigDecimal(value));
        }
    }
}