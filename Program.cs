using System;
using System.Collections.Generic;

namespace TextRpg
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game();


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
                        Status();
                        break;
                    case "2":
                        // 인벤토리 기능 구현 해야 함


                        break;
                    case "3":
                        // 상점 기능 구현 해야하함

                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }



        public void Status()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level:D2}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.Attack}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write("         >>          ");

            string input = Console.ReadLine();

            if (input == "0")
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
              
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
            public string Job { get; set; } = "전사";
            public int Level { get; set; } = 1;
            public int Attack { get; set; } = 10;
            public int Defense { get; set; } = 5;
            public int Health { get; set; } = 100;
            public int Gold { get; set; } = 1500;



            public void UseItem(IUsable item)
            {
                item.Use();
            }
        }










    }
}