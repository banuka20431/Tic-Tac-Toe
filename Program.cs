using System;

namespace TicTacToe
{
    class Program
    {

        static void PrintBoard(char[] board)
        {
            Console.WriteLine($"\n\t{board[0]} | {board[1]} | {board[2]}\n\t--+---+--\n\t{board[3]} | {board[4]} | {board[5]}\n\t--+---+--\n\t{board[6]} | {board[7]} | {board[8]}");
        }


        static void ResetBoard(char[] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = ' ';
            }
        }

        static Boolean HorizontalCheck(char[] board, int originIndex, Boolean ascending)
        {
            int sign = ascending ? 1 : -1;

            for (int k = 1; k < 3; k++)
            {

                if (board[originIndex + k * sign] != board[originIndex])
                {
                    return false;
                }
            }

            return true;
        }
        static Boolean VerticalCheck(char[] board, int originIndex, Boolean ascending)
        {
            int sign = ascending ? 1 : -1;

            for (int k = 1; k < 3; k++)
            {
                if (board[originIndex + 3 * k * sign] != board[originIndex])
                {
                    return false;
                }
            }

            return true;
        }

        static Boolean DiagonalCheck(char[] board, int originIndex, Boolean ascending)
        {
            int sign = ascending ? 1 : -1;

            for (int k = 1; k < 3; k++)
            {

                Console.WriteLine("OG IDX: " + originIndex);
                Console.WriteLine("NEXT IDX: " + originIndex + 4 * k * sign);
                if (board[originIndex + 4 * k * sign] != board[originIndex])
                {
                    return false;
                }
            }

            return true;
        }


        static Side GetClosestSide(int index)
        {
            if (index == (int)Boundary.CENTER) { return Side.NONE; }

            int diffUpperRight = Math.Abs((int)(Boundary.UPPER_RIGHT - index));
            int diffUpperLeft = Math.Abs((int)(Boundary.UPPER_LEFT - index));
            int diffLowerRight = Math.Abs((int)(Boundary.LOWER_RIGHT - index));
            int diffLowerLeft = Math.Abs((int)(Boundary.LOWER_LEFT - index));

            /*
            Console.WriteLine($"\n\nDUR {diffUpperRight}");
            Console.WriteLine($"DUL {diffUpperLeft}");
            Console.WriteLine($"DLR {diffLowerRight}");
            Console.WriteLine($"DLL {diffLowerLeft}\n");
            */

            if (diffUpperRight <= 2 && diffUpperLeft <= 2)
            {
                return Side.UP;
            }

            if (diffUpperRight <=3 && diffLowerRight <= 3)
            {
                return Side.RIGHT;
            }

            if (diffLowerRight <= 2 && diffLowerRight <=2)
            {
                return Side.DOWN;
            }

            if (diffLowerLeft <=3 && diffUpperLeft <= 3)
            {
                return Side.LEFT;
            }

            return Side.NONE;

        }


