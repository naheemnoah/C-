using System.Collections.Generic;
using System;
namespace Gradebook
{
    public class Book 
    {
        private List<double> grades;
        public string Name;
        public Book (string name) {
            grades = new List<double>();
            Name = name;
        }
        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public Statistics getStatistics(){
            var result = new Statistics();
            result.average = 0.0;
            result.high = double.MinValue;
            result.low = double.MaxValue;

            foreach(var grade in grades){
                if (grade > result.high) {
                    result.high = grade;
                }
                else if (grade < result.low){
                    result.low = grade;
                }
                result.average += grade;
            }

            result.average /= grades.Count;
            return result;
        }

    }
}