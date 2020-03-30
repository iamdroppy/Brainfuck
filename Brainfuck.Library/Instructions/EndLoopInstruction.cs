using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class EndLoopInstruction : ICpuInstruction
    {
        public Task ProcessInstruction(ICpu cpu)
        {
            long returnAddress = cpu.Stack.Pop();
            if (cpu.Memory.Address < 0)
            {
                cpu.Memory.Address = 0;
            }
            else if (cpu.Memory.GetMemoryValue() != 0) // repeats the loop.
            {
                cpu.Rom.Address = returnAddress - 1;
            }

            return Task.CompletedTask;
        }
    }
}
