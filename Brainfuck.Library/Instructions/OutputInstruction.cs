using System;
using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class OutputInstruction : ICpuInstruction
    {
        public Task ProcessInstruction(ICpu cpu)
        {
            cpu.IO.Output(((char)cpu.Memory.GetMemoryValue()).ToString());
            return Task.CompletedTask;
        }
    }
}
