Console.CursorVisible = false;


const int x = 25;
const int y = 40;

List<int> Px = new List<int>{x / 2 + 1};
List<int> Py = new List<int>{ y / 2 + 1};



Random sRandX = new Random();
Random sRandY = new Random();

int Sx = sRandX.Next(1, x-1);
int Sy = sRandX.Next(1, y-1);

ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
string key = "W";

int score = 0;

bool gameOver = false;

bool updatePlayerTail(int x , int y)
{
    bool Xx = false;
    bool Yy = false;
    
    for (int i = 0; i < Px.Count(); i++)
    {
        Xx = Px[i] == x;
    }
    for (int i = 0; i < Py.Count(); i++)
    {
        Yy = Py[i] == y;
    }

    return Xx && Yy;
}


void playGround(int x , int y)
{
    Console.SetCursorPosition(0, 0);

    if (Console.KeyAvailable)
    {
        keyInfo = Console.ReadKey(true);
        bool valid = keyInfo.Key.ToString() == "W" || keyInfo.Key.ToString() == "S" || keyInfo.Key.ToString() == "D" || keyInfo.Key.ToString() == "A";
        key = valid ? keyInfo.Key.ToString() : key;

    }


    for (int i = 0; i < x; i++)
    {
        for (int j = 0; j < y; j++)
        {


            if (i == 0 || i == x - 1 || j == 0 || j == y - 1)
            {
                Console.Write("#");
            }
            else if (Px[0] == 0 || Px[0] == x || Py[0] == 0 || Py[0] == y)
            {
                gameOver = true;
                break;
            }
            else if (i == Sx && j == Sy && i == Px[0] && j == Py[0])
            {
                Console.Write("O");
                score++;
                Px.Add(Px[score-1]);
                Py.Add(Py[score-1]);
                Sx = sRandX.Next(1, x - 1);
                Sy = sRandX.Next(1, y - 1);
            }
            else if (i == Px[0] && j == Py[0])
            {
                Console.Write("O");
            }
            else if (i == Sx && j == Sy)
            {
                Console.Write("*");
            }
            else if (updatePlayerTail(i,j))
            {
                Console.Write("0");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();

    }

    switch (key)
    {
        case "W":
            for (int i = 0; i < Px.Count(); i++)
            {
                Px[i]--;
            }
            break;
        case "S":
            for (int i = 0; i < Px.Count(); i++)
            {
                Px[i]++;
            }
            break;
        case "D":
            for (int i = 0; i < Py.Count(); i++)
            {
                Py[i]++;
            }
            break;
        case "A":
            for (int i = 0; i < Py.Count(); i++)
            {
                Py[i]--;
            }
            break;
        default: break;
    }
}


while (!gameOver)
{
    Console.WriteLine(key + "  " + score.ToString());
    for (global::System.Int32 i = 0; i < Px.Count(); i++)
    {
        Console.Write(Px[i] + " " + Py[i] + "   |");
    }
    playGround(x, y);
    Thread.Sleep(200);
    //Px++;
}




Console.WriteLine("game over!");