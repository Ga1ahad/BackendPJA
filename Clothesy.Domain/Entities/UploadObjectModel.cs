using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothesy.Domain.Entities
{
    public class UploadObjectModel
    {
        public bool Success { get; set; }
        public string FileName { get; set; }

        public static implicit operator string(UploadObjectModel v)
        {
            throw new NotImplementedException();
        }
    }
}
