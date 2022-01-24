using BasicDelegates;

public class Program
{
  public static void Main()
  {
    var myDeleg = new MyVoidDelegate(PrintInteger);

    PassSomeDelegate(myDeleg);
  }
  
  public static void PassSomeDelegate(MyVoidDelegate del)
  {
    del(5);
  }
  public static void PrintInteger(int number)
  {
    Console.WriteLine(number);
  }
}
