using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelShare.Utils
{
    public class ConnectionMongoDB : IConnectionMongoDB
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
