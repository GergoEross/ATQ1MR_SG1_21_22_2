using ATQ1MR_HFT_2021221.Logic.Intefaces;
using ATQ1MR_HFT_2021221.Models.Entities;
using ATQ1MR_HFT_2021221.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MotherboardController : ControllerBase
    {
        readonly IMotherboardLogic motherboardLogic;

        public MotherboardController(IMotherboardLogic motherboardLogic)
        {
            this.motherboardLogic = motherboardLogic;
        }

        // GET: api/Motherboard/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Motherboard> Get()
        {
            return motherboardLogic.ReadAll();
        }

        // GET api/Motherboard/Get/5
        [HttpGet("{id}")]
        public Motherboard Get(int id)
        {
            return motherboardLogic.Read(id);
        }

        // POST api/Motherboard/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Motherboard motherboard)
        {
            var result = new ApiResult(true);

            try
            {
                motherboardLogic.Create(motherboard);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/Motherboard/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Motherboard motherboard)
        {
            var result = new ApiResult(true);

            try
            {
                motherboardLogic.Update(motherboard);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/Motherboard/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                motherboardLogic.Delete(id);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }
        // GET: api/Motherboard/GetMotherboardWhitProcessors
        [HttpGet]
        public IEnumerable<MotherboardWhitProcessorsModel> GetMotherboardWhitProcessors()
        {
            return motherboardLogic.MotherboardsWhitItsProcessors();
        }
        // GET: api/Motherboard/GetMotherboardProcessorAvaragePrices
        [HttpGet]
        public IEnumerable<MotherboardPAvarageModel> GetMotherboardProcessorAvaragePrices()
        {
            return motherboardLogic.MotherboardProcessorAvaragePrices();
        }
        // GET: api/Motherboard/GetBestPPPForMotherboard/5
        [HttpGet("{id}")]
        public IEnumerable<BestPricePerPerformaceModel> GetBestPPPForMotherboard(int id)
        {
            return motherboardLogic.BestPPPForMotherboard(id);
        }
    }   
}
