using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.LoadCells
{
    /// <summary>
    /// Generates events and processes commands for the LoadCells device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the LoadCells device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="LoadCells"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1232;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(LoadCells);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(AcquisitionState) },
            { 33, typeof(LoadCellData) },
            { 34, typeof(DigitalInputState) },
            { 35, typeof(SyncOutputState) },
            { 36, typeof(BufferThresholdState) },
            { 37, typeof(Reserved0) },
            { 38, typeof(Reserved1) },
            { 39, typeof(DI0Trigger) },
            { 40, typeof(DO0Sync) },
            { 41, typeof(DO0PulseWidth) },
            { 42, typeof(DigitalOutputSet) },
            { 43, typeof(DigitalOutputClear) },
            { 44, typeof(DigitalOutputToggle) },
            { 45, typeof(DigitalOutputState) },
            { 46, typeof(Reserved2) },
            { 47, typeof(Reserved3) },
            { 48, typeof(OffsetLoadCell0) },
            { 49, typeof(OffsetLoadCell1) },
            { 50, typeof(OffsetLoadCell2) },
            { 51, typeof(OffsetLoadCell3) },
            { 52, typeof(OffsetLoadCell4) },
            { 53, typeof(OffsetLoadCell5) },
            { 54, typeof(OffsetLoadCell6) },
            { 55, typeof(OffsetLoadCell7) },
            { 56, typeof(Reserved4) },
            { 57, typeof(Reserved5) },
            { 58, typeof(DO1TargetLoadCell) },
            { 59, typeof(DO2TargetLoadCell) },
            { 60, typeof(DO3TargetLoadCell) },
            { 61, typeof(DO4TargetLoadCell) },
            { 62, typeof(DO5TargetLoadCell) },
            { 63, typeof(DO6TargetLoadCell) },
            { 64, typeof(DO7TargetLoadCell) },
            { 65, typeof(DO8TargetLoadCell) },
            { 66, typeof(DO1Threshold) },
            { 67, typeof(DO2Threshold) },
            { 68, typeof(DO3Threshold) },
            { 69, typeof(DO4Threshold) },
            { 70, typeof(DO5Threshold) },
            { 71, typeof(DO6Threshold) },
            { 72, typeof(DO7Threshold) },
            { 73, typeof(DO8Threshold) },
            { 74, typeof(DO1TimeAboveThreshold) },
            { 75, typeof(DO2TimeAboveThreshold) },
            { 76, typeof(DO3TimeAboveThreshold) },
            { 77, typeof(DO4TimeAboveThreshold) },
            { 78, typeof(DO5TimeAboveThreshold) },
            { 79, typeof(DO6TimeAboveThreshold) },
            { 80, typeof(DO7TimeAboveThreshold) },
            { 81, typeof(DO8TimeAboveThreshold) },
            { 82, typeof(DO1TimeBelowThreshold) },
            { 83, typeof(DO2TimeBelowThreshold) },
            { 84, typeof(DO3TimeBelowThreshold) },
            { 85, typeof(DO4TimeBelowThreshold) },
            { 86, typeof(DO5TimeBelowThreshold) },
            { 87, typeof(DO6TimeBelowThreshold) },
            { 88, typeof(DO7TimeBelowThreshold) },
            { 89, typeof(DO8TimeBelowThreshold) },
            { 90, typeof(EnableEvents) }
        };

        /// <summary>
        /// Gets the contents of the metadata file describing the <see cref="LoadCells"/>
        /// device registers.
        /// </summary>
        public static readonly string Metadata = GetDeviceMetadata();

        static string GetDeviceMetadata()
        {
            var deviceType = typeof(Device);
            using var metadataStream = deviceType.Assembly.GetManifestResourceStream($"{deviceType.Namespace}.device.yml");
            using var streamReader = new System.IO.StreamReader(metadataStream);
            return streamReader.ReadToEnd();
        }
    }

    /// <summary>
    /// Represents an operator that returns the contents of the metadata file
    /// describing the <see cref="LoadCells"/> device registers.
    /// </summary>
    [Description("Returns the contents of the metadata file describing the LoadCells device registers.")]
    public partial class GetMetadata : Source<string>
    {
        /// <summary>
        /// Returns an observable sequence with the contents of the metadata file
        /// describing the <see cref="LoadCells"/> device registers.
        /// </summary>
        /// <returns>
        /// A sequence with a single <see cref="string"/> object representing the
        /// contents of the metadata file.
        /// </returns>
        public override IObservable<string> Generate()
        {
            return Observable.Return(Device.Metadata);
        }
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="LoadCells"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of LoadCells messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="LoadCells"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="LoadCells"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="LoadCells"/> device.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="LoadCellData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="SyncOutputState"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="OffsetLoadCell0"/>
    /// <seealso cref="OffsetLoadCell1"/>
    /// <seealso cref="OffsetLoadCell2"/>
    /// <seealso cref="OffsetLoadCell3"/>
    /// <seealso cref="OffsetLoadCell4"/>
    /// <seealso cref="OffsetLoadCell5"/>
    /// <seealso cref="OffsetLoadCell6"/>
    /// <seealso cref="OffsetLoadCell7"/>
    /// <seealso cref="DO1TargetLoadCell"/>
    /// <seealso cref="DO2TargetLoadCell"/>
    /// <seealso cref="DO3TargetLoadCell"/>
    /// <seealso cref="DO4TargetLoadCell"/>
    /// <seealso cref="DO5TargetLoadCell"/>
    /// <seealso cref="DO6TargetLoadCell"/>
    /// <seealso cref="DO7TargetLoadCell"/>
    /// <seealso cref="DO8TargetLoadCell"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO4Threshold"/>
    /// <seealso cref="DO5Threshold"/>
    /// <seealso cref="DO6Threshold"/>
    /// <seealso cref="DO7Threshold"/>
    /// <seealso cref="DO8Threshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO4TimeAboveThreshold"/>
    /// <seealso cref="DO5TimeAboveThreshold"/>
    /// <seealso cref="DO6TimeAboveThreshold"/>
    /// <seealso cref="DO7TimeAboveThreshold"/>
    /// <seealso cref="DO8TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    /// <seealso cref="DO4TimeBelowThreshold"/>
    /// <seealso cref="DO5TimeBelowThreshold"/>
    /// <seealso cref="DO6TimeBelowThreshold"/>
    /// <seealso cref="DO7TimeBelowThreshold"/>
    /// <seealso cref="DO8TimeBelowThreshold"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(LoadCellData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(SyncOutputState))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(OffsetLoadCell0))]
    [XmlInclude(typeof(OffsetLoadCell1))]
    [XmlInclude(typeof(OffsetLoadCell2))]
    [XmlInclude(typeof(OffsetLoadCell3))]
    [XmlInclude(typeof(OffsetLoadCell4))]
    [XmlInclude(typeof(OffsetLoadCell5))]
    [XmlInclude(typeof(OffsetLoadCell6))]
    [XmlInclude(typeof(OffsetLoadCell7))]
    [XmlInclude(typeof(DO1TargetLoadCell))]
    [XmlInclude(typeof(DO2TargetLoadCell))]
    [XmlInclude(typeof(DO3TargetLoadCell))]
    [XmlInclude(typeof(DO4TargetLoadCell))]
    [XmlInclude(typeof(DO5TargetLoadCell))]
    [XmlInclude(typeof(DO6TargetLoadCell))]
    [XmlInclude(typeof(DO7TargetLoadCell))]
    [XmlInclude(typeof(DO8TargetLoadCell))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO4Threshold))]
    [XmlInclude(typeof(DO5Threshold))]
    [XmlInclude(typeof(DO6Threshold))]
    [XmlInclude(typeof(DO7Threshold))]
    [XmlInclude(typeof(DO8Threshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO4TimeAboveThreshold))]
    [XmlInclude(typeof(DO5TimeAboveThreshold))]
    [XmlInclude(typeof(DO6TimeAboveThreshold))]
    [XmlInclude(typeof(DO7TimeAboveThreshold))]
    [XmlInclude(typeof(DO8TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [XmlInclude(typeof(DO4TimeBelowThreshold))]
    [XmlInclude(typeof(DO5TimeBelowThreshold))]
    [XmlInclude(typeof(DO6TimeBelowThreshold))]
    [XmlInclude(typeof(DO7TimeBelowThreshold))]
    [XmlInclude(typeof(DO8TimeBelowThreshold))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Filters register-specific messages reported by the LoadCells device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new AcquisitionState();
        }

        string INamedElement.Name
        {
            get => $"{nameof(LoadCells)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the LoadCells device.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="LoadCellData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="SyncOutputState"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="OffsetLoadCell0"/>
    /// <seealso cref="OffsetLoadCell1"/>
    /// <seealso cref="OffsetLoadCell2"/>
    /// <seealso cref="OffsetLoadCell3"/>
    /// <seealso cref="OffsetLoadCell4"/>
    /// <seealso cref="OffsetLoadCell5"/>
    /// <seealso cref="OffsetLoadCell6"/>
    /// <seealso cref="OffsetLoadCell7"/>
    /// <seealso cref="DO1TargetLoadCell"/>
    /// <seealso cref="DO2TargetLoadCell"/>
    /// <seealso cref="DO3TargetLoadCell"/>
    /// <seealso cref="DO4TargetLoadCell"/>
    /// <seealso cref="DO5TargetLoadCell"/>
    /// <seealso cref="DO6TargetLoadCell"/>
    /// <seealso cref="DO7TargetLoadCell"/>
    /// <seealso cref="DO8TargetLoadCell"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO4Threshold"/>
    /// <seealso cref="DO5Threshold"/>
    /// <seealso cref="DO6Threshold"/>
    /// <seealso cref="DO7Threshold"/>
    /// <seealso cref="DO8Threshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO4TimeAboveThreshold"/>
    /// <seealso cref="DO5TimeAboveThreshold"/>
    /// <seealso cref="DO6TimeAboveThreshold"/>
    /// <seealso cref="DO7TimeAboveThreshold"/>
    /// <seealso cref="DO8TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    /// <seealso cref="DO4TimeBelowThreshold"/>
    /// <seealso cref="DO5TimeBelowThreshold"/>
    /// <seealso cref="DO6TimeBelowThreshold"/>
    /// <seealso cref="DO7TimeBelowThreshold"/>
    /// <seealso cref="DO8TimeBelowThreshold"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(LoadCellData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(SyncOutputState))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(OffsetLoadCell0))]
    [XmlInclude(typeof(OffsetLoadCell1))]
    [XmlInclude(typeof(OffsetLoadCell2))]
    [XmlInclude(typeof(OffsetLoadCell3))]
    [XmlInclude(typeof(OffsetLoadCell4))]
    [XmlInclude(typeof(OffsetLoadCell5))]
    [XmlInclude(typeof(OffsetLoadCell6))]
    [XmlInclude(typeof(OffsetLoadCell7))]
    [XmlInclude(typeof(DO1TargetLoadCell))]
    [XmlInclude(typeof(DO2TargetLoadCell))]
    [XmlInclude(typeof(DO3TargetLoadCell))]
    [XmlInclude(typeof(DO4TargetLoadCell))]
    [XmlInclude(typeof(DO5TargetLoadCell))]
    [XmlInclude(typeof(DO6TargetLoadCell))]
    [XmlInclude(typeof(DO7TargetLoadCell))]
    [XmlInclude(typeof(DO8TargetLoadCell))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO4Threshold))]
    [XmlInclude(typeof(DO5Threshold))]
    [XmlInclude(typeof(DO6Threshold))]
    [XmlInclude(typeof(DO7Threshold))]
    [XmlInclude(typeof(DO8Threshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO4TimeAboveThreshold))]
    [XmlInclude(typeof(DO5TimeAboveThreshold))]
    [XmlInclude(typeof(DO6TimeAboveThreshold))]
    [XmlInclude(typeof(DO7TimeAboveThreshold))]
    [XmlInclude(typeof(DO8TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [XmlInclude(typeof(DO4TimeBelowThreshold))]
    [XmlInclude(typeof(DO5TimeBelowThreshold))]
    [XmlInclude(typeof(DO6TimeBelowThreshold))]
    [XmlInclude(typeof(DO7TimeBelowThreshold))]
    [XmlInclude(typeof(DO8TimeBelowThreshold))]
    [XmlInclude(typeof(EnableEvents))]
    [XmlInclude(typeof(TimestampedAcquisitionState))]
    [XmlInclude(typeof(TimestampedLoadCellData))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedSyncOutputState))]
    [XmlInclude(typeof(TimestampedDI0Trigger))]
    [XmlInclude(typeof(TimestampedDO0Sync))]
    [XmlInclude(typeof(TimestampedDO0PulseWidth))]
    [XmlInclude(typeof(TimestampedDigitalOutputSet))]
    [XmlInclude(typeof(TimestampedDigitalOutputClear))]
    [XmlInclude(typeof(TimestampedDigitalOutputToggle))]
    [XmlInclude(typeof(TimestampedDigitalOutputState))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell0))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell1))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell2))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell3))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell4))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell5))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell6))]
    [XmlInclude(typeof(TimestampedOffsetLoadCell7))]
    [XmlInclude(typeof(TimestampedDO1TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO2TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO3TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO4TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO5TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO6TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO7TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO8TargetLoadCell))]
    [XmlInclude(typeof(TimestampedDO1Threshold))]
    [XmlInclude(typeof(TimestampedDO2Threshold))]
    [XmlInclude(typeof(TimestampedDO3Threshold))]
    [XmlInclude(typeof(TimestampedDO4Threshold))]
    [XmlInclude(typeof(TimestampedDO5Threshold))]
    [XmlInclude(typeof(TimestampedDO6Threshold))]
    [XmlInclude(typeof(TimestampedDO7Threshold))]
    [XmlInclude(typeof(TimestampedDO8Threshold))]
    [XmlInclude(typeof(TimestampedDO1TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO2TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO3TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO4TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO5TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO6TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO7TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO8TimeAboveThreshold))]
    [XmlInclude(typeof(TimestampedDO1TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO2TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO3TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO4TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO5TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO6TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO7TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedDO8TimeBelowThreshold))]
    [XmlInclude(typeof(TimestampedEnableEvents))]
    [Description("Filters and selects specific messages reported by the LoadCells device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new AcquisitionState();
        }

        string INamedElement.Name => $"{nameof(LoadCells)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// LoadCells register messages.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    /// <seealso cref="LoadCellData"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="SyncOutputState"/>
    /// <seealso cref="DI0Trigger"/>
    /// <seealso cref="DO0Sync"/>
    /// <seealso cref="DO0PulseWidth"/>
    /// <seealso cref="DigitalOutputSet"/>
    /// <seealso cref="DigitalOutputClear"/>
    /// <seealso cref="DigitalOutputToggle"/>
    /// <seealso cref="DigitalOutputState"/>
    /// <seealso cref="OffsetLoadCell0"/>
    /// <seealso cref="OffsetLoadCell1"/>
    /// <seealso cref="OffsetLoadCell2"/>
    /// <seealso cref="OffsetLoadCell3"/>
    /// <seealso cref="OffsetLoadCell4"/>
    /// <seealso cref="OffsetLoadCell5"/>
    /// <seealso cref="OffsetLoadCell6"/>
    /// <seealso cref="OffsetLoadCell7"/>
    /// <seealso cref="DO1TargetLoadCell"/>
    /// <seealso cref="DO2TargetLoadCell"/>
    /// <seealso cref="DO3TargetLoadCell"/>
    /// <seealso cref="DO4TargetLoadCell"/>
    /// <seealso cref="DO5TargetLoadCell"/>
    /// <seealso cref="DO6TargetLoadCell"/>
    /// <seealso cref="DO7TargetLoadCell"/>
    /// <seealso cref="DO8TargetLoadCell"/>
    /// <seealso cref="DO1Threshold"/>
    /// <seealso cref="DO2Threshold"/>
    /// <seealso cref="DO3Threshold"/>
    /// <seealso cref="DO4Threshold"/>
    /// <seealso cref="DO5Threshold"/>
    /// <seealso cref="DO6Threshold"/>
    /// <seealso cref="DO7Threshold"/>
    /// <seealso cref="DO8Threshold"/>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    /// <seealso cref="DO4TimeAboveThreshold"/>
    /// <seealso cref="DO5TimeAboveThreshold"/>
    /// <seealso cref="DO6TimeAboveThreshold"/>
    /// <seealso cref="DO7TimeAboveThreshold"/>
    /// <seealso cref="DO8TimeAboveThreshold"/>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    /// <seealso cref="DO4TimeBelowThreshold"/>
    /// <seealso cref="DO5TimeBelowThreshold"/>
    /// <seealso cref="DO6TimeBelowThreshold"/>
    /// <seealso cref="DO7TimeBelowThreshold"/>
    /// <seealso cref="DO8TimeBelowThreshold"/>
    /// <seealso cref="EnableEvents"/>
    [XmlInclude(typeof(AcquisitionState))]
    [XmlInclude(typeof(LoadCellData))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(SyncOutputState))]
    [XmlInclude(typeof(DI0Trigger))]
    [XmlInclude(typeof(DO0Sync))]
    [XmlInclude(typeof(DO0PulseWidth))]
    [XmlInclude(typeof(DigitalOutputSet))]
    [XmlInclude(typeof(DigitalOutputClear))]
    [XmlInclude(typeof(DigitalOutputToggle))]
    [XmlInclude(typeof(DigitalOutputState))]
    [XmlInclude(typeof(OffsetLoadCell0))]
    [XmlInclude(typeof(OffsetLoadCell1))]
    [XmlInclude(typeof(OffsetLoadCell2))]
    [XmlInclude(typeof(OffsetLoadCell3))]
    [XmlInclude(typeof(OffsetLoadCell4))]
    [XmlInclude(typeof(OffsetLoadCell5))]
    [XmlInclude(typeof(OffsetLoadCell6))]
    [XmlInclude(typeof(OffsetLoadCell7))]
    [XmlInclude(typeof(DO1TargetLoadCell))]
    [XmlInclude(typeof(DO2TargetLoadCell))]
    [XmlInclude(typeof(DO3TargetLoadCell))]
    [XmlInclude(typeof(DO4TargetLoadCell))]
    [XmlInclude(typeof(DO5TargetLoadCell))]
    [XmlInclude(typeof(DO6TargetLoadCell))]
    [XmlInclude(typeof(DO7TargetLoadCell))]
    [XmlInclude(typeof(DO8TargetLoadCell))]
    [XmlInclude(typeof(DO1Threshold))]
    [XmlInclude(typeof(DO2Threshold))]
    [XmlInclude(typeof(DO3Threshold))]
    [XmlInclude(typeof(DO4Threshold))]
    [XmlInclude(typeof(DO5Threshold))]
    [XmlInclude(typeof(DO6Threshold))]
    [XmlInclude(typeof(DO7Threshold))]
    [XmlInclude(typeof(DO8Threshold))]
    [XmlInclude(typeof(DO1TimeAboveThreshold))]
    [XmlInclude(typeof(DO2TimeAboveThreshold))]
    [XmlInclude(typeof(DO3TimeAboveThreshold))]
    [XmlInclude(typeof(DO4TimeAboveThreshold))]
    [XmlInclude(typeof(DO5TimeAboveThreshold))]
    [XmlInclude(typeof(DO6TimeAboveThreshold))]
    [XmlInclude(typeof(DO7TimeAboveThreshold))]
    [XmlInclude(typeof(DO8TimeAboveThreshold))]
    [XmlInclude(typeof(DO1TimeBelowThreshold))]
    [XmlInclude(typeof(DO2TimeBelowThreshold))]
    [XmlInclude(typeof(DO3TimeBelowThreshold))]
    [XmlInclude(typeof(DO4TimeBelowThreshold))]
    [XmlInclude(typeof(DO5TimeBelowThreshold))]
    [XmlInclude(typeof(DO6TimeBelowThreshold))]
    [XmlInclude(typeof(DO7TimeBelowThreshold))]
    [XmlInclude(typeof(DO8TimeBelowThreshold))]
    [XmlInclude(typeof(EnableEvents))]
    [Description("Formats a sequence of values as specific LoadCells register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new AcquisitionState();
        }

        string INamedElement.Name => $"{nameof(LoadCells)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that enables the data acquisition.
    /// </summary>
    [Description("Enables the data acquisition.")]
    public partial class AcquisitionState
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="AcquisitionState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="AcquisitionState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="AcquisitionState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// AcquisitionState register.
    /// </summary>
    /// <seealso cref="AcquisitionState"/>
    [Description("Filters and selects timestamped messages from the AcquisitionState register.")]
    public partial class TimestampedAcquisitionState
    {
        /// <summary>
        /// Represents the address of the <see cref="AcquisitionState"/> register. This field is constant.
        /// </summary>
        public const int Address = AcquisitionState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="AcquisitionState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return AcquisitionState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value of single ADC read from all load cell channels.
    /// </summary>
    [Description("Value of single ADC read from all load cell channels.")]
    public partial class LoadCellData
    {
        /// <summary>
        /// Represents the address of the <see cref="LoadCellData"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="LoadCellData"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="LoadCellData"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 8;

        static LoadCellDataPayload ParsePayload(short[] payload)
        {
            LoadCellDataPayload result;
            result.Channel0 = payload[0];
            result.Channel1 = payload[1];
            result.Channel2 = payload[2];
            result.Channel3 = payload[3];
            result.Channel4 = payload[4];
            result.Channel5 = payload[5];
            result.Channel6 = payload[6];
            result.Channel7 = payload[7];
            return result;
        }

        static short[] FormatPayload(LoadCellDataPayload value)
        {
            short[] result;
            result = new short[8];
            result[0] = value.Channel0;
            result[1] = value.Channel1;
            result[2] = value.Channel2;
            result[3] = value.Channel3;
            result[4] = value.Channel4;
            result[5] = value.Channel5;
            result[6] = value.Channel6;
            result[7] = value.Channel7;
            return result;
        }

        /// <summary>
        /// Returns the payload data for <see cref="LoadCellData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellDataPayload GetPayload(HarpMessage message)
        {
            return ParsePayload(message.GetPayloadArray<short>());
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LoadCellData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellDataPayload> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadArray<short>();
            return Timestamped.Create(ParsePayload(payload.Value), payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LoadCellData"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LoadCellData"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellDataPayload value)
        {
            return HarpMessage.FromInt16(Address, messageType, FormatPayload(value));
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LoadCellData"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LoadCellData"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellDataPayload value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, FormatPayload(value));
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LoadCellData register.
    /// </summary>
    /// <seealso cref="LoadCellData"/>
    [Description("Filters and selects timestamped messages from the LoadCellData register.")]
    public partial class TimestampedLoadCellData
    {
        /// <summary>
        /// Represents the address of the <see cref="LoadCellData"/> register. This field is constant.
        /// </summary>
        public const int Address = LoadCellData.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LoadCellData"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellDataPayload> GetPayload(HarpMessage message)
        {
            return LoadCellData.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
    /// </summary>
    [Description("Status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
    /// </summary>
    [Description("Status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
    public partial class SyncOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="SyncOutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SyncOutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="SyncOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static SyncOutputs GetPayload(HarpMessage message)
        {
            return (SyncOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SyncOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((SyncOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SyncOutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncOutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, SyncOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SyncOutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SyncOutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, SyncOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SyncOutputState register.
    /// </summary>
    /// <seealso cref="SyncOutputState"/>
    [Description("Filters and selects timestamped messages from the SyncOutputState register.")]
    public partial class TimestampedSyncOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="SyncOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = SyncOutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SyncOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncOutputs> GetPayload(HarpMessage message)
        {
            return SyncOutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that state of the buffer thresholds.
    /// </summary>
    [Description("State of the buffer thresholds.")]
    internal partial class BufferThresholdState
    {
        /// <summary>
        /// Represents the address of the <see cref="BufferThresholdState"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="BufferThresholdState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="BufferThresholdState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved0
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved1
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int Address = 38;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that configuration of the digital input pin 0.
    /// </summary>
    [Description("Configuration of the digital input pin 0.")]
    public partial class DI0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static TriggerConfig GetPayload(HarpMessage message)
        {
            return (TriggerConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((TriggerConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0Trigger"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Trigger"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0Trigger"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Trigger"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, TriggerConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0Trigger register.
    /// </summary>
    /// <seealso cref="DI0Trigger"/>
    [Description("Filters and selects timestamped messages from the DI0Trigger register.")]
    public partial class TimestampedDI0Trigger
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Trigger"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0Trigger.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0Trigger"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<TriggerConfig> GetPayload(HarpMessage message)
        {
            return DI0Trigger.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that configuration of the digital output pin 0.
    /// </summary>
    [Description("Configuration of the digital output pin 0.")]
    public partial class DO0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static SyncConfig GetPayload(HarpMessage message)
        {
            return (SyncConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((SyncConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0Sync"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Sync"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, SyncConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0Sync"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Sync"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, SyncConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0Sync register.
    /// </summary>
    /// <seealso cref="DO0Sync"/>
    [Description("Filters and selects timestamped messages from the DO0Sync register.")]
    public partial class TimestampedDO0Sync
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Sync"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0Sync.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0Sync"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<SyncConfig> GetPayload(HarpMessage message)
        {
            return DO0Sync.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [Description("Pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class DO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0PulseWidth"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0PulseWidth"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0PulseWidth"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0PulseWidth register.
    /// </summary>
    /// <seealso cref="DO0PulseWidth"/>
    [Description("Filters and selects timestamped messages from the DO0PulseWidth register.")]
    public partial class TimestampedDO0PulseWidth
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0PulseWidth"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0PulseWidth.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0PulseWidth"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return DO0PulseWidth.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class DigitalOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 42;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputSet register.
    /// </summary>
    /// <seealso cref="DigitalOutputSet"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputSet register.")]
    public partial class TimestampedDigitalOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines.")]
    public partial class DigitalOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputClear register.
    /// </summary>
    /// <seealso cref="DigitalOutputClear"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputClear register.")]
    public partial class TimestampedDigitalOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class DigitalOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputToggle register.
    /// </summary>
    /// <seealso cref="DigitalOutputToggle"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputToggle register.")]
    public partial class TimestampedDigitalOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [Description("Write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class DigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadUInt16();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, messageType, (ushort)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, (ushort)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputState register.
    /// </summary>
    /// <seealso cref="DigitalOutputState"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputState register.")]
    public partial class TimestampedDigitalOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return DigitalOutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved2
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved3
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 0.
    /// </summary>
    [Description("Offset value for Load Cell channel 0.")]
    public partial class OffsetLoadCell0
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell0"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell0 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell0"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell0 register.")]
    public partial class TimestampedOffsetLoadCell0
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell0"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 1.
    /// </summary>
    [Description("Offset value for Load Cell channel 1.")]
    public partial class OffsetLoadCell1
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell1"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell1 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell1"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell1 register.")]
    public partial class TimestampedOffsetLoadCell1
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell1"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 2.
    /// </summary>
    [Description("Offset value for Load Cell channel 2.")]
    public partial class OffsetLoadCell2
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell2"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell2"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell2"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell2"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell2"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell2"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell2"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell2 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell2"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell2 register.")]
    public partial class TimestampedOffsetLoadCell2
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell2"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell2.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell2"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell2.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 3.
    /// </summary>
    [Description("Offset value for Load Cell channel 3.")]
    public partial class OffsetLoadCell3
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell3"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell3"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell3"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell3"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell3"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell3"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell3"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell3 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell3"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell3 register.")]
    public partial class TimestampedOffsetLoadCell3
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell3"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell3.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell3"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell3.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 4.
    /// </summary>
    [Description("Offset value for Load Cell channel 4.")]
    public partial class OffsetLoadCell4
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell4"/> register. This field is constant.
        /// </summary>
        public const int Address = 52;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell4"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell4"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell4"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell4"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell4"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell4"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell4"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell4"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell4 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell4"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell4 register.")]
    public partial class TimestampedOffsetLoadCell4
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell4"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell4.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell4"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell4.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 5.
    /// </summary>
    [Description("Offset value for Load Cell channel 5.")]
    public partial class OffsetLoadCell5
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell5"/> register. This field is constant.
        /// </summary>
        public const int Address = 53;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell5"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell5"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell5"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell5"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell5"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell5"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell5"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell5"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell5 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell5"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell5 register.")]
    public partial class TimestampedOffsetLoadCell5
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell5"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell5.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell5"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell5.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 6.
    /// </summary>
    [Description("Offset value for Load Cell channel 6.")]
    public partial class OffsetLoadCell6
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell6"/> register. This field is constant.
        /// </summary>
        public const int Address = 54;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell6"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell6"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell6"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell6"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell6"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell6"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell6"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell6"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell6 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell6"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell6 register.")]
    public partial class TimestampedOffsetLoadCell6
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell6"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell6.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell6"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell6.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that offset value for Load Cell channel 7.
    /// </summary>
    [Description("Offset value for Load Cell channel 7.")]
    public partial class OffsetLoadCell7
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell7"/> register. This field is constant.
        /// </summary>
        public const int Address = 55;

        /// <summary>
        /// Represents the payload type of the <see cref="OffsetLoadCell7"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="OffsetLoadCell7"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OffsetLoadCell7"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OffsetLoadCell7"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OffsetLoadCell7"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell7"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OffsetLoadCell7"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OffsetLoadCell7"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OffsetLoadCell7 register.
    /// </summary>
    /// <seealso cref="OffsetLoadCell7"/>
    [Description("Filters and selects timestamped messages from the OffsetLoadCell7 register.")]
    public partial class TimestampedOffsetLoadCell7
    {
        /// <summary>
        /// Represents the address of the <see cref="OffsetLoadCell7"/> register. This field is constant.
        /// </summary>
        public const int Address = OffsetLoadCell7.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OffsetLoadCell7"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return OffsetLoadCell7.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved4
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int Address = 56;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved4"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that reserved.
    /// </summary>
    [Description("Reserved")]
    internal partial class Reserved5
    {
        /// <summary>
        /// Represents the address of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int Address = 57;

        /// <summary>
        /// Represents the payload type of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="Reserved5"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
    public partial class DO1TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 58;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO1TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO1TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO1TargetLoadCell register.")]
    public partial class TimestampedDO1TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO1TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
    public partial class DO2TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 59;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO2TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO2TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO2TargetLoadCell register.")]
    public partial class TimestampedDO2TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO2TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
    public partial class DO3TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 60;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO3TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO3TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO3TargetLoadCell register.")]
    public partial class TimestampedDO3TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO3TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO4 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
    public partial class DO4TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 61;

        /// <summary>
        /// Represents the payload type of the <see cref="DO4TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO4TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO4TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO4TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO4TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO4TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO4TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO4TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO4TargetLoadCell register.")]
    public partial class TimestampedDO4TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO4TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO4TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO4TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO5 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
    public partial class DO5TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 62;

        /// <summary>
        /// Represents the payload type of the <see cref="DO5TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO5TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO5TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO5TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO5TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO5TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO5TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO5TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO5TargetLoadCell register.")]
    public partial class TimestampedDO5TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO5TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO5TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO5TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO6 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
    public partial class DO6TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 63;

        /// <summary>
        /// Represents the payload type of the <see cref="DO6TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO6TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO6TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO6TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO6TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO6TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO6TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO6TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO6TargetLoadCell register.")]
    public partial class TimestampedDO6TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO6TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO6TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO6TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO7 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
    public partial class DO7TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 64;

        /// <summary>
        /// Represents the payload type of the <see cref="DO7TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO7TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO7TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO7TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO7TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO7TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO7TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO7TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO7TargetLoadCell register.")]
    public partial class TimestampedDO7TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO7TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO7TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO7TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that target Load Cell that will be used to trigger a threshold event on DO8 pin.
    /// </summary>
    [Description("Target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
    public partial class DO8TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = 65;

        /// <summary>
        /// Represents the payload type of the <see cref="DO8TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO8TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO8TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellChannel GetPayload(HarpMessage message)
        {
            return (LoadCellChannel)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO8TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellChannel)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO8TargetLoadCell"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TargetLoadCell"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO8TargetLoadCell"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TargetLoadCell"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellChannel value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO8TargetLoadCell register.
    /// </summary>
    /// <seealso cref="DO8TargetLoadCell"/>
    [Description("Filters and selects timestamped messages from the DO8TargetLoadCell register.")]
    public partial class TimestampedDO8TargetLoadCell
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TargetLoadCell"/> register. This field is constant.
        /// </summary>
        public const int Address = DO8TargetLoadCell.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO8TargetLoadCell"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellChannel> GetPayload(HarpMessage message)
        {
            return DO8TargetLoadCell.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO1 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO1 pin.")]
    public partial class DO1Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 66;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1Threshold register.
    /// </summary>
    /// <seealso cref="DO1Threshold"/>
    [Description("Filters and selects timestamped messages from the DO1Threshold register.")]
    public partial class TimestampedDO1Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO1Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO2 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO2 pin.")]
    public partial class DO2Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 67;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2Threshold register.
    /// </summary>
    /// <seealso cref="DO2Threshold"/>
    [Description("Filters and selects timestamped messages from the DO2Threshold register.")]
    public partial class TimestampedDO2Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO2Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO3 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO3 pin.")]
    public partial class DO3Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 68;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3Threshold register.
    /// </summary>
    /// <seealso cref="DO3Threshold"/>
    [Description("Filters and selects timestamped messages from the DO3Threshold register.")]
    public partial class TimestampedDO3Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO3Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO4 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO4 pin.")]
    public partial class DO4Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 69;

        /// <summary>
        /// Represents the payload type of the <see cref="DO4Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO4Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO4Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO4Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO4Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO4Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO4Threshold register.
    /// </summary>
    /// <seealso cref="DO4Threshold"/>
    [Description("Filters and selects timestamped messages from the DO4Threshold register.")]
    public partial class TimestampedDO4Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO4Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO4Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO4Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO5 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO5 pin.")]
    public partial class DO5Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 70;

        /// <summary>
        /// Represents the payload type of the <see cref="DO5Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO5Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO5Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO5Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO5Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO5Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO5Threshold register.
    /// </summary>
    /// <seealso cref="DO5Threshold"/>
    [Description("Filters and selects timestamped messages from the DO5Threshold register.")]
    public partial class TimestampedDO5Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO5Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO5Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO5Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO6 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO6 pin.")]
    public partial class DO6Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 71;

        /// <summary>
        /// Represents the payload type of the <see cref="DO6Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO6Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO6Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO6Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO6Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO6Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO6Threshold register.
    /// </summary>
    /// <seealso cref="DO6Threshold"/>
    [Description("Filters and selects timestamped messages from the DO6Threshold register.")]
    public partial class TimestampedDO6Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO6Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO6Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO6Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO7 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO7 pin.")]
    public partial class DO7Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 72;

        /// <summary>
        /// Represents the payload type of the <see cref="DO7Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO7Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO7Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO7Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO7Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO7Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO7Threshold register.
    /// </summary>
    /// <seealso cref="DO7Threshold"/>
    [Description("Filters and selects timestamped messages from the DO7Threshold register.")]
    public partial class TimestampedDO7Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO7Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO7Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO7Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that value used to threshold a Load Cell read, and trigger DO8 pin.
    /// </summary>
    [Description("Value used to threshold a Load Cell read, and trigger DO8 pin.")]
    public partial class DO8Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 73;

        /// <summary>
        /// Represents the payload type of the <see cref="DO8Threshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.S16;

        /// <summary>
        /// Represents the length of the <see cref="DO8Threshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO8Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static short GetPayload(HarpMessage message)
        {
            return message.GetPayloadInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO8Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO8Threshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8Threshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO8Threshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8Threshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, short value)
        {
            return HarpMessage.FromInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO8Threshold register.
    /// </summary>
    /// <seealso cref="DO8Threshold"/>
    [Description("Filters and selects timestamped messages from the DO8Threshold register.")]
    public partial class TimestampedDO8Threshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8Threshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO8Threshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO8Threshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<short> GetPayload(HarpMessage message)
        {
            return DO8Threshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 74;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO1TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO1TimeAboveThreshold register.")]
    public partial class TimestampedDO1TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 75;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO2TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO2TimeAboveThreshold register.")]
    public partial class TimestampedDO2TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 76;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO3TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO3TimeAboveThreshold register.")]
    public partial class TimestampedDO3TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO4 pin event.")]
    public partial class DO4TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 77;

        /// <summary>
        /// Represents the payload type of the <see cref="DO4TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO4TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO4TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO4TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO4TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO4TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO4TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO4TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO4TimeAboveThreshold register.")]
    public partial class TimestampedDO4TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO4TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO4TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO4TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO5 pin event.")]
    public partial class DO5TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 78;

        /// <summary>
        /// Represents the payload type of the <see cref="DO5TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO5TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO5TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO5TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO5TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO5TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO5TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO5TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO5TimeAboveThreshold register.")]
    public partial class TimestampedDO5TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO5TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO5TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO5TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO6 pin event.")]
    public partial class DO6TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 79;

        /// <summary>
        /// Represents the payload type of the <see cref="DO6TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO6TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO6TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO6TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO6TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO6TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO6TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO6TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO6TimeAboveThreshold register.")]
    public partial class TimestampedDO6TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO6TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO6TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO6TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO7 pin event.")]
    public partial class DO7TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 80;

        /// <summary>
        /// Represents the payload type of the <see cref="DO7TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO7TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO7TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO7TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO7TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO7TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO7TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO7TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO7TimeAboveThreshold register.")]
    public partial class TimestampedDO7TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO7TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO7TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO7TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) above threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [Description("Time (ms) above threshold value that is required to trigger a DO8 pin event.")]
    public partial class DO8TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 81;

        /// <summary>
        /// Represents the payload type of the <see cref="DO8TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO8TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO8TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO8TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO8TimeAboveThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TimeAboveThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO8TimeAboveThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TimeAboveThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO8TimeAboveThreshold register.
    /// </summary>
    /// <seealso cref="DO8TimeAboveThreshold"/>
    [Description("Filters and selects timestamped messages from the DO8TimeAboveThreshold register.")]
    public partial class TimestampedDO8TimeAboveThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TimeAboveThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO8TimeAboveThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO8TimeAboveThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO8TimeAboveThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class DO1TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 82;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO1TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO1TimeBelowThreshold register.")]
    public partial class TimestampedDO1TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO1TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class DO2TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 83;

        /// <summary>
        /// Represents the payload type of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO2TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO2TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO2TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO2TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO2TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO2TimeBelowThreshold register.")]
    public partial class TimestampedDO2TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO2TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO2TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO2TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO2TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class DO3TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 84;

        /// <summary>
        /// Represents the payload type of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO3TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO3TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO3TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO3TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO3TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO3TimeBelowThreshold register.")]
    public partial class TimestampedDO3TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO3TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO3TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO3TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO3TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO4 pin event.")]
    public partial class DO4TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 85;

        /// <summary>
        /// Represents the payload type of the <see cref="DO4TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO4TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO4TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO4TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO4TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO4TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO4TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO4TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO4TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO4TimeBelowThreshold register.")]
    public partial class TimestampedDO4TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO4TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO4TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO4TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO4TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO5 pin event.")]
    public partial class DO5TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 86;

        /// <summary>
        /// Represents the payload type of the <see cref="DO5TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO5TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO5TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO5TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO5TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO5TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO5TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO5TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO5TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO5TimeBelowThreshold register.")]
    public partial class TimestampedDO5TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO5TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO5TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO5TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO5TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO6 pin event.")]
    public partial class DO6TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 87;

        /// <summary>
        /// Represents the payload type of the <see cref="DO6TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO6TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO6TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO6TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO6TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO6TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO6TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO6TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO6TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO6TimeBelowThreshold register.")]
    public partial class TimestampedDO6TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO6TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO6TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO6TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO6TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO7 pin event.")]
    public partial class DO7TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 88;

        /// <summary>
        /// Represents the payload type of the <see cref="DO7TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO7TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO7TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO7TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO7TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO7TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO7TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO7TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO7TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO7TimeBelowThreshold register.")]
    public partial class TimestampedDO7TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO7TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO7TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO7TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO7TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that time (ms) below threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [Description("Time (ms) below threshold value that is required to trigger a DO8 pin event.")]
    public partial class DO8TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = 89;

        /// <summary>
        /// Represents the payload type of the <see cref="DO8TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DO8TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO8TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO8TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO8TimeBelowThreshold"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TimeBelowThreshold"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO8TimeBelowThreshold"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO8TimeBelowThreshold"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO8TimeBelowThreshold register.
    /// </summary>
    /// <seealso cref="DO8TimeBelowThreshold"/>
    [Description("Filters and selects timestamped messages from the DO8TimeBelowThreshold register.")]
    public partial class TimestampedDO8TimeBelowThreshold
    {
        /// <summary>
        /// Represents the address of the <see cref="DO8TimeBelowThreshold"/> register. This field is constant.
        /// </summary>
        public const int Address = DO8TimeBelowThreshold.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO8TimeBelowThreshold"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DO8TimeBelowThreshold.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the active events in the device.
    /// </summary>
    [Description("Specifies the active events in the device.")]
    public partial class EnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = 90;

        /// <summary>
        /// Represents the payload type of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static LoadCellEvents GetPayload(HarpMessage message)
        {
            return (LoadCellEvents)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellEvents> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((LoadCellEvents)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EnableEvents"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, LoadCellEvents value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EnableEvents"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EnableEvents"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, LoadCellEvents value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EnableEvents register.
    /// </summary>
    /// <seealso cref="EnableEvents"/>
    [Description("Filters and selects timestamped messages from the EnableEvents register.")]
    public partial class TimestampedEnableEvents
    {
        /// <summary>
        /// Represents the address of the <see cref="EnableEvents"/> register. This field is constant.
        /// </summary>
        public const int Address = EnableEvents.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EnableEvents"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<LoadCellEvents> GetPayload(HarpMessage message)
        {
            return EnableEvents.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// LoadCells device.
    /// </summary>
    /// <seealso cref="CreateAcquisitionStatePayload"/>
    /// <seealso cref="CreateLoadCellDataPayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateSyncOutputStatePayload"/>
    /// <seealso cref="CreateDI0TriggerPayload"/>
    /// <seealso cref="CreateDO0SyncPayload"/>
    /// <seealso cref="CreateDO0PulseWidthPayload"/>
    /// <seealso cref="CreateDigitalOutputSetPayload"/>
    /// <seealso cref="CreateDigitalOutputClearPayload"/>
    /// <seealso cref="CreateDigitalOutputTogglePayload"/>
    /// <seealso cref="CreateDigitalOutputStatePayload"/>
    /// <seealso cref="CreateOffsetLoadCell0Payload"/>
    /// <seealso cref="CreateOffsetLoadCell1Payload"/>
    /// <seealso cref="CreateOffsetLoadCell2Payload"/>
    /// <seealso cref="CreateOffsetLoadCell3Payload"/>
    /// <seealso cref="CreateOffsetLoadCell4Payload"/>
    /// <seealso cref="CreateOffsetLoadCell5Payload"/>
    /// <seealso cref="CreateOffsetLoadCell6Payload"/>
    /// <seealso cref="CreateOffsetLoadCell7Payload"/>
    /// <seealso cref="CreateDO1TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO2TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO3TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO4TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO5TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO6TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO7TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO8TargetLoadCellPayload"/>
    /// <seealso cref="CreateDO1ThresholdPayload"/>
    /// <seealso cref="CreateDO2ThresholdPayload"/>
    /// <seealso cref="CreateDO3ThresholdPayload"/>
    /// <seealso cref="CreateDO4ThresholdPayload"/>
    /// <seealso cref="CreateDO5ThresholdPayload"/>
    /// <seealso cref="CreateDO6ThresholdPayload"/>
    /// <seealso cref="CreateDO7ThresholdPayload"/>
    /// <seealso cref="CreateDO8ThresholdPayload"/>
    /// <seealso cref="CreateDO1TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO2TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO3TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO4TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO5TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO6TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO7TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO8TimeAboveThresholdPayload"/>
    /// <seealso cref="CreateDO1TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO2TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO3TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO4TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO5TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO6TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO7TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateDO8TimeBelowThresholdPayload"/>
    /// <seealso cref="CreateEnableEventsPayload"/>
    [XmlInclude(typeof(CreateAcquisitionStatePayload))]
    [XmlInclude(typeof(CreateLoadCellDataPayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateSyncOutputStatePayload))]
    [XmlInclude(typeof(CreateDI0TriggerPayload))]
    [XmlInclude(typeof(CreateDO0SyncPayload))]
    [XmlInclude(typeof(CreateDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateDigitalOutputSetPayload))]
    [XmlInclude(typeof(CreateDigitalOutputClearPayload))]
    [XmlInclude(typeof(CreateDigitalOutputTogglePayload))]
    [XmlInclude(typeof(CreateDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateOffsetLoadCell0Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell1Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell2Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell3Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell4Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell5Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell6Payload))]
    [XmlInclude(typeof(CreateOffsetLoadCell7Payload))]
    [XmlInclude(typeof(CreateDO1TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO2TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO3TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO4TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO5TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO6TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO7TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO8TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateDO1ThresholdPayload))]
    [XmlInclude(typeof(CreateDO2ThresholdPayload))]
    [XmlInclude(typeof(CreateDO3ThresholdPayload))]
    [XmlInclude(typeof(CreateDO4ThresholdPayload))]
    [XmlInclude(typeof(CreateDO5ThresholdPayload))]
    [XmlInclude(typeof(CreateDO6ThresholdPayload))]
    [XmlInclude(typeof(CreateDO7ThresholdPayload))]
    [XmlInclude(typeof(CreateDO8ThresholdPayload))]
    [XmlInclude(typeof(CreateDO1TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO2TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO3TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO4TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO5TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO6TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO7TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO8TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateDO1TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO2TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO3TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO4TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO5TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO6TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO7TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateDO8TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateEnableEventsPayload))]
    [XmlInclude(typeof(CreateTimestampedAcquisitionStatePayload))]
    [XmlInclude(typeof(CreateTimestampedLoadCellDataPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedSyncOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedDI0TriggerPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0SyncPayload))]
    [XmlInclude(typeof(CreateTimestampedDO0PulseWidthPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputSetPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputClearPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputTogglePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell0Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell1Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell2Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell3Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell4Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell5Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell6Payload))]
    [XmlInclude(typeof(CreateTimestampedOffsetLoadCell7Payload))]
    [XmlInclude(typeof(CreateTimestampedDO1TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO4TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO5TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO6TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO7TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO8TargetLoadCellPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO4ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO5ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO6ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO7ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO8ThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO4TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO5TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO6TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO7TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO8TimeAboveThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO1TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO2TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO3TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO4TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO5TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO6TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO7TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedDO8TimeBelowThresholdPayload))]
    [XmlInclude(typeof(CreateTimestampedEnableEventsPayload))]
    [Description("Creates standard message payloads for the LoadCells device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateAcquisitionStatePayload();
        }

        string INamedElement.Name => $"{nameof(LoadCells)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("AcquisitionStatePayload")]
    [Description("Creates a message payload that enables the data acquisition.")]
    public partial class CreateAcquisitionStatePayload
    {
        /// <summary>
        /// Gets or sets the value that enables the data acquisition.
        /// </summary>
        [Description("The value that enables the data acquisition.")]
        public EnableFlag AcquisitionState { get; set; }

        /// <summary>
        /// Creates a message payload for the AcquisitionState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EnableFlag GetPayload()
        {
            return AcquisitionState;
        }

        /// <summary>
        /// Creates a message that enables the data acquisition.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the AcquisitionState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.AcquisitionState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("TimestampedAcquisitionStatePayload")]
    [Description("Creates a timestamped message payload that enables the data acquisition.")]
    public partial class CreateTimestampedAcquisitionStatePayload : CreateAcquisitionStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that enables the data acquisition.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the AcquisitionState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.AcquisitionState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value of single ADC read from all load cell channels.
    /// </summary>
    [DisplayName("LoadCellDataPayload")]
    [Description("Creates a message payload that value of single ADC read from all load cell channels.")]
    public partial class CreateLoadCellDataPayload
    {
        /// <summary>
        /// Gets or sets a value to write on payload member Channel0.
        /// </summary>
        [Description("")]
        public short Channel0 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel1.
        /// </summary>
        [Description("")]
        public short Channel1 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel2.
        /// </summary>
        [Description("")]
        public short Channel2 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel3.
        /// </summary>
        [Description("")]
        public short Channel3 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel4.
        /// </summary>
        [Description("")]
        public short Channel4 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel5.
        /// </summary>
        [Description("")]
        public short Channel5 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel6.
        /// </summary>
        [Description("")]
        public short Channel6 { get; set; }

        /// <summary>
        /// Gets or sets a value to write on payload member Channel7.
        /// </summary>
        [Description("")]
        public short Channel7 { get; set; }

        /// <summary>
        /// Creates a message payload for the LoadCellData register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellDataPayload GetPayload()
        {
            LoadCellDataPayload value;
            value.Channel0 = Channel0;
            value.Channel1 = Channel1;
            value.Channel2 = Channel2;
            value.Channel3 = Channel3;
            value.Channel4 = Channel4;
            value.Channel5 = Channel5;
            value.Channel6 = Channel6;
            value.Channel7 = Channel7;
            return value;
        }

        /// <summary>
        /// Creates a message that value of single ADC read from all load cell channels.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the LoadCellData register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.LoadCellData.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value of single ADC read from all load cell channels.
    /// </summary>
    [DisplayName("TimestampedLoadCellDataPayload")]
    [Description("Creates a timestamped message payload that value of single ADC read from all load cell channels.")]
    public partial class CreateTimestampedLoadCellDataPayload : CreateLoadCellDataPayload
    {
        /// <summary>
        /// Creates a timestamped message that value of single ADC read from all load cell channels.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the LoadCellData register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.LoadCellData.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [Description("Creates a message payload that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
    public partial class CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        [Description("The value that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputState;
        }

        /// <summary>
        /// Creates a message that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DigitalInputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
    /// </summary>
    [DisplayName("TimestampedDigitalInputStatePayload")]
    [Description("Creates a timestamped message payload that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
    public partial class CreateTimestampedDigitalInputStatePayload : CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DigitalInputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
    /// </summary>
    [DisplayName("SyncOutputStatePayload")]
    [Description("Creates a message payload that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
    public partial class CreateSyncOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        [Description("The value that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
        public SyncOutputs SyncOutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the SyncOutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public SyncOutputs GetPayload()
        {
            return SyncOutputState;
        }

        /// <summary>
        /// Creates a message that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SyncOutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.SyncOutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
    /// </summary>
    [DisplayName("TimestampedSyncOutputStatePayload")]
    [Description("Creates a timestamped message payload that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
    public partial class CreateTimestampedSyncOutputStatePayload : CreateSyncOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SyncOutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.SyncOutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("DI0TriggerPayload")]
    [Description("Creates a message payload that configuration of the digital input pin 0.")]
    public partial class CreateDI0TriggerPayload
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital input pin 0.
        /// </summary>
        [Description("The value that configuration of the digital input pin 0.")]
        public TriggerConfig DI0Trigger { get; set; }

        /// <summary>
        /// Creates a message payload for the DI0Trigger register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public TriggerConfig GetPayload()
        {
            return DI0Trigger;
        }

        /// <summary>
        /// Creates a message that configuration of the digital input pin 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DI0Trigger register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DI0Trigger.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("TimestampedDI0TriggerPayload")]
    [Description("Creates a timestamped message payload that configuration of the digital input pin 0.")]
    public partial class CreateTimestampedDI0TriggerPayload : CreateDI0TriggerPayload
    {
        /// <summary>
        /// Creates a timestamped message that configuration of the digital input pin 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DI0Trigger register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DI0Trigger.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("DO0SyncPayload")]
    [Description("Creates a message payload that configuration of the digital output pin 0.")]
    public partial class CreateDO0SyncPayload
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital output pin 0.
        /// </summary>
        [Description("The value that configuration of the digital output pin 0.")]
        public SyncConfig DO0Sync { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0Sync register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public SyncConfig GetPayload()
        {
            return DO0Sync;
        }

        /// <summary>
        /// Creates a message that configuration of the digital output pin 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0Sync register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO0Sync.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("TimestampedDO0SyncPayload")]
    [Description("Creates a timestamped message payload that configuration of the digital output pin 0.")]
    public partial class CreateTimestampedDO0SyncPayload : CreateDO0SyncPayload
    {
        /// <summary>
        /// Creates a timestamped message that configuration of the digital output pin 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0Sync register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO0Sync.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [DisplayName("DO0PulseWidthPayload")]
    [Description("Creates a message payload that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Gets or sets the value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        [Range(min: 1, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
        public byte DO0PulseWidth { get; set; } = 1;

        /// <summary>
        /// Creates a message payload for the DO0PulseWidth register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return DO0PulseWidth;
        }

        /// <summary>
        /// Creates a message that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO0PulseWidth.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [DisplayName("TimestampedDO0PulseWidthPayload")]
    [Description("Creates a timestamped message payload that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class CreateTimestampedDO0PulseWidthPayload : CreateDO0PulseWidthPayload
    {
        /// <summary>
        /// Creates a timestamped message that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0PulseWidth register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO0PulseWidth.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputSetPayload")]
    [Description("Creates a message payload that set the specified digital output lines.")]
    public partial class CreateDigitalOutputSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs DigitalOutputSet { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputSet;
        }

        /// <summary>
        /// Creates a message that set the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputSetPayload")]
    [Description("Creates a timestamped message payload that set the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputSetPayload : CreateDigitalOutputSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputClearPayload")]
    [Description("Creates a message payload that clear the specified digital output lines.")]
    public partial class CreateDigitalOutputClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs DigitalOutputClear { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputClear;
        }

        /// <summary>
        /// Creates a message that clear the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputClearPayload : CreateDigitalOutputClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputTogglePayload")]
    [Description("Creates a message payload that toggle the specified digital output lines.")]
    public partial class CreateDigitalOutputTogglePayload
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs DigitalOutputToggle { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputToggle register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputToggle;
        }

        /// <summary>
        /// Creates a message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputToggle register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputToggle.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputTogglePayload")]
    [Description("Creates a timestamped message payload that toggle the specified digital output lines.")]
    public partial class CreateTimestampedDigitalOutputTogglePayload : CreateDigitalOutputTogglePayload
    {
        /// <summary>
        /// Creates a timestamped message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputToggle register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputToggle.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [DisplayName("DigitalOutputStatePayload")]
    [Description("Creates a message payload that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class CreateDigitalOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        [Description("The value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
        public DigitalOutputs DigitalOutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return DigitalOutputState;
        }

        /// <summary>
        /// Creates a message that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class CreateTimestampedDigitalOutputStatePayload : CreateDigitalOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DigitalOutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 0.
    /// </summary>
    [DisplayName("OffsetLoadCell0Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 0.")]
    public partial class CreateOffsetLoadCell0Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 0.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 0.")]
        public short OffsetLoadCell0 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell0;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 0.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 0.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell0Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 0.")]
    public partial class CreateTimestampedOffsetLoadCell0Payload : CreateOffsetLoadCell0Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 0.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 1.
    /// </summary>
    [DisplayName("OffsetLoadCell1Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 1.")]
    public partial class CreateOffsetLoadCell1Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 1.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 1.")]
        public short OffsetLoadCell1 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell1;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 1.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 1.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell1Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 1.")]
    public partial class CreateTimestampedOffsetLoadCell1Payload : CreateOffsetLoadCell1Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 1.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 2.
    /// </summary>
    [DisplayName("OffsetLoadCell2Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 2.")]
    public partial class CreateOffsetLoadCell2Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 2.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 2.")]
        public short OffsetLoadCell2 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell2 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell2;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 2.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell2 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell2.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 2.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell2Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 2.")]
    public partial class CreateTimestampedOffsetLoadCell2Payload : CreateOffsetLoadCell2Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 2.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell2 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell2.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 3.
    /// </summary>
    [DisplayName("OffsetLoadCell3Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 3.")]
    public partial class CreateOffsetLoadCell3Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 3.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 3.")]
        public short OffsetLoadCell3 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell3 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell3;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 3.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell3 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell3.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 3.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell3Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 3.")]
    public partial class CreateTimestampedOffsetLoadCell3Payload : CreateOffsetLoadCell3Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 3.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell3 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell3.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 4.
    /// </summary>
    [DisplayName("OffsetLoadCell4Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 4.")]
    public partial class CreateOffsetLoadCell4Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 4.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 4.")]
        public short OffsetLoadCell4 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell4 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell4;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 4.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell4 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell4.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 4.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell4Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 4.")]
    public partial class CreateTimestampedOffsetLoadCell4Payload : CreateOffsetLoadCell4Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 4.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell4 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell4.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 5.
    /// </summary>
    [DisplayName("OffsetLoadCell5Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 5.")]
    public partial class CreateOffsetLoadCell5Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 5.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 5.")]
        public short OffsetLoadCell5 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell5 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell5;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 5.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell5 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell5.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 5.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell5Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 5.")]
    public partial class CreateTimestampedOffsetLoadCell5Payload : CreateOffsetLoadCell5Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 5.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell5 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell5.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 6.
    /// </summary>
    [DisplayName("OffsetLoadCell6Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 6.")]
    public partial class CreateOffsetLoadCell6Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 6.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 6.")]
        public short OffsetLoadCell6 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell6 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell6;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 6.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell6 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell6.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 6.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell6Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 6.")]
    public partial class CreateTimestampedOffsetLoadCell6Payload : CreateOffsetLoadCell6Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 6.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell6 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell6.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that offset value for Load Cell channel 7.
    /// </summary>
    [DisplayName("OffsetLoadCell7Payload")]
    [Description("Creates a message payload that offset value for Load Cell channel 7.")]
    public partial class CreateOffsetLoadCell7Payload
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 7.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 7.")]
        public short OffsetLoadCell7 { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the OffsetLoadCell7 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return OffsetLoadCell7;
        }

        /// <summary>
        /// Creates a message that offset value for Load Cell channel 7.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OffsetLoadCell7 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell7.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that offset value for Load Cell channel 7.
    /// </summary>
    [DisplayName("TimestampedOffsetLoadCell7Payload")]
    [Description("Creates a timestamped message payload that offset value for Load Cell channel 7.")]
    public partial class CreateTimestampedOffsetLoadCell7Payload : CreateOffsetLoadCell7Payload
    {
        /// <summary>
        /// Creates a timestamped message that offset value for Load Cell channel 7.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OffsetLoadCell7 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.OffsetLoadCell7.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("DO1TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateDO1TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
        public LoadCellChannel DO1TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO1TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO1TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO1TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("TimestampedDO1TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateTimestampedDO1TargetLoadCellPayload : CreateDO1TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO1TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("DO2TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateDO2TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
        public LoadCellChannel DO2TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO2TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO2TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO2TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("TimestampedDO2TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateTimestampedDO2TargetLoadCellPayload : CreateDO2TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO2TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("DO3TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateDO3TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
        public LoadCellChannel DO3TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO3TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO3TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO3TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("TimestampedDO3TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateTimestampedDO3TargetLoadCellPayload : CreateDO3TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO3TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO4 pin.
    /// </summary>
    [DisplayName("DO4TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
    public partial class CreateDO4TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
        public LoadCellChannel DO4TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO4TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO4TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO4TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO4TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO4 pin.
    /// </summary>
    [DisplayName("TimestampedDO4TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
    public partial class CreateTimestampedDO4TargetLoadCellPayload : CreateDO4TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO4TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO4TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO5 pin.
    /// </summary>
    [DisplayName("DO5TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
    public partial class CreateDO5TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
        public LoadCellChannel DO5TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO5TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO5TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO5TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO5TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO5 pin.
    /// </summary>
    [DisplayName("TimestampedDO5TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
    public partial class CreateTimestampedDO5TargetLoadCellPayload : CreateDO5TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO5TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO5TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO6 pin.
    /// </summary>
    [DisplayName("DO6TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
    public partial class CreateDO6TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
        public LoadCellChannel DO6TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO6TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO6TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO6TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO6TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO6 pin.
    /// </summary>
    [DisplayName("TimestampedDO6TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
    public partial class CreateTimestampedDO6TargetLoadCellPayload : CreateDO6TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO6TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO6TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO7 pin.
    /// </summary>
    [DisplayName("DO7TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
    public partial class CreateDO7TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
        public LoadCellChannel DO7TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO7TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO7TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO7TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO7TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO7 pin.
    /// </summary>
    [DisplayName("TimestampedDO7TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
    public partial class CreateTimestampedDO7TargetLoadCellPayload : CreateDO7TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO7TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO7TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO8 pin.
    /// </summary>
    [DisplayName("DO8TargetLoadCellPayload")]
    [Description("Creates a message payload that target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
    public partial class CreateDO8TargetLoadCellPayload
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
        public LoadCellChannel DO8TargetLoadCell { get; set; }

        /// <summary>
        /// Creates a message payload for the DO8TargetLoadCell register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellChannel GetPayload()
        {
            return DO8TargetLoadCell;
        }

        /// <summary>
        /// Creates a message that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO8TargetLoadCell register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO8TargetLoadCell.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that target Load Cell that will be used to trigger a threshold event on DO8 pin.
    /// </summary>
    [DisplayName("TimestampedDO8TargetLoadCellPayload")]
    [Description("Creates a timestamped message payload that target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
    public partial class CreateTimestampedDO8TargetLoadCellPayload : CreateDO8TargetLoadCellPayload
    {
        /// <summary>
        /// Creates a timestamped message that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO8TargetLoadCell register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO8TargetLoadCell.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("DO1ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO1 pin.")]
    public partial class CreateDO1ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO1 pin.")]
        public short DO1Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO1Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO1Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO1Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("TimestampedDO1ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO1 pin.")]
    public partial class CreateTimestampedDO1ThresholdPayload : CreateDO1ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO1Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("DO2ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO2 pin.")]
    public partial class CreateDO2ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO2 pin.")]
        public short DO2Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO2Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO2Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO2Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("TimestampedDO2ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO2 pin.")]
    public partial class CreateTimestampedDO2ThresholdPayload : CreateDO2ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO2Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("DO3ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO3 pin.")]
    public partial class CreateDO3ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO3 pin.")]
        public short DO3Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO3Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO3Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO3Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("TimestampedDO3ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO3 pin.")]
    public partial class CreateTimestampedDO3ThresholdPayload : CreateDO3ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO3Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO4 pin.
    /// </summary>
    [DisplayName("DO4ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO4 pin.")]
    public partial class CreateDO4ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO4 pin.")]
        public short DO4Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO4Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO4Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO4Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO4Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO4 pin.
    /// </summary>
    [DisplayName("TimestampedDO4ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO4 pin.")]
    public partial class CreateTimestampedDO4ThresholdPayload : CreateDO4ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO4Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO4Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO5 pin.
    /// </summary>
    [DisplayName("DO5ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO5 pin.")]
    public partial class CreateDO5ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO5 pin.")]
        public short DO5Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO5Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO5Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO5Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO5Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO5 pin.
    /// </summary>
    [DisplayName("TimestampedDO5ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO5 pin.")]
    public partial class CreateTimestampedDO5ThresholdPayload : CreateDO5ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO5Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO5Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO6 pin.
    /// </summary>
    [DisplayName("DO6ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO6 pin.")]
    public partial class CreateDO6ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO6 pin.")]
        public short DO6Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO6Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO6Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO6Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO6Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO6 pin.
    /// </summary>
    [DisplayName("TimestampedDO6ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO6 pin.")]
    public partial class CreateTimestampedDO6ThresholdPayload : CreateDO6ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO6Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO6Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO7 pin.
    /// </summary>
    [DisplayName("DO7ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO7 pin.")]
    public partial class CreateDO7ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO7 pin.")]
        public short DO7Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO7Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO7Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO7Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO7Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO7 pin.
    /// </summary>
    [DisplayName("TimestampedDO7ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO7 pin.")]
    public partial class CreateTimestampedDO7ThresholdPayload : CreateDO7ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO7Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO7Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that value used to threshold a Load Cell read, and trigger DO8 pin.
    /// </summary>
    [DisplayName("DO8ThresholdPayload")]
    [Description("Creates a message payload that value used to threshold a Load Cell read, and trigger DO8 pin.")]
    public partial class CreateDO8ThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO8 pin.")]
        public short DO8Threshold { get; set; }

        /// <summary>
        /// Creates a message payload for the DO8Threshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public short GetPayload()
        {
            return DO8Threshold;
        }

        /// <summary>
        /// Creates a message that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO8Threshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO8Threshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that value used to threshold a Load Cell read, and trigger DO8 pin.
    /// </summary>
    [DisplayName("TimestampedDO8ThresholdPayload")]
    [Description("Creates a timestamped message payload that value used to threshold a Load Cell read, and trigger DO8 pin.")]
    public partial class CreateTimestampedDO8ThresholdPayload : CreateDO8ThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO8Threshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO8Threshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
        public ushort DO1TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO1TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO1TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO1TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("TimestampedDO1TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateTimestampedDO1TimeAboveThresholdPayload : CreateDO1TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO1TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
        public ushort DO2TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO2TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO2TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO2TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("TimestampedDO2TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateTimestampedDO2TimeAboveThresholdPayload : CreateDO2TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO2TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
        public ushort DO3TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO3TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO3TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO3TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("TimestampedDO3TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateTimestampedDO3TimeAboveThresholdPayload : CreateDO3TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO3TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("DO4TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateDO4TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO4 pin event.")]
        public ushort DO4TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO4TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO4TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO4TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO4TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("TimestampedDO4TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateTimestampedDO4TimeAboveThresholdPayload : CreateDO4TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO4TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO4TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("DO5TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateDO5TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO5 pin event.")]
        public ushort DO5TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO5TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO5TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO5TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO5TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("TimestampedDO5TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateTimestampedDO5TimeAboveThresholdPayload : CreateDO5TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO5TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO5TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("DO6TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateDO6TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO6 pin event.")]
        public ushort DO6TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO6TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO6TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO6TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO6TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("TimestampedDO6TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateTimestampedDO6TimeAboveThresholdPayload : CreateDO6TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO6TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO6TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("DO7TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateDO7TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO7 pin event.")]
        public ushort DO7TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO7TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO7TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO7TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO7TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("TimestampedDO7TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateTimestampedDO7TimeAboveThresholdPayload : CreateDO7TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO7TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO7TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) above threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("DO8TimeAboveThresholdPayload")]
    [Description("Creates a message payload that time (ms) above threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateDO8TimeAboveThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO8 pin event.")]
        public ushort DO8TimeAboveThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO8TimeAboveThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO8TimeAboveThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO8TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO8TimeAboveThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) above threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("TimestampedDO8TimeAboveThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) above threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateTimestampedDO8TimeAboveThresholdPayload : CreateDO8TimeAboveThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO8TimeAboveThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO8TimeAboveThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
        public ushort DO1TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO1TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO1TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO1TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("TimestampedDO1TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateTimestampedDO1TimeBelowThresholdPayload : CreateDO1TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO1TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
        public ushort DO2TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO2TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO2TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO2TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO2TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("TimestampedDO2TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateTimestampedDO2TimeBelowThresholdPayload : CreateDO2TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO2TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO2TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
        public ushort DO3TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO3TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO3TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO3TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO3TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("TimestampedDO3TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateTimestampedDO3TimeBelowThresholdPayload : CreateDO3TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO3TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO3TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("DO4TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateDO4TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO4 pin event.")]
        public ushort DO4TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO4TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO4TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO4TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO4TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("TimestampedDO4TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateTimestampedDO4TimeBelowThresholdPayload : CreateDO4TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO4TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO4TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("DO5TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateDO5TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO5 pin event.")]
        public ushort DO5TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO5TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO5TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO5TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO5TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("TimestampedDO5TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateTimestampedDO5TimeBelowThresholdPayload : CreateDO5TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO5TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO5TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("DO6TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateDO6TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO6 pin event.")]
        public ushort DO6TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO6TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO6TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO6TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO6TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("TimestampedDO6TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateTimestampedDO6TimeBelowThresholdPayload : CreateDO6TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO6TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO6TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("DO7TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateDO7TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO7 pin event.")]
        public ushort DO7TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO7TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO7TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO7TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO7TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("TimestampedDO7TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateTimestampedDO7TimeBelowThresholdPayload : CreateDO7TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO7TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO7TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that time (ms) below threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("DO8TimeBelowThresholdPayload")]
    [Description("Creates a message payload that time (ms) below threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateDO8TimeBelowThresholdPayload
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO8 pin event.")]
        public ushort DO8TimeBelowThreshold { get; set; } = 0;

        /// <summary>
        /// Creates a message payload for the DO8TimeBelowThreshold register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DO8TimeBelowThreshold;
        }

        /// <summary>
        /// Creates a message that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO8TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.DO8TimeBelowThreshold.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that time (ms) below threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("TimestampedDO8TimeBelowThresholdPayload")]
    [Description("Creates a timestamped message payload that time (ms) below threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateTimestampedDO8TimeBelowThresholdPayload : CreateDO8TimeBelowThresholdPayload
    {
        /// <summary>
        /// Creates a timestamped message that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO8TimeBelowThreshold register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.DO8TimeBelowThreshold.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EnableEventsPayload")]
    [Description("Creates a message payload that specifies the active events in the device.")]
    public partial class CreateEnableEventsPayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public LoadCellEvents EnableEvents { get; set; }

        /// <summary>
        /// Creates a message payload for the EnableEvents register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public LoadCellEvents GetPayload()
        {
            return EnableEvents;
        }

        /// <summary>
        /// Creates a message that specifies the active events in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EnableEvents register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.LoadCells.EnableEvents.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("TimestampedEnableEventsPayload")]
    [Description("Creates a timestamped message payload that specifies the active events in the device.")]
    public partial class CreateTimestampedEnableEventsPayload : CreateEnableEventsPayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the active events in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EnableEvents register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.LoadCells.EnableEvents.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents the payload of the LoadCellData register.
    /// </summary>
    public struct LoadCellDataPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCellDataPayload"/> structure.
        /// </summary>
        /// <param name="channel0"></param>
        /// <param name="channel1"></param>
        /// <param name="channel2"></param>
        /// <param name="channel3"></param>
        /// <param name="channel4"></param>
        /// <param name="channel5"></param>
        /// <param name="channel6"></param>
        /// <param name="channel7"></param>
        public LoadCellDataPayload(
            short channel0,
            short channel1,
            short channel2,
            short channel3,
            short channel4,
            short channel5,
            short channel6,
            short channel7)
        {
            Channel0 = channel0;
            Channel1 = channel1;
            Channel2 = channel2;
            Channel3 = channel3;
            Channel4 = channel4;
            Channel5 = channel5;
            Channel6 = channel6;
            Channel7 = channel7;
        }

        /// <summary>
        /// 
        /// </summary>
        public short Channel0;

        /// <summary>
        /// 
        /// </summary>
        public short Channel1;

        /// <summary>
        /// 
        /// </summary>
        public short Channel2;

        /// <summary>
        /// 
        /// </summary>
        public short Channel3;

        /// <summary>
        /// 
        /// </summary>
        public short Channel4;

        /// <summary>
        /// 
        /// </summary>
        public short Channel5;

        /// <summary>
        /// 
        /// </summary>
        public short Channel6;

        /// <summary>
        /// 
        /// </summary>
        public short Channel7;

        /// <summary>
        /// Returns a <see cref="string"/> that represents the payload of
        /// the LoadCellData register.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the payload of the
        /// LoadCellData register.
        /// </returns>
        public override string ToString()
        {
            return "LoadCellDataPayload { " +
                "Channel0 = " + Channel0 + ", " +
                "Channel1 = " + Channel1 + ", " +
                "Channel2 = " + Channel2 + ", " +
                "Channel3 = " + Channel3 + ", " +
                "Channel4 = " + Channel4 + ", " +
                "Channel5 = " + Channel5 + ", " +
                "Channel6 = " + Channel6 + ", " +
                "Channel7 = " + Channel7 + " " +
            "}";
        }
    }

    /// <summary>
    /// Available digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        DI0 = 0x1
    }

    /// <summary>
    /// Specifies the state output synchronization lines.
    /// </summary>
    [Flags]
    public enum SyncOutputs : byte
    {
        None = 0x0,
        DO0 = 0x1
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
        None = 0x0,
        DO1 = 0x1,
        DO2 = 0x2,
        DO3 = 0x4,
        DO4 = 0x8,
        DO5 = 0x10,
        DO6 = 0x20,
        DO7 = 0x40,
        DO8 = 0x80
    }

    /// <summary>
    /// The events that can be enabled/disabled.
    /// </summary>
    [Flags]
    public enum LoadCellEvents : byte
    {
        None = 0x0,
        LoadCellData = 0x1,
        DigitalInput = 0x2,
        SyncOutput = 0x4,
        Thresholds = 0x8
    }

    /// <summary>
    /// Available configurations when using a digital input as an acquisition trigger.
    /// </summary>
    public enum TriggerConfig : byte
    {
        None = 0,
        RisingEdge = 1,
        FallingEdge = 2
    }

    /// <summary>
    /// Available configurations when using a digital output pin to report firmware events.
    /// </summary>
    public enum SyncConfig : byte
    {
        None = 0,
        Heartbeat = 1,
        Pulse = 2
    }

    /// <summary>
    /// Available target load cells to be targeted on threshold events.
    /// </summary>
    public enum LoadCellChannel : byte
    {
        Channel0 = 0,
        Channel1 = 1,
        Channel2 = 2,
        Channel3 = 3,
        Channel4 = 4,
        Channel5 = 5,
        Channel6 = 6,
        Channel7 = 7,
        None = 8
    }
}
