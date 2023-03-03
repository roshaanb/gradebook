using System;
using Xunit;

// using unit tests to see how c# and dotnet behave

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            // log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello");
            //this calls ReturnMessage twice and IncrementCount once
            Assert.Equal(3, count);
        }

        string IncrementCount(string message){
            count++;
            return message.ToLower();
        }

        string ReturnMessage(string message){
            count++;
            return message;
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");
            /*
            this copies the value inside var book1 then pastes it into the parameter GetBookSetName(book1, ...)
            invoking the method DOESN'T CHANGE the value, so            
            */

            Assert.Equal("Book 1", book1.Name);
        }

        void GetBookSetName(Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");
            /*
            the ref keywork ensures book1 is now pointing at a different object, rather than just copy pasting the value        
            */

            Assert.Equal("New Name", book1.Name);
        }

        void GetBookSetName(ref Book book, string name)
        {
            book = new Book(name);
        }

        [Fact]
        public void ValueTypesPassByValue()
        {
            var x = GetInt();
            SetInt(x);
            /*
            same as above for reference type, SetInt just copies the value 3 but the var x is unchanged    
            */

            Assert.Equal(3, x);
        }

        void SetInt(int x)
        {
            x = 42;
        }

        [Fact]
        public void ValueTypesCanPassByRef()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        void SetInt(ref int x)
        {
            x = 42;
        }

        int GetInt()
        {
            return 3;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "barb";
            MakeUpperCase(name);
            /* 
            because ToUpper returns copy of string
            */

            Assert.Equal("barb", name);
        }

        private void MakeUpperCase(string param)
        {
            param.ToUpper();
        }
        
        [Fact]
        public void StringsStillBehaveLikeValueTypes()
        {
            string name = "barb";
            var upper = MakeUpperCaseAgain(name);
            // we can reassign var name = MakeUpperC... same as value types

            Assert.Equal("barb", name);
            Assert.Equal("BARB", upper);
        }

        string MakeUpperCaseAgain(string param)
        {
            return param.ToUpper();
            // remember to change return type from void to string if we return string
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        void SetName(Book book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            // two variables pointing at two different objects
        }

        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        Book GetBook(string name)
        {
            return new Book(name);        
        }
    }
}
