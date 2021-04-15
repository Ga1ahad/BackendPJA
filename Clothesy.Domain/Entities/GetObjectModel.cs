using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothesy.Domain.Entities
{
    public class GetObjectModel
    {
        public string ContentType { get; set; }
        public Stream Content { get; set; }
    }
}
