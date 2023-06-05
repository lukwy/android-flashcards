using Flashcards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Guid = "bf8f6bff-bd3c-43a9-b2fc-60d17192125c", Word = "sove, sov, sovet", Meaning = "spać" },
                new Item { Guid = "ad194bdb-ad7d-491e-bbd5-726801e87d4a", Word = "spise, spiste, spist", Meaning = "jeść" },
                new Item { Guid = "6d4be454-7708-4b85-91a4-145337db7148", Word = "stige, steg, steget", Meaning = "spać" }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Guid == item.Guid).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string guid)
        {
            var oldItem = items.Where((Item arg) => arg.Guid == guid).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string guid)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Guid == guid));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}