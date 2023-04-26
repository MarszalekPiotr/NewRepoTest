using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TikTakToe
{
    public class Game
    {
        public string[,] board;
        public string userMark;
        public string computerMark;
        private bool gameFinished;
        private int CurrentUserRow;
        private int CurrentUserCol;

        public Game()
        {
            InitializeBoard();


        }

        public void MainGame()
        {
            gameFinished = false;
            string[] options = { "X", "O" };
            

            for (; ; )
            {
                Console.WriteLine("Wybierz czy chcesz zacząc X czy O");

                string choice = Console.ReadLine();
                if (!options.Contains(choice))
                {
                    Console.WriteLine("Niepoprawny wybór wybierz ponownie");
                    continue;
                }
                else
                {
                    userMark = choice;
                    
                    if(choice == "X")
                    {
                        computerMark = "O";
                    }
                    else
                    {
                        computerMark = "X";
                    }

                    break;

                }
            }

            //start gry pusta plansza
            DrawBoard();
            //gdy komputer zaczyna
            if (computerMark == "X")
            {
                // głowna pętla programu
                while (!gameFinished)
                {
                    ComputerMove();
                    if (CheckComputerWin())
                    {

                        Console.Clear();
                        DrawBoard();
                        Console.WriteLine("Niestety Potężne AI wygrało \n");
                        break;
                    }
                    else if(!CheckComputerWin() && !CheckUserWin() && FullBoard())
                    {    Console.Clear();
                        DrawBoard();
                        Console.WriteLine("remis :D");
                        break;

                    }
                    Console.Clear();
                    DrawBoard();
                    //pętla wyboru przez użytkownika
                    for (; ; )
                    {
                        Console.WriteLine(" Twój ruch Podaj numer wiersza ");
                        if (!int.TryParse(Console.ReadLine(), out CurrentUserRow))
                        {
                            Console.WriteLine("podano zły numer wiersza Nie udało się sparsowac");
                            continue;
                        }
                        else
                        {
                            if (CurrentUserRow < 1 || CurrentUserRow > 3)
                            {
                                Console.WriteLine("Nie ma takiego numeru wiersza");
                            }
                            else
                            {
                                Console.WriteLine("Podaj numer kolumny");

                                if (!int.TryParse(Console.ReadLine(), out CurrentUserCol))
                                {
                                    Console.WriteLine("Podano zły numer kolumny");
                                    continue;
                                }
                                else
                                {
                                    if (CurrentUserCol < 1 || CurrentUserCol > 3)
                                    {
                                        Console.WriteLine("Nie ma takiego numeru kolumny");
                                        continue;
                                    }
                                    else
                                    {
                                        if (!(board[CurrentUserRow - 1, CurrentUserCol - 1] == userMark || board[CurrentUserRow - 1, CurrentUserCol - 1] == computerMark))
                                        {
                                            board[CurrentUserRow - 1, CurrentUserCol - 1] = userMark;
                                            Console.Clear();
                                            DrawBoard();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("To pole jest zajęte");
                                            continue;
                                        }

                                    }
                                }
                            }
                        }



                    }
                    //sprawdzenie czy aby czasem uzytkownik nie wygral 
                    if (CheckUserWin())
                    {
                        Console.WriteLine("Brawo Pokonałeś potężne AI");

                        gameFinished = true;
                        break;

                    }
                    else
                    {
                        Console.WriteLine("dzialam");
                    }
                }

            }
            else
            {
                while (!gameFinished)
                {
                    Console.Clear();
                    DrawBoard();
                    // uzytkownik wybiera
                    for (; ; )
                    {
                        Console.WriteLine(" Twój ruch Podaj numer wiersza ");
                        if (!int.TryParse(Console.ReadLine(), out CurrentUserRow))
                        {
                            Console.WriteLine("podano zły numer wiersza Nie udało się sparsowac");
                            continue;
                        }
                        else
                        {
                            if (CurrentUserRow < 1 || CurrentUserRow > 3)
                            {
                                Console.WriteLine("Nie ma takiego numeru wiersza");
                            }
                            else
                            {
                                Console.WriteLine("Podaj numer kolumny");

                                if (!int.TryParse(Console.ReadLine(), out CurrentUserCol))
                                {
                                    Console.WriteLine("Podano zły numer kolumny");
                                    continue;
                                }
                                else
                                {
                                    if (CurrentUserCol < 1 || CurrentUserCol > 3)
                                    {
                                        Console.WriteLine("Nie ma takiego numeru kolumny");
                                        continue;
                                    }
                                    else
                                    {
                                        if (!(board[CurrentUserRow - 1, CurrentUserCol - 1] == userMark || board[CurrentUserRow - 1, CurrentUserCol - 1] == computerMark))
                                        {
                                            board[CurrentUserRow - 1, CurrentUserCol - 1] = userMark;
                                            Console.Clear();
                                            DrawBoard();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("To pole jest zajęte");
                                            continue;
                                        }

                                    }
                                }
                            }
                        }


                    }
                    if (CheckUserWin())
                    {
                        Console.WriteLine("Brawo Pokonałeś potężne AI");

                        gameFinished = true;
                        break;

                    }
                    else if (!CheckComputerWin() && !CheckUserWin() && FullBoard())
                    {
                        Console.Clear();
                        DrawBoard();
                        Console.WriteLine("remis :D");
                        break;

                    }

                    ComputerMove();
                    if (CheckComputerWin())
                    {

                        Console.Clear();
                        DrawBoard();
                        Console.WriteLine("Niestety Potężne AI wygrało \n");
                        
                        break;
                    }
                    
                    Console.Clear();
                    DrawBoard();

                    
                   




                }
            }





        }

        private bool FullBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(board[i, j] == userMark || board[i, j] == computerMark))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ComputerMove()
        {
            List<Tuple<int, int>> available = new List<Tuple<int, int>>();

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j<3; j++)
                {
                    if (!(board[i,j] == userMark || board[i,j] == computerMark))
                    {
                        available.Add(new Tuple<int, int> ( i, j ));
                    }
                }
            }

            if(available.Count > 0) 
            {
                Random random = new Random();
                int indexTmp = random.Next(available.Count);


                Tuple<int, int> randomChoice = available[indexTmp];
                board[randomChoice.Item1, randomChoice.Item2] = computerMark;
            }
            
            
            
        }

        private bool CheckUserWin()
        {
            bool isWinner = false;
          
           for(int i = 0; i < 3; i++)
            {
                if (board[i,0] == userMark)
                {    
                    isWinner = true;
                    for (int j = 1; j < 3; j++)
                    {
                        if (board[i,j] != userMark)
                        {
                            isWinner = false;
                        }
                        
                    }

                    if (isWinner)
                    {
                        return isWinner;
                    }


                }
            }
                

           for(int ii = 0; ii<3; ii++)
            {
                if (board[0,ii] == userMark)
                {
                    isWinner = true;
                    for (int jj = 1; jj < 3; jj++)
                    {
                        if (board[jj,ii] != userMark)
                        {
                            isWinner = false;

                        }

                    }

                    if (isWinner)
                    {
                        return isWinner;
                    }
                }
            }

            if (board[0,0] == userMark)
            {
                isWinner = true;
                for(int iii = 1; iii < 3; iii++)
                {
                    
                    
                        if (board[iii,iii] != userMark)
                        {
                            isWinner = false; 
                        }
                    
                }

                if (isWinner)
                {
                    return isWinner;
                }
            }

            if (board[2,0] == userMark)
            {
                isWinner = true;
                int tmp = 1;

                for(int jjj = 1; jjj< 3;jjj++ )
                {
                    if (board[tmp,jjj] != userMark)
                    {
                        isWinner = false;
                    }
                    tmp--;
                }

                if (isWinner)
                {
                    return isWinner;
                }
            }


            return isWinner;
        }


        private bool CheckComputerWin()
        {
            bool isWinner = false;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == computerMark)
                {
                    isWinner = true;
                    for (int j = 1; j < 3; j++)
                    {
                        if (board[i, j] != computerMark)
                        {
                            isWinner = false;
                        }

                    }

                    if (isWinner)
                    {
                        return isWinner;
                    }
                }
            }

            for (int ii = 0; ii < 3; ii++)
            {
                if (board[0, ii] == computerMark)
                {
                    isWinner = true;
                    for (int jj = 1; jj < 3; jj++)
                    {
                        if (board[jj, ii] != computerMark)
                        {
                            isWinner = false;

                        }

                    }
                    if (isWinner)
                    {
                        return isWinner;
                    }
                }
            }

            if (board[0, 0] == computerMark)
            {
                isWinner = true;
                for (int iii = 1; iii < 3; iii++)
                {


                    if (board[iii, iii] != computerMark)
                    {
                        isWinner = false;
                    }

                }
                if (isWinner)
                {
                    return isWinner;
                }
            }

            if (board[2, 0] == computerMark)
            {
                isWinner = true;
                int tmp = 1;

                for (int jjj = 1; jjj < 3; jjj++)
                {
                    if (board[tmp, jjj] != computerMark)
                    {
                        isWinner = false;
                    }
                    tmp--;
                }
                if (isWinner)
                {
                    return isWinner;
                }
            }


            return isWinner;
        }
    
        private void DrawBoard()
        {
            Console.WriteLine("------------------------");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write($"   {board[i,j]}   ");
                   if(j == 0 || j == 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine("\n ------------------------ ");
            }

        }

        

        private void InitializeBoard()
        {
            board = new string[3, 3];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "?";
                }
            }
        }
    }
}
