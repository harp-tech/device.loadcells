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
        /// Asynchronously reads the contents of the AcquisitionState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadAcquisitionStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionState.Address));
            return AcquisitionState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the AcquisitionState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedAcquisitionStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(AcquisitionState.Address));
            return AcquisitionState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the AcquisitionState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteAcquisitionStateAsync(EnableFlag value)
        {
            var request = AcquisitionState.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DI0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalState> ReadDI0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0State.Address));
            return DI0State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalState>> ReadTimestampedDI0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0State.Address));
            return DI0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalState> ReadDO0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0State.Address));
            return DO0State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0State register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalState>> ReadTimestampedDO0StateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0State.Address));
            return DO0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DI0Trigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DI0TriggerConfig> ReadDI0TriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Trigger.Address));
            return DI0Trigger.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0Trigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DI0TriggerConfig>> ReadTimestampedDI0TriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Trigger.Address));
            return DI0Trigger.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Trigger register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0TriggerAsync(DI0TriggerConfig value)
        {
            var request = DI0Trigger.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Sync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DO0SyncConfig> ReadDO0SyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Sync.Address));
            return DO0Sync.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Sync register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DO0SyncConfig>> ReadTimestampedDO0SyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Sync.Address));
            return DO0Sync.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Sync register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0SyncAsync(DO0SyncConfig value)
        {
            var request = DO0Sync.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseWidth.Address));
            return DO0PulseWidth.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0PulseWidth register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedDO0PulseWidthAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0PulseWidth.Address));
            return DO0PulseWidth.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0PulseWidth register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0PulseWidthAsync(byte value)
        {
            var request = DO0PulseWidth.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputSet.Address));
            return DigitalOutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputSet register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputSetAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputSet.Address));
            return DigitalOutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputSetAsync(DigitalOutputs value)
        {
            var request = DigitalOutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputClear.Address));
            return DigitalOutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputClear register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputClearAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputClear.Address));
            return DigitalOutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputClearAsync(DigitalOutputs value)
        {
            var request = DigitalOutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputToggle.Address));
            return DigitalOutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputToggle register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputToggleAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputToggle.Address));
            return DigitalOutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputToggleAsync(DigitalOutputs value)
        {
            var request = DigitalOutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputState.Address));
            return DigitalOutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedDigitalOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputState.Address));
            return DigitalOutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputStateAsync(DigitalOutputs value)
        {
            var request = DigitalOutputState.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DO8TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ThresholdOnLoadCell> ReadDO8TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO8TargetLoadCell.Address));
            return DO8TargetLoadCell.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8TargetLoadCell register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ThresholdOnLoadCell>> ReadTimestampedDO8TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO8TargetLoadCell.Address));
            return DO8TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8TargetLoadCellAsync(ThresholdOnLoadCell value)
        {
            var request = DO8TargetLoadCell.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DO8Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<short> ReadDO8ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO8Threshold.Address));
            return DO8Threshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8Threshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<short>> ReadTimestampedDO8ThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadInt16(DO8Threshold.Address));
            return DO8Threshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8Threshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8ThresholdAsync(short value)
        {
            var request = DO8Threshold.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DO8BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO8BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8BufferRisingEdge.Address));
            return DO8BufferRisingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8BufferRisingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO8BufferRisingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8BufferRisingEdge.Address));
            return DO8BufferRisingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8BufferRisingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8BufferRisingEdgeAsync(ushort value)
        {
            var request = DO8BufferRisingEdge.FromPayload(MessageType.Write, value);
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
        /// Asynchronously reads the contents of the DO8BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO8BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8BufferFallingEdge.Address));
            return DO8BufferFallingEdge.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8BufferFallingEdge register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO8BufferFallingEdgeAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8BufferFallingEdge.Address));
            return DO8BufferFallingEdge.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8BufferFallingEdge register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8BufferFallingEdgeAsync(ushort value)
        {
            var request = DO8BufferFallingEdge.FromPayload(MessageType.Write, value);
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
