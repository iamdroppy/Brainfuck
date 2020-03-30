using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck.Shared
{
    public interface IMemoryRead
    {
        /// <summary>
        /// Gets the current address.
        /// </summary>
        long Address { get; }

        /// <summary>
        /// Gets the current Memory Size.
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Get the Memory Value for the current position.
        /// </summary>
        /// <returns>value on the current memory pointer</returns>
        byte GetMemoryValue();

        /// <summary>
        /// Dumps the memory.
        /// </summary>
        IEnumerable<byte> Dump();
    }

    public interface IMemoryWrite
    {
        /// <summary>
        /// Sets the current memory address.
        /// </summary>
        long Address { set; }

        /// <summary>
        /// Increments Memory Value for the current pointer.
        /// </summary>
        Task IncrementMemoryValue();

        /// <summary>
        /// Decrements Memory Value for the current pointer.
        /// </summary>
        Task DecrementMemoryValue();

        /// <summary>
        /// Sets the Memory Value for the current pointer.
        /// </summary>
        /// <param name="value">Memory value</param>
        Task SetMemoryValue(byte value);
    }

    public interface IMemory : IMemoryRead, IMemoryWrite, IDisposable
    {
        new long Address { get; set; }
        long MemoryUsed { get; set; }
    }

    public interface ICpuInstruction
    {
        Task ProcessInstruction(ICpu cpu);
    }

    public interface ICpu : IDisposable
    {
        IInputOutput IO { get; set; }
        IMemory Rom { get; }
        IMemory Memory { get; }
        Stack<long> Stack { get; }
        Task<byte> ProcessInput();
        Task Process();
    }
}
