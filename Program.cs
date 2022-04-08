namespace ConnectFour
{
    class Program
    {
        private static void Main(string[] args)
        {
            string[,] board = new string[6, 7];
            bool isPlayerOne;
            int counter = 0;

            ResetBoard(board);
            ShowBoard(board);
            Console.WriteLine("Player turn: O");

            while (Win(board) != "The X's have won!" && Win(board) != "The O's have won!" && Win(board) != "It's a tie!")
            {
                if (counter % 2 == 0) isPlayerOne = true; else isPlayerOne = false;
                Play(board, isPlayerOne);
                Win(board);
                counter++;
                Console.SetCursorPosition(0, 11);
                Console.WriteLine("\n" + Win(board));
            }
        }



        private static void ShowBoard(string[,] board)
        {
            Console.WriteLine();
            string boardLines = "";
            int newLine = 0;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    boardLines = boardLines + ("| " + board[row, column] + " ");
                    newLine++;
                    if (newLine % 7 == 0) boardLines = boardLines + "|" + "\n";
                }
            }
            boardLines = boardLines + "  1   2   3   4   5   6   7";
            Console.WriteLine(boardLines);
        }

        private static void ResetBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = " ";
                }
            }
        }
        private static void Play(string[,] board, bool isPlayerOne)
        {
            Console.SetCursorPosition(0, 10);
            Console.Write("Last input: ");
            char playerInput = Console.ReadKey().KeyChar;
            bool taken = false;
            int token = 5;
            char[] columnInput = new char[] { '1', '2', '3', '4', '5', '6', '7' };

            while (!taken)
            {
                string playerToken;
                if (isPlayerOne) playerToken = "O"; else playerToken = "X";
                
                if (token < 0)
                {
                    Console.SetCursorPosition(0, 10);
                    Console.Write("\nThis column is either full or does not exist. Try again.");
                    Console.ReadKey();
                    Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
                    Play(board, isPlayerOne);
                    break;
                }

                if (columnInput.Contains(playerInput) && board[token, Array.IndexOf(columnInput, playerInput)] == " ")
                {
                    for (int z = 0; z < token; z++)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.CursorVisible = false;
                        board[z, Array.IndexOf(columnInput, playerInput)] = playerToken;
                        ShowBoard(board);
                        Thread.Sleep(32);
                        board[z, Array.IndexOf(columnInput, playerInput)] = " ";
                    }

                    board[token, Array.IndexOf(columnInput, playerInput)] = playerToken;
                    taken = true;
                    Console.SetCursorPosition(0, 0);
                    ShowBoard(board);
                    if (playerToken == "O") Console.WriteLine("Player turn: X");
                    else Console.WriteLine("Player turn: O");
                }
                else token -= 1;
            }
        }
        private static string Win(string[,] board)
        {
            bool hasWon = false;
            string whoWon = "";
            string[,] boardWon = new string[25, 1];

            int m = 12;
            int n = 18;

            boardWon[0, 0] = board[0, 3] + board[1, 2] + board[2, 1] + board[3, 0];
            boardWon[1, 0] = board[0, 4] + board[1, 3] + board[2, 2] + board[3, 1] + board[4, 0];
            boardWon[2, 0] = board[0, 5] + board[1, 4] + board[2, 3] + board[3, 2] + board[4, 1] + board[5, 0];
            boardWon[3, 0] = board[0, 6] + board[1, 5] + board[2, 4] + board[3, 3] + board[4, 2] + board[5, 1];
            boardWon[4, 0] = board[1, 6] + board[2, 5] + board[3, 4] + board[4, 3] + board[5, 2];
            boardWon[5, 0] = board[2, 6] + board[3, 5] + board[4, 4] + board[5, 3];
            boardWon[6, 0] = board[2, 0] + board[3, 1] + board[4, 2] + board[5, 3];
            boardWon[7, 0] = board[1, 0] + board[2, 1] + board[3, 2] + board[4, 3] + board[5, 4];
            boardWon[8, 0] = board[0, 0] + board[1, 1] + board[2, 2] + board[3, 3] + board[4, 4] + board[5, 5];
            boardWon[9, 0] = board[0, 1] + board[1, 2] + board[2, 3] + board[3, 4] + board[4, 5] + board[5, 6];
            boardWon[10, 0] = board[0, 2] + board[1, 3] + board[2, 4] + board[3, 5] + board[4, 6];
            boardWon[11, 0] = board[0, 3] + board[1, 4] + board[2, 5] + board[3, 6];


            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardWon[m, 0] += board[i, j];
                }
                m++;
            }

            for (int k = 0; k < board.GetLength(1); k++)
            {
                for (int l = 0; l < board.GetLength(0); l++)
                {
                    boardWon[n, 0] += board[l, k];
                }
                n++;
            }

            for (int o = 0; o < boardWon.GetLength(0); o++)
            {
                if (boardWon[o, 0].Contains("OOOO"))
                {
                    whoWon = "The O's have won!";
                    hasWon = true;
                }
                else if (boardWon[o, 0].Contains("XXXX"))
                {
                    whoWon = "The X's have won!";
                    hasWon = true;
                }
            }

            if (!board[0, 0].Contains(" ") && !board[0, 1].Contains(" ") && !board[0, 2].Contains(" ") &&
                !board[0, 3].Contains(" ") && !board[0, 4].Contains(" ") && !board[0, 5].Contains(" ") &&
                !board[0, 6].Contains(" ") && hasWon == false) whoWon = "It's a tie!";

            return whoWon;
        }
    }
}