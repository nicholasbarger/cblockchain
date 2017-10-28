using System;
using System.Collections.Generic;
using System.Linq;

namespace cblockchain
{
    public class Network
    {
        private List<Node> _nodes;
        private List<Block> _chain;

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

            // Give initial credit to first node
            var genesisNode = new Node(_chain)
            {
                Credits = 100
            };
            _nodes.Add(genesisNode);

            // Create genesis block
            var genesisBlock = new Block()
            {
                Transaction = new Transaction(genesisNode.Id, "In the beginning...")
            };

            // Add genesis block to chain
            _chain.Add(genesisBlock);
        }

        public void RegisterNode(string id)
        {
            // Apply the current chain to the node
            var node = new Node(_chain)
            {
                Id = id
            };

            // For now, just add to the collection
            _nodes.Add(node);
        }

        public Block Write(Node from, ITransaction transaction)
        {
            // Check node requesting the write, to see if they have enough
            // credit to propose
            if (from.Credits < 1)
            {
                // Todo: make a better return message telling the requestor
                // they don't have enough credits yet.
                return null;
            }
            else {
                // Use 1 credit for the transaction
                from.Credits--;
            }

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
            const int number = 5;
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
