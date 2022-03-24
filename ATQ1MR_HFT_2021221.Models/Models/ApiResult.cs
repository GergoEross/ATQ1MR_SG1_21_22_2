using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Models.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }

        public ApiResult()
        {
        }

        public ApiResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
