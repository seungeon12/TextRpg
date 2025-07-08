using System;
using System.Collections.Generic;

namespace TextRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            // 게임 객체 생성
            Game game = new Game();

            // 게임 시작
            game.Start();
        }
    }

    class Game
    {
        private Player player;

        public void Start()
        {
            Console.WriteLine("용사의 이름을 알려주시오.");
            string playerName = Console.ReadLine();

            // 플레이어 객체 생성 및 이름 설정
            player = new Player();
            player.Name = playerName;

            // 메인 메뉴 표시
            MainMenu();
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear(); // 화면 지우기

                Console.WriteLine($"{player.Name}, 스파르타 마을에 오신 용사여 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write("         >>          ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // 상태 보기 기능 구현
                        Console.WriteLine("상태 보기를 선택했습니다.");
                        Console.ReadKey(); // 아무 키나 누를 때까지 대기
                        break;
                    case "2":
                        // 인벤토리 기능 구현
                        Console.WriteLine("인벤토리를 선택했습니다.");
                        Console.ReadKey();
                        break;
                    case "3":
                        // 상점 기능 구현
                        Console.WriteLine("상점을 선택했습니다.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

    public interface IUsable
    {
        void Use();
    }

    public class Item : IUsable
    {
        public string ItemName { get; set; }

        public void Use()
        {
            Console.WriteLine("아이템 {0}을 사용했습니다.", ItemName);
        }
    }

    public class Player
    {
        public string Name { get; set; }

        public void UseItem(IUsable item)
        {
            item.Use();
        }
    }

    








}