using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class InputInstruction : ICpuInstruction
    {
        public async Task ProcessInstruction(ICpu cpu)
        {
            await cpu.Memory.SetMemoryValue(await cpu.ProcessInput());
        }
    }
}
