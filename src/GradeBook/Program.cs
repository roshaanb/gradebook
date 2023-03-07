using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Roshaan's gradebook");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            try
            {
                var stats = book.GetStatistics();

                Console.WriteLine($"For the book named {book.Name}");
                Console.WriteLine($"The average grade is {stats.Average:N1}");
                Console.WriteLine($"The highest grade is {stats.High:N1}");
                Console.WriteLine($"The lowest grade is {stats.Low:N1}");
                Console.WriteLine($"The letter is {stats.Letter}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Please enter your grade, or enter 'q' to quit:");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input ?? "101");
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
