using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServer2CSharpLib {
    
    public class Student {

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? SAT { get; set; }
        public decimal GPA { get; set; }
        public int? MajorId { get; set; }

    }
}
