using System;
namespace cblockchain
{
    public interface ITransaction
    {
        string From { get; set; }
        string Message { get; set; }
    }
}
