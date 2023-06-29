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
        /// Asynchronously reads the contents of the DigitalInputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address));
            return DigitalInputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address));
            return DigitalInputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the SyncOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<SyncOutputs> ReadSyncOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(SyncOutputState.Address));
            return SyncOutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the SyncOutputState register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<SyncOutputs>> ReadTimestampedSyncOutputStateAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(SyncOutputState.Address));
            return SyncOutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DI0Trigger register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<TriggerConfig> ReadDI0TriggerAsync()
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
        public async Task<Timestamped<TriggerConfig>> ReadTimestampedDI0TriggerAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Trigger.Address));
            return DI0Trigger.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Trigger register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0TriggerAsync(TriggerConfig value)
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
        public async Task<SyncConfig> ReadDO0SyncAsync()
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
        public async Task<Timestamped<SyncConfig>> ReadTimestampedDO0SyncAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Sync.Address));
            return DO0Sync.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Sync register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0SyncAsync(SyncConfig value)
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
        public async Task<LoadCellChannel> ReadDO1TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO1TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1TargetLoadCell.Address));
            return DO1TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO2TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO2TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO2TargetLoadCell.Address));
            return DO2TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO3TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO3TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO3TargetLoadCell.Address));
            return DO3TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO4TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO4TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO4TargetLoadCell.Address));
            return DO4TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO5TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO5TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO5TargetLoadCell.Address));
            return DO5TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO6TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO6TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO6TargetLoadCell.Address));
            return DO6TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO7TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO7TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO7TargetLoadCell.Address));
            return DO7TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7TargetLoadCellAsync(LoadCellChannel value)
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
        public async Task<LoadCellChannel> ReadDO8TargetLoadCellAsync()
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
        public async Task<Timestamped<LoadCellChannel>> ReadTimestampedDO8TargetLoadCellAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO8TargetLoadCell.Address));
            return DO8TargetLoadCell.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8TargetLoadCell register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8TargetLoadCellAsync(LoadCellChannel value)
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
        /// Asynchronously reads the contents of the DO1TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1TimeAboveThreshold.Address));
            return DO1TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1TimeAboveThreshold.Address));
            return DO1TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1TimeAboveThresholdAsync(ushort value)
        {
            var request = DO1TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2TimeAboveThreshold.Address));
            return DO2TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2TimeAboveThreshold.Address));
            return DO2TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2TimeAboveThresholdAsync(ushort value)
        {
            var request = DO2TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3TimeAboveThreshold.Address));
            return DO3TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3TimeAboveThreshold.Address));
            return DO3TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3TimeAboveThresholdAsync(ushort value)
        {
            var request = DO3TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO4TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4TimeAboveThreshold.Address));
            return DO4TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO4TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4TimeAboveThreshold.Address));
            return DO4TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4TimeAboveThresholdAsync(ushort value)
        {
            var request = DO4TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO5TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5TimeAboveThreshold.Address));
            return DO5TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO5TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5TimeAboveThreshold.Address));
            return DO5TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5TimeAboveThresholdAsync(ushort value)
        {
            var request = DO5TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO6TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6TimeAboveThreshold.Address));
            return DO6TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO6TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6TimeAboveThreshold.Address));
            return DO6TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6TimeAboveThresholdAsync(ushort value)
        {
            var request = DO6TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO7TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7TimeAboveThreshold.Address));
            return DO7TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO7TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7TimeAboveThreshold.Address));
            return DO7TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7TimeAboveThresholdAsync(ushort value)
        {
            var request = DO7TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO8TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO8TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8TimeAboveThreshold.Address));
            return DO8TimeAboveThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8TimeAboveThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO8TimeAboveThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8TimeAboveThreshold.Address));
            return DO8TimeAboveThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8TimeAboveThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8TimeAboveThresholdAsync(ushort value)
        {
            var request = DO8TimeAboveThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO1TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1TimeBelowThreshold.Address));
            return DO1TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO1TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO1TimeBelowThreshold.Address));
            return DO1TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1TimeBelowThresholdAsync(ushort value)
        {
            var request = DO1TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO2TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO2TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2TimeBelowThreshold.Address));
            return DO2TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO2TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO2TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO2TimeBelowThreshold.Address));
            return DO2TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO2TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO2TimeBelowThresholdAsync(ushort value)
        {
            var request = DO2TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO3TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO3TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3TimeBelowThreshold.Address));
            return DO3TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO3TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO3TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO3TimeBelowThreshold.Address));
            return DO3TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO3TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO3TimeBelowThresholdAsync(ushort value)
        {
            var request = DO3TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO4TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO4TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4TimeBelowThreshold.Address));
            return DO4TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO4TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO4TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO4TimeBelowThreshold.Address));
            return DO4TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO4TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO4TimeBelowThresholdAsync(ushort value)
        {
            var request = DO4TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO5TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO5TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5TimeBelowThreshold.Address));
            return DO5TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO5TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO5TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO5TimeBelowThreshold.Address));
            return DO5TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO5TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO5TimeBelowThresholdAsync(ushort value)
        {
            var request = DO5TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO6TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO6TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6TimeBelowThreshold.Address));
            return DO6TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO6TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO6TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO6TimeBelowThreshold.Address));
            return DO6TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO6TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO6TimeBelowThresholdAsync(ushort value)
        {
            var request = DO6TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO7TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO7TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7TimeBelowThreshold.Address));
            return DO7TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO7TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO7TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO7TimeBelowThreshold.Address));
            return DO7TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO7TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO7TimeBelowThresholdAsync(ushort value)
        {
            var request = DO7TimeBelowThreshold.FromPayload(MessageType.Write, value);
            await CommandAsync(request);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO8TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDO8TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8TimeBelowThreshold.Address));
            return DO8TimeBelowThreshold.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO8TimeBelowThreshold register.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDO8TimeBelowThresholdAsync()
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DO8TimeBelowThreshold.Address));
            return DO8TimeBelowThreshold.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO8TimeBelowThreshold register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO8TimeBelowThresholdAsync(ushort value)
        {
            var request = DO8TimeBelowThreshold.FromPayload(MessageType.Write, value);
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
