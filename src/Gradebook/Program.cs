using System;
using System.Collections.Generic;

namespace Gradebook
{

    class Program
    {
        static void Main(string[] args)
        {

            var book = new InMemoryBook("Scott's grade book");

            // calling the action on delegate
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.getStatistics();

            Console.WriteLine($"The average result is {stats.average,1}");
            Console.WriteLine($"The highest grade is {stats.high}");
            Console.WriteLine($"The lowest grade is {stats.low}");
            Console.WriteLine($"The letter grade is {stats.letter}");

        }

        private static void EnterGrades(InMemoryBook book)
        {
            while (true)
            {
                Console.Write("Enter grade or 'q' to quit: ");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    Console.WriteLine("Finally is running");
                }

            }
        }

        //action on delegate GradeAdded
        static void OnGradeAdded(object sender, EventArgs e){
            Console.WriteLine("A grade was added");
        }
    }
} 