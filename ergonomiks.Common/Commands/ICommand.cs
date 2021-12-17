using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.Commands.Command
{
    public interface ICommand
    {
        void Validate();
    }
}
