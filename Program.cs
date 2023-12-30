using System.Diagnostics;
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

            private const int space = 5;

            private const string keyUp = "W";
            private const string keyDown = "S";
            private const string keyRight = "D";
            private const string keyLeft = "A";

            private const string keySpace = "Spacebar";


            public int prevKey = 1;
            public System.ConsoleKeyInfo keyInfo;

            public int currentKey = 1;


            public void updateMovmentKey()
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    bool valid = keyInfo.Key.ToString() == keySpace || keyInfo.Key.ToString() ==  keyUp || keyInfo.Key.ToString() == keyDown || keyInfo.Key.ToString() == keyRight || keyInfo.Key.ToString() == keyLeft;
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
                        case keySpace: currentKey = space; break;
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

                int gameSpeed = 50;


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
            class Bird
            {
                public int x;
                public int y;

                private bool isTop = true;

                public Bird(int x , int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public void updateMovment(MovmentKeys key)
                {

                    if (key.currentKey == 5)
                    {
                        changeDir();
                        key.currentKey = 0;
                    }


                    if (isTop)
                    {
                        this.y--;
                    }
                    else
                    {
                        this.y++;
                    }
                }

                public void changeDir()
                {
                    this.isTop = !this.isTop;
                }

                public bool checkBird(int x , int y)
                {
                    if (this.x == x && this.y == y)
                    {
                        return true;
                    }

                    return false;
                }
            }

            class SolarPart
            {
                public int x;
                public int y;

                public SolarPart(int x , int y)
                {
                    this.x=x;
                    this.y=y;
            }

                }
            class Solar
            {
                public List<SolarPart> solarList = [];
                public int hight;
                public int x;

                public Random rnd = new Random();

                public int space;

                public Solar(int hight , int x) {

                    space = rnd.Next(6, hight - 6);

                    this.x = x;

                    for (int i = 0; i < hight; i++)
                    {
                        solarList.Add(new SolarPart(x , i));
                    }

                }

                public void updateSolar()
                {
                    x--;
                    for (int i = 0; i < solarList.Count; i++)
                    {
                        solarList[i].x = x;
                    }
                }

                private bool checkSpace(SolarPart part , int lenght)
                {

                    bool isSpace = false;

                    if (part.y == space)
                    {
                        isSpace = true;
                    }

                    for (int i = 1; i <= lenght; i++)
                    {
                        if (part.y == space + i || part.y == space - i)
                        {
                            isSpace = true; break;
                        }
                    }

                    
                    return !isSpace;
                }

                public bool checkSolar(int x , int y)
                {
                    for (int i = 0; i < solarList.Count; i++)
                    {
                        if (solarList[i].x == x && solarList[i].y == y && checkSpace(solarList[i] , 3))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            class Solars
            {
                Random rnd = new Random();

                List<Solar> solars = [];

                int spawnTimer = 0;

                public Solars()
                {
                    //spawnTimer = rnd.Next(5, 10);
                }

                public void updateSolars(int hight , int x)
                {
                    if (spawnTimer == 0)
                    {
                        solars.Add(new Solar(hight, x));
                        spawnTimer = rnd.Next(15, 40);
                    }
                    else
                    {
                        spawnTimer--;
                    }

                    if (solars.Count > 0)
                    {
                        if (solars[0].x < 1)
                        {
                            solars.RemoveAt(0);
                        }

                        for (global::System.Int32 i = 0; i < solars.Count; i++)
                        {
                            solars[i].updateSolar();
                        }
                    }

                }

                public bool checkSolars(int x, int y)
                {
                    for (int i = 0; i < solars.Count; i++)
                    {
                        if (solars[i].checkSolar(x , y))
                        {
                            return true;
                        }
                    }

                    return false;
                }



            }

            static int x = 70;
            static int y = 30;

            public bool gameOver = false;

            public int score = 0;

            public MovmentKeys key = new MovmentKeys();

            public int gameSpeed = 10;

            private Bird bird = new Bird(5, y / 2);

            Solars solars = new Solars();


            private void playGround()
            {

                Console.SetCursorPosition(0, 0);

                this.key.updateMovmentKey();


                bird.updateMovment(key);
                solars.updateSolars(y, x);

                for (int i = 0; i < y; i++)
                {
                    for (global::System.Int32 j = 0; j < x; j++)
                    {
                        if (i == 0 || i == y - 1 || j == 0 || j == x - 1)
                        {
                            Console.Write("#");
                        }
                        else if (solars.checkSolars(j , i))
                        {
                            Console.Write("=");
                        }
                        else if (bird.y == y || bird.y == 0 || solars.checkSolars(bird.x , bird.y))
                        {
                            gameOver = true;
                            break;
                        }
                        else if (bird.checkBird(j, i))
                        {
                            Console.Write(">");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

            }

            public void play()
            {
                Console.CursorVisible = false;
                

                while (!this.gameOver)
                {
                    Console.WriteLine("score : " + this.score.ToString());
                    //Console.WriteLine("solar x : " + solar.x.ToString());



                    this.playGround();

                    score++;

                    Thread.Sleep(gameSpeed);

                    //if (this.key.currentKey == 1 || this.key.currentKey == -1)
                    //{
                    //    Thread.Sleep(gameSpeed);
                    //}
                    //else
                    //{
                    //    Thread.Sleep((int)(gameSpeed / 2));
                    //}
                }

                Console.CursorVisible = true;
                Console.WriteLine("score : " + this.score.ToString());
                Console.WriteLine("game over!");
                Console.ReadKey();
            }
        }

        class spaceInvaderGmae
        {

            class Ship
            {
                public int x;
                public int y;

                public Ship(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }

                public void updateShip(MovmentKeys key , int width)
                {

                    if (key.currentKey == 2 && this.x != (width - 2))
                    {
                        this.x++;
                    }
                    else if (key.currentKey == -2 && this.x != 1)
                    {
                        this.x--;
                    }
                    key.currentKey = 0;


                }
            }


            class Bullet
            {
                public int x;
                public int y;

                public Bullet(int x , int y)
                {
                    this.x = x ; this.y = y;
                }

                public void updaeBullet()
                {
                    this.y--;
                }
            }

            class Bullets
            {
                List<Bullet> bullets = [];

                int delay = 0;

                public void addBullet(int x, int y)
                {
                    if (delay == 0)
                    {
                        bullets.Add(new Bullet(x, y));
                        delay = 2;
                    }
                    else
                    {
                        delay--;
                    }
                }

                public void uodateBullet(int y)
                {
                    if (bullets.Count > 1)
                    {
                        if (bullets[0].y >  y)
                        {
                            bullets.RemoveAt(0);
                        }
                    }
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].updaeBullet();
                    }
                }

                public bool checkBullets(int x, int y)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        if (bullets[i].x == x && bullets[i].y == y)
                        {
                            return true;
                        }
                    }

                    return false;
                }

            }


            class Enemy
            {
                public int x;
                public int y;

                public Enemy(int x, int y)
                {
                    this.x = x; this.y = y;
                }

                public void updateEnemy()
                {
                    this.y++;
                }
            }

            class Enemies
            {
                List<Enemy> enemies = [];

                int delay = 0;
                int updateDelay = 0;


                Random random = new Random();

                public void addEnemy(int x)
                {
                    int rnd = random.Next(1, x - 1);


                    if (delay == 0)
                    {
                        enemies.Add(new Enemy(rnd, 1));
                        delay = 5;
                    }
                    else
                    {
                        delay--;
                    }
                }

                public void updateEnemies(int y)
                {
                    if (enemies.Count > 1)
                    {
                        if (enemies[0].y > y)
                        {
                            enemies.RemoveAt(0);
                        }
                    }
                    if (updateDelay == 0)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].updateEnemy();
                        }
                        updateDelay = 4;
                    }
                    else
                    {
                        updateDelay--;
                    }
                }

                public bool checkEnemies(int x, int y)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].x == x && enemies[i].y == y)
                        {
                            return true;
                        }
                    }

                    return false;
                }

                public bool bulletCollisionCheck(int x , int y , bool bulletCheck)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].x == x && enemies[i].y == y && bulletCheck)
                        {
                            enemies.RemoveAt(i);
                            return true;
                        }
                    }
                    return false;
                }

            }



            public MovmentKeys key = new MovmentKeys();

            const int x = 40;
            const int y = 20;

            public int score = 0;

            public bool gameOver = false;

            private Ship ship = new Ship(x / 2, y - 3);
            private Bullets bullets = new Bullets();
            private Enemies enemies = new Enemies();


            public void playGround()
            {
                Console.SetCursorPosition(0, 0);



                key.updateMovmentKey();

                ship.updateShip(key , x);

                bullets.addBullet(ship.x , ship.y-1);

                bullets.uodateBullet(y);

                enemies.addEnemy(x);

                enemies.updateEnemies(y);


                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {


                        if (j == 0 || j == x - 1 || i == 0 || i == y - 1)
                        {
                            Console.Write("#");
                        }
                        else if (ship.x == j && ship.y == i)
                        {
                            Console.Write("^");
                        }
                        else if (enemies.checkEnemies(ship.x, ship.y))
                        {
                            Console.Write("*");
                            gameOver = true;
                        }
                        else if (ship.x == j && ship.y+1 == i)
                        {
                            Console.Write("-");
                        }
                        else if (bullets.checkBullets(j, i))
                        {
                            Console.Write("!");
                            if(enemies.bulletCollisionCheck(j, i, bullets.checkBullets(j, i)))
                            {
                                score++;
                            };
                        }
                        else if (enemies.checkEnemies(j,i))
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


            }


            public void play()
            {
                Console.CursorVisible = false;

                int gameSpeed = 10;


                while (!this.gameOver)
                {
                    Console.WriteLine("score : " + this.score.ToString());
                    Console.WriteLine("key : " + this.key.currentKey.ToString());
                    Console.WriteLine("x : " + this.ship.x.ToString() + "y : " + this.ship.y.ToString());


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

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 40);


            while (true)
            {
                //Console.SetCursorPosition(0, 0);
                Console.Clear();

                Console.WriteLine("-----------welocme-----------");
                Console.WriteLine();
                Console.WriteLine("snake game : 1");
                Console.WriteLine("floppy bird : 2");
                Console.WriteLine("space invader : 3");
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

                    case "2":
                        {
                            floppyBirdGmae floppyBird = new floppyBirdGmae();

                            floppyBird.play();

                            break;
                        }
                    case "3":
                        {
                            spaceInvaderGmae spaceInvader = new spaceInvaderGmae();

                            spaceInvader.play();

                            break;
                        }

                     case "9":
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey();


                            Console.WriteLine(keyInfo.Key.ToString());

                            Console.ReadKey();

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