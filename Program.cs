using System;
using System.Collections.Generic;

namespace cblockchain
{
    class Program
    {
        private static Network _network;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting block chain network.");
            StartNetwork();

            Console.WriteLine("Registering nodes on the block chain network.");
            RegisterNodes();

            Console.WriteLine("Simulating activity on the network.");
            SimulateActivity();

            Console.WriteLine("Node stats:");
            DisplayNodeStats();
        }

        private static void StartNetwork()
        {
            _network = new Network();
        }

        private static void RegisterNodes()
        {
            // Create a few fake nodes for simulation
            for (var i = 0; i < 100; i++)
            {
                _network.RegisterNode(new Node());
            }
        }

        private static void SimulateActivity()
        {
            var random = new Random();
            for (var i = 0; i < 1000; i++) 
            {
                // Get random number
                var randomIndex = random.Next(0, _network.Nodes.Count - 1);

                // Create random transaction
                var transaction = new Transaction(_network.Nodes[randomIndex].Id, "Random message with index:" + i.ToString());

                // Write to the blockchain
                Console.WriteLine("Writing block: {0}", i.ToString());
                var block = _network.Write(transaction);
                Console.WriteLine("Finished writing block: {0}", block.ToString());
            }
        }

        private static void DisplayNodeStats()
        {
            foreach(var processor in _network.Nodes)
            {
                Console.WriteLine("Node {0}: {1} credits accrued from {2} transactions processed.", processor.Id, processor.Credits, processor.ProcessedTransactionCount);
            }
        }
    }
}
