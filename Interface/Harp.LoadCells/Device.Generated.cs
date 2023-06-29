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
            { 39, typeof(DI0Trigger) },
            { 40, typeof(DO0Sync) },
            { 41, typeof(DO0PulseWidth) },
            { 42, typeof(DigitalOutputSet) },
            { 43, typeof(DigitalOutputClear) },
            { 44, typeof(DigitalOutputToggle) },
            { 45, typeof(DigitalOutputState) },
            { 48, typeof(OffsetLoadCell0) },
            { 49, typeof(OffsetLoadCell1) },
            { 50, typeof(OffsetLoadCell2) },
            { 51, typeof(OffsetLoadCell3) },
            { 52, typeof(OffsetLoadCell4) },
            { 53, typeof(OffsetLoadCell5) },
            { 54, typeof(OffsetLoadCell6) },
            { 55, typeof(OffsetLoadCell7) },
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
    public class FilterMessage : FilterMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMessage"/> class.
        /// </summary>
        public FilterMessage()
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
    /// Represents an operator that creates a sequence of message payloads
    /// that enables the data acquisition.
    /// </summary>
    [DisplayName("AcquisitionStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that enables the data acquisition.")]
    public partial class CreateAcquisitionStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that enables the data acquisition.
        /// </summary>
        [Description("The value that enables the data acquisition.")]
        public EnableFlag Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that enables the data acquisition.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that enables the data acquisition.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => AcquisitionState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value of single ADC read from all load cell channels.
    /// </summary>
    [DisplayName("LoadCellDataPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value of single ADC read from all load cell channels.")]
    public partial class CreateLoadCellDataPayload : HarpCombinator
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
        /// Creates an observable sequence that contains a single message
        /// that value of single ADC read from all load cell channels.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value of single ADC read from all load cell channels.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ =>
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
                return LoadCellData.FromPayload(MessageType, value);
            });
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
    public partial class CreateDigitalInputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        [Description("The value that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.")]
        public DigitalInputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that status of the digital input pin 0. An event will be emitted when DI0Trigger == None.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalInputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
    /// </summary>
    [DisplayName("SyncOutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
    public partial class CreateSyncOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        [Description("The value that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.")]
        public SyncOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that status of the digital output pin 0. An periodic event will be emitted when DO0Sync == ToggleEachSecond.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => SyncOutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital input pin 0.
    /// </summary>
    [DisplayName("DI0TriggerPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital input pin 0.")]
    public partial class CreateDI0TriggerPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital input pin 0.
        /// </summary>
        [Description("The value that configuration of the digital input pin 0.")]
        public TriggerConfig Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital input pin 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital input pin 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DI0Trigger.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that configuration of the digital output pin 0.
    /// </summary>
    [DisplayName("DO0SyncPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that configuration of the digital output pin 0.")]
    public partial class CreateDO0SyncPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that configuration of the digital output pin 0.
        /// </summary>
        [Description("The value that configuration of the digital output pin 0.")]
        public SyncConfig Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that configuration of the digital output pin 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that configuration of the digital output pin 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0Sync.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
    /// </summary>
    [DisplayName("DO0PulseWidthPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
    public partial class CreateDO0PulseWidthPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        [Range(min: 1, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.")]
        public byte Value { get; set; } = 1;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that pulse duration (ms) for the digital output pin 0. The pulse will only be emitted when DO0Sync == Pulse.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO0PulseWidth.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputSetPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that set the specified digital output lines.")]
    public partial class CreateDigitalOutputSetPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that set the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that set the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalOutputSet.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputClearPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that clear the specified digital output lines.")]
    public partial class CreateDigitalOutputClearPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that clear the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that clear the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalOutputClear.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputTogglePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that toggle the specified digital output lines.")]
    public partial class CreateDigitalOutputTogglePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that toggle the specified digital output lines.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalOutputToggle.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
    /// </summary>
    [DisplayName("DigitalOutputStatePayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
    public partial class CreateDigitalOutputStatePayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        [Description("The value that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.")]
        public DigitalOutputs Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that write the state of all digital output lines. An event will be emitted when the value of any pin was changed by a threshold event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DigitalOutputState.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 0.
    /// </summary>
    [DisplayName("OffsetLoadCell0Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 0.")]
    public partial class CreateOffsetLoadCell0Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 0.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 0.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 0.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 0.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell0.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 1.
    /// </summary>
    [DisplayName("OffsetLoadCell1Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 1.")]
    public partial class CreateOffsetLoadCell1Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 1.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 1.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 1.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 1.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell1.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 2.
    /// </summary>
    [DisplayName("OffsetLoadCell2Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 2.")]
    public partial class CreateOffsetLoadCell2Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 2.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 2.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 2.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 2.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell2.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 3.
    /// </summary>
    [DisplayName("OffsetLoadCell3Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 3.")]
    public partial class CreateOffsetLoadCell3Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 3.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 3.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 3.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 3.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell3.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 4.
    /// </summary>
    [DisplayName("OffsetLoadCell4Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 4.")]
    public partial class CreateOffsetLoadCell4Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 4.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 4.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 4.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 4.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell4.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 5.
    /// </summary>
    [DisplayName("OffsetLoadCell5Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 5.")]
    public partial class CreateOffsetLoadCell5Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 5.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 5.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 5.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 5.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell5.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 6.
    /// </summary>
    [DisplayName("OffsetLoadCell6Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 6.")]
    public partial class CreateOffsetLoadCell6Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 6.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 6.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 6.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 6.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell6.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that offset value for Load Cell channel 7.
    /// </summary>
    [DisplayName("OffsetLoadCell7Payload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that offset value for Load Cell channel 7.")]
    public partial class CreateOffsetLoadCell7Payload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that offset value for Load Cell channel 7.
        /// </summary>
        [Range(min: -255, max: 255)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that offset value for Load Cell channel 7.")]
        public short Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that offset value for Load Cell channel 7.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that offset value for Load Cell channel 7.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => OffsetLoadCell7.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO1 pin.
    /// </summary>
    [DisplayName("DO1TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
    public partial class CreateDO1TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO1 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO1 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO2 pin.
    /// </summary>
    [DisplayName("DO2TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
    public partial class CreateDO2TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO2 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO2 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO3 pin.
    /// </summary>
    [DisplayName("DO3TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
    public partial class CreateDO3TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO3 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO3 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO4 pin.
    /// </summary>
    [DisplayName("DO4TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
    public partial class CreateDO4TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO4 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO4 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO4TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO5 pin.
    /// </summary>
    [DisplayName("DO5TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
    public partial class CreateDO5TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO5 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO5 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO5TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO6 pin.
    /// </summary>
    [DisplayName("DO6TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
    public partial class CreateDO6TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO6 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO6 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO6TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO7 pin.
    /// </summary>
    [DisplayName("DO7TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
    public partial class CreateDO7TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO7 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO7 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO7TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that target Load Cell that will be used to trigger a threshold event on DO8 pin.
    /// </summary>
    [DisplayName("DO8TargetLoadCellPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
    public partial class CreateDO8TargetLoadCellPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        [Description("The value that target Load Cell that will be used to trigger a threshold event on DO8 pin.")]
        public LoadCellChannel Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that target Load Cell that will be used to trigger a threshold event on DO8 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO8TargetLoadCell.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO1 pin.
    /// </summary>
    [DisplayName("DO1ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO1 pin.")]
    public partial class CreateDO1ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO1 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO1 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO2 pin.
    /// </summary>
    [DisplayName("DO2ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO2 pin.")]
    public partial class CreateDO2ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO2 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO2 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO3 pin.
    /// </summary>
    [DisplayName("DO3ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO3 pin.")]
    public partial class CreateDO3ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO3 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO3 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO4 pin.
    /// </summary>
    [DisplayName("DO4ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO4 pin.")]
    public partial class CreateDO4ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO4 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO4 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO4Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO5 pin.
    /// </summary>
    [DisplayName("DO5ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO5 pin.")]
    public partial class CreateDO5ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO5 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO5 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO5Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO6 pin.
    /// </summary>
    [DisplayName("DO6ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO6 pin.")]
    public partial class CreateDO6ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO6 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO6 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO6Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO7 pin.
    /// </summary>
    [DisplayName("DO7ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO7 pin.")]
    public partial class CreateDO7ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO7 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO7 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO7Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that value used to threshold a Load Cell read, and trigger DO8 pin.
    /// </summary>
    [DisplayName("DO8ThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that value used to threshold a Load Cell read, and trigger DO8 pin.")]
    public partial class CreateDO8ThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        [Description("The value that value used to threshold a Load Cell read, and trigger DO8 pin.")]
        public short Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that value used to threshold a Load Cell read, and trigger DO8 pin.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO8Threshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO1 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO2 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO3 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("DO4TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateDO4TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO4 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO4TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("DO5TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateDO5TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO5 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO5TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("DO6TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateDO6TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO6 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO6TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("DO7TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateDO7TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO7 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO7TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) above threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("DO8TimeAboveThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) above threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateDO8TimeAboveThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        [Description("The value that time (ms) above threshold value that is required to trigger a DO8 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) above threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO8TimeAboveThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
    /// </summary>
    [DisplayName("DO1TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
    public partial class CreateDO1TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO1 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO1 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO1TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
    /// </summary>
    [DisplayName("DO2TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
    public partial class CreateDO2TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO2 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO2 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO2TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
    /// </summary>
    [DisplayName("DO3TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
    public partial class CreateDO3TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO3 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO3 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO3TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO4 pin event.
    /// </summary>
    [DisplayName("DO4TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO4 pin event.")]
    public partial class CreateDO4TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO4 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO4 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO4TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO5 pin event.
    /// </summary>
    [DisplayName("DO5TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO5 pin event.")]
    public partial class CreateDO5TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO5 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO5 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO5TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO6 pin event.
    /// </summary>
    [DisplayName("DO6TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO6 pin event.")]
    public partial class CreateDO6TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO6 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO6 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO6TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO7 pin event.
    /// </summary>
    [DisplayName("DO7TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO7 pin event.")]
    public partial class CreateDO7TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO7 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO7 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO7TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that time (ms) below threshold value that is required to trigger a DO8 pin event.
    /// </summary>
    [DisplayName("DO8TimeBelowThresholdPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that time (ms) below threshold value that is required to trigger a DO8 pin event.")]
    public partial class CreateDO8TimeBelowThresholdPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        [Description("The value that time (ms) below threshold value that is required to trigger a DO8 pin event.")]
        public ushort Value { get; set; } = 0;

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that time (ms) below threshold value that is required to trigger a DO8 pin event.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => DO8TimeBelowThreshold.FromPayload(MessageType, Value));
        }
    }

    /// <summary>
    /// Represents an operator that creates a sequence of message payloads
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EnableEventsPayload")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("Creates a sequence of message payloads that specifies the active events in the device.")]
    public partial class CreateEnableEventsPayload : HarpCombinator
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public LoadCellEvents Value { get; set; }

        /// <summary>
        /// Creates an observable sequence that contains a single message
        /// that specifies the active events in the device.
        /// </summary>
        /// <returns>
        /// A sequence containing a single <see cref="HarpMessage"/> object
        /// representing the created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process()
        {
            return Process(Observable.Return(System.Reactive.Unit.Default));
        }

        /// <summary>
        /// Creates an observable sequence of message payloads
        /// that specifies the active events in the device.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements in the <paramref name="source"/> sequence.
        /// </typeparam>
        /// <param name="source">
        /// The sequence containing the notifications used for emitting message payloads.
        /// </param>
        /// <returns>
        /// A sequence of <see cref="HarpMessage"/> objects representing each
        /// created message payload.
        /// </returns>
        public IObservable<HarpMessage> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(_ => EnableEvents.FromPayload(MessageType, Value));
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
    }

    /// <summary>
    /// Available digital input lines.
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        DI0 = 0x1
    }

    /// <summary>
    /// Specifies the state output synchronization lines.
    /// </summary>
    [Flags]
    public enum SyncOutputs : byte
    {
        DO0 = 0x1
    }

    /// <summary>
    /// Specifies the state of port digital output lines.
    /// </summary>
    [Flags]
    public enum DigitalOutputs : byte
    {
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
