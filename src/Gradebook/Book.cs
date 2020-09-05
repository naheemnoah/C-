using System.Collections.Generic;
using System;
namespace Gradebook
{

    // declaring a delegate for an event
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    public class Book
    {
        private List<double> grades;
        private string name;

        // class property
        public string Name {
            get{
                return name;
            }set {
                if(!String.IsNullOrEmpty(value)){
                    name = value;
                } else {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }
            }
        }

        // declaring the delegate event
        public event GradeAddedDelegate GradeAdded;

        // constructor
        public Book(string name)
        {
            grades = new List<double>();
            this.name = name;
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        public void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0){
                grades.Add(grade);
                if(GradeAdded != null){
                    // invoke GradeAdded delegate. this = sender
                    // action performed in program.cs
                    GradeAdded(this, new EventArgs());
                }
            }
            else {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            
        }

    

        public Statistics getStatistics()
        {
            var result = new Statistics();
            result.average = 0.0;
            result.high = double.MinValue;
            result.low = double.MaxValue;

            for (var index = 0; index < grades.Count; index++)
            {
                if (grades[index] > result.high)
                {
                    result.high = grades[index];
                }
                else if (grades[index] < result.low)
                {
                    result.low = grades[index];
                }
                result.average += grades[index];
            }

            result.average /= grades.Count;

            switch(result.average){
                case var d when d >= 90:
                    result.letter = 'A';
                    break;

                case var d when d >= 80:
                    result.letter = 'B';
                    break;

                case var d when d >= 70:
                    result.letter = 'C';
                    break;

                case var d when d >= 60:
                    result.letter = 'D';
                    break;

                default:
                    result.letter = 'F';
                    break;
            }
            return result;
        }

    }
}