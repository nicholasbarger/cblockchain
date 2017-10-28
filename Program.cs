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
                _network.RegisterNode(Guid.NewGuid().ToString());
            }
        }

        private static void SimulateActivity()
        {
            var random = new Random();
            var i = 0;
            while(i < 1000)
            {
                // Get random number
                var randomIndex = random.Next(0, _network.Nodes.Count);

                // Get the node selected at random that wants to write to the block chain
                var node = _network.Nodes[randomIndex];

                // Create random transaction
                var transaction = new Transaction(node.Id, "Random message with index:" + i.ToString());

                // Write to the blockchain
                var block = _network.Write(node, transaction);
                if(block != null) 
                {
                    Console.WriteLine("Wrote block: {0}", i.ToString());
                    i++;    
                }
                else 
                {
                    Console.WriteLine("Not enough credit to write block for {0}", node.Id);    
                }

            }
        }

        private static void DisplayNodeStats()
        {
            foreach(var processor in _network.Nodes)
            {
                Console.WriteLine("Node {0}: {1} credits remaining from {2} transactions processed.", processor.Id, processor.Credits, processor.ProcessedTransactionCount);
            }
        }
    }
}
