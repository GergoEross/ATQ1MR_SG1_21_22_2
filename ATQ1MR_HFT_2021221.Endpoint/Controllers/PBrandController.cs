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
    public class PBrandController : ControllerBase
    {
        readonly IPBrandLogic pBrandLogic;

        public PBrandController(IPBrandLogic pBrandLogic)
        {
            this.pBrandLogic = pBrandLogic;
        }

        // GET: api/PBrand/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<PBrand> Get()
        {
            return pBrandLogic.ReadAll();
        }

        // GET api/PBrand/Get/5
        [HttpGet("{id}")]
        public PBrand Get(int id)
        {
            return pBrandLogic.Read(id);
        }

        // POST api/PBrand/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(PBrand pBrand)
        {
            var result = new ApiResult(true);

            try
            {
                pBrandLogic.Create(pBrand);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/PBrand/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(PBrand pBrand)
        {
            var result = new ApiResult(true);

            try
            {
                pBrandLogic.Update(pBrand);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/PBrand/Delete/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                pBrandLogic.Delete(id);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
