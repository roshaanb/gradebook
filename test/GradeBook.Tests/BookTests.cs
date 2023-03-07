using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalcuatesAnAverageGrade()
        {
            // arrange
            Book book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(53.2);
            book.AddGrade(73.7);

            // assert
            var results = book.GetStatistics();

            // act
            Assert.Equal(72.0, results.Average, 1);
            Assert.Equal(89.1, results.High, 1);
            Assert.Equal(53.2, results.Low, 1);
            Assert.Equal('C', results.Letter);
        }

        [Fact]
        public void BookIgnoresInvalidGrade()
        {
            // arrange
            Book book = new InMemoryBook("");
            book.AddGrade(90);

            // assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                book.AddGrade(105);
            });

            // act
            Assert.Equal("Invalid grade", ex.Message);
        }

        [Fact]
        public void BookIgnoresInvalidInput()
        {
            // arrange
            Book book = new InMemoryBook("Roshaan's gradebook");

            // assert
            var ex = Assert.Throws<FormatException>(() =>
            {
                book.AddGrade(double.Parse("barb"));
            });

            // act
            Assert.Equal("The input string 'barb' was not in a correct format.", ex.Message);
        }

        [Fact]
        public void BookIgnoresStatsWhenNoGradesAdded()
        {
            // arrange
            Book book = new InMemoryBook("Roshaan's gradebook");

            // assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var stats = book.GetStatistics();
            });

            // act
            Assert.Equal("No grades were added", ex.Message);
        }
    }
}
