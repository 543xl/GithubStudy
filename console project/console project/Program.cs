using System;
using System.Net.Security;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Xsl;

namespace console_project;

class Program
{
    // 소코반 게임 구현
    // 1. 기본 구성
    // 2. 로직 수행
    
    // 심볼 선언
    private const char PLAYER = 'P';             // 플레이어
    private const char BOX = 'B';                       // 택배
    private const char ADDRESS = 'D';                   // 주소
    private const char ARRiVE = 'R';                    // 택배 도착
    private const char PLAYER_WITH_BOX = 'W';           // 택배 들고 있는 플레이어
    private const char PLAYER_IN_ADDRESS = '@';         // 플레이어만 주소에
    private const char PLAYER_WITH_BOX_IN_STORAGE = '!'; // 플레이어가 박스를 들고 창고 = 박스 들고 있는 플레이어
    private const char WALL = '#';
    private const char EMPTY = ' ';

    private static char[,] map = new char[,]   // 맵 생성
    {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'D', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'B', 'P', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', 'D', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', 'B', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    }; 
    

    private static char[,] map1 = new char[,]
    {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'D', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', 'B', 'P', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', 'D', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', 'B', ' ', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[,] map2 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', '#', ' ', 'D', ' ', ' ', '#' },
            { '#', ' ', 'B', ' ', '#', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', 'P', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', 'D', '#' },
            { '#', ' ', ' ', ' ', ' ', 'B', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };
            
    private static char[,] map3 = new char[,]
    {
            { '#','#','#','#','#','#','#','#','#','#' },
            { '#',' ',' ',' ',' ',' ',' ',' ',' ','#' },
            { '#',' ',' ','#','#','#',' ',' ',' ','#' },
            { '#',' ',' ','#','D','#',' ',' ',' ','#' },
            { '#',' ','B',' ',' ',' ',' ','#',' ','#' },
            { '#',' ',' ',' ','P',' ',' ','#','D','#' },
            { '#',' ',' ','#','#','#',' ',' ',' ','#' },
            { '#',' ',' ',' ',' ','B',' ',' ',' ','#' },
            { '#',' ',' ',' ',' ',' ',' ',' ',' ','#' },
            { '#','#','#','#','#','#','#','#','#','#' },
    };
    private static char[,] map4 = new char[,]
    {
            { '#','#','#','#','#','#','#','#','#','#' },
            { '#',' ',' ',' ',' ',' ',' ',' ',' ','#' },
            { '#',' ','#','#','#','#','#',' ',' ','#' },
            { '#',' ','#',' ',' ',' ','#',' ','D','#' },
            { '#',' ','#',' ','B',' ','#',' ',' ','#' },
            { '#',' ','#',' ',' ','P','#',' ',' ','#' },
            { '#',' ','#',' ','B',' ','#',' ','D','#' },
            { '#',' ','#','#',' ','#','#',' ',' ','#' },
            { '#',' ',' ',' ',' ',' ',' ',' ',' ','#' },
            { '#','#','#','#','#','#','#','#','#','#' },
    };

