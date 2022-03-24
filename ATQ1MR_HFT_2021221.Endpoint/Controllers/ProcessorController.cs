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
    public class ProcessorController : ControllerBase
    {
        readonly IProcessorLogic processorLogic;

        public ProcessorController(IProcessorLogic processorLogic)
        {
            this.processorLogic = processorLogic;
        }

        // GET: api/Processor/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Processor> Get()
        {
            return processorLogic.ReadAll();
        }

        // GET api/Processor/Get/5
        [HttpGet("{id}")]
        public Processor Get(int id)
        {
            return processorLogic.Read(id);
        }

        // POST api/Processor/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Processor processor)
        {
            var result = new ApiResult(true);

            try
            {
                processorLogic.Create(processor);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/Processor/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Processor processor)
        {
            var result = new ApiResult(true);

            try
            {
                processorLogic.Update(processor);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/Processor/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                processorLogic.Delete(id);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }
        // GET: api/Processor/GetProcessorsWhitHighestPriceMotherboard
        [HttpGet]
        public IEnumerable<ProcessorWhitHighestPriceMotherboardModel> GetProcessorsWhitHighestPriceMotherboard()
        {
            return processorLogic.ProcessorWhitHighestPriceMotherboard();
        }
    }
}
