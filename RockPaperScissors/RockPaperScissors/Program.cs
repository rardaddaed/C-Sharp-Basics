using System;

namespace RockPaperScissors
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string inputPlayer;
      string inputAI;

      int randomInt;

      bool playAgain = true;

      int scorePlayer = 0;
      int scoreAI = 0;

      while (playAgain)
      {

        while (scorePlayer < 3 && scoreAI < 3)
        {

          Console.WriteLine("Choose between ROCK, PAPER AND SCISSORS:       ");
          inputPlayer = Console.ReadLine();
          inputPlayer = inputPlayer.ToUpper();

          Random random = new Random();

          randomInt = random.Next(1, 4);

          switch (randomInt)
          {
            case 1:
              inputAI = "ROCK";
              Console.WriteLine("Computer chose ROCK");
              if (inputPlayer == "ROCK")
              {
                Console.WriteLine("DRAW!!\n\n");
              }
              else if (inputPlayer == "PAPER")
              {
                Console.WriteLine("PLAYER WINS!!\n\n");
                scorePlayer++;
              }
              else if (inputPlayer == "SCISSORS")
              {
                Console.WriteLine("AI WINS!!\n\n");
                scoreAI++;
              }
              break;

            case 2:
              inputAI = "PAPER";
              Console.WriteLine("Computer chose PAPER");
              if (inputPlayer == "PAPER")
              {
                Console.WriteLine("DRAW!!\n\n");
              }
              else if (inputPlayer == "SCISSORS")
              {
                Console.WriteLine("PLAYER WINS!!\n\n");
                scorePlayer++;
              }
              else if (inputPlayer == "ROCK")
              {
                Console.WriteLine("AI WINS!!\n\n");
                scoreAI++;
              }
              break;

            case 3:
              inputAI = "SCISSORS";
              Console.WriteLine("Computer chose SCISSORS");
              if (inputPlayer == "SCISSORS")
              {
                Console.WriteLine("DRAW!!\n\n");
              }
              else if (inputPlayer == "ROCK")
              {
                Console.WriteLine("PLAYER WINS!!\n\n");
                scorePlayer++;
              }
              else if (inputPlayer == "PAPER")
              {
                Console.WriteLine("AI WINS!!\n\n");
                scoreAI++;
              }
              break;

            default:
              Console.WriteLine("Invalid input!");
              break;
          }
        }

        if (scorePlayer == 3)
        {
          Console.WriteLine("Player wins!!\n\n");
        }
        else if (scoreAI == 3)
        {
          Console.WriteLine("AI wins!!\n\n");
        }
        else { }

        Console.WriteLine("Wanna play again?(y/n)");
        string loop = Console.ReadLine();

        if (loop == "y")
        {
          playAgain = true;
        }
        else if (loop == "n")
        {
          playAgain = false;
        }
        else { }
      }

    }
  }
}
