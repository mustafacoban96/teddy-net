using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helper;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            
            _stockRepo = stockRepo;
        }



        // [HttpGet]
        // public IActionResult GetAll(){
        //     var stocks = _context.Stocks.ToList()
        //     .Select(s => s.ToStockDto());
        //     return Ok(stocks);
        // }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _stockRepo.DeleteAsync(id);

            if( stockModel == null){
                return NotFound();
            }

            return NoContent();

        }







    }
}