        static GameStatus CheckState(char[] board)
        {


            Boolean horizontalMatch = false;
            Boolean verticalMatch = false;
            Boolean diagonalMatch = false;

            Boolean ASCENDING = true;
            Boolean DESCENDING = false;


            for (int currentIdx = 0; currentIdx < board.Length; currentIdx++)
            {

                if (board[currentIdx] != 'X' && board[currentIdx] != 'O') { continue; }
                
                // Console.WriteLine($"\nCurrent Squre: {currentIdx + 1}");

                Side side = GetClosestSide(currentIdx + 1);

                // Console.WriteLine("\nSide: " + side);

                if (side == Side.UP)
                {

                    if (currentIdx == (int)Boundary.UPPER_LEFT || currentIdx == (int)Boundary.UPPER_RIGHT)
                    {

                        horizontalMatch = HorizontalCheck(board, currentIdx, ASCENDING);
                        diagonalMatch = DiagonalCheck(board, currentIdx, ASCENDING);
                        verticalMatch = VerticalCheck(
                            board,
                            currentIdx,
                            currentIdx == (int)Boundary.UPPER_LEFT ? ASCENDING : DESCENDING
                        );

                    }
                    else
                    {
                        verticalMatch = VerticalCheck(board, currentIdx, ASCENDING);
                    }


                }
                else if (side == Side.RIGHT)
                {

                    if (currentIdx == (int)Boundary.UPPER_RIGHT || currentIdx == (int)Boundary.LOWER_RIGHT)
                    {

                        verticalMatch = VerticalCheck(board, currentIdx, DESCENDING);
                        diagonalMatch = DiagonalCheck(board, currentIdx, DESCENDING);
                        horizontalMatch = HorizontalCheck(
                            board,
                            currentIdx,
                            currentIdx == (int)Boundary.UPPER_RIGHT ? DESCENDING : ASCENDING
                        );

                    }
                    else
                    {
                        horizontalMatch = HorizontalCheck(board, currentIdx, DESCENDING);
                    }

                }
                else if (side == Side.DOWN)
                {

                    if (currentIdx == (int)Boundary.LOWER_RIGHT || currentIdx == (int)Boundary.LOWER_LEFT)
                    {
                        horizontalMatch = HorizontalCheck(board, currentIdx, DESCENDING);
                        diagonalMatch = DiagonalCheck(board, currentIdx, DESCENDING);
                        verticalMatch = VerticalCheck(
                            board,
                            currentIdx,
                            currentIdx == (int)Boundary.LOWER_RIGHT ? ASCENDING : DESCENDING
                        );

                    }
                    else
                    {
                        horizontalMatch = HorizontalCheck(board, currentIdx, DESCENDING);
                    }

                } else if (side == Side.LEFT)
                {

                    if (currentIdx == (int)Boundary.UPPER_LEFT || currentIdx == (int)Boundary.LOWER_LEFT)
                    {

                        horizontalMatch = HorizontalCheck(board, currentIdx, DESCENDING);
                        diagonalMatch = DiagonalCheck(board, currentIdx, DESCENDING);
                        verticalMatch = VerticalCheck(board,
                            currentIdx,
                            currentIdx == (int)Boundary.UPPER_LEFT ? ASCENDING : DESCENDING
                        );

                    }
                    else
                    {
                        horizontalMatch = HorizontalCheck(board, currentIdx, DESCENDING);
                    }

                }

            }

            if (verticalMatch || horizontalMatch || diagonalMatch)
            {
                return GameStatus.WIN;
            } 

            return GameStatus.ONGOING;
        } 


        static void PlayHand(string[] players, char[] board, char currentPlayerSymbole)
        {
            string currentPlayer = currentPlayerSymbole == 'X' ? players[0] : players[1];
            Boolean invalidInput = true;

            while (invalidInput) {
                Console.WriteLine($"\n\n{currentPlayer}, it's your move!");
                Console.Write($"\n + Enter number 1 - 9 to select a squre: ");
                if (!int.TryParse(Console.ReadLine(), out int index))
                {
                    Console.Write("Invalid Input!");
                }
                else if (index < 1 || index > 9)
                {
                    Console.Write("Input Out of Bound! Enter a number between 1 through 9.");
                }
                else if (board[index - 1] == 'X' || board[index - 1] == 'O') {
                    Console.Write($"Squre {index} is already claimed!");

                } else
                {
                    board[index - 1] = currentPlayerSymbole;
                    PrintBoard(board);
                    invalidInput = false;
                }

                if (invalidInput)
                {
                    Console.WriteLine(" Try Again :)");
                }
            }

        }

        static void Main(string[] args)
        {
            string[] players = { "Player 01", "Player 02" };
            char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Boolean playAgain = false;
            GameStatus gameState;
            int handID;

            Console.WriteLine("\n\nWelcome to Tic Tac Toe!\n");

            do
            {

                gameState = GameStatus.ONGOING;
                handID = 1;

                Console.WriteLine($"! {players[0]} is X and {players[1]} is O.");
                Console.WriteLine("\n - Board Cofiguration: \n");
                PrintBoard(board);
                
                board = new char[board.Length];
                ResetBoard(board);

                while (gameState == GameStatus.ONGOING)
                {
                    char currentPlayerSymbole = handID % 2 == 0 ? 'O' : 'X';

                    PlayHand(players, board, currentPlayerSymbole);
                    
                    gameState = CheckState(board);

                    if(gameState == GameStatus.WIN) { Console.WriteLine("\nYou Won!"); }

                    handID++;
                }

                Console.Write("\nPlay Again? (y/n): ");

                playAgain = (char)Console.Read() == 'y';

            } while (playAgain);
        }
    }
    
}

enum GameStatus
{
    WIN = 1,
    DRAW = 2,
    ONGOING = 3
} 

enum Boundary
{
    UPPER_LEFT = 1,
    UPPER_RIGHT = 3,
    LOWER_LEFT = 7,
    LOWER_RIGHT = 9,
    CENTER = 5
}

enum Side
{
    UP = -1,
    RIGHT = -2,
    DOWN = -3,
    LEFT = -4,
    NONE = -5
}
