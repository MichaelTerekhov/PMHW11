using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Returned response error model
    /// </summary>
    public class ResponseError
    {
        /// <summary>
        /// Custom status code of the error that occurred
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        ///   Detailed message about the error that occurred
        /// </summary>
        public string Message { get; set; }
    }
}
