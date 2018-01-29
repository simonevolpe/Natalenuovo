using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natale.Classes
{
    public interface IDataBase
    {
        User GetUser(User user);

        IEnumerable<Toy> GetAllToys();
        
        Toy GetToy(string name);

        IEnumerable<RequestKid> GetAllRequestKid();

        RequestKid GetRequest(string id);

        bool UpdateStatus(RequestKid requestKid);

        bool UpdateAmountToy(Toy toy);

        bool RemoveToy(string id);

        List<Decimal> SumRequest(string name);


    }
}
