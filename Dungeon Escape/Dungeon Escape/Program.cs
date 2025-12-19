using System;
using System.Runtime.CompilerServices;

namespace Dungeon_Escape;
//  기본 규칙
// 플레이어는 상하좌우로 이동 가능
// 벽(#)은 통과 불가
// 문(r, b)은 같은 색 열쇠가 있어야 통과 가능
// 열쇠는 획득 즉시 보유 상태가 됨
// 출구(E)에 도달하면 게임 클리어

// | 기호  | 의미    |
// | --- | ----- |
// | `#` | 벽     |
// | `P` | 플레이어  |
// | `R` | 빨간 열쇠 |
// | `B` | 파란 열쇠 |
// | `r` | 빨간 문  |
// | `b` | 파란 문  |
// | `E` | 출구    |
// | ` ` | 빈 공간  |

class Program
{
    private static char Wall = '#';
    private static char PLAYER = 'P';
    private static char R_Key = 'R';
    private static char B_Key = 'B';
    private static char R_Door = 'r';
    private static char B_Door = 'b';
    private static char Exit = 'E';
    private static char Empty = ' ';
    
    // 맵 초기값
    private static char[,] Map = new char[,]
    {
        { '#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
        { '#','P',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','E','#'},
        { '#','#','#',' ','#','#','#','#',' ','#',' ','#','#','#'},
        { '#',' ',' ',' ',' ','R',' ',' ','#',' ',' ',' ','r','#'},
        { '#',' ','#','#','#','#','#','#','#',' ','#','#','#','#'},
        { '#',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#'},
        { '#','#','#','#','#',' ','#','#','#',' ','#','#','#',' '},
        { '#',' ',' ',' ','B',' ',' ',' ','#',' ',' ',' ','b','#'},
        { '#',' ','#','#','#',' ','#','#','#','#','#','#','#','#'},
        { '#',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        { '#','#','#',' ','#',' ','#','#','#','#','#','#','#','#'},
        { '#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','#'},
        { '#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
    };

    // 플레이어 좌표 초기값
    private static Position _playerPos = new Position()
    {
        X = 1,
        Y = 1
    };
    
    
    static void Main(string[] args)
    {
        // 가이드 제시
        GuideText();
        
        while (true)
        {
            // 커서 위치 지정
            Console.SetCursorPosition(1, 4);
            // 맵 프린트
            PrintMap();
            
            // 탈출 성공시 반복 탈출
            if (IsClear(Map))
            {
                break;
            }

            // 이동로직 
            // 다음 좌표
            NextPosition(Console.ReadKey().Key);
            
            // 벽 X 문은 같은 색의 열쇠가 있어야 통과

            // 문에 접근시
            // 열쇠 없을 때, 있을 때

            // 플레이어 좌표 이동 좌표로 초기화
            
        }
        // 게임 끝
        Console.ReadKey();
    }

    static void GuideText()
    {
        Console.Clear();
        Console.WriteLine("w/a/s/d 로 이동하세요");
        Console.WriteLine("문과 같은 색의 열쇠로 문을 열고 탈출구로 탈출하세요");
        Console.WriteLine();
    }
    
    static void PrintMap()
    {
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                Console.Write(Map[i, j]);
            }
            Console.WriteLine();
        }
    }

    static bool IsClear(char[,] Map)
    {
        foreach (char tile in Map)
        {
            if (tile == Exit)
            {
                return false;
            }
        }
        return true;
    }

    static Position NextPosition (ConsoleKey inputKey)
    {
        int newX = _playerPos.X;        // 인자값을 주고 있지 않지만 static 필드의 변수를 가져와서 사용 가능
        int newY = _playerPos.Y;        // Main 함수의 반복문이 끝날 때마다 해당 변수를 초기화 해주고 있음.
        
        if (inputKey == ConsoleKey.W) newY--;
        else if (inputKey == ConsoleKey.A) newX--;
        else if (inputKey == ConsoleKey.S) newY++;
        if (inputKey == ConsoleKey.D) newX++;
        
        return new Position
            {
                X = newX,
                Y = newY
            };
    }

    static void Move(Position pos, Position nextPos)
    {
        // 타일 변경 로직
    }
    
    struct Position
    {
        public int X;
        public int Y;
    }
    
}