using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Brainfuck.Library.Instructions;
using Brainfuck.Shared;

namespace Brainfuck.Library
{
    internal class Cpu : ICpu
    {
        public IInputOutput IO { get; set; }
        public IMemory Rom { get; }
        public IMemory Memory { get; }
        public Stack<long> Stack { get; }

        private readonly IDictionary<char, ICpuInstruction> _instructions;
        
        public Cpu(IInputOutput io, string rom) : this(rom)
        {
            IO = io;
            _instructions = new Dictionary<char, ICpuInstruction>()
            {
                ['+'] = new AddInstruction(),
                ['-'] = new SubInstruction(),
                ['>'] = new AddAddressInstruction(),
                ['<'] = new SubAddressInstruction(),
                ['['] = new InitLoopInstruction(),
                [']'] = new EndLoopInstruction(),
                [','] = new InputInstruction(),
                ['.'] = new OutputInstruction(),
                ['#'] = new OutputNumberInstruction(),
            };
        }
        private Cpu(string rom)
        {
            Stack = new Stack<long>();

            Rom = new Memory(Encoding.UTF8.GetBytes(rom));

            Memory = new Memory();
        }

        public async Task<byte> ProcessInput()
        {
            byte @byte = (byte) await IO.Input();

            return @byte;
        }
        
        // will only return the output buffer later.
        public async Task Process()
        {
            while (Rom.Size > Rom.Address)
            {
                var readMemory = Rom.GetMemoryValue();
                await ProcessInstruction((char) readMemory);

                Rom.Address++;
            }
        }

        private async Task ProcessInstruction(char instruction)
        {
            if (_instructions.TryGetValue(instruction, out ICpuInstruction cpuInstruction))
            {
                try
                {
                    await cpuInstruction.ProcessInstruction(this);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void Dispose()
        {
            Rom?.Dispose();
            Memory?.Dispose();
        }
    }
}
