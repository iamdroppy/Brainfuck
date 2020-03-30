using System;
using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class OutputNumberInstruction : ICpuInstruction
    {
        public Task ProcessInstruction(ICpu cpu)
        {
            cpu.IO.Output("[" + (int) cpu.Memory.GetMemoryValue() + "]");
            return Task.CompletedTask;
        }
    }
}
