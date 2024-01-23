namespace NumberService
{
    public class NumberService
    {
        public bool IsPrime(int number)
        {
            // Return true if the number is a prime number
            // Otherwise, return false
            // A prime number is a number that is only divisible by 1 and itself
            // For example, 2, 3, 5, 7, 11, 13, 17, 19, 23 are prime numbers
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
        public double CalculateAverage(IEnumerable<int> numbers)
        {
            // Return the average of the numbers
            // For example, if the numbers are 1, 2, 3, 4, 5, the average is 3
            // If the numbers are 1, 3, 5, 7, the average is 4
            // If the numbers are 1, 2, 3, the average is 2
            if (numbers == null || numbers.Count() == 0) return 0;
            return numbers.Average();
        }
        public bool IsEven(int number)
        {
            // Return true if the number is even
            if (number % 2 == 0) { return true; }
            else { return false; }
        }
        public bool IsDivisibleByThree(int number)
        {
            // Return true if the number is divisible by 3
            // Otherwise, return false
            // For example, 3, 6, 9, 12, 15 are divisible by 3
            if (number % 3 == 0) return true;
            else return false;
        }
    }
}