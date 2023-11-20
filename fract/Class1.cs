using System.Globalization;

namespace fract
{
    public class Fract
    {
        public int numerator { get; set; }
        public int denominator { get; set; }
        public int integer { get; set; }

        public void RoundOff()
        {
            var tmp = numerator / denominator;
            var remains = numerator % denominator;
            numerator = remains;
            integer += tmp;
        }

        public void RoundOn()
        {
            var tmp = integer * denominator;
            integer = 0;
            numerator += tmp;
        }

        public static Fract operator *(Fract first, Fract second)
        {
            return Multiply(first, second);
        }
        public static Fract operator /(Fract first, Fract second)
        {
            return Divide(first, second);
        }


        public static Fract operator *(Fract fract, int value)
        {
            return Multiply(fract, new Fract(value, 1));
        }

        public static Fract operator /(Fract fract, int value)
        {
            return Divide(fract, new Fract(value, 1));
        }
        public static Fract operator -(Fract first, Fract second)
        {
            return minus(first, second);
        }
        public static Fract operator -(Fract fract, int value)
        {

            return minus(fract, new Fract(value, 1));
        }
        public static Fract operator +(Fract fract, int value)
        {

            return plus(fract, new Fract(value, 1));
        }


        private static Fract BringToDenominator(Fract fract, int NOK)
        {
            var tmp = fract;
            tmp.numerator *= NOK / fract.denominator;
            tmp.denominator = NOK;
            return tmp;
        }

        public static bool operator ==(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int NOK = FindCommunismDenominator(first, second);
            first = BringToDenominator(first, NOK);
            second = BringToDenominator(second, NOK);
            return first.numerator == second.numerator;
        }

        public static bool operator !=(Fract first, Fract second)
        {
            return !(first == second);
        }
        public static bool operator <=(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int NOK = FindCommunismDenominator(first, second);
            first = BringToDenominator(first, NOK);
            second = BringToDenominator(second, NOK);
            return first.numerator <= second.numerator;
        }

        public static bool operator >=(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int NOK = FindCommunismDenominator(first, second);
            first = BringToDenominator(first, NOK);
            second = BringToDenominator(second, NOK);
            return first.numerator >= second.numerator;
        }

        public static bool operator <(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int NOK = FindCommunismDenominator(first, second);
            first = BringToDenominator(first, NOK);
            second = BringToDenominator(second, NOK);
            return first.numerator < second.numerator;
        }

        public static bool operator >(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int NOK = FindCommunismDenominator(first, second);
            first = BringToDenominator(first, NOK);
            second = BringToDenominator(second, NOK);
            return first.numerator >= second.numerator;
        }















        private static Fract plus(Fract first, Fract second)
        {
            int NOK = FindCommunismDenominator(first, second);
            first.numerator *= NOK / first.denominator;
            second.numerator *= NOK / second.denominator;
            first.denominator = NOK;
            second.denominator = NOK;
            return new Fract(first.numerator + second.numerator, NOK);
        }
        private static Fract minus(Fract first, Fract second)
        {
            int NOK = FindCommunismDenominator(first, second);
            first.numerator *= NOK / first.denominator;
            second.numerator *= NOK / second.denominator;
            first.denominator = NOK;
            second.denominator = NOK;
            return new Fract(first.numerator - second.numerator, NOK);
        }









        public static Fract operator +(Fract first, Fract second)
        {
            return plus(first, second);
        }

        //public static Fract operator -() 
        //{

        //}



        private static Fract Multiply(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            int numerator = first.numerator * second.numerator;
            int denominator = first.denominator * second.denominator;
            var fract = new Fract(numerator, denominator);
            return fract;
        }

        private static Fract Divide(Fract first, Fract second)
        {
            first.RoundOn();
            second.RoundOn();
            return new Fract(first.numerator * second.denominator, first.denominator * second.numerator);
        }



        public Fract Simplify()
        {
            this.RoundOn();
            var smallest = 0;

            int? simp = null;
            if (this.numerator > this.denominator) smallest = denominator;
            else smallest = this.numerator;



            for (int i = 1; i < smallest + 1; i++)
            {
                if (this.denominator % i == 0 && this.numerator % i == 0) simp = i;
            }

            if (simp != null)
            {
                this.numerator /= simp.Value;
                this.denominator /= simp.Value;
            }

            return this;
        }

        private static int FindCommunismDenominator(Fract first, Fract second)
        {
            int smallest = 0;

            if (first.denominator > second.denominator) smallest = second.denominator;
            else smallest = first.denominator;
            for (int i = smallest; i > smallest - 1; i++)
            {
                if (i % first.denominator == 0 && i % second.denominator == 0) return i;
            }
            return 0;
        }

        public override string ToString()
        {
            string str = this.integer.ToString() + " " + this.numerator + "/" + this.denominator;
            return str;
        }



        public double normal_value
        {
            get
            {
                double tmp = numerator + (denominator * integer);
                double res = tmp / (double)denominator;
                return res;
            }
        }

        public static implicit operator string(Fract fract)
        {
            string str = fract.integer.ToString() + " " + fract.numerator + "/" + fract.denominator;
            return str;
        }

        private Fract() { }
        public Fract(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            this.integer = 0;
        }
        public Fract(int numerator, int determinator, int integer)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            this.integer = integer;
        }

    }






}