using System;
using System.Collections.Generic;
using System.Linq;

namespace cblockchain
{
    public class Network
    {
        private List<Node> _nodes;
        private static List<Block> _chain;

        public List<Node> Nodes 
        {
            get 
            {
                return _nodes;
            }
        }

        public Network()
        {
            // Create collections
            _nodes = new List<Node>();
            _chain = new List<Block>();

            // Create genesis block
            var genesis = new Block()
            {
                Transaction = new Transaction(Guid.NewGuid().ToString(), "In the beginning...")
            };

            // Add genesis block to chain
            _chain.Add(genesis);
        }

        public void RegisterNode(Node node)
        {
            // For now, just add to the collection
            _nodes.Add(node);
        }

        public Block Write(ITransaction transaction)
        {
            // Get processors to participate
            var processors = ElectProcessorsFromNetwork();

            // Create string array of processor ids to store with transaction
            var processorIds = processors.Select(a => a.Id).ToArray();

            // Get previous block
            var previousBlock = _chain[_chain.Count - 1].Hash;

            Block block = null;

            // Loop through processors and assign data to build hash pieces
            for (var i = 0; i < processors.Count - 1; i++)
            {
                block = processors[i].Write(previousBlock, processorIds, transaction, i);
            }

            // Todo: check if the block came out with consensus

            // Todo: Write the block in pieces so no single node is responsible
            // for all of the data

            // Update stats for processors
            foreach(var processor in processors)
            {
                // Update stats for processors and assign credits
                processor.LastTransactionDate = DateTimeOffset.UtcNow;
                processor.ProcessedTransactionCount++;  // Todo: this is not thread safe
                processor.Credits += (double)1 / processors.Count();
            }

            // Assemble block
            return block;
        }

        private List<Node> ElectProcessorsFromNetwork()
        {
            // Set number to select to participate
            var number = 3;
            var random = new Random();
            var processors = new List<Node>();

            // Return them randomly to start
            for (var i = 0; i < number; i++)
            {
                // Get random number to use to select index in collection
                var randomIndex = random.Next(0, _nodes.Count - 1);

                processors.Add(_nodes[randomIndex]);
            }

            return processors;
        }
    }
}
