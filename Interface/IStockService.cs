using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helper;

namespace api.Interface
{
    public interface IStockService
    {
        Task<List<StockDto>> GetAllAsync(QueryObject query);
        Task<StockDto?> GetByIdAsync(int id);
        Task<StockDto> CreateAsync(CreateStockRequestDto stockDto);
        Task<StockDto?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
