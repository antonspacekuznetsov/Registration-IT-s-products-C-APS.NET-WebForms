using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegITProducts.administator.Data
{
    interface IUnitOfWork:IDisposable
    {
        Repository<T> Repository<T>() where T : class;
    }
}
