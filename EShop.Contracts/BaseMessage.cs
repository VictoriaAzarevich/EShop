using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Contracts
{
    public interface IBaseMessage
    {
        public Guid Id { get; }
        public DateTime MessageCreated { get; }
    }
}
