using MiniMart.Models;

namespace MiniMart.Repositories
{
    public interface ICheckOutRepository
    {
        bool CheckOrderValidity(int id);
        Order GetOrder(int id);
    }
}