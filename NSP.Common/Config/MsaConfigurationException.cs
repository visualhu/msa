using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Config
{
    public class MsaConfigurationException : MsaException
    {
        public MsaConfigurationException() { }

        public MsaConfigurationException(string message) : base(message)
        { }

        public MsaConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public MsaConfigurationException(string format, params object[] args)
            : base(string.Format(format, args))
        { }
    }
}
