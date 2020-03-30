using System;
using System.Threading.Tasks;
using Brainfuck.Interpreter;
using Brainfuck.Shared;

namespace Brainfuck.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var brainfuck = BrainfuckInterpreter.Create();

            ICpu cpu = brainfuck.CreateCpu("++++++++++[>+>+++>+++<<<-]>>++>++++++++++++>+[>+>+<<-]>+++++++++[<<<.>>>-]>[<<<.>>>-]<<<<<.>>>+++[>+>+<<-]>++++++[<<<.>>>-]>[<<<.>>>-]<<<<<.>>>+++++[>+>+<<-]>+++[<<<.>>>-]>[<<<.>>>-]<<<<<.>>>+++++++[>+>+<<-]>[<<<.>>>-]>[<<<.>>>-]<<<<<.>>>++++++++++[>++++++<-]>+++++<<<...>>>+++++.-.+++++++.---.+++++++++++++++++.<<<.>>>------------.-------------.+++++++++++++++++++++.-------------------.+++++++++++.<<<.>>>-----------.+++++++++++++++.------------.---.");
            await cpu.Process();

            Console.ReadKey();
            // dump the memory in screen.
            int i = 0;
            foreach (var @byte in cpu.Memory.Dump())
            {
                Console.Write($"[{i}] {@byte}");
                if (i == cpu.Memory.Address)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
                i++;
            }

            Console.WriteLine("Finished.");
            Console.ReadKey();
        }
    }
}
