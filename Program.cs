using FootbalTeamManager.Classes;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

internal class Program
{
    //We are using files for the teams, so we do not need to add them as separated project files (cleaner version)
    public static List<Nation> nations = new List<Nation>();
    public static Team team = new Team();

    private static void Main(string[] args)
    {
        ReadFromFiles();

        GenerateTeam();

        ChooseTeamCaptain();

        AskToChangePlayer();
    }

    public static void ReadFromFiles()
    {
        string[] filePaths = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Files\"));

        foreach (string filePath in filePaths)
        {
            string[] lines = File.ReadAllLines(filePath);
            var nation = new Nation();
            nation.Players = new List<Player>();
            nation.Name = Path.GetFileName(filePath).Split(".txt")[0];
            foreach (var line in lines)
            {
                var player = new Player();
                var splittedLine = line.Split(" ");
                player.PlayerNumber = splittedLine[0];
                player.Name = $"{splittedLine[1]} {splittedLine[2]}";
                player.Position = splittedLine[3];

                nation.Players.Add(player);
            }

            nations.Add(nation);
        }
    }

    private static void GenerateTeam()
    {
        Console.WriteLine($"\nEnter nation name(available nations: Argentina, England, France, Germany, Italy, Netherlands, Portugal, Spain):");
        string result = Console.ReadLine();

        foreach (var nation in nations)
        {
            if (result.ToLower() == nation.Name.ToLower()) // just in case someone enters "argentina" instead of "Argentina"
            {
                team.Name = result;

                for (int i = 0; i < 12; i++)
                {
                    var random = new Random();
                    var index = random.Next(0, nation.Players.Count - 1);

                    team.Players.Add(nation.Players[index]);

                    nation.Players.RemoveAt(index);
                }
            }
        }
        
        Console.WriteLine("Randomly generated team:");
        PrintPlayers();
    }

    private static void ChooseTeamCaptain()
    {
        bool isCaptainChosen = false;
        while (true)
        {
            Console.Write("\nPlease choose team captain by entering the player's number: ");
            var number = Console.ReadLine();
            for (int i = 0; i < team.Players.Count; i++)
            {
                if (team.Players[i].PlayerNumber == number)
                {
                    isCaptainChosen = true;
                    team.Players[i].IsCaptain = true;
                }
            }

            if(isCaptainChosen == true)
            {
                break;
            }
        }

        PrintPlayers();     
    }

    private static void AskToChangePlayer()
    {
        Console.Write("\nWould you like to change a player from this team with someone else?[Y/N]: ");
        var result = Console.ReadLine();
        if(result.ToLower() == "y")
        {
            Console.Write("\nPlease choose the player's number you want to switch:");
            var playerNumber = Console.ReadLine();
            foreach(var player in team.Players)
            {
                if (player.PlayerNumber == playerNumber)
                {
                    //we want to change only the player and not the position
                    Console.WriteLine("\nPlease enter on one line([player number] [player's first and last name]): ");
                    var newPlayerString = Console.ReadLine();
                    var newPlayer = new Player();
                    newPlayer.PlayerNumber = newPlayerString.Split(" ")[0];
                    newPlayer.Name = $"{newPlayerString.Split(" ")[1]} {newPlayerString.Split(" ")[2]}";
                    newPlayer.Position = player.Position;
                    newPlayer.IsCaptain = player.IsCaptain;

                    team.Players.Add(newPlayer);
                    team.Players.Remove(player);

                    Console.WriteLine($"Player {player.PlayerNumber} {player.Name} was switched with {newPlayer.PlayerNumber} {newPlayer.Name}");
                    break;
                }
            }
                PrintPlayers();
        }
        else
        {
            Console.WriteLine("\nNo player changed, your current team is:");
            PrintPlayers();
        }
    }

    private static void PrintPlayers()
    {
        Console.WriteLine("=======================================================================");

        Console.WriteLine($"Team Nation:{team.Name}");
        foreach (var player in team.Players)
        {
            string captainTitle = player.IsCaptain ? "[Captain]" : "";
            Console.WriteLine($"{player.PlayerNumber} {player.Name} {player.Position} {captainTitle}");
        }
    }
}