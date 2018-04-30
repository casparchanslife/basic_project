using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ViewModel
{
    public class AccessRightModel
    {
        public string FunctionId { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
        public bool Copy { get; set; }
        public bool Read { get; set; }
    }
}
