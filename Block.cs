using System;
using System.Text;

namespace cblockchain
{
    public class Block
    {
        /// <summary>
        /// The hash of the previous block in the chain.
        /// Provided for data integrity and security of attacked forking.
        /// </summary>
        /// <value>The previous block.</value>
        public int PreviousBlock { get; set; }

        /// <summary>
        /// The hash of the content (message, transaction, document, etc.)
        /// of this block.
        /// </summary>
        /// <value>The hash of the block.</value>
        public int Hash
        {
            get
            {
                return this.GetHashCode();
            }
        }

        /// <summary>
        /// The list of selected processors (id/endpoints).
        /// Note: This is a historical record, so endpoints could no longer
        /// be active.
        /// </summary>
        /// <value>The processors.</value>
        public string[] Processors { get; set; }

        /// <summary>
        /// The raw transaction being stored in the block.
        /// </summary>
        /// <value>The transaction.</value>
        public ITransaction Transaction { get; set; }

        public Block()
        {
        }

        public override string ToString()
        {
            return string.Format("[Block: PreviousBlock={0}, Hash={1}, Processors={2}, Transaction={3}]", PreviousBlock, Hash, Processors, Transaction);
        }
    }
}
