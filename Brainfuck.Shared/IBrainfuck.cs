using System;
using System.Collections.Generic;
using System.Text;

namespace Brainfuck.Shared
{
    public interface IBrainfuck
    {
        ICpu CreateCpu(string rom);
    }
}
