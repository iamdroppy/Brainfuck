using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class AddInstruction : ICpuInstruction
    {
        public async Task ProcessInstruction(ICpu cpu)
        {
            await cpu.Memory.IncrementMemoryValue();
        }
    }
}
