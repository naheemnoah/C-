using System;
using System.Collections.Generic;

namespace Gradebook
{

    class Program
    {
        static void Main(string[] args)
        {

            var book = new Book("Scott's grade book");

            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);

            var stats = book.getStatistics();

            Console.WriteLine($"The average result is {stats.average}");
            Console.WriteLine($"The highest grade is {stats.high}");
            Console.WriteLine($"The lowest grade is {stats.low}");

        }
    }
}