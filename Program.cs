using System;


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


            // 플레이어 생성 및 이름 설정
            player = new Player();
            player.Name = playerName;

            // 메인 메뉴로 가기
            MainMenu();



        }

        public void MainMenu()
        {
            while (true) //무한루프
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
                        Shop();
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break; // 나중에는 게임 종료도 따로 만들어야함
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
            Console.WriteLine($"공격력 :  = {player.TotalAttack}");
            Console.WriteLine($"방어력 :  = {player.TotalDefense}");
            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write("         >>          ");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("\n[장착 중인 아이템]");
            bool hasEquippedItems = false;
            foreach (Item item in player.Inventory)
            {
                if (item.IsEquipped)

                {
                    hasEquippedItems = true;
                    Console.WriteLine($"- {item.ItemName}: 공격력 +{item.AttackBonus}, 방어력 +{item.DefenseBonus}");
                }
            }

            if (!hasEquippedItems)
            {
                Console.WriteLine("장착 중인 아이템이 없습니다.");
            }

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
                        string equippedMark = item.IsEquipped ? "[장착중]" : "";
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
                    string equippedMark = item.IsEquipped ? "[장착중]" : "";
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




        public struct ShopItem
        {
            public Item Item;
            public int Price;

            public ShopItem(string name, string description, int attackBonus, int defenseBonus, int price)
            {
                Item = new Item(name, description, attackBonus, defenseBonus) { IsEquipped = false };
                Price = price;
            }
        }

        public void Shop()
        {
            // 상점 아이템 배열 생성
            ShopItem[] shopItems = new ShopItem[]
            {
                new ShopItem("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000),
                new ShopItem("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 2000),
                new ShopItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3500),
                new ShopItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 600),
                new ShopItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500),
                new ShopItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 2000)
            };

            // 이미 구매한 아이템 표시용
            HashSet<string> purchasedItems = new HashSet<string>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 구매할 수 있습니다.");
                Console.WriteLine($"보유 골드: {player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                // 상점 아이템 목록 표시
                for (int i = 0; i < shopItems.Length; i++)
                {
                    ShopItem shopItem = shopItems[i];
                    string itemInfo = shopItem.Item.GetItemInfo();

                    // 이미 구매한 아이템인지 확인
                    if (purchasedItems.Contains(shopItem.Item.ItemName))
                    {
                        Console.WriteLine($"- {itemInfo} | 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($"- {i + 1} : {itemInfo} | {shopItem.Price} G");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("구매하실 아이템을 입력해주세요.");
                Console.Write("         >>          ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }
                else if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= shopItems.Length)
                {
                    // 선택한 아이템
                    ShopItem selectedShopItem = shopItems[itemIndex - 1]; // 아이템 인덱스는 0부터 시작하니까 -1
                    Item selectedItem = selectedShopItem.Item;

                    // 이미 구매한 아이템인지 확인
                    if (purchasedItems.Contains(selectedItem.ItemName))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Console.ReadKey();
                        continue;
                    }

                    int price = selectedShopItem.Price;


                    if (player.Gold >= price) // 골드가 충분한지 확인
                    {
                        // 아이템 복사본 생성
                        Item newItem = new Item
                        (
                            selectedItem.ItemName,
                            selectedItem.Description,
                            selectedItem.AttackBonus,
                            selectedItem.DefenseBonus
                        );


                        newItem.IsEquipped = false;  // 구매한 아이템은 장착되지 않은 상태로 시작

                        // 인벤토리에 추가하고 골드 차감
                        player.Inventory.Add(newItem);
                        player.Gold -= price;
                        purchasedItems.Add(selectedItem.ItemName);

                        Console.WriteLine($"{selectedItem.ItemName}을(를) 구매했습니다.");
                        Console.WriteLine($"남은 골드: {player.Gold} G");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                        Console.ReadKey();
                    }
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
        public int BaseAttack { get; set; } = 10; // 기본 공격력
        public int BaseDefense { get; set; } = 5; // 기본 방어력
        public int TotalAttack { get; set; }
        public int TotalDefense { get; set; }
        public int Health { get; set; } = 100;
        public int Gold { get; set; } = 1500;
        public int AttackBonus { get; set; }


        public int DefenseBonus { get; set; }

        public List<Item> Inventory { get; set; }

        public Player()
        {
            Inventory = new List<Item>();
            BaseAttack = 10; // 초기 기본 공격력 설정
            BaseDefense = 5; // 초기 기본 방어력 설정
            TotalAttack = BaseAttack;
            TotalDefense = BaseDefense;
            AttackBonus = 0;
            DefenseBonus = 0;


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
                TotalAttack += item.AttackBonus;
                TotalDefense += item.DefenseBonus;
                Console.WriteLine($"{item.ItemName}을(를) 장착했습니다.");
            }
            else
            {
                TotalAttack -= item.AttackBonus;
                TotalDefense -= item.DefenseBonus;
                Console.WriteLine($"{item.ItemName}을(를) 장착 해제했습니다.");
            }
        }


    }










}
