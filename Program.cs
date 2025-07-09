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
                        Inventory();


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
                return; // 0 누르면 돌아가기
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                Status();  // 잘못 누르면 다시 상태보기창으로 돌아가기
            }




        }

        public void Inventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 아이템이 없는 경우
                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("보유 중인 아이템이 없습니다.");
                }
                else
                {
                    // 아이템 목록 표시
                    foreach (Item item in player.Inventory)
                    {
                        string equippedMark = item.IsEquipped ? "[E]" : "";
                        Console.WriteLine($"{equippedMark}{item.GetItemInfo()}");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write("         >>          ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        EquipManagement();
                        break;
                    case "0":
                        return; // 메인 메뉴로 돌아가기
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        Inventory();
                        break;
                }
            }
        }

        public void EquipManagement()
        {
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("장착할 아이템이 없습니다.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 아이템 목록 표시 (번호 포함)
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Item item = player.Inventory[i];                 
                    string equippedMark = item.IsEquipped ? "[E]" : "";
                    Console.WriteLine($"- {i + 1} {equippedMark}{item.GetItemInfo()}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write("         >>          ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return; // 인벤토리 메뉴로 돌아가기
                }
                else if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= player.Inventory.Count)
                {
                    // 선택한 아이템 장착/해제
                    Item selectedItem = player.Inventory[itemIndex - 1];
                    player.EquipItem(selectedItem);
                    Console.ReadKey(); //  결과를 볼 수 있도록 대기
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadKey();
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
            public string ItemName { get; set; }  //아이템 이름
            public string Description { get; set; } // 아이템 설명
            public int AttackBonus { get; set; } // 추가 공격력
            public int DefenseBonus { get; set; } // 추가 방어력
            public bool IsEquipped { get; set; } // 장착여부


            public Item(string name, string description, int attackBonus = 0, int defenseBonus = 0)
            {
                ItemName = name;
                Description = description;
                AttackBonus = attackBonus;
                DefenseBonus = defenseBonus;
                IsEquipped = false;
            }


            public void Use()
            {
                Console.WriteLine("아이템 {0}을 사용했습니다.", ItemName);
            }



            public string GetItemInfo()
            {
                string info = "";

                if (AttackBonus > 0)
                    info = $"공격력 +{AttackBonus}";
                else if (DefenseBonus > 0)
                    info = $"방어력 +{DefenseBonus}";

                return $"{ItemName} | {info} | {Description}";
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

            public List<Item> Inventory { get; set; }

            public Player()
            {
                Inventory = new List<Item>();
            }


            public void UseItem(IUsable item)
            {
                item.Use();
            }

            public void EquipItem(Item item)
            {
                item.IsEquipped = !item.IsEquipped;

                // 장착 상태에 따라 능력치 조정
                if (item.IsEquipped)
                {
                    Attack += item.AttackBonus;
                    Defense += item.DefenseBonus;
                    Console.WriteLine($"{item.ItemName}을(를) 장착했습니다.");
                }
                else
                {
                    Attack -= item.AttackBonus;
                    Defense -= item.DefenseBonus;
                    Console.WriteLine($"{item.ItemName}을(를) 장착 해제했습니다.");
                }
            }
        }










    }
