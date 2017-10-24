using System;
namespace cblockchain
{
    public class Transaction : ITransaction
    {
        public string From { get; set; }
        public string Message { get; set; }

        public Transaction()
        {    
        }

        public Transaction(string from, string message)
        {
            this.From = from;
            this.Message = message;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.From, this.Message);
        }
    }
}
