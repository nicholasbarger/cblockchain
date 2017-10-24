using System;
namespace cblockchain
{
    public class Payment : ITransaction
    {
        public string Message { get; set; }
        public double Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public Payment()
        {
        }
    }
}
