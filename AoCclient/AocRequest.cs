using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoCclient
{
    internal class AocRequest
    {
        public int day { get; set; }
        public int puzzle { get; set; }
        public List<string> input { get; set; }
        public AocRequest(int day,int puzzle,List<string> input) 
        {
            this.day = day;
            this.puzzle = puzzle;
            this.input = input;
        }
    }
}
