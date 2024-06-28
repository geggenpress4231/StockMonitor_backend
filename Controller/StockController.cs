using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helper;
using api.Interface;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stockDtos = await _stockService.GetAllAsync(query);
            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stockDto = await _stockService.GetByIdAsync(id);
            if (stockDto == null)
            {
                return NotFound();
            }
            return Ok(stockDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var createdStockDto = await _stockService.CreateAsync(stockDto);
            return CreatedAtAction(nameof(GetById), new { id = createdStockDto.Id }, createdStockDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var updatedStockDto = await _stockService.UpdateAsync(id, updateDto);
            if (updatedStockDto == null)
            {
                return NotFound();
            }
            return Ok(updatedStockDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _stockService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}