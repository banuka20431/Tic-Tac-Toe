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
            int increment = originIndex == 2 || originIndex == 6 ? 2 : 4;


            for (int k = 1; k < 3; k++)
            {
                if (board[originIndex + increment * k * sign] != board[originIndex])
                {
                    return false;
                }
            }

            return true;
        }


        static Side GetClosestSide(int index)
        {
            if (index == (int)Boundary.CENTER) { return Side.NONE; }

            int diffUpperRight = Math.Abs((int)(Boundary.UPPER_RIGHT + 1 - index));
            int diffUpperLeft = Math.Abs((int)(Boundary.UPPER_LEFT + 1 - index));
            int diffLowerRight = Math.Abs((int)(Boundary.LOWER_RIGHT + 1 - index));
            int diffLowerLeft = Math.Abs((int)(Boundary.LOWER_LEFT + 1 - index));


            if (diffUpperRight <= 2 && diffUpperLeft <= 2)
            {
                return Side.UP;
            }

            if (diffUpperRight <= 3 && diffLowerRight <= 3)
            {
                return Side.RIGHT;
            }

            if (diffLowerRight <= 2 && diffLowerRight <= 2)
            {
                return Side.DOWN;
            }

            if (diffLowerLeft <= 3 && diffUpperLeft <= 3)
            {
                return Side.LEFT;
            }

            return Side.NONE;

        }


        static GameStatus CheckState(char[] board, int handID)
        {



            Boolean horizontalMatch = false;
            Boolean verticalMatch = false;
            Boolean diagonalMatch = false;

            Boolean ASCENDING = true;
            Boolean DESCENDING = false;


            for (int currentIdx = 0; currentIdx < board.Length; currentIdx++)
            {

                if (board[currentIdx] != 'X' && board[currentIdx] != 'O') { continue; }

                Side side = GetClosestSide(currentIdx + 1);


                if (side == Side.UP)
                {

                    if (currentIdx == (int)Boundary.UPPER_LEFT || currentIdx == (int)Boundary.UPPER_RIGHT)
                    {

                        horizontalMatch = HorizontalCheck(
                            board,
                            currentIdx,
                            currentIdx == (int)Boundary.UPPER_LEFT ? ASCENDING : DESCENDING
                         );
                        diagonalMatch = DiagonalCheck(board, currentIdx, ASCENDING);
                        verticalMatch = VerticalCheck(board, currentIdx, ASCENDING);

                    }
                    else
                    {
                        verticalMatch = VerticalCheck(board, currentIdx, ASCENDING);
                    }


                    Console.WriteLine($"\n\nUP");
                    Console.WriteLine($"Vertical Match: {verticalMatch}");
                    Console.WriteLine($"Horizontal Match: {horizontalMatch}");
                    Console.WriteLine($"Diagonal Match: {diagonalMatch}\n\n");

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


                    Console.WriteLine($"\n\nRIGHT");
                    Console.WriteLine($"Vertical Match: {verticalMatch}");
                    Console.WriteLine($"Horizontal Match: {horizontalMatch}");
                    Console.WriteLine($"Diagonal Match: {diagonalMatch}\n\n");

                }
                else if (side == Side.DOWN)
                {

                    if (currentIdx == (int)Boundary.LOWER_RIGHT || currentIdx == (int)Boundary.LOWER_LEFT)
                    {
                        horizontalMatch = HorizontalCheck(
                            board,
                            currentIdx,
                            currentIdx == (int)Boundary.LOWER_LEFT ? ASCENDING : DESCENDING
                        );
                        diagonalMatch = DiagonalCheck(board, currentIdx, DESCENDING);
                        verticalMatch = VerticalCheck(board, currentIdx, DESCENDING);
                    }
                    else
                    {
                        verticalMatch = VerticalCheck(board, currentIdx, DESCENDING);
                    }


                    Console.WriteLine($"\n\nDOWN");
                    Console.WriteLine($"Vertical Match: {verticalMatch}");
                    Console.WriteLine($"Horizontal Match: {horizontalMatch}");
                    Console.WriteLine($"Diagonal Match: {diagonalMatch}\n\n");

                }
                else if (side == Side.LEFT)
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


                    Console.WriteLine($"\n\nLEFT");
                    Console.WriteLine($"Vertical Match: {verticalMatch}");
                    Console.WriteLine($"Horizontal Match: {horizontalMatch}");
                    Console.WriteLine($"Diagonal Match: {diagonalMatch}\n\n");

                }

                if (verticalMatch || horizontalMatch || diagonalMatch)
                {
                    return GameStatus.WIN;
                }
            }

            if(handID == 9) { return GameStatus.DRAW; }


            return GameStatus.ONGOING;
        }


        static void PlayHand(string[] players, char[] board, char currentPlayerSymbole)
        {
            string currentPlayer = currentPlayerSymbole == 'X' ? players[0] : players[1];
            Boolean invalidInput = true;

            while (invalidInput)
            {
                Console.WriteLine($"\n\n{currentPlayer}, it's your move!");
                Console.Write($"\n + Enter number 1 - 9 to select a squre: ");
                if (!int.TryParse(Console.ReadLine(), out int squreID))
                {
                    Console.Write("Invalid Input!");
                }
                else if (squreID < 1 || squreID > 9)
                {
                    Console.Write("Input Out of Bound! Enter a number from 1 to 9.");
                }
                else if (board[squreID - 1] == 'X' || board[squreID - 1] == 'O')
                {
                    Console.Write($"Squre {squreID} is already claimed!");

                }
                else
                {
                    board[squreID - 1] = currentPlayerSymbole;
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
            Boolean playAgain = false;
            GameStatus gameStatus;
            int handID;

            string HR = "=+++++++++++++++++++++++++++++++++++++++++++++++++++++=";
            string TITLE = "Welcome to Tic Tac Toe (idkwid edition)!";
            int PAD = (HR.Length - TITLE.Length) / 2;

            Console.WriteLine(HR);
            Console.WriteLine(string.Format("{0," + (TITLE.Length + PAD) + "}", TITLE));
            Console.WriteLine(HR);

            do
            {
                char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                gameStatus = GameStatus.ONGOING;
                handID = 1;

                Console.WriteLine($"\n! {players[0]} is X and {players[1]} is O.");
                Console.WriteLine("\n - Board Cofiguration: \n");
                PrintBoard(board);

                board = new char[board.Length];
                ResetBoard(board);

                while (gameStatus == GameStatus.ONGOING)
                {
                    char currentPlayerSymbole = handID % 2 == 0 ? 'O' : 'X';

                    PlayHand(players, board, currentPlayerSymbole);

                    gameStatus = CheckState(board, handID);

                    if (gameStatus == GameStatus.WIN) { Console.WriteLine($"\n{(handID % 2 == 0 ? players[1] : players[0])} Won!"); }
                    if (gameStatus == GameStatus.DRAW) { Console.WriteLine("\nGame Drawn!"); }

                    handID++;
                }

                Console.Write("\nPlay Again? (y/n): ");

                playAgain = (char)Console.Read() == 'y';
                Console.ReadLine();
                Console.WriteLine(HR);

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
    UPPER_LEFT = 0,
    UPPER_RIGHT = 2,
    LOWER_LEFT = 6,
    LOWER_RIGHT = 8,
    CENTER = 4
}

enum Side
{
    UP = -1,
    RIGHT = -2,
    DOWN = -3,
    LEFT = -4,
    NONE = -5
}
