using System;
using System.Collections.Generic;

namespace cblockchain
{
    public class Node
    {
        private List<Block> _chain;

        /// <summary>
        /// The unique endpoint of this node.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// The total number of credits this node currently has available
        /// to create new entries onto the block chain.
        /// </summary>
        /// <value>The credits.</value>
        public double Credits { get; set; }

        /// <summary>
        /// The last 
        /// </summary>
        /// <value>The last transaction date.</value>
        public DateTimeOffset? LastTransactionDate { get; set; }
        public int ProcessedTransactionCount { get; set; }

        public Node(List<Block> chain)
        {
            // assign a copy of the current chain to this node
            this._chain = chain;

            // For simulation, let's just use a guid as the id for now
            this.Id = Guid.NewGuid().ToString();
        }

        public Block Write(int previousBlock, string[] processors, ITransaction transaction, int piece)
        {
            // Hash transaction raw data and create block
            var block = new Block()
            {
                PreviousBlock = previousBlock,
                Processors = processors,
                Transaction = transaction
            };

            // Return block to the chain
            // Todo: I actually want to write part of the block and return just 
            // a piece of the hash to be assembled together.
            return block;
        }
    }
}
