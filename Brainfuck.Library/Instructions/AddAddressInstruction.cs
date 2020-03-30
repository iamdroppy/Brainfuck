using System;
using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class AddAddressInstruction : ICpuInstruction
    {
        public Task ProcessInstruction(ICpu cpu)
        {
            if (cpu.Memory.Address++ <= cpu.Memory.Size) return Task.CompletedTask;
            cpu.Memory.Address--;
            throw new Exception("You have exceeded the memory.");

        }
    }
}
