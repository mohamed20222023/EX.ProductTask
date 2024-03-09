using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Message;
    public class RepositoryMessage  
    {
        public bool Status { get; set; }
        public object ReturnEntity { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
