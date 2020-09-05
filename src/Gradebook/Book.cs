using System.Collections.Generic;
using System;
using System.IO;

namespace Gradebook
{

    // declaring a delegate for an event
    public delegate void GradeAddedDelegate(object sender, EventArgs args);
    
    
    public class NamedObject{

        // property
        public string Name{
            get;
            set;
        }

        // constructor
        public NamedObject(string name){
            Name = name;
        }
    }


    // interface
    public interface IBook {
        void AddGrade(double grade);
        Statistics GetStatistics();
        String Name {get;}
        event GradeAddedDelegate GradeAdded;

    }

    //abstract class
    public abstract class Book : NamedObject, IBook {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            // using IDisposable interface implicitly on Disposeable objects
            using(var writer = File.AppendText($"{Name}.txt")){
                writer.WriteLine(grade);
                if(GradeAdded != null){
                    GradeAdded(this, new EventArgs());
                }
            }
        
        }

        public override Statistics GetStatistics()
        {
           var result = new Statistics();
           using(var reader = File.OpenText($"{Name}.txt")){
               var line = reader.ReadLine();
               while(line != null){
                   var number = double.Parse(line);
                   result.Add(number);
                   line = reader.ReadLine();
               }
           }
           return result;
        }
    }

    // inheritance
    public class InMemoryBook : Book
    {
        private List<double> grades;

        // declaring the delegate event
        public override event GradeAddedDelegate GradeAdded;

        // constructor referencing base class
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
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
        public override void AddGrade(double grade)
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

    

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            

            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
            }
            return result;
        }

    }
}