using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brainfuck.Shared;

namespace Brainfuck.Library
{
    internal class Memory : IMemory
    {
        private readonly byte[] _memory;

        public Memory(byte[] predefinedMemory)
        {
            _memory = predefinedMemory;
        }

        public Memory()
        {
            _memory = new byte[1024];
        }

        private long _address;

        public long Address
        {
            get => _address;
            set
            {
                if (value > MemoryUsed)
                    MemoryUsed = value;
                _address = value;
            }
        }
        public long MemoryUsed { get; set; }

        public long Size => _memory.Length;

        public byte GetMemoryValue()
        {
            return _memory[Address];
        }

        public IEnumerable<byte> Dump()
        {
            for (int i = 0; i < MemoryUsed + 1; i++)
            {
                yield return _memory[i];
            }
        }

        public async Task IncrementMemoryValue()
        {
            byte memoryValue;
            unchecked
            {
                memoryValue = (byte)(_memory[Address] + 1);
            }

            await SetMemoryValue(memoryValue);
        }

        public async Task DecrementMemoryValue()
        {
            byte memoryValue;
            if (Address < 0)
                Address = 0;
            unchecked
            {
                memoryValue = (byte) (_memory[Address] - 1);
            }

            await SetMemoryValue(memoryValue);
        }

        public Task SetMemoryValue(byte value)
        {
            _memory[Address] = value;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
