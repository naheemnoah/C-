using System;
using Xunit;

namespace Gradebook.Tests
{

    //delegate method syntax
    public delegate string WriteLogDelegate(string message);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void writeLogDelegateCanPointToMethod(){
            // declare delegate method
            WriteLogDelegate log = ReturnMessage;

            //initialize delegate method
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");

            Assert.Equal(3, count);
        }

        //method defined in the form of delegate
        string ReturnMessage(string message){
            count++;
            return message;
        }

        string IncrementCount(string message){
            count++;
            return message;
        }

        [Fact]
        public void StringBehaveLikeValueType()
        {
            // string are reference type
            string name = "Scott";
            var upper = MakeUpperCase(name);

            Assert.Equal("Scott", name);
            Assert.Equal("SCOTT", upper);
            
        }

        private string MakeUpperCase(string parameter){
            return parameter.ToUpper();
        }

        [Fact]
        public void ValueTypesAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
            
        }

        private void SetInt(ref int z){
            z = 42;
        }

        private int GetInt(){
            return 3;
        }


        [Fact]
        public void CSharpIsPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(out book1, "New name");
            
            Assert.Equal("New name", book1.Name);
            
        }

        // ref or out is a keyword that allowes pointing to the same reference location
        // out keyword ensures compulsory initialization in the method
        private void GetBookSetName(out InMemoryBook book, string name){
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New name");
            
            Assert.Equal("Book 1", book1.Name);
            
        }

        private void GetBookSetName(InMemoryBook book, string name){
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");
            
            Assert.Equal("New Name", book1.Name);
            
        }

        private void SetName(InMemoryBook book, string name){
            book.Name = name;
        }


        [Fact]
        public void GetBookReturnDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        InMemoryBook GetBook(string name) {
            return new InMemoryBook(name);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;
            
            // Asserting if they point to the same reference in memory
            Assert.Same(book1, book2);
        }
    }
}
