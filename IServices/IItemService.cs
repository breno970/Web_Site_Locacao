using aluguel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aluguel.IServices
{
    public interface IItemService
    {
        Item Save(Item item);
        Item GetSavedItem();
    }
}
