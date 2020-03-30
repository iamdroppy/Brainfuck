using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck.Shared
{
    public interface IInputOutput
    {
        Task Output(string data);
        Task<char> Input();
    }
}
