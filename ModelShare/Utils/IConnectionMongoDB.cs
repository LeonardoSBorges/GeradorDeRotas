using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelShare.Utils
{
    public interface IConnectionMongoDB
    {
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
        string CollectionName { get; set; }
    }
}
