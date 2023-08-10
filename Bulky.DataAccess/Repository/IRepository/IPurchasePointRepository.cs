using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IPurchasePointRepository : IRepository<PurchasePoint>
    {
        void Update(PurchasePoint obj);
    }
}
