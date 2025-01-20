using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helper;
using api.Models;

namespace api.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
        
    }
}