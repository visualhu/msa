using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common
{
    public class MsaException : Exception
    {
        public MsaException() { }

        public MsaException(string message)
            : base(message) { }

        public MsaException(string message, Exception innerException)
            : base(message, innerException) { }

        public MsaException(string format, params object[] args)
            : base(string.Format(format, args)) { }
    }
}
