using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public interface INewEntity<in T>
    {
        bool Equals(int id);
    }
}
