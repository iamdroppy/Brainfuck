using System;
using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library.Instructions
{
    internal class InitLoopInstruction : ICpuInstruction
    {
        private long _lastAddress = 0;
        private long _repeatedTimes = 0;
        public Task ProcessInstruction(ICpu cpu)
        {
            cpu.Stack.Push(cpu.Rom.Address);
            if (_lastAddress != cpu.Rom.Address)
            {
                _lastAddress = cpu.Rom.Address;
                _repeatedTimes = 0;
            }
            else
            {
                _repeatedTimes++;

                if (_repeatedTimes >= 8196)
                    throw new Exception("Repeated too many times..");
            }
            return Task.CompletedTask;
        }
    }
}