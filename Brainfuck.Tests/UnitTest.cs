
using System;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Brainfuck.Library;
using Brainfuck.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Brainfuck.Tests
{
    public class InputOutputMock : IInputOutput
    {
        public Task Output(char data)
        {
            return Task.CompletedTask;
        }

        public Task<char> Input()
        {
            return Task.FromResult('a');
        }
    }

    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void Test_CreateInterpreter_IsInitialized()
        {
            InputOutputMock ioMock = new InputOutputMock();
            BrainfuckInterpreter interpreter = BrainfuckInterpreter.Create(ioMock);
            string input = "+++++--";
            char[] inputChars = input.ToCharArray();
            using (ICpu cpu = interpreter.CreateCpu(input))
            {

                Assert.IsNotNull(cpu.IO);
                Assert.Equals(cpu.IO, ioMock);
                Assert.IsNotNull(cpu.Rom);
                Assert.IsNotNull(cpu.IO);
                Assert.IsTrue(cpu.Rom.Address == 0);
                Assert.IsTrue(cpu.Rom.Size == input.Length);
                Assert.IsTrue(cpu.Memory.Address == 0);

                int i = 0;
                foreach (byte b in cpu.Rom.Dump())
                {
                    Assert.IsTrue(inputChars[i] == (char) b);
                    i++;
                }
            }
        }
    }
}
