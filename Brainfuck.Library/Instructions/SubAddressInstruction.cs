using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class SubAddressInstruction : ICpuInstruction
    {
        public Task ProcessInstruction(ICpu cpu)
        {
            if (cpu.Memory.Address-- < 0)
                cpu.Memory.Address = 0;
            return Task.CompletedTask;
        }
    }
}
