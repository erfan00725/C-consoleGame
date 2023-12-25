namespace Application{    class Program    {        class Snake        {            public int x; public int y;            public bool isHead = false;            public Snake(int x , int y , bool isHead = false)            {                this.x = x; this.y = y; this.isHead = isHead;            }        };        static void Main(string[] args)        {            Console.CursorVisible = false;            const int x = 25;            const int y = 40;            List<Snake> snake = new List<Snake>() { new Snake(x/2-1, y/2-1, true) };        Random sRandX = new Random();        Random sRandY = new Random();        int Sx = sRandX.Next(1, x - 1);        int Sy = sRandX.Next(1, y - 1);        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();        string key = "W";        int score = 0;        bool gameOver = false;        bool checkPlayertail(int i , int j)
            {
                for (global::System.Int32 k = 0; k < snake.Count; k++)
                {
                    if (snake[k].x == i && snake[k].y == j)
                    {
                        return true;
                    }
                }
                return false;
            }        void updatePlayerTail()        {                Snake prevS;
                if (snake.Count > 1)
                {
                    prevS = snake[1];                    snake[1] = snake[0];

                    for (int i = 2; i < snake.Count; i++)
                    {
                        Snake temp = snake[i]; 
                        snake[i] = prevS;
                        prevS = temp;
                    }                }        }        void playGround(int x, int y)        {            Console.SetCursorPosition(0, 0);            if (Console.KeyAvailable)            {                keyInfo = Console.ReadKey(true);                bool valid = keyInfo.Key.ToString() == "W" || keyInfo.Key.ToString() == "S" || keyInfo.Key.ToString() == "D" || keyInfo.Key.ToString() == "A";                key = valid ? keyInfo.Key.ToString() : key;            }            for (int i = 0; i < x; i++)            {                for (int j = 0; j < y; j++)                {                    if (i == 0 || i == x - 1 || j == 0 || j == y - 1)                    {                        Console.Write("#");                    }                    else if (snake[0].x == 0 || snake[0].x == x-1 || snake[0].y == 0 || snake[0].y == y-1)                    {                            gameOver = true;                            break;                    }                    else if (i == Sx && j == Sy && i == snake[0].x && j == snake[0].y)                    {                        Console.Write("O");                        score++;                        snake.Add(new Snake(snake[snake.Count - 1].x, snake[snake.Count - 1].y));                        Sx = sRandX.Next(1, x - 1);                        Sy = sRandX.Next(1, y - 1);                    }                    else if (i == snake[0].x && j == snake[0].y)
                    {
                        Console.Write("O");
                    }
                    else if (checkPlayertail(i,j))
                        {
                            Console.Write("0");
                        }
                        else if (i == Sx && j == Sy)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }                Console.WriteLine();            }                updatePlayerTail();                    switch (key)                    {                        case "W":                                snake[0].x--;                            break;                        case "S":                            snake[0].x++;                            break;                        case "D":                                snake[0].y++;                            break;                        case "A":                                snake[0].y--;                            break;                        default: break;                    }            }while (!gameOver){    Console.WriteLine(key + "  " + score.ToString());    for (int i = 0; i < snake.Count; i++)    {        Console.Write(i + ":  " + snake[i].x + " " + snake[i].y + "   |");    }    playGround(x, y);    Thread.Sleep(100);}Console.WriteLine("game over!");        }    }}