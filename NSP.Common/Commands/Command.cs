using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Commands
{
    public abstract class Command : ICommand
    {
        public Command()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get; set;
        }
    }
}
