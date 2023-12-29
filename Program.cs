using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Application
{
    class Program
    {

        class MovmentKeys
        {
            private const int up = 1;
            private const int down = -1;
            private const int right = 2;
            private const int left = -2;

            private const string keyUp = "W";
            private const string keyDown = "S";
            private const string keyRight = "D";
            private const string keyLeft = "A";


            public int prevKey = 1;
            private System.ConsoleKeyInfo keyInfo;

            public int currentKey = 1;


            public void updateMovmentKey()
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    bool valid = keyInfo.Key.ToString() == keyUp || keyInfo.Key.ToString() == keyDown || keyInfo.Key.ToString() == keyRight || keyInfo.Key.ToString() == keyLeft;
                    if (!valid)
                    {
                        return ;
                    }
                    prevKey = currentKey;
                    switch (keyInfo.Key.ToString())
                    {
                        case keyUp:
                            currentKey = up; break;
                        case keyDown:
                            currentKey = down; break;
                        case keyRight:
                            currentKey = right; break;
                        case keyLeft:
                            currentKey = left; break;
                            default: break;
                    }
                }
                return ;
            }

        }


        class SnakeGame
        {

            class Snake
            {
                public int x; public int y;
                public bool isHead = false;

                public Snake(int x, int y, bool isHead = false)
                {
                    this.x = x; this.y = y; this.isHead = isHead;
                }
                public void setLocation(int x, int y)
                {
                    this.x = x; this.y = y;
                }
            };

            public MovmentKeys key = new MovmentKeys();

            const int x = 25;
            const int y = 40;

            public int score = 0;

            public bool gameOver = false;

            private static Random sRandX = new Random();
            private static Random sRandY = new Random();

            private int Sx = sRandX.Next(1, x - 1);
            private int Sy = sRandX.Next(1, y - 1);

            private List<Snake> snake = new List<Snake>() { new Snake(x/2-1, y/2-1, true) };

            private bool checkPlayertail(int i, int j, bool checkHitHead = false)
            {
                if (!checkHitHead)
                {
                    for (global::System.Int32 k = 0; k < snake.Count; k++)
                    {
                        if (snake[k].x == i && snake[k].y == j)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    for (global::System.Int32 k = 2; k < snake.Count; k++)
                    {
                        if (snake[k].x == snake[0].x && snake[k].y == snake[0].y)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            private void updatePlayerHead()
            {
                if (key.currentKey + key.prevKey != 0)
                {
                    switch (key.currentKey)
                    {
                        case 1:
                            snake[0].x--;
                            break;
                        case -1:
                            snake[0].x++;
                            break;
                        case 2:
                            snake[0].y++;
                            break;
                        case -2:
                            snake[0].y--;
                            break;
                        default: break;
                    }
                }
                else
                {
                    key.currentKey = key.prevKey;
                    switch (key.currentKey)
                    {
                        case 1:
                            snake[0].x--;
                            break;
                        case -1:
                            snake[0].x++;
                            break;
                        case 2:
                            snake[0].y++;
                            break;
                        case -2:
                            snake[0].y--;
                            break;
                        default: break;
                    }
                }
            }
            private void updatePlayerTail()
            {
                Snake prevS;
                if (snake.Count > 1)
                {
                    prevS = new Snake(snake[1].x, snake[1].y);
                    snake[1].setLocation(snake[0].x, snake[0].y);

                    for (int i = 2; i < snake.Count; i++)
                    {
                        Snake temp = new Snake(snake[i].x, snake[i].y);
                        snake[i].setLocation(prevS.x, prevS.y);
                        prevS.setLocation(temp.x, temp.y);
                    }
                }
            }
            public void playGround()
            {
                Console.SetCursorPosition(0, 0);



                key.updateMovmentKey();


                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {


                        if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
                        {
                            Console.Write("#");
                        }
                        else if (snake[0].x == 0 || snake[0].x == x - 1 || snake[0].y == 0 || snake[0].y == y - 1 || checkPlayertail(i, j, true))
                        {
                            gameOver = true;
                            break;
                        }
                        else if (i == Sx && j == Sy && i == snake[0].x && j == snake[0].y)
                        {
                            Console.Write("O");
                            snake.Add(new Snake(snake[snake.Count - 1].x, snake[snake.Count - 1].y));
                            score++;

                            Sx = sRandX.Next(1, x - 1);
                            Sy = sRandX.Next(1, y - 1);
                        }
                        else if (i == snake[0].x && j == snake[0].y)
                        {
                            Console.Write("O");
                        }
                        else if (checkPlayertail(i, j))
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
                    }
                    Console.WriteLine();

                }

                updatePlayerTail();
                updatePlayerHead();

            }

            public void play()
            {
                Console.CursorVisible = false;

                int gameSpeed = 100;


                while (!this.gameOver)
                {
                    Console.WriteLine("score : " + this.score.ToString());
                    //for (int i = 0; i < snake.Count; i++)
                    //{
                    //    Console.Write(i + ":  " + snake[i].x + " " + snake[i].y + "   |");
                    //}

                    this.playGround();

                    if (this.key.currentKey == 1 || this.key.currentKey == -1)
                    {
                        Thread.Sleep(gameSpeed);
                    }
                    else
                    {
                        Thread.Sleep((int)(gameSpeed / 2));
                    }
                }

                Console.CursorVisible = true;
                Console.WriteLine("score : " + this.score.ToString());
                Console.WriteLine("game over!");
                Console.ReadKey();
            }
        }

        class floppyBirdGmae
        {

        }

        static void Main(string[] args)
        {



            while (true)
            {
                //Console.SetCursorPosition(0, 0);
                Console.Clear();

                Console.WriteLine("-----------welocme!-----------");
                Console.WriteLine();
                Console.WriteLine("snake game : 1");
                Console.WriteLine("exit : 0");
                Console.WriteLine();

                string choice = Console.ReadLine();

                Console.WriteLine(choice);

                switch (choice)
                {
                    case "1":
                        {
                            SnakeGame snakeGame = new SnakeGame();

                            snakeGame.play();

                            break;
                        }

                    case "0":
                        {
                            Environment.Exit(0);

                            break;
                        }
                }

            }


        }
    }
}