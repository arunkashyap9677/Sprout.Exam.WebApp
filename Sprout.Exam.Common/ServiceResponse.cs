using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common
{
    public class ServiceResponse<T>
    {
        public T Result { get; set; }
        public string Identifier { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
