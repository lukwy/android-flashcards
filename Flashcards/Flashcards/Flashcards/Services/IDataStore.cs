using Flashcards.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flashcards.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string guid);
        Task<T> GetItemAsync(string guid);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<int> GetItemsCountAsync();
        Task<Item> GetRandItemAsync(int shift);
    }
}