    private static char[,] map5= new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', '#', '#', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', 'D', '#', ' ', '#', ' ', 'D', ' ', '#' },
            { '#', ' ', ' ', '#', 'B', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', 'P', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', '#', 'B', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[,] map6 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', ' ', '#', '#', '#', ' ', '#' },
            { '#', ' ', '#', 'D', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', 'B', ' ', '#', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', 'P', '#', 'D', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', ' ', ' ', 'B', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', '#', '#', '#', '#', '#', '#', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[,] map7 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', '#', ' ', '#', '#', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', '#', ' ', '#', 'D', ' ', ' ', '#' },
            { '#', ' ', '#', 'B', ' ', '#', ' ', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', '#', '#', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', '#', 'D', '#', ' ', 'B', '#', 'P', '#' },
            { '#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[,] map8 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', '#', '#', '#', ' ', ' ', '#' },
            { '#', ' ', '#', 'D', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', ' ', 'B', ' ', '#', ' ', '#', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', 'P', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', '#', ' ', ' ', '#', 'B', ' ', 'D', '#' },
            { '#', ' ', ' ', ' ', '#', '#', ' ', '#', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };


    private static char[,] map9 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', 'B', ' ', '#', 'D', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', 'P', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', '#', '#', '#', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', 'D', ' ', ' ', ' ', ' ', 'B', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', '#', '#', '#', '#', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[,] map10 = new char[,]
    {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', ' ', '#', '#', '#', '#', ' ', '#', '#', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', '#', ' ', '#', 'D', ' ', ' ', '#' },
            { '#', ' ', '#', ' ', 'B', '#', ' ', '#', ' ', '#', ' ', '#' },
            { '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', '#', '#', '#', '#', '#', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', 'B', ' ', 'D', '#' },
            { '#', ' ', '#', '#', '#', ' ', ' ', '#', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', '#', ' ', 'P', ' ', ' ', '#', ' ', '#' },
            { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
    };

    private static char[][,] stages = new char[][,]
        { map1, map2, map3, map4, map5, map6, map7, map8, map9, map10 };
    
    private static POSITION _playerPos = new POSITION()  // 플레이어 위치 초기값
    {
        X = 5,
        Y = 4
    };
    
    private static int _movecount = 0;   // static 변수 선언 시 이름에 _를 넣는 이유는 구분용. 알아보기 쉽게.

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        for (int i = 0; i < stages.Length; i++)
        {
            // 단계가 순차적으로 증가하면서 맵을 불러오기
            LoadMap(i);
            
            // 게임 안내
            GameGuide(i+1);
            
            while (true)
            {
                // 로직 수행
                // 먼저 이동부터 구현
                Console.SetCursorPosition(0, 4);
                Console.WriteLine($"이동 거리 : {_movecount}");
                PrintMap();
                // 커서 위치를 잡아주고 거기에서 반복 프린트를 해서 같은 자리에 덮어쓰기 되는 것.

                // 게임 성공 시
                if (IsClear(map))
                {
                    PrintClear();
                    break;
                }

                // 입력된 키를 인식하게 변수 선언 초기화
                ConsoleKey inputKey = Console.ReadKey(true).Key; 
                // ConsoleKeyInfo.Key = ConsoleKey 이고 Console.ReadKey().key = ConsoleKey 임.
                // ConsoleKeyInfo 구조체에서 Key값을 불러오는 것.
                
                // 입력된 키가 아닌 경우 반목문 처음으로
                if (CantKey(inputKey)) continue;

                // 게임 종료 Q 입력 시
                if (inputKey == ConsoleKey.Q) break;

                // 다음 위치 좌표 초기화
                POSITION nextPos = GetNextPos(inputKey);

                // 택배가 이미 도착한 곳은 X, 벽 이상 X
                if (!CanMove(nextPos)) continue;

                // 현재 위치에서 다음 위치로 이동 실행
                char status = GetTile(_playerPos); // 현재 상태 초기화
                if (status == PLAYER || status == PLAYER_WITH_BOX) // 현재 상태가 박스를 들고 있는지 아닌지
                {
                    Move(_playerPos, nextPos, status);
                    _movecount++;

                }
                else
                {
                    Move(_playerPos, nextPos, PLAYER); // 배달 성공 시 PLAYER만 주소지에서 나오도록 상태를 지정.
                    _movecount++;
                }

                // 플레이어 위치 이동한 위치로 초기화
                _playerPos = nextPos;
            }
            Console.WriteLine("게임을 재시작하려면 아무 키나 눌러주세요");
            Console.WriteLine("게임을 종료하려면 Q를 눌러주세요");
            if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
        }
        Console.WriteLine();
        Console.WriteLine("게임 종료");
        Console.ReadKey(true);
    }

    static void GameGuide(int stage)
    {
        Console.Clear();
        Console.WriteLine("이동 : W/A/S/D, Q : 종료");
        Console.WriteLine("택배를 주소지에 배달해 주세요");
        Console.WriteLine($"{stage} 단계");
        Console.WriteLine();
    }

    static bool IsClear(char[,] map)
    {
        foreach (char tile in map)
        {
            if (tile == BOX || tile == ADDRESS) return false;
        }
        return true;
    }

    static void PrintClear()
    {
        Console.WriteLine();
        Console.WriteLine("축하합니다. 게임을 클리어 하셨습니다.");
    }

    static void LoadMap(int i)
    {
        // char 배열의 맵을 stages 배열의 맵과 같은 주소를 가리키게함.
        char[,] nowMap = stages[i];
        for (int y = 0; y <  nowMap.GetLength(0); y++)               
        {
            for (int x = 0; x <  nowMap.GetLength(1); x++)
            {
                map[y, x] =  nowMap[y, x];          // map 배열로 값 복사
                if (map[y, x] == PLAYER)
                {
                    _playerPos.X = x; 
                    _playerPos.Y = y;
                }
            }
        }
        _movecount = 0;
    }
    
    static bool CantKey(ConsoleKey inputKey)
    {
        if (!(inputKey == ConsoleKey.W ||
              inputKey == ConsoleKey.A ||
              inputKey == ConsoleKey.S ||
              inputKey == ConsoleKey.D ||
              inputKey == ConsoleKey.Q
            )) return true;
        return false;
    }

    static POSITION GetNextPos(ConsoleKey inputKey)
    {
        int newX = _playerPos.X;      // 다음 좌표를 플레이어 좌표에서 잡는데 아 마지막에 플레이어 좌표를 초기화 하니까 
        int newY = _playerPos.Y;      // 지역 변수여서 초기화 값을 함수 인자값으로 따로 안줘도 되는 것??
        
        if (inputKey == ConsoleKey.W) newY--;       // playerPos를 직접 증감하게 되면 newX,Y가 변화가 없음.
        else if (inputKey == ConsoleKey.A) newX--;  // 기존 값은 그대로 두고 값을 대입한 새 변수를 이용해서 다음 좌표 얻음.
        else if (inputKey == ConsoleKey.S) newY++;
        else if (inputKey == ConsoleKey.D) newX++; // wasd 플레이어 좌표는 변경

        return new POSITION()
        {
            X = newX,
            Y = newY
        };
    }
    
    static char OriginTile(char tile)
    {
        return tile switch
        {
            PLAYER => EMPTY,
            PLAYER_IN_ADDRESS => ADDRESS,
            PLAYER_WITH_BOX_IN_STORAGE => ARRiVE,
            PLAYER_WITH_BOX => EMPTY,
            _ => tile
        };
    }
    
    static char GetTile(POSITION pos)
    {
        return map[pos.Y, pos.X];
    }

    static void SetTile(POSITION pos, char tile)
    {
        map[pos.Y, pos.X] = tile;
    }

    static char ChangeTile(char nextTile, char originTile)
    {
        if (nextTile == EMPTY)
        {
            if (originTile == PLAYER_WITH_BOX)
            {
                return PLAYER_WITH_BOX;
            }
            return PLAYER;
        }
        else if (nextTile == BOX) return PLAYER_WITH_BOX;
        else if (nextTile == ADDRESS)
        {
            if (originTile == PLAYER_WITH_BOX)
            {
                return ARRiVE;
            }
            return PLAYER_IN_ADDRESS;
        }
        return PLAYER;
    }

    static bool CanMove(POSITION nextPos)
    {
        if (nextPos.X <= 0 || nextPos.X >= map.GetLength(1) - 1 || nextPos.Y <= 0 ||
            nextPos.Y >= map.GetLength(0) - 1
            || GetTile(nextPos) == ARRiVE || GetTile(nextPos) == WALL) return false;
        return true;
    }

    static void Move(POSITION pos, POSITION nextPos, char status)
    {
        // 기존타일 초기화
        if (status == PLAYER)
        {
            SetTile(pos, OriginTile(GetTile(pos)));
            SetTile(nextPos, ChangeTile(GetTile(nextPos), GetTile(pos)));
        }
        else if (status == PLAYER_WITH_BOX)
        {
            SetTile(pos, OriginTile(GetTile(pos)));
            // 다음 타일 변경
            SetTile(nextPos, ChangeTile(GetTile(nextPos), PLAYER_WITH_BOX));
        }
        else if (status == ARRiVE)
        {
            SetTile(pos, OriginTile(GetTile(pos)));
            SetTile(pos, ChangeTile(GetTile(nextPos), PLAYER));
        }
    }

    static void PrintMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                char tile = map[i, j];
                if (tile == PLAYER) Console.Write("🙂");
                else if (tile == PLAYER_WITH_BOX) Console.Write("🤹");
                else if (tile == BOX) Console.Write("📦");
                else if (tile == ADDRESS) Console.Write("🎯");
                else if (tile == ARRiVE) Console.Write("✅");
                else if (tile == WALL) Console.Write("🧱");
                else if (tile == EMPTY) Console.Write("  ");
                else if (tile == PLAYER_IN_ADDRESS) Console.Write("😎");
            }
            Console.WriteLine();
        }
    }
}

struct POSITION // 위치 좌표 구조체
{
    public int X;
    public int Y;
}
