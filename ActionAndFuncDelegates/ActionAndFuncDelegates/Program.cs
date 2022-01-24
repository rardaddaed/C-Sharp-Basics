public class Program
{
  public static void Main()
  {
    Func<int> func = SomeMethod;
    func += () => 42;

    Console.WriteLine(func());
  }

  public static int SomeMethod()
  {
    return 42;
  }
}