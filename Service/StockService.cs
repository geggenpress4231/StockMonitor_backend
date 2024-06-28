using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interface;
using api.Models;
using api.Helper;
using api.Dtos;
using api.Mappers;
namespace api.Services
{
    public class StockService : IStockService
    {
        private readonly IStockrepository _stockRepo;

        public StockService(IStockrepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<List<StockDto>> GetAllAsync(QueryObject query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);
            return stocks.Select(s => s.ToStockDto()).ToList();
        }

        public async Task<StockDto?> GetByIdAsync(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            return stock?.ToStockDto();//exception handling to be added,use logger
        }

        public async Task<StockDto> CreateAsync(CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            var createdStock = await _stockRepo.CreateAsync(stockModel);
            return createdStock.ToStockDto();
        }

        public async Task<StockDto?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            var updatedStock = await _stockRepo.UpdateAsync(id, updateDto);
            return updatedStock?.ToStockDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deletedStock = await _stockRepo.DeleteAsync(id);
            return deletedStock != null;
        }
    }
}