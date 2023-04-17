using Bonsai.Harp;
using System.Threading.Tasks;

namespace Harp.LoadCells
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with LoadCells devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a LoadCells device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with LoadCells devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the StartAcquisition register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadStartAcquisitionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartAcquisition.Address));
            return StartAcquisition.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the StartAcquisition register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedStartAcquisitionAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(StartAcquisition.Address));
            return StartAcquisition.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the StartAcquisition register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteStartAcquisitionAsync(EnableFlag value)
        {
            var request = StartAcquisition.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LoadCellData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LoadCellDataPayload> ReadLoadCellDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(LoadCellData.Address));
            return LoadCellData.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LoadCellData register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LoadCellDataPayload>> ReadTimestampedLoadCellDataAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(LoadCellData.Address));
            return LoadCellData.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the InputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputState> ReadInputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputEvent.Address));
            return InputEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the InputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputState>> ReadTimestampedInputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(InputEvent.Address));
            return InputEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputState> ReadOutputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputEvent.Address));
            return OutputEvent.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputEvent register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputState>> ReadTimestampedOutputEventAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputEvent.Address));
            return OutputEvent.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DI0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DI0Configuration> ReadDI0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address));
            return DI0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DI0Configuration>> ReadTimestampedDI0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address));
            return DI0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0ModeAsync(DI0Configuration value)
        {
            var request = DI0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DO0Configuration> ReadDO0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address));
            return DO0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Mode register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DO0Configuration>> ReadTimestampedDO0ModeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address));
            return DO0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ModeAsync(DO0Configuration value)
        {
            var request = DO0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0PulseDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadDO0PulseDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseDuration.Address));
            return DO0PulseDuration.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0PulseDuration register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedDO0PulseDurationAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseDuration.Address));
            return DO0PulseDuration.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0PulseDuration register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0PulseDurationAsync(byte value)
        {
            var request = DO0PulseDuration.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputSet.Address));
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputClear.Address));
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputToggle.Address));
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(OutputState.Address));
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell0.Address));
            return OffsetLoadCell0.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell0 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell0Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell0.Address));
            return OffsetLoadCell0.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell0 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell0Async(short value)
        {
            var request = OffsetLoadCell0.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell1.Address));
            return OffsetLoadCell1.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell1 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell1Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell1.Address));
            return OffsetLoadCell1.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell1 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell1Async(short value)
        {
            var request = OffsetLoadCell1.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell2.Address));
            return OffsetLoadCell2.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell2 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell2Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell2.Address));
            return OffsetLoadCell2.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell2 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell2Async(short value)
        {
            var request = OffsetLoadCell2.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell3.Address));
            return OffsetLoadCell3.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell3 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell3Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell3.Address));
            return OffsetLoadCell3.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell3 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell3Async(short value)
        {
            var request = OffsetLoadCell3.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell4 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell4Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell4.Address));
            return OffsetLoadCell4.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell4 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell4Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell4.Address));
            return OffsetLoadCell4.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell4 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell4Async(short value)
        {
            var request = OffsetLoadCell4.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell5 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell5Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell5.Address));
            return OffsetLoadCell5.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell5 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell5Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell5.Address));
            return OffsetLoadCell5.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell5 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell5Async(short value)
        {
            var request = OffsetLoadCell5.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell6 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell6Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell6.Address));
            return OffsetLoadCell6.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell6 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell6Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell6.Address));
            return OffsetLoadCell6.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell6 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell6Async(short value)
        {
            var request = OffsetLoadCell6.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OffsetLoadCell7 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadOffsetLoadCell7Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell7.Address));
            return OffsetLoadCell7.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OffsetLoadCell7 register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedOffsetLoadCell7Async()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(OffsetLoadCell7.Address));
            return OffsetLoadCell7.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OffsetLoadCell7 register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOffsetLoadCell7Async(short value)
        {
            var request = OffsetLoadCell7.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO0TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0TargetLoadCell.Address));
            return DO0TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO0TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0TargetLoadCell.Address));
            return DO0TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO0TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO1TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1TargetLoadCell.Address));
            return DO1TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO1TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1TargetLoadCell.Address));
            return DO1TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO1TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO2TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO2TargetLoadCell.Address));
            return DO2TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO2TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO2TargetLoadCell.Address));
            return DO2TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO2TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO3TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO3TargetLoadCell.Address));
            return DO3TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO3TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO3TargetLoadCell.Address));
            return DO3TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO3TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO4TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO4TargetLoadCell.Address));
            return DO4TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO4TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO4TargetLoadCell.Address));
            return DO4TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO4TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO5TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO5TargetLoadCell.Address));
            return DO5TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO5TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO5TargetLoadCell.Address));
            return DO5TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO5TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO6TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO6TargetLoadCell.Address));
            return DO6TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO6TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO6TargetLoadCell.Address));
            return DO6TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO6TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO7TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO7TargetLoadCell.Address));
            return DO7TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO7TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO7TargetLoadCell.Address));
            return DO7TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO7TargetLoadCell.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO0ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO0Threshold.Address));
            return DO0Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO0ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO0Threshold.Address));
            return DO0Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ThresholdAsync(short value)
        {
            var request = DO0Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO1ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO1Threshold.Address));
            return DO1Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO1ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO1Threshold.Address));
            return DO1Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1ThresholdAsync(short value)
        {
            var request = DO1Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO2ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO2Threshold.Address));
            return DO2Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO2ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO2Threshold.Address));
            return DO2Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2ThresholdAsync(short value)
        {
            var request = DO2Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO3ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO3Threshold.Address));
            return DO3Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO3ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO3Threshold.Address));
            return DO3Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3ThresholdAsync(short value)
        {
            var request = DO3Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO4ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO4Threshold.Address));
            return DO4Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO4ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO4Threshold.Address));
            return DO4Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4ThresholdAsync(short value)
        {
            var request = DO4Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO5ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO5Threshold.Address));
            return DO5Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO5ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO5Threshold.Address));
            return DO5Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5ThresholdAsync(short value)
        {
            var request = DO5Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO6ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO6Threshold.Address));
            return DO6Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO6ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO6Threshold.Address));
            return DO6Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6ThresholdAsync(short value)
        {
            var request = DO6Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO7ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO7Threshold.Address));
            return DO7Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO7ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO7Threshold.Address));
            return DO7Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7ThresholdAsync(short value)
        {
            var request = DO7Threshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO0BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferRisingEdge.Address));
            return DO0BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO0BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferRisingEdge.Address));
            return DO0BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0BufferRisingEdgeAsync(ushort value)
        {
            var request = DO0BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferRisingEdge.Address));
            return DO1BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferRisingEdge.Address));
            return DO1BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1BufferRisingEdgeAsync(ushort value)
        {
            var request = DO1BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferRisingEdge.Address));
            return DO2BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferRisingEdge.Address));
            return DO2BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2BufferRisingEdgeAsync(ushort value)
        {
            var request = DO2BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferRisingEdge.Address));
            return DO3BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferRisingEdge.Address));
            return DO3BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3BufferRisingEdgeAsync(ushort value)
        {
            var request = DO3BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO4BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4BufferRisingEdge.Address));
            return DO4BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO4BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4BufferRisingEdge.Address));
            return DO4BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4BufferRisingEdgeAsync(ushort value)
        {
            var request = DO4BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO5BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5BufferRisingEdge.Address));
            return DO5BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO5BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5BufferRisingEdge.Address));
            return DO5BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5BufferRisingEdgeAsync(ushort value)
        {
            var request = DO5BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO6BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6BufferRisingEdge.Address));
            return DO6BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO6BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6BufferRisingEdge.Address));
            return DO6BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6BufferRisingEdgeAsync(ushort value)
        {
            var request = DO6BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO7BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7BufferRisingEdge.Address));
            return DO7BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO7BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7BufferRisingEdge.Address));
            return DO7BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7BufferRisingEdgeAsync(ushort value)
        {
            var request = DO7BufferRisingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO0BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferFallingEdge.Address));
            return DO0BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO0BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO0BufferFallingEdge.Address));
            return DO0BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0BufferFallingEdgeAsync(ushort value)
        {
            var request = DO0BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferFallingEdge.Address));
            return DO1BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1BufferFallingEdge.Address));
            return DO1BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1BufferFallingEdgeAsync(ushort value)
        {
            var request = DO1BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferFallingEdge.Address));
            return DO2BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2BufferFallingEdge.Address));
            return DO2BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2BufferFallingEdgeAsync(ushort value)
        {
            var request = DO2BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferFallingEdge.Address));
            return DO3BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3BufferFallingEdge.Address));
            return DO3BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3BufferFallingEdgeAsync(ushort value)
        {
            var request = DO3BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO4BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4BufferFallingEdge.Address));
            return DO4BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO4BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4BufferFallingEdge.Address));
            return DO4BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4BufferFallingEdgeAsync(ushort value)
        {
            var request = DO4BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO5BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5BufferFallingEdge.Address));
            return DO5BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO5BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5BufferFallingEdge.Address));
            return DO5BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5BufferFallingEdgeAsync(ushort value)
        {
            var request = DO5BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO6BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6BufferFallingEdge.Address));
            return DO6BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO6BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6BufferFallingEdge.Address));
            return DO6BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6BufferFallingEdgeAsync(ushort value)
        {
            var request = DO6BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO7BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7BufferFallingEdge.Address));
            return DO7BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO7BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7BufferFallingEdge.Address));
            return DO7BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7BufferFallingEdgeAsync(ushort value)
        {
            var request = DO7BufferFallingEdge.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EnableEvents register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<LoadCellEvents> ReadEnableEventsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEvents.Address));
            return EnableEvents.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EnableEvents register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<LoadCellEvents>> ReadTimestampedEnableEventsAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EnableEvents.Address));
            return EnableEvents.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EnableEvents register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEnableEventsAsync(LoadCellEvents value)
        {
            var request = EnableEvents.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }
    }
}
