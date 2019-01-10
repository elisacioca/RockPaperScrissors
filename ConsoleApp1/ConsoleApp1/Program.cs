using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RockPaperScrissors
{
    public struct State
    {
        public int move_player1;
        public int move_player2;
        public int score_player1;
        public int score_player2;
    }

    

    public class RockPaperScrissors
    {
        public static State[] states = new State[15];
        public static State final_state = new State { };
        public static int contor = 3;

        public static void  Initialize()
        {
            for (int i = 0; i < 15; i++)
            {
                states[i].move_player1 = 0;
                states[i].move_player2 = 0;
                states[i].score_player1 = 0;
                states[i].score_player2 = 0;
            }
        }

        public static void Finalize()
        {
            final_state.move_player1 = 0;
            final_state.move_player2 = 0;
            final_state.score_player1 = 8;
            final_state.score_player2 = 8;
        }

        public static bool IsFinal(int round)
        {
            for (int i = 0; i < round; i++)
                if (states[i].score_player1 == final_state.score_player1 || states[i].score_player2 == final_state.score_player2 || round==15)
                    return true;
            return false;
        }

        public static void DisplayStates()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(/*"Move of player 1: ", */states[i].move_player1);
                Console.WriteLine(/*"Move of player2: ",*/ states[i].move_player2);
                Console.WriteLine(/*"Score player 1: ",*/ states[i].score_player1);
                Console.WriteLine(/*"Score player 2: ",*/ states[i].score_player2);
            }
        }

        public static void Transition(int round, int winningPlayer, int move1, int move2)
        {
            states[round].move_player1 = move1;
            states[round].move_player2 = move2;

            if (round == 0 && winningPlayer == 1)
            {
                states[round].score_player1 = 1;
                states[round].score_player2 = 0;
            }
            if (round == 0 && winningPlayer == 2)
            {
                states[round].score_player1 = 0;
                states[round].score_player2 = 1;
            }

            if (winningPlayer == 1 & round != 0)
            {
                states[round].score_player1 = states[round - 1].score_player1 + 1;
                states[round].score_player2 = states[round - 1].score_player2;
            }
            if (winningPlayer == 2 && round != 0)
            {
                states[round].score_player1 = states[round - 1].score_player1;
                states[round].score_player2 = states[round - 1].score_player2 + 1;
            }

        }

        public static string IntToStringMove(int contor)
        {
            if (states[contor].move_player1 == 1) return "r";
            if (states[contor].move_player1 == 2) return "p";
            else return "s";
        }

        public static string FindInPattern(string[] patterns, string myPattern, int nor, int nop, int nos)
        {
            int noR = 0;
            int noP = 0;
            int noS = 0;

            for (int i = 3; i < patterns.Length; i++)
            {
                foreach (Match match in Regex.Matches(patterns[i], myPattern))
                {
                    Console.WriteLine(patterns[i].Substring(match.Index, myPattern.Length));
                    if (patterns[i].Length != match.Index + myPattern.Length)
                    {
                        if (patterns[i].Substring(match.Index + myPattern.Length + 1, 1) == "r") noR++;
                        if (patterns[i].Substring(match.Index + myPattern.Length + 1, 1) == "p") noP++;
                        if (patterns[i].Substring(match.Index + myPattern.Length + 1, 1) == "s") noS++;
                    }
                }
            }

            if (noR == 0 && noP == 0 && noS == 0)
            {
                Console.WriteLine("Nu am gasit pattern.");
                return "false";
            }

            string winner="";
            if (noR >= noP && noR >= noS) winner = "r";
            if (noS >= noP && noS >= noR) winner = "s";
            if (noP >= noR && noP >= noS) winner = "p";
            nor = noR; nop = noP; nos = noS;
            return winner;

        }

        public static string IsPattern(int round, string[] patterns)
        {
            /* int ok2 = 0, ok3 = 0, ok4 = 0, ok5 = 0;
             if (patterns.Length >= 4) ok2 = 1;
             if (patterns.Length >= 5) ok3 = 1;
             if (patterns.Length >= 6) ok4 = 1;
             if (patterns.Length >= 7) ok5 = 1;
             if (round == 2 && ok2 != 0)
             {
                 string[] list2 = patterns[4].Split(' ');
                 List<int> no2 = new List<int>();
                 for (int i = 0; i < list2.Length; i++)
                 {
                     if (list2[i].Length == 6) no2.Add(Int32.Parse(list2[i].Substring(4, 1)));
                     else no2.Add(Int32.Parse(list2[i].Substring(4, 2)));
                     list2[i] = list2[i].Substring(0, 3);
                 }
             }

             if (round == 3 && ok3 != 0)
             {
                 string[] list2 = patterns[4].Split(' ');
                 List<int> no2 = new List<int>();
                 for (int i = 0; i < list2.Length; i++)
                 {
                     if (list2[i].Length == 6) no2.Add(Int32.Parse(list2[i].Substring(4, 1)));
                     else no2.Add(Int32.Parse(list2[i].Substring(4, 2)));
                     list2[i] = list2[i].Substring(0, 3);
                 }
                 string[] list3 = patterns[5].Split(' ');
                 List<int> no2 = new List<int>();
                 for (int i = 0; i < list2.Length; i++)
                 {
                     if (list2[i].Length == 6) no2.Add(Int32.Parse(list2[i].Substring(4, 1)));
                     else no2.Add(Int32.Parse(list2[i].Substring(4, 2)));
                     list2[i] = list2[i].Substring(0, 3);
                 }
             }
             if (round == 4 && ok4 != 0)
             {
             }
             if (round >= 5 && ok5 != 0)
             {
             }
             */
            if (round < 3) return "false";

            int noS = 0, noP = 0, noR = 0;
            int nos = 0, nop = 0, nor = 0;
            for (int j = 2; j <= 5 && (round-j)>=0 ; j++)
            {
                Console.WriteLine("Pattern de " + j + " miscari");
                string myPattern = "";
                for (int i = round - j; i < round; i++)
                {
                    if (myPattern == "") myPattern = IntToStringMove(i);
                    else myPattern = myPattern + "-" + IntToStringMove(i);
                }
                Console.WriteLine("My pattern: " + myPattern);
                
                string winner = FindInPattern(patterns, myPattern, noR, noP, noS);
                if (winner == "false") return winner;
                nos = nos + noS;
                nop = nop + noP;
                nor = nor + noR;
            }

            string chosen = "";
            if (noR >= noP && noR >= noS) chosen = "r";
            if (noS >= noP && noS >= noR) chosen = "s";
            if (noP >= noR && noP >= noS) chosen = "p";
            nor = noR; nop = noP; nos = noS;
            return chosen;
        }

        public static int Oponent(string anticipation)
        {
            if (anticipation == "r") return 2;
            if (anticipation == "p") return 3;
            if (anticipation == "s") return 1;
            return 0;
        }

        public static int Oponent(int move)
        {
            if (move == 1) return 2;
            if (move == 2) return 3;
            if (move == 3) return 1;
            return 0;
        }

        public static int WhoIsWinner(int move1, int move2)
        {
            if (Oponent(move1) == move2) return 2;
            if (Oponent(move2) == move1) return 1;
            if (move1 == move2) return 0;
            return -1;
        }

        //cu random
        public static void EasyStrategy(string path)
        {
            int round = 0;
            int player2_move;
            while (!IsFinal(round))
            {
                string move;
                Console.WriteLine("\nRock? (r) Paper? (p) Scrissors? (s)");
                Random rnd = new Random();
                player2_move = rnd.Next(1, 4);

                move = Console.ReadLine();
                if (move != "s" && move != "p" && move != "r")
                {
                    Console.WriteLine("Invalid move!");
                    continue;
                }
                UpdateMove(path, move, round);

                string move2 = "";
                if (player2_move == 1) move2 = "r";
                if (player2_move == 2) move2 = "p";
                if (player2_move == 3) move2 = "s";

                Console.WriteLine("Your oponent's move: " + move2);

                int player1_move = 0;
                if (move == "r") player1_move = 1;
                if (move == "p") player1_move = 2;
                if (move == "s") player1_move = 3;

                int winner = WhoIsWinner(player1_move, player2_move);
                if (winner != 0)
                {
                    Transition(round, winner, player1_move, player2_move);
                    states[round].move_player2 = player2_move;
                    Console.WriteLine("Runda " + (round + 1) + " a fost castigata de " + winner);
                }
                if (winner == 0)
                {
                    Console.WriteLine("Aceasta runda s-a incheiat in remiza");
                    if (round != 0)
                    {
                        states[round].score_player1 = states[round - 1].score_player1;
                        states[round].score_player2 = states[round - 1].score_player2;
                    }

                }

                Console.WriteLine("Scorul curent este: " + states[round].score_player1 + " la " + states[round].score_player2);
                round++;
            }
        }

        //cu random si anticipare cu o anumita probabilitate - statistici pe user
        public static void MediumStrategy()
        {

        }

        //patterns and probs
        public static void HardStrategy(string path)
        {
            int round = 0;
            int player2_move;
            string[] patterns = System.IO.File.ReadAllLines(path);
            while (!IsFinal(round))
            {
                string move;
                Console.WriteLine("\nRock? (r) Paper? (p) Scrissors? (s)");
                string anticipation;
                if ((anticipation = IsPattern(round, patterns)) != "false")
                {
                    Console.WriteLine("My anticipation: ");
                    Console.WriteLine(anticipation);
                    player2_move = Oponent(anticipation);
                }
                else
                {
                    Random rnd = new Random();
                    player2_move = rnd.Next(1, 4);
                }

                move = Console.ReadLine();
                if (move != "s" && move != "p" && move != "r")
                    Console.WriteLine("Invalid move!");

                string move2="";
                if (player2_move == 1) move2 = "r";
                if (player2_move == 2) move2 = "p";
                if (player2_move == 3) move2 = "s";

                Console.WriteLine("Your oponent's move: ");
                Console.WriteLine(move2);

                int player1_move = 0;
                if (move == "r") player1_move = 1;
                if (move == "p") player1_move = 2;
                if (move == "s") player1_move = 3;

                //states[round].move_player1=
                UpdateMove(path, move, round);

                int winner = WhoIsWinner(player1_move, player2_move);
                if (winner != 0)
                {
                    Transition(round, winner, player1_move, player2_move);
                    UpdateMove(path, move, round);
                    states[round].move_player2 = player2_move;
                    Console.WriteLine("Runda " + (round + 1) + " a fost castigata de " + winner);
                    round++;
                }
                
              
            }
            
        }

        public static string Authentification()
        {
            string user;
            Console.WriteLine("Please enter your user: ");
            user = Console.ReadLine();
            // FileStream stream = File.Open("D:\\New folder\\Sample.txt", FileMode.Open);
            string[] lines = System.IO.File.ReadAllLines("D:\\New folder\\Sample.txt");
            int ok = 0;

            foreach (string line in lines)
            {
                if (line == user)
                {
                    Console.WriteLine("Welcome back, " + user);
                    ok = 1;
                }
            }

            if (ok == 0)
            {
                Console.WriteLine("Do you want to become an user? Yes or No");
                string response = Console.ReadLine();
                if (response == "Yes")
                {
                    var sw = new StreamWriter("D:\\New folder\\Sample.txt", true);
                    Console.WriteLine("Welcome " + user);
                    string user2 = "\n" + user;
                    sw.Write(user2);
                    sw.Dispose();
                }
                else return "false";

            }

            string path = "D:\\New folder\\" + user + ".txt";
            if (ok == 0)
            {
                File.Create(path);
                string[] patterns = { "0", "0", "0" };
                File.WriteAllLines(path, patterns);
                //StreamWriter sw = new StreamWriter(path);
                //sw.WriteLine("0");
                //sw.WriteLine("0");
                //sw.WriteLine("0");
            }
            return path;
        }

        public static void UpdateMove(string path, string move, int round)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            int x = Int32.Parse(lines[0]);
            int y = Int32.Parse(lines[1]);
            int z = Int32.Parse(lines[2]);
            if (move == "r") x++;
            if (move == "p") y++;
            if (move == "s") z++;
            string rock = x.ToString();
            string paper = y.ToString();
            string scrissors = z.ToString();
            string game;
            string[] dataFile;
            if (round == 0)
            { dataFile = new string[lines.Length + 1]; }
            else { dataFile = new string[lines.Length]; }
            dataFile[0] = rock;
            dataFile[1] = paper;
            dataFile[2] = scrissors;
            if (round != 0)
                game = lines[lines.Length - 1] + "-" + move;
            else game = move;
            if (round != 0)
            {
                for (int i = 3; i < lines.Length - 1; i++)
                {
                    dataFile[i] = lines[i];
                }
                dataFile[lines.Length-1] = game;
            }
            else
            {
                for(int i=3;i<lines.Length;i++)
                {
                    dataFile[i] = lines[i];
                }
                dataFile[lines.Length] = game;
            }
            File.WriteAllLines(path, dataFile);

        }

        static int Main(string[] args)
        {
            Initialize();
            Finalize();
            // DisplayStates();

            string path;
            if ((path = Authentification()) == "0")
            {
                Console.WriteLine("Game over.");
                return 0;
            }

            string difficulty="a";
            while (difficulty != "e" && difficulty != "m" && difficulty != "h")
            {
                Console.WriteLine("Easy (e), Medium (m) or Hard (h)? ");
                difficulty = Console.ReadLine();
            }

            if (difficulty == "e") EasyStrategy(path);
            if (difficulty == "m") MediumStrategy();
            if (difficulty == "h") HardStrategy(path);

            return 1;
        }
    }
}
