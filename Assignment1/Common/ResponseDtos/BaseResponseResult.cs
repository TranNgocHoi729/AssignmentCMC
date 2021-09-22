using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseDtos
{
    public class BaseResponseResult<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
