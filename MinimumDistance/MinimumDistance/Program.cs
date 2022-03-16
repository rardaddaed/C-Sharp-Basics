namespace MinimumDistance
{
  class Program
  {

    static void Main(string[] args)
    {
      int[] numbers = { 4, 20, 12, 3, 7, 9, 15, 488 };
      int result = MinimumDistance(numbers);
      Console.WriteLine("The minimum distance between the two closest elements is " + result);
      Console.Write("\nHit any key to continue");
      Console.ReadKey();
    }

    static int MinimumDistance(int[] values)
    {
      int dMin = 10000000;
      for (int i = 0; i < values.Length - 2; i++)
      {
        for (int j = i + 1; j < values.Length - 1; j++)
        {
          int temp = Math.Abs(values[i] - values[j]);
          if (temp < dMin)
          {
            dMin = temp;
          }
        }
      }
      return dMin;
    }
  }
}