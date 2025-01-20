using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }



        // [HttpGet]
        // public IActionResult GetAll(){
        //     var stocks = _context.Stocks.ToList()
        //     .Select(s => s.ToStockDto());
        //     return Ok(stocks);
        // }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }







        // [HttpGet("{id:int}")]
        // public IActionResult GetById([FromRoute] int id){

        //     var stock = _context.Stocks.Find(id);

        //     if(stock == null){
        //         return NotFound();
        //     }

        //     return Ok(stock.ToStockDto());
        // }

         [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){

            var stock = await _stockRepo.GetByIdAsync(id);

            if(stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }




        // [HttpPost]
        // public IActionResult Create([FromBody] CreateStockRequestDto createStockRequestDto){
        //     var stockModel = createStockRequestDto.ToStockFromCreateDTO();
        //     _context.Stocks.Add(stockModel);
        //     _context.SaveChanges();
        //     return CreatedAtAction(nameof(GetById), new {id = stockModel.Id} , stockModel.ToStockDto());
        // }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockRequestDto){
            var stockModel = createStockRequestDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id} , stockModel.ToStockDto());
        }



        // [HttpPut]
        // [Route("{id}")]
        // public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto){
        //     var stockModel = _context.Stocks.FirstOrDefault(
        //         x => x.Id == id
        //     );

        //     if(stockModel == null){
        //         return NotFound();
        //     }

        //     stockModel.Symbol = updateStockRequestDto.Symbol;
        //     stockModel.CompanyName = updateStockRequestDto.CompanyName;
        //     stockModel.Purchase = updateStockRequestDto.Purchase;
        //     stockModel.LastDiv = updateStockRequestDto.LastDiv;
        //     stockModel.Industry = updateStockRequestDto.Industry;
        //     stockModel.MarketCap = updateStockRequestDto.MarketCap;

        //     _context.SaveChanges();

        //     return Ok(stockModel.ToStockDto());
        // }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto){
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockRequestDto);

            if(stockModel == null){
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }



        // [HttpDelete]
        // [Route("{id}")]
        // public IActionResult Delete([FromRoute] int id){
        //     var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

        //     if( stockModel == null){
        //         return NotFound();
        //     }

        //     _context.Stocks.Remove(stockModel);

        //     _context.SaveChanges();

        //     return NoContent();

        // }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var stockModel = await _stockRepo.DeleteAsync(id);

            if( stockModel == null){
                return NotFound();
            }

            return NoContent();

        }







    }
}