using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public enum ModelDbState : sbyte
    {
        None = 0, 
        Insert = 1,
        Update = 2, 
        Delete = 3
    }
}
