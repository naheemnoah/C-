using System;

namespace Gradebook
{
    public class Statistics
    {
        public double average
        {
            get
            {
                return sum / count;
            }
        }
        public double high;
        public double low;
        public char letter
        {
            get
            {
                switch (average)
                {
                    case var d when d >= 90:
                        return 'A';

                    case var d when d >= 80:
                        return 'B';

                    case var d when d >= 70:
                        return 'C';

                    case var d when d >= 60:
                        return 'D';

                    default:
                        return 'F';
                }
            }
        }

        public double sum;
        public int count;


        public void Add(double number)
        {
            sum += number;
            count += 1;
            low = Math.Min(number, low);
            high = Math.Min(number, high);
        }

        public Statistics()
        {
            count = 0;
            high = double.MinValue;
            low = double.MaxValue;
            sum = 0.0;
        }
    }
}