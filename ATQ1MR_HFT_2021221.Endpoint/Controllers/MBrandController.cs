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
    public class MBrandController : ControllerBase
    {
        readonly IMBrandLogic mBrandLogic;

        public MBrandController(IMBrandLogic mBrandLogic)
        {
            this.mBrandLogic = mBrandLogic;
        }

        // GET: api/MBrand/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<MBrand> Get()
        {
            return mBrandLogic.ReadAll();
        }

        // GET api/MBrand/Get/5
        [HttpGet("{id}")]
        public MBrand Get(int id)
        {
            return mBrandLogic.Read(id);
        }

        // POST api/MBrand/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(MBrand mBrand)
        {
            var result = new ApiResult(true);

            try
            {
                mBrandLogic.Create(mBrand);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/MBrand/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(MBrand mBrand)
        {
            var result = new ApiResult(true);

            try
            {
                mBrandLogic.Update(mBrand);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/MBrand/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                mBrandLogic.Delete(id);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }
        // GET: api/MBrand/GetMBrandsWithAvarageProcessorPrices
        [HttpGet]
        public IEnumerable<MBrandAverageProcessorPricesModel> GetMBrandsWithAvarageProcessorPrices()
        {
            return mBrandLogic.MBrandsWithAvarageProcessorPrices();
        }
    }
}
