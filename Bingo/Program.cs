using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Xml;

namespace BingoProject
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            Stopwatch watch = new Stopwatch();
            watch.Start();
                Dictionary <int, int[,]> tickets = new Dictionary<int, int[,]>();
                Dictionary<int, int[,]> ticketsWonLine = new Dictionary<int, int[,]>();
                Dictionary<int, int[,]> ticketsWonBingo = new Dictionary<int, int[,]>();

            while (true)
            {
                Random rnd = new Random();
                int soldTickets = rnd.Next(0, 150);
                for (int i = 0; i < soldTickets; i++)
                {
                    int[,] ticket = new int[5, 5];
                    HashSet<int> callednumbersFilledintickets = new HashSet<int>();
                    for (int row = 0; row < 5; row++)
                    {
                        for (int cols = 0; cols < 5; cols++)
                        {
                            int number = rnd.Next(0, 90);
                            if (callednumbersFilledintickets.Contains(number))
                            {
                                cols--;
                                continue;
                            }
                            else
                            {
                                ticket[row, cols] = number;
                                callednumbersFilledintickets.Add(number);
                            }

                        }
                    }
                    
                    tickets.Add(i, ticket);
                }

                    HashSet<int> calledNumbers = new HashSet<int>();
                int linesAnnounced = 0;
                

                while (true)
                {
                    int calledInteger = rnd.Next(0, 100);
                    if (calledNumbers.Contains(calledInteger))
                    {
                        continue;
                    }
                    calledNumbers.Add(calledInteger);
                    for (int m = 0; m < tickets.Count; m++)
                    {
                        for (int rows = 0; rows < 5; rows++)
                        {
                            for (int cols = 0; cols < 5; cols++)
                            {
                                if (tickets[m][rows, cols] == calledInteger)
                                {
                                    tickets[m][rows, cols] = 150;
                                }

                                if (tickets[m][rows, 0] == 150 && tickets[m][rows, 1] == 150 && tickets[m][rows, 2] == 150 && tickets[m][rows, 3] == 150 && tickets[m][rows, 4] == 150)
                                {
                                    if (ticketsWonLine.ContainsKey(m))
                                    {
                                        continue;
                                    }
                                    int[,] matrixToAdd = tickets[m];
                                    ticketsWonLine.Add(m, matrixToAdd);

                                }
                                foreach (var one in ticketsWonLine.Keys)
                                {
                                    int bingoNumbers = 0;
                                    for (int row = 0; row < 5; row++)
                                    {
                                        for (int col = 0; col < 5; col++)
                                        {
                                            if (ticketsWonLine[one][row, col] == 150)
                                            {
                                                bingoNumbers++;
                                            }
                                        }

                                    }
                                    if (bingoNumbers == 25)
                                    {
                                        if (ticketsWonBingo.ContainsKey(one))
                                        {
                                            continue;
                                        }
                                        int[,] matrixToAdd = tickets[m];
                                        ticketsWonBingo.Add(one, matrixToAdd);
                                    }
                                    if(ticketsWonBingo.Count > 0)
                                    {
                                        break;
                                    }
                                }

                                if(ticketsWonBingo.Count > 0)
                                {
                                    break;
                                }
                            }
                            if (ticketsWonBingo.Count > 0)
                            {
                                break;
                            }

                        }
                        if (ticketsWonBingo.Count > 0)
                        {
                            break;
                        }
                        if (ticketsWonBingo.Count > 0)
                        {
                            break;
                        }
                    }

                    if (linesAnnounced == 0)
                    {
                        if (ticketsWonLine.Count > 0)
                        {
                            linesAnnounced++;
                            foreach (var bingo in ticketsWonLine.Keys)
                            {
                                Console.WriteLine($"Ticket number {bingo + 1} won Line!");
                                continue;
                            }

                        }
                    } 
                    
                    if (ticketsWonBingo.Count > 0)
                    {
                        break;
                    }
                }

                if (ticketsWonBingo.Count > 0)
                {
                    foreach (var bingo in ticketsWonBingo.Keys)
                    {
                        Console.WriteLine($"Ticket number {bingo+1} won Bingo!");
                       watch.Stop();
                        Console.WriteLine(watch.ElapsedMilliseconds);
                    }
                    break;
                }
                
            }
            
        }
    }
}
