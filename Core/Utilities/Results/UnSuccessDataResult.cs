using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class UnSuccessDataResult<T> : DataResult<T>
    {
        public UnSuccessDataResult(T data, string message) : base(data, false, message)
        {
        }
        public UnSuccessDataResult(T data) : base(data, false)
        {

        }
        public UnSuccessDataResult(string message) : base(default, false, message)
        {

        }
        public UnSuccessDataResult() : base(default, false)
        {

        }
    }
}
