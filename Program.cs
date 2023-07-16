using System;
using System.Collections.Generic;
using System.Linq;

namespace JustPlaying
{
    public class Bats
    {
        public static string Name = "Bats";
        public static int Hp = 14;
        public static int Dmg;
        public static int RewardGold = 4;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Wolves
    {
        public static string Name = "Wolves";
        public static int Hp = 24;
        public static int Dmg;
        public static int RewardGold = 6;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Dragons
    {
        public static string Name = "Dragons";
        public static int Hp = 75;
        public static int Dmg;
        public static double RewardDmgMultipliyer = 1.6;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class Goblins
    {
        public static string Name = "Goblins";
        public static int Hp = 29;
        public static int Dmg;
        public static int RewardGold = 9;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    public class RiftHerald
    {
        public static string Name = "RiftHerald";
        public static int Hp = 95;
        public static int Dmg;
        public static int RewardBonusHp = 35;
        public static int RewardMana;

        public static int Dmg_
        {
            get => Dmg;
            set => Dmg = value;
        }
    }

    internal class Program
    {
        private const double defaultHp = 100;
        private const double defaultMana = 100;
        private static int playerGold = 10;
        private static string[] availableChampions = { "Viego", "Ahri", "Jinx" };
        private static int currentLevel = 5;
        private static Character playerCharacter;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome \nSelect your character");
            Console.WriteLine("\\Available Champions/ \n:Viego - 1\n:Ahri - 2\n:Jinx - 3");

            var playerInput = ChampSelection();
            while (!availableChampions.Contains(playerInput))
            {
                Console.WriteLine("Choose another champion");
                playerInput = ChampSelection();
            }

            switch (playerInput)
            {
                case "Viego":
                    playerCharacter = new Character(defaultHp, 10, 70, "Blade of the Ruined King", "Spectral Maw",13 ,25, 7, 16);
                    Console.WriteLine("Great Choice");
                    break;
                case "Ahri":
                    playerCharacter = new Character(defaultHp, 12, 80, "Orb of Deception", "Charm",28,12, 20, 10);
                    Console.WriteLine("Great Choice");
                    break;
                case "Jinx":
                    playerCharacter = new Character(defaultHp, 8, 60, "Switchero!", "Zap!", 25, 14, 6, 10);
                    Console.WriteLine("Great Choice");
                    break;
            }

            while (currentLevel <= 5 && playerCharacter.Hp > 0)
            {
                Console.WriteLine($"Welcome to level {currentLevel}.");
                Console.WriteLine($"You are facing {GetEnemyName(currentLevel)}!");

                double enemyHp = GetEnemyHp(currentLevel);
                while (enemyHp > 0 && playerCharacter.Hp > 0)
                {
                    if (currentLevel == 5)
                    {
                        MiniBossFight();
                    }
                    var playerMove = PlayerMove().ToLower();
                    switch (playerMove)
                    {
                        case "q":
                            Console.WriteLine($"You used {playerCharacter.AbilityOne} - {playerCharacter.AbilityOneCost}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityOneDmg}");
                            enemyHp -= playerCharacter.AbilityOneDmg;
                            break;
                        case "w":
                            Console.WriteLine($"You used {playerCharacter.AbilityTwo}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityTwoDmg}");
                            enemyHp -= playerCharacter.AbilityTwoDmg;
                            break;
                        case "shop":
                            OpenShop();
                            continue;
                        default:
                            Console.WriteLine("Invalid move. Try again.");
                            continue;
                    }

                    if (enemyHp <= 0)
                    {
                        Console.WriteLine("The enemy is defeated");
                        playerGold += GetEnemyRewardGold(currentLevel);
                        RewardMana(GetEnemyName(currentLevel));
                        playerCharacter.Mana += GetEnemyRewardMana(currentLevel);
                        PassedLevel(GetEnemyRewardGold(currentLevel), GetEnemyRewardMana(currentLevel), playerCharacter.Hp);
                        if (GetEnemyName(currentLevel) == "Dragons")
                        {
                            Console.WriteLine("Choose the ability you want to upgrade!");
                            if (PlayerMove() == "q")
                            {
                                playerCharacter.AbilityOneDmg *= Dragons.RewardDmgMultipliyer;
                            }
                            else if(PlayerMove() == "w")
                            {
                                playerCharacter.AbilityTwoDmg *= Dragons.RewardDmgMultipliyer;
                            }
                        }
                    }
                    else
                    {
                        MonsterDamage(GetEnemyName(currentLevel));
                        playerCharacter.Hp -= GetEnemyDamage(currentLevel);
                        if (playerCharacter.Hp > 0)
                            Console.WriteLine($"You survived a hit for {GetEnemyDamage(currentLevel)}");
                        else
                            CharacterDied();
                    }
                }

                currentLevel++;
            }

            Console.WriteLine("Game Over");
        }

        private static void MiniBossFight()
        {
            Console.WriteLine("You've encountered a RiftHerald, do you want to fight it [y/n], or open shop ONCE!");
            if (PlayerMove().ToLower() == "shop")
            {
                OpenShop();
            }

            if (PlayerMove().ToLower() == "y")
            {
                double enemyHp = RiftHerald.Hp;
                while (true)
                {
                    var playerMove = PlayerMove().ToLower();
                    switch (playerMove)
                    {
                        case "q":
                            Console.WriteLine($"You used {playerCharacter.AbilityOne} - {playerCharacter.AbilityOneCost}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityOneDmg}");
                            enemyHp -= playerCharacter.AbilityOneDmg;
                            break;
                        case "w":
                            Console.WriteLine($"You used {playerCharacter.AbilityTwo}");
                            Console.WriteLine($"You dealt {playerCharacter.AbilityTwoDmg}");
                            enemyHp -= playerCharacter.AbilityTwoDmg;
                            break;
                        default:
                            Console.WriteLine("Invalid move. Try again.");
                            continue;
                    }
                    if (enemyHp <= 0)
                    {
                        Console.WriteLine("The Herald is defeated");
                        playerCharacter.Hp += RiftHerald.RewardBonusHp;
                        PassedLevel(GetEnemyRewardGold(currentLevel), GetEnemyRewardMana(currentLevel), playerCharacter.Hp);
                    }
                    else
                    {
                        MonsterDamage(GetEnemyName(currentLevel));
                        playerCharacter.Hp -= GetEnemyDamage(currentLevel);
                        if (playerCharacter.Hp > 0)
                            Console.WriteLine($"You survived a hit for {GetEnemyDamage(currentLevel)}");
                        else
                            CharacterDied();
                    }
                }
            }
        }

        private static string GetEnemyName(int level)
        {
            switch (level)
            {
                case 1:
                    return Bats.Name;
                case 2:
                    return Wolves.Name;
                case 3:
                    return Goblins.Name;
                case 4:
                    return Dragons.Name;
                case 5:
                    return RiftHerald.Name;
                default:
                    return string.Empty;
            }
        }

        private static int GetEnemyHp(int level)
        {
            switch (level)
            {
                case 1:
                    return Bats.Hp;
                case 2:
                    return Wolves.Hp;
                case 3:
                    return Goblins.Hp;
                case 4:
                    return Dragons.Hp;
                default:
                    return 0;
            }
        }

        private static int GetEnemyDamage(int level)
        {
            switch (level)
            {
                case 1:
                    return Bats.Dmg_;
                case 2:
                    return Wolves.Dmg_;
                case 3:
                    return Goblins.Dmg_;
                case 4:
                    return Dragons.Dmg_;
                case 5:
                    return RiftHerald.Dmg_;
                default:
                    return 0;
            }
        }

        private static int GetEnemyRewardGold(int level)
        {
            switch (level)
            {
                case 1:
                    return Bats.RewardGold;
                case 2:
                    return Wolves.RewardGold;
                case 3:
                    return Goblins.RewardGold;
                case 4:
                    return Dragons.RewardGold;
                default:
                    return 0;
            }
        }

        private static int GetEnemyRewardMana(int level)
        {
            switch (level)
            {
                case 1:
                    return Bats.RewardMana;
                case 2:
                    return Wolves.RewardMana;
                case 3:
                    return Goblins.RewardMana;
                case 4:
                    return Dragons.RewardMana;
                default:
                    return 0;
            }
        }

        private static void PassedLevel(int goldReward, int manaReward, double hpLeft)
        {
            Console.WriteLine($"Gold: {goldReward}");
            Console.WriteLine($"Mana: {manaReward}");
            Console.WriteLine($"HpLeft: {hpLeft}");
        }

        private static void CharacterDied()
        {
            Console.WriteLine("You died");
        }

        private static void RewardMana(string typeOfEnemy)
        {
            Random rand = new Random();
            int rewardMana = 0;

            switch (typeOfEnemy)
            {
                case "Bats":
                    rewardMana = rand.Next(14, 22);
                    Bats.RewardMana = rewardMana;
                    break;
                case "Wolves":
                    rewardMana = rand.Next(18, 30);
                    Wolves.RewardMana = rewardMana;
                    break;
                case "Dragons":
                    rewardMana = rand.Next(25, 40);
                    Dragons.RewardMana = rewardMana;
                    break;
                case "Goblins":
                    rewardMana = rand.Next(10, 18);
                    Goblins.RewardMana = rewardMana;
                    break;
            }

            Console.WriteLine($"You received {rewardMana} mana as a reward!");
        }

        private static void OpenShop()
        {
            Console.WriteLine("Welcome to the shop!");
            Console.WriteLine($"Your current gold: {playerGold}");
            Console.WriteLine("Available items:");
            Console.WriteLine("1. HealthPot - 5 gold");
            Console.WriteLine("2. ManaPot - 5 gold");
            Console.WriteLine("Enter the item number you wish to purchase (0 to exit):");

            string input = Console.ReadLine();
            string[] shopItems = new[] { "HealthPot", "ManaPot","LargeHealthPot"};
            if (shopItems.Contains(input))
            {
                int cost;
                int regen;
                switch (input)
                {
                    case "HealthPot":
                        cost = 5;
                        regen = 10;
                        if (DoesPlayerHasEnoughGold(playerGold, cost))
                        {
                            playerGold -= cost;
                            playerCharacter.Hp += regen;
                            Console.WriteLine("You received 10 hp!");
                        }
                        break;
                    case "ManaPot":
                    {
                        cost = 6;
                        regen = 11;
                        if (DoesPlayerHasEnoughGold(playerGold, cost))
                        {
                            playerGold -= cost;
                            playerCharacter.Mana += regen;
                            Console.WriteLine("You received 11 mana!");
                        }
                        break;
                    }
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Select a valid item!");
                        break;

                }
            }
        }

        private static bool DoesPlayerHasEnoughGold(int playerGold,int cost)
        {
           return playerGold >= cost;
        }


        private static string PlayerMove()
        {
            return Console.ReadLine()?.ToLower();
        }

        private static string ChampSelection()
        {
            return Console.ReadLine();
        }

        private static void MonsterDamage(string typeOfEnemy)
        {
            Random rand = new Random();
            switch (typeOfEnemy)
            {
                case "Bats":
                    Bats.Dmg_ = rand.Next(5, 11);
                    break;
                case "Wolves":
                    Wolves.Dmg_ = rand.Next(9, 16);
                    break;
                case "Dragons":
                    Dragons.Dmg_ = rand.Next(14, 24);
                    break;
                case "Goblins":
                    break;
                    Goblins.Dmg_ = rand.Next(6, 10);
                case "RiftHerald":
                    RiftHerald.Dmg_ = rand.Next(17, 29);
                    break;
            }
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public double Hp { get; set; }
        public double DMG { get; set; }
        public int Mana { get; set; }
        public string AbilityOne { get; set; }
        public string AbilityTwo { get; set; }
        public int AbilityOneCost { get; set; }
        public int AbilityTwoCost { get; set; }
        public double AbilityOneDmg { get; set; }
        public double AbilityTwoDmg { get; set; }

        public Character(double hp, double dMG, int mana,
            string abilityOne, string abilityTwo,
            int abilityOneConst, int abilityTwoCost,
            int abilityOneDmg, int abilityTwoDmg)
        {
            Name = "Kartof";
            Hp = hp;
            DMG = dMG;
            Mana = mana;
            AbilityOne = abilityOne;
            AbilityTwo = abilityTwo;
            AbilityOneCost = abilityOneConst;
            AbilityTwoCost = abilityTwoCost ;
            AbilityOneDmg = abilityOneDmg;
            AbilityTwoDmg = abilityTwoDmg;
        }
    }

}

