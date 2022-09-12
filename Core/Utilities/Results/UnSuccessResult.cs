using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class UnSuccessResult : Result
    {
        public UnSuccessResult(string message) : base(false, message)
        {

        }
        public UnSuccessResult() : base(false)
        {

        }
    }
}
