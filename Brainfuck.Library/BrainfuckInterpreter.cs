using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Brainfuck.Shared;

namespace Brainfuck.Library
{
    public class BrainfuckInterpreter : IBrainfuck
    {
        private readonly IInputOutput _inputOutput;
        private BrainfuckInterpreter(IInputOutput inputOutput)
        {
            _inputOutput = inputOutput;
        }

        public static BrainfuckInterpreter Create(IInputOutput inputOutput)
        {
            return new BrainfuckInterpreter(inputOutput);
        }

        public ICpu CreateCpu(string rom)
        {
            return new Cpu(_inputOutput, rom);
        }
    }
}
