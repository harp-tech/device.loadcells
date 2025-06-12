using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Bonsai.Harp;
using Harp.LoadCells.Design.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
/*using System;*/
using System.Collections.Generic;
/*using System.Collections.ObjectModel;
using System.Threading.Tasks;*/
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
namespace Harp.LoadCells.Design.ViewModels;


public class LoadCellsViewModel : ViewModelBase
{
    public string AppVersion { get; set; }
    public ReactiveCommand<Unit, Unit> LoadDeviceInformation { get; }

    #region Connection Information

    [Reactive] public ObservableCollection<string> Ports { get; set; }
    [Reactive] public string SelectedPort { get; set; }
    [Reactive] public bool Connected { get; set; }
    [Reactive] public string ConnectButtonText { get; set; } = "Connect";
    public ReactiveCommand<Unit, Unit> ConnectAndGetBaseInfoCommand { get; }

    #endregion

    #region Operations

    public ReactiveCommand<bool, Unit> SaveConfigurationCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetConfigurationCommand { get; }

    #endregion

    #region Device basic information

    [Reactive] public int DeviceID { get; set; }
    [Reactive] public string DeviceName { get; set; }
    [Reactive] public HarpVersion HardwareVersion { get; set; }
    [Reactive] public HarpVersion FirmwareVersion { get; set; }
    [Reactive] public int SerialNumber { get; set; }

    #endregion

    #region Registers

    [Reactive] public EnableFlag AcquisitionState { get; set; }
    [Reactive] public LoadCellDataPayload LoadCellData { get; set; }
    [Reactive] public DigitalInputs DigitalInputState { get; set; }
    [Reactive] public SyncOutputs SyncOutputState { get; set; }
    [Reactive] public TriggerConfig DI0Trigger { get; set; }
    [Reactive] public SyncConfig DO0Sync { get; set; }
    [Reactive] public byte DO0PulseWidth { get; set; }
    [Reactive] public DigitalOutputs DigitalOutputSet { get; set; }
    [Reactive] public DigitalOutputs DigitalOutputClear { get; set; }
    [Reactive] public DigitalOutputs DigitalOutputToggle { get; set; }
    [Reactive] public DigitalOutputs DigitalOutputState { get; set; }
    [Reactive] public short OffsetLoadCell0 { get; set; }
    [Reactive] public short OffsetLoadCell1 { get; set; }
    [Reactive] public short OffsetLoadCell2 { get; set; }
    [Reactive] public short OffsetLoadCell3 { get; set; }
    [Reactive] public short OffsetLoadCell4 { get; set; }
    [Reactive] public short OffsetLoadCell5 { get; set; }
    [Reactive] public short OffsetLoadCell6 { get; set; }
    [Reactive] public short OffsetLoadCell7 { get; set; }
    [Reactive] public LoadCellChannel DO1TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO2TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO3TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO4TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO5TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO6TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO7TargetLoadCell { get; set; }
    [Reactive] public LoadCellChannel DO8TargetLoadCell { get; set; }
    [Reactive] public short DO1Threshold { get; set; }
    [Reactive] public short DO2Threshold { get; set; }
    [Reactive] public short DO3Threshold { get; set; }
    [Reactive] public short DO4Threshold { get; set; }
    [Reactive] public short DO5Threshold { get; set; }
    [Reactive] public short DO6Threshold { get; set; }
    [Reactive] public short DO7Threshold { get; set; }
    [Reactive] public short DO8Threshold { get; set; }
    [Reactive] public ushort DO1TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO2TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO3TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO4TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO5TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO6TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO7TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO8TimeAboveThreshold { get; set; }
    [Reactive] public ushort DO1TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO2TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO3TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO4TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO5TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO6TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO7TimeBelowThreshold { get; set; }
    [Reactive] public ushort DO8TimeBelowThreshold { get; set; }
    [Reactive] public LoadCellEvents EnableEvents { get; set; }

    #endregion

    #region Array Collections


    #endregion

    #region Events Flags

    public bool IsLoadCellDataEnabled
    {
        get
        {
            return EnableEvents.HasFlag(LoadCellEvents.LoadCellData);
        }
        set
        {
            if (value)
            {
                EnableEvents |= LoadCellEvents.LoadCellData;
            }
            else
            {
                EnableEvents &= ~LoadCellEvents.LoadCellData;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsLoadCellDataEnabled));
            this.RaisePropertyChanged(nameof(EnableEvents));
        }
    }

    public bool IsDigitalInputEnabled
    {
        get
        {
            return EnableEvents.HasFlag(LoadCellEvents.DigitalInput);
        }
        set
        {
            if (value)
            {
                EnableEvents |= LoadCellEvents.DigitalInput;
            }
            else
            {
                EnableEvents &= ~LoadCellEvents.DigitalInput;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDigitalInputEnabled));
            this.RaisePropertyChanged(nameof(EnableEvents));
        }
    }

    public bool IsSyncOutputEnabled
    {
        get
        {
            return EnableEvents.HasFlag(LoadCellEvents.SyncOutput);
        }
        set
        {
            if (value)
            {
                EnableEvents |= LoadCellEvents.SyncOutput;
            }
            else
            {
                EnableEvents &= ~LoadCellEvents.SyncOutput;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsSyncOutputEnabled));
            this.RaisePropertyChanged(nameof(EnableEvents));
        }
    }

    public bool IsThresholdsEnabled
    {
        get
        {
            return EnableEvents.HasFlag(LoadCellEvents.Thresholds);
        }
        set
        {
            if (value)
            {
                EnableEvents |= LoadCellEvents.Thresholds;
            }
            else
            {
                EnableEvents &= ~LoadCellEvents.Thresholds;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsThresholdsEnabled));
            this.RaisePropertyChanged(nameof(EnableEvents));
        }
    }

    #endregion

    #region DigitalInputs_DigitalInputState Flags

    public bool IsDI0Enabled_DigitalInputState
    {
        get
        {
            return DigitalInputState.HasFlag(DigitalInputs.DI0);
        }
        set
        {
            if (value)
            {
                DigitalInputState |= DigitalInputs.DI0;
            }
            else
            {
                DigitalInputState &= ~DigitalInputs.DI0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDI0Enabled_DigitalInputState));
            this.RaisePropertyChanged(nameof(DigitalInputState));
        }
    }

    #endregion

    #region SyncOutputs_SyncOutputState Flags

    public bool IsDO0Enabled_SyncOutputState
    {
        get
        {
            return SyncOutputState.HasFlag(SyncOutputs.DO0);
        }
        set
        {
            if (value)
            {
                SyncOutputState |= SyncOutputs.DO0;
            }
            else
            {
                SyncOutputState &= ~SyncOutputs.DO0;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO0Enabled_SyncOutputState));
            this.RaisePropertyChanged(nameof(SyncOutputState));
        }
    }

    #endregion

    #region DigitalOutputs_DigitalOutputSet Flags

    public bool IsDO1Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO1;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO2Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO2;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO3Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO3;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO4Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO4);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO4;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO4;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO4Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO5Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO5);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO5;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO5;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO5Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO6Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO6);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO6;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO6;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO6Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO7Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO7);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO7;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO7;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO7Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    public bool IsDO8Enabled_DigitalOutputSet
    {
        get
        {
            return DigitalOutputSet.HasFlag(DigitalOutputs.DO8);
        }
        set
        {
            if (value)
            {
                DigitalOutputSet |= DigitalOutputs.DO8;
            }
            else
            {
                DigitalOutputSet &= ~DigitalOutputs.DO8;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO8Enabled_DigitalOutputSet));
            this.RaisePropertyChanged(nameof(DigitalOutputSet));
        }
    }

    #endregion

    #region DigitalOutputs_DigitalOutputClear Flags

    public bool IsDO1Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO1;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO2Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO2;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO3Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO3;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO4Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO4);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO4;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO4;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO4Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO5Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO5);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO5;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO5;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO5Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO6Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO6);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO6;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO6;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO6Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO7Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO7);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO7;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO7;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO7Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    public bool IsDO8Enabled_DigitalOutputClear
    {
        get
        {
            return DigitalOutputClear.HasFlag(DigitalOutputs.DO8);
        }
        set
        {
            if (value)
            {
                DigitalOutputClear |= DigitalOutputs.DO8;
            }
            else
            {
                DigitalOutputClear &= ~DigitalOutputs.DO8;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO8Enabled_DigitalOutputClear));
            this.RaisePropertyChanged(nameof(DigitalOutputClear));
        }
    }

    #endregion

    #region DigitalOutputs_DigitalOutputToggle Flags

    public bool IsDO1Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO1;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO2Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO2;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO3Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO3;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO4Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO4);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO4;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO4;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO4Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO5Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO5);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO5;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO5;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO5Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO6Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO6);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO6;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO6;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO6Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO7Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO7);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO7;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO7;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO7Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    public bool IsDO8Enabled_DigitalOutputToggle
    {
        get
        {
            return DigitalOutputToggle.HasFlag(DigitalOutputs.DO8);
        }
        set
        {
            if (value)
            {
                DigitalOutputToggle |= DigitalOutputs.DO8;
            }
            else
            {
                DigitalOutputToggle &= ~DigitalOutputs.DO8;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO8Enabled_DigitalOutputToggle));
            this.RaisePropertyChanged(nameof(DigitalOutputToggle));
        }
    }

    #endregion

    #region DigitalOutputs_DigitalOutputState Flags

    public bool IsDO1Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO1);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO1;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO1;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO1Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO2Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO2);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO2;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO2;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO2Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO3Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO3);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO3;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO3;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO3Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO4Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO4);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO4;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO4;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO4Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO5Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO5);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO5;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO5;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO5Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO6Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO6);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO6;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO6;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO6Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO7Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO7);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO7;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO7;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO7Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    public bool IsDO8Enabled_DigitalOutputState
    {
        get
        {
            return DigitalOutputState.HasFlag(DigitalOutputs.DO8);
        }
        set
        {
            if (value)
            {
                DigitalOutputState |= DigitalOutputs.DO8;
            }
            else
            {
                DigitalOutputState &= ~DigitalOutputs.DO8;
            }

            // Notify the UI about the change
            this.RaisePropertyChanged(nameof(IsDO8Enabled_DigitalOutputState));
            this.RaisePropertyChanged(nameof(DigitalOutputState));
        }
    }

    #endregion

    #region Application State

    [ObservableAsProperty] public bool IsLoadingPorts { get; }
    [ObservableAsProperty] public bool IsConnecting { get; }
    [ObservableAsProperty] public bool IsResetting { get; }
    [ObservableAsProperty] public bool IsSaving { get; }

    [Reactive] public bool ShowWriteMessages { get; set; }
    [Reactive] public ObservableCollection<string> HarpEvents { get; set; } = new ObservableCollection<string>();
    [Reactive] public ObservableCollection<string> SentMessages { get; set; } = new ObservableCollection<string>();

    public ReactiveCommand<Unit, Unit> ShowAboutCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ClearMessagesCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> ShowMessagesCommand { get; private set; }


    #endregion

    private Harp.LoadCells.AsyncDevice? _device;
    private IObservable<string> _deviceEventsObservable;
    private IDisposable? _deviceEventsSubscription;

    public LoadCellsViewModel()
    {
        var assembly = typeof(LoadCellsViewModel).Assembly;
        var informationVersion = assembly.GetName().Version;
        if (informationVersion != null)
            AppVersion = $"v{informationVersion.Major}.{informationVersion.Minor}.{informationVersion.Build}";

        Ports = new ObservableCollection<string>();

        ClearMessagesCommand = ReactiveCommand.Create(() => { SentMessages.Clear(); });
        ShowMessagesCommand = ReactiveCommand.Create(() => { ShowWriteMessages = !ShowWriteMessages; });


        LoadDeviceInformation = ReactiveCommand.CreateFromObservable(LoadUsbInformation);
        LoadDeviceInformation.IsExecuting.ToPropertyEx(this, x => x.IsLoadingPorts);
        LoadDeviceInformation.ThrownExceptions.Subscribe(ex =>
            Console.WriteLine($"Error loading device information with exception: {ex.Message}"));
        //Log.Error(ex, "Error loading device information with exception: {Exception}", ex));

        // can connect if there is a selection and also if the new selection is different than the old one
        var canConnect = this.WhenAnyValue(x => x.SelectedPort)
            .Select(selectedPort => !string.IsNullOrEmpty(selectedPort));

        ShowAboutCommand = ReactiveCommand.CreateFromTask(async () =>
                await new About() { DataContext = new AboutViewModel() }.ShowDialog(
                    (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow));

        ConnectAndGetBaseInfoCommand = ReactiveCommand.CreateFromTask(ConnectAndGetBaseInfo, canConnect);
        ConnectAndGetBaseInfoCommand.IsExecuting.ToPropertyEx(this, x => x.IsConnecting);
        ConnectAndGetBaseInfoCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex));
            Console.WriteLine($"Error connecting to device with error: {ex}"));

        var canChangeConfig = this.WhenAnyValue(x => x.Connected).Select(connected => connected);
        // Handle Save and Reset
        SaveConfigurationCommand =
            ReactiveCommand.CreateFromObservable<bool, Unit>(SaveConfiguration, canChangeConfig);
        SaveConfigurationCommand.IsExecuting.ToPropertyEx(this, x => x.IsSaving);
        SaveConfigurationCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error saving configuration with error: {Exception}", ex));
            Console.WriteLine($"Error saving configuration with error: {ex}"));

        ResetConfigurationCommand = ReactiveCommand.CreateFromObservable(ResetConfiguration, canChangeConfig);
        ResetConfigurationCommand.IsExecuting.ToPropertyEx(this, x => x.IsResetting);
        ResetConfigurationCommand.ThrownExceptions.Subscribe(ex =>
            //Log.Error(ex, "Error resetting device configuration with error: {Exception}", ex));
            Console.WriteLine($"Error resetting device configuration with error: {ex}"));

        this.WhenAnyValue(x => x.Connected)
            .Subscribe(x => { ConnectButtonText = x ? "Disconnect" : "Connect"; });

        this.WhenAnyValue(x => x.EnableEvents)
            .Subscribe(x =>
            {
                IsLoadCellDataEnabled = x.HasFlag(LoadCellEvents.LoadCellData);
                IsDigitalInputEnabled = x.HasFlag(LoadCellEvents.DigitalInput);
                IsSyncOutputEnabled = x.HasFlag(LoadCellEvents.SyncOutput);
                IsThresholdsEnabled = x.HasFlag(LoadCellEvents.Thresholds);
            });


        // handle the events from the device
        // When Connected changes subscribe/unsubscribe the device events.
        this.WhenAnyValue(x => x.Connected)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(isConnected =>
            {
                if (isConnected && _deviceEventsObservable != null)
                {
                    // Subscribe on the UI thread so that the HarpEvents collection can be updated safely.
                    _deviceEventsSubscription = _deviceEventsObservable
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Subscribe(
                            msg => HarpEvents.Add(msg.ToString()),
                            ex => Debug.WriteLine($"Error in device events: {ex}")
                        );
                }
                else
                {
                    // Dispose subscription and clear messages.
                    _deviceEventsSubscription?.Dispose();
                    _deviceEventsSubscription = null;
                }
            });

        this.WhenAnyValue(x => x.DigitalInputState)
            .Subscribe(x =>
            {
                IsDI0Enabled_DigitalInputState = x.HasFlag(DigitalInputs.DI0);
            });

        this.WhenAnyValue(x => x.SyncOutputState)
            .Subscribe(x =>
            {
                IsDO0Enabled_SyncOutputState = x.HasFlag(SyncOutputs.DO0);
            });

        this.WhenAnyValue(x => x.DigitalOutputSet)
            .Subscribe(x =>
            {
                IsDO1Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO3);
                IsDO4Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO4);
                IsDO5Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO5);
                IsDO6Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO6);
                IsDO7Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO7);
                IsDO8Enabled_DigitalOutputSet = x.HasFlag(DigitalOutputs.DO8);
            });

        this.WhenAnyValue(x => x.DigitalOutputClear)
            .Subscribe(x =>
            {
                IsDO1Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO3);
                IsDO4Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO4);
                IsDO5Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO5);
                IsDO6Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO6);
                IsDO7Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO7);
                IsDO8Enabled_DigitalOutputClear = x.HasFlag(DigitalOutputs.DO8);
            });

        this.WhenAnyValue(x => x.DigitalOutputToggle)
            .Subscribe(x =>
            {
                IsDO1Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO3);
                IsDO4Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO4);
                IsDO5Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO5);
                IsDO6Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO6);
                IsDO7Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO7);
                IsDO8Enabled_DigitalOutputToggle = x.HasFlag(DigitalOutputs.DO8);
            });

        this.WhenAnyValue(x => x.DigitalOutputState)
            .Subscribe(x =>
            {
                IsDO1Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO1);
                IsDO2Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO2);
                IsDO3Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO3);
                IsDO4Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO4);
                IsDO5Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO5);
                IsDO6Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO6);
                IsDO7Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO7);
                IsDO8Enabled_DigitalOutputState = x.HasFlag(DigitalOutputs.DO8);
            });

            // force initial population of currently connected ports
            LoadUsbInformation();

            Series = new ObservableCollection<ISeries>();
            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = [_customAxis];

            UpdateSeries();


            _ = ReadData();
    }

    private IObservable<Unit> LoadUsbInformation()
    {
        return Observable.Start(() =>
        {
            var devices = SerialPort.GetPortNames();

            if (OperatingSystem.IsMacOS())
                // except with Bluetooth in the name
                Ports = new ObservableCollection<string>(devices.Where(d => d.Contains("cu.")).Except(devices.Where(d => d.Contains("Bluetooth"))));
            else
                Ports = new ObservableCollection<string>(devices);

            Console.WriteLine("Loaded USB information");
            //Log.Information("Loaded USB information");
        });
    }

    private async Task ConnectAndGetBaseInfo()
    {
        if (string.IsNullOrEmpty(SelectedPort))
            throw new Exception("invalid parameter");

        if (Connected)
        {
            _device?.Dispose();
            _device = null;
            Connected = false;
            SentMessages.Clear();
            return;
        }

        try
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
            _device = await Harp.LoadCells.Device.CreateAsync(SelectedPort, cts.Token);
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"Error connecting to device with error: {ex}");
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex);
            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    "Timeout when trying to connect to a device. Most likely not an Harp device.",
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();
            _device?.Dispose();
            _device = null;
            return;

        }
        catch (HarpException ex)
        {
            Console.WriteLine($"Error connecting to device with error: {ex}");
            //Log.Error(ex, "Error connecting to device with error: {Exception}", ex);

            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    ex.Message,
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();

            _device?.Dispose();
            _device = null;
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"COM port still in use and most likely not the expected Harp device");
            var messageBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandard("Unexpected device found",
                    $"COM port still in use and most likely not the expected Harp device.{Environment.NewLine}Specific error: {ex.Message}",
                    icon: Icon.Error);
            await messageBoxStandardWindow.ShowAsync();

            _device?.Dispose();
            _device = null;
            return;
        }

        // Clear the sent messages list
        SentMessages.Clear();

        //Log.Information("Attempting connection to port \'{SelectedPort}\'", SelectedPort);
        Console.WriteLine($"Attempting connection to port \'{SelectedPort}\'");

        DeviceID = await _device.ReadWhoAmIAsync();
        DeviceName = await _device.ReadDeviceNameAsync();
        HardwareVersion = await _device.ReadHardwareVersionAsync();
        FirmwareVersion = await _device.ReadFirmwareVersionAsync();
        try
        {
            // some devices may not have a serial number
            SerialNumber = await _device.ReadSerialNumberAsync();
        }
        catch (HarpException)
        {
            // Device does not have a serial number, simply continue by ignoring the exception
        }

        /*****************************************************************
        * TODO: Please REVIEW all these registers and update the values
        * ****************************************************************/
        AcquisitionState = await _device.ReadAcquisitionStateAsync();
        LoadCellData = await _device.ReadLoadCellDataAsync();
        DigitalInputState = await _device.ReadDigitalInputStateAsync();
        SyncOutputState = await _device.ReadSyncOutputStateAsync();
        DI0Trigger = await _device.ReadDI0TriggerAsync();
        DO0Sync = await _device.ReadDO0SyncAsync();
        DO0PulseWidth = await _device.ReadDO0PulseWidthAsync();
        DigitalOutputSet = await _device.ReadDigitalOutputSetAsync();
        DigitalOutputClear = await _device.ReadDigitalOutputClearAsync();
        DigitalOutputToggle = await _device.ReadDigitalOutputToggleAsync();
        DigitalOutputState = await _device.ReadDigitalOutputStateAsync();
        OffsetLoadCell0 = await _device.ReadOffsetLoadCell0Async();
        OffsetLoadCell1 = await _device.ReadOffsetLoadCell1Async();
        OffsetLoadCell2 = await _device.ReadOffsetLoadCell2Async();
        OffsetLoadCell3 = await _device.ReadOffsetLoadCell3Async();
        OffsetLoadCell4 = await _device.ReadOffsetLoadCell4Async();
        OffsetLoadCell5 = await _device.ReadOffsetLoadCell5Async();
        OffsetLoadCell6 = await _device.ReadOffsetLoadCell6Async();
        OffsetLoadCell7 = await _device.ReadOffsetLoadCell7Async();
        DO1TargetLoadCell = await _device.ReadDO1TargetLoadCellAsync();
        DO2TargetLoadCell = await _device.ReadDO2TargetLoadCellAsync();
        DO3TargetLoadCell = await _device.ReadDO3TargetLoadCellAsync();
        DO4TargetLoadCell = await _device.ReadDO4TargetLoadCellAsync();
        DO5TargetLoadCell = await _device.ReadDO5TargetLoadCellAsync();
        DO6TargetLoadCell = await _device.ReadDO6TargetLoadCellAsync();
        DO7TargetLoadCell = await _device.ReadDO7TargetLoadCellAsync();
        DO8TargetLoadCell = await _device.ReadDO8TargetLoadCellAsync();
        DO1Threshold = await _device.ReadDO1ThresholdAsync();
        DO2Threshold = await _device.ReadDO2ThresholdAsync();
        DO3Threshold = await _device.ReadDO3ThresholdAsync();
        DO4Threshold = await _device.ReadDO4ThresholdAsync();
        DO5Threshold = await _device.ReadDO5ThresholdAsync();
        DO6Threshold = await _device.ReadDO6ThresholdAsync();
        DO7Threshold = await _device.ReadDO7ThresholdAsync();
        DO8Threshold = await _device.ReadDO8ThresholdAsync();
        DO1TimeAboveThreshold = await _device.ReadDO1TimeAboveThresholdAsync();
        DO2TimeAboveThreshold = await _device.ReadDO2TimeAboveThresholdAsync();
        DO3TimeAboveThreshold = await _device.ReadDO3TimeAboveThresholdAsync();
        DO4TimeAboveThreshold = await _device.ReadDO4TimeAboveThresholdAsync();
        DO5TimeAboveThreshold = await _device.ReadDO5TimeAboveThresholdAsync();
        DO6TimeAboveThreshold = await _device.ReadDO6TimeAboveThresholdAsync();
        DO7TimeAboveThreshold = await _device.ReadDO7TimeAboveThresholdAsync();
        DO8TimeAboveThreshold = await _device.ReadDO8TimeAboveThresholdAsync();
        DO1TimeBelowThreshold = await _device.ReadDO1TimeBelowThresholdAsync();
        DO2TimeBelowThreshold = await _device.ReadDO2TimeBelowThresholdAsync();
        DO3TimeBelowThreshold = await _device.ReadDO3TimeBelowThresholdAsync();
        DO4TimeBelowThreshold = await _device.ReadDO4TimeBelowThresholdAsync();
        DO5TimeBelowThreshold = await _device.ReadDO5TimeBelowThresholdAsync();
        DO6TimeBelowThreshold = await _device.ReadDO6TimeBelowThresholdAsync();
        DO7TimeBelowThreshold = await _device.ReadDO7TimeBelowThresholdAsync();
        DO8TimeBelowThreshold = await _device.ReadDO8TimeBelowThresholdAsync();
        EnableEvents = await _device.ReadEnableEventsAsync();


        // generate observable for the _deviceSync
        _deviceEventsObservable = GenerateEventMessages();

        Connected = true;

        //Log.Information("Connected to device");
        Console.WriteLine("Connected to device");
    }

    public IObservable<string> GenerateEventMessages()
    {
        return Observable.Create<string>(async (observer, cancellationToken) =>
        {
            // Loop until cancellation is requested or the device is no longer available.
            while (!cancellationToken.IsCancellationRequested && _device != null)
            {
                // Capture local reference and check for null.
                var device = _device;
                if (device == null)
                {
                    observer.OnCompleted();
                    break;
                }

                try
                {
                    // Check if LoadCellData event is enabled
                    if (IsLoadCellDataEnabled)
                    {
                        var result = await device.ReadLoadCellDataAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        LoadCellData = result;
                        observer.OnNext($"LoadCellData: {result}");
                    }

                    // Check if DigitalInput event is enabled
                    if (IsDigitalInputEnabled)
                    {
                        var result = await device.ReadDigitalInputStateAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        DigitalInputState = result;
                        observer.OnNext($"DigitalInput: {result}");
                    }

                    // Check if SyncOutput event is enabled
                    if (IsSyncOutputEnabled)
                    {
                        var result = await device.ReadSyncOutputStateAsync(cancellationToken);
                        // Update the corresponding property with the result
                        // FIXME: this might not be the most appropriate action, please review for each case
                        SyncOutputState = result;
                        observer.OnNext($"SyncOutput: {result}");
                    }

                    // Check if Thresholds event is enabled
                    if (IsThresholdsEnabled)
                    {
                        // NOTE: This is a fallback approach since no matching register was found!
                        // TODO: Implement reading the correct event register and update the observer
                        // Example for each register associated with this event. 
                        // In case there are multiple registers associated with this event, repeat for each register
                        // var result = await device.ReadXXXXXAsync(cancellationToken);
                        // observer.OnNext($"XXXXXX: {result}");
                        //throw new NotImplementedException("Thresholds event handling is not implemented yet");
                    }


                    // NOTE: Move the below entries to the correct event validation.
                    // The following registers have Event access but don't have a direct mapping to event flags
                    // These should be moved to appropriate event validation sections once their triggering events are identified
                    var DigitalOutputStateResult = await device.ReadDigitalOutputStateAsync(cancellationToken);
                    observer.OnNext($"DigitalOutputState: {DigitalOutputStateResult}");

                    // Wait a short while before polling again. Adjust delay as necessary.
                    await Task.Delay(TimeSpan.FromMilliseconds(10), cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                    break;
                }
            }
            observer.OnCompleted();
            return Disposable.Empty;
        });
    }

    private IObservable<Unit> SaveConfiguration(bool savePermanently)
    {
        return Observable.StartAsync(async () =>
        {
            if (_device == null)
                throw new Exception("You need to connect to the device first");

            /*****************************************************************
            * TODO: Please REVIEW all these registers and update the values
            * ****************************************************************/
            await WriteAndLogAsync(
                value => _device.WriteAcquisitionStateAsync(value),
                AcquisitionState,
                "AcquisitionState");
            await WriteAndLogAsync(
                value => _device.WriteDI0TriggerAsync(value),
                DI0Trigger,
                "DI0Trigger");
            await WriteAndLogAsync(
                value => _device.WriteDO0SyncAsync(value),
                DO0Sync,
                "DO0Sync");
            await WriteAndLogAsync(
                value => _device.WriteDO0PulseWidthAsync(value),
                DO0PulseWidth,
                "DO0PulseWidth");
            await WriteAndLogAsync(
                value => _device.WriteDigitalOutputSetAsync(value),
                DigitalOutputSet,
                "DigitalOutputSet");
            await WriteAndLogAsync(
                value => _device.WriteDigitalOutputClearAsync(value),
                DigitalOutputClear,
                "DigitalOutputClear");
            await WriteAndLogAsync(
                value => _device.WriteDigitalOutputToggleAsync(value),
                DigitalOutputToggle,
                "DigitalOutputToggle");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell0Async(value),
                OffsetLoadCell0,
                "OffsetLoadCell0");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell1Async(value),
                OffsetLoadCell1,
                "OffsetLoadCell1");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell2Async(value),
                OffsetLoadCell2,
                "OffsetLoadCell2");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell3Async(value),
                OffsetLoadCell3,
                "OffsetLoadCell3");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell4Async(value),
                OffsetLoadCell4,
                "OffsetLoadCell4");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell5Async(value),
                OffsetLoadCell5,
                "OffsetLoadCell5");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell6Async(value),
                OffsetLoadCell6,
                "OffsetLoadCell6");
            await WriteAndLogAsync(
                value => _device.WriteOffsetLoadCell7Async(value),
                OffsetLoadCell7,
                "OffsetLoadCell7");
            await WriteAndLogAsync(
                value => _device.WriteDO1TargetLoadCellAsync(value),
                DO1TargetLoadCell,
                "DO1TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO2TargetLoadCellAsync(value),
                DO2TargetLoadCell,
                "DO2TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO3TargetLoadCellAsync(value),
                DO3TargetLoadCell,
                "DO3TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO4TargetLoadCellAsync(value),
                DO4TargetLoadCell,
                "DO4TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO5TargetLoadCellAsync(value),
                DO5TargetLoadCell,
                "DO5TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO6TargetLoadCellAsync(value),
                DO6TargetLoadCell,
                "DO6TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO7TargetLoadCellAsync(value),
                DO7TargetLoadCell,
                "DO7TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO8TargetLoadCellAsync(value),
                DO8TargetLoadCell,
                "DO8TargetLoadCell");
            await WriteAndLogAsync(
                value => _device.WriteDO1ThresholdAsync(value),
                DO1Threshold,
                "DO1Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO2ThresholdAsync(value),
                DO2Threshold,
                "DO2Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO3ThresholdAsync(value),
                DO3Threshold,
                "DO3Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO4ThresholdAsync(value),
                DO4Threshold,
                "DO4Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO5ThresholdAsync(value),
                DO5Threshold,
                "DO5Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO6ThresholdAsync(value),
                DO6Threshold,
                "DO6Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO7ThresholdAsync(value),
                DO7Threshold,
                "DO7Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO8ThresholdAsync(value),
                DO8Threshold,
                "DO8Threshold");
            await WriteAndLogAsync(
                value => _device.WriteDO1TimeAboveThresholdAsync(value),
                DO1TimeAboveThreshold,
                "DO1TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO2TimeAboveThresholdAsync(value),
                DO2TimeAboveThreshold,
                "DO2TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO3TimeAboveThresholdAsync(value),
                DO3TimeAboveThreshold,
                "DO3TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO4TimeAboveThresholdAsync(value),
                DO4TimeAboveThreshold,
                "DO4TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO5TimeAboveThresholdAsync(value),
                DO5TimeAboveThreshold,
                "DO5TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO6TimeAboveThresholdAsync(value),
                DO6TimeAboveThreshold,
                "DO6TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO7TimeAboveThresholdAsync(value),
                DO7TimeAboveThreshold,
                "DO7TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO8TimeAboveThresholdAsync(value),
                DO8TimeAboveThreshold,
                "DO8TimeAboveThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO1TimeBelowThresholdAsync(value),
                DO1TimeBelowThreshold,
                "DO1TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO2TimeBelowThresholdAsync(value),
                DO2TimeBelowThreshold,
                "DO2TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO3TimeBelowThresholdAsync(value),
                DO3TimeBelowThreshold,
                "DO3TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO4TimeBelowThresholdAsync(value),
                DO4TimeBelowThreshold,
                "DO4TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO5TimeBelowThresholdAsync(value),
                DO5TimeBelowThreshold,
                "DO5TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO6TimeBelowThresholdAsync(value),
                DO6TimeBelowThreshold,
                "DO6TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO7TimeBelowThresholdAsync(value),
                DO7TimeBelowThreshold,
                "DO7TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteDO8TimeBelowThresholdAsync(value),
                DO8TimeBelowThreshold,
                "DO8TimeBelowThreshold");
            await WriteAndLogAsync(
                value => _device.WriteEnableEventsAsync(value),
                EnableEvents,
                "EnableEvents");

            // Save the configuration to the device permanently
            if (savePermanently)
            {
                await WriteAndLogAsync(
                    value => _device.WriteResetDeviceAsync(value),
                    ResetFlags.Save,
                    "SavePermanently");
            }
        });
    }

    private IObservable<Unit> ResetConfiguration()
    {
        return Observable.StartAsync(async () =>
        {
            if (_device != null)
            {
                await WriteAndLogAsync(
                    value => _device.WriteResetDeviceAsync(value),
                    ResetFlags.RestoreDefault,
                    "ResetDevice");
            }
        });
    }

    private async Task WriteAndLogAsync<T>(Func<T, Task> writeFunc, T value, string registerName)
    {
        if (_device == null)
            throw new Exception("Device is not connected");

        await writeFunc(value);

        // Log the message to the SentMessages collection on the UI thread
        RxApp.MainThreadScheduler.Schedule(() =>
        {
            SentMessages.Add($"{DateTime.Now:HH:mm:ss.fff} - Write {registerName}: {value}");
        });
    }



    public class ArrayItemWrapper<T> : ReactiveObject
    {
        public int Index { get; }

        [Reactive]
        public T Value { get; set; }

        public ArrayItemWrapper(int index, T value)
        {
            Index = index;
            Value = value;
        }
    }
    private void UpdateArrayCollection<T>(T[] array, ObservableCollection<ArrayItemWrapper<T>> collection)
    {
        if (array == null)
            return;

        RxApp.MainThreadScheduler.Schedule(() =>
        {
            collection.Clear();

            for (int i = 0; i < array.Length; i++)
            {
                collection.Add(new ArrayItemWrapper<T>(i, array[i]));
            }
        });
    }

    // Add these fields for each channel
    private readonly List<DateTimePoint> _values_0 = [];
    private readonly List<DateTimePoint> _values_1 = [];
    private readonly List<DateTimePoint> _values_2 = [];
    private readonly List<DateTimePoint> _values_3 = [];
    private readonly List<DateTimePoint> _values_4 = [];
    private readonly List<DateTimePoint> _values_5 = [];
    private readonly List<DateTimePoint> _values_6 = [];
    private readonly List<DateTimePoint> _values_7 = [];

    private readonly DateTimeAxis _customAxis; /*new DateTimeAxis
    {
        Position = AxisPosition.Bottom,
        StringFormat = "HH:mm:ss",
        IntervalType = DateTimeIntervalType.Seconds,
        Interval = 1,
        Title = "Time"
    };
*/

    //private readonly Random _random = new();
    // Add show/hide properties for each channel 

    private bool _show0 = true, _show1 = true, _show2 = true, _show3 = true, _show4 = true, _show5 = true, _show6 = true, _show7 = true;
    public bool Show0 { get => _show0; set { if (_show0 != value) { _show0 = value; this.RaisePropertyChanged(nameof(Show0)); UpdateSeries(); } } }
    public bool Show1 { get => _show1; set { if (_show1 != value) { _show1 = value; this.RaisePropertyChanged(nameof(Show1)); UpdateSeries(); } } }
    public bool Show2 { get => _show2; set { if (_show2 != value) { _show2 = value; this.RaisePropertyChanged(nameof(Show2)); UpdateSeries(); } } }
    public bool Show3 { get => _show3; set { if (_show3 != value) { _show3 = value; this.RaisePropertyChanged(nameof(Show3)); UpdateSeries(); } } }
    public bool Show4 { get => _show4; set { if (_show4 != value) { _show4 = value; this.RaisePropertyChanged(nameof(Show4)); UpdateSeries(); } } }
    public bool Show5 { get => _show5; set { if (_show5 != value) { _show5 = value; this.RaisePropertyChanged(nameof(Show5)); UpdateSeries(); } } }
    public bool Show6 { get => _show6; set { if (_show6 != value) { _show6 = value; this.RaisePropertyChanged(nameof(Show6)); UpdateSeries(); } } }
    public bool Show7 { get => _show7; set { if (_show7 != value) { _show7 = value; this.RaisePropertyChanged(nameof(Show7)); UpdateSeries(); } } }


    public ObservableCollection<ISeries> Series { get; set; }

    public Axis[] XAxes { get; set; }

    public object Sync { get; } = new object();

    public bool IsReading { get; set; } = true;

/*    public bool ShowBlue
    {
        get => _showBlue;
        set
        {
            if (_showBlue != value)
            {
                _showBlue = value;
                this.RaisePropertyChanged(nameof(ShowBlue)); // Use ReactiveUI's RaisePropertyChanged method
                UpdateSeries();
            }
        }
    }

    public bool ShowRed
    {
        get => _showRed;
        set
        {
            if (_showRed != value)
            {
                _showRed = value;
                this.RaisePropertyChanged(nameof(ShowRed)); // Use ReactiveUI's RaisePropertyChanged method
                UpdateSeries();
            }
        }
    }*/


    // Update the UpdateSeries method:
    private void UpdateSeries()
    {
        Series.Clear();
        if (Show0)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_0, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Blue, 2) });
        if (Show1)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_1, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Red, 2) });
        if (Show2)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_2, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Green, 2) });
        if (Show3)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_3, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Orange, 2) });
        if (Show4)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_4, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Purple, 2) });
        if (Show5)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_5, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Brown, 2) });
        if (Show6)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_6, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Cyan, 2) });
        if (Show7)
            Series.Add(new LineSeries<DateTimePoint> { Values = _values_7, Fill = null, GeometryFill = null, GeometryStroke = null, Stroke = new SolidColorPaint(SKColors.Magenta, 2) });
    }

    // Update ReadData to add data for all 8 channels
    private async Task ReadData()
    {
        while (IsReading)
        {
            await Task.Delay(10);

            lock (Sync)
            {
                var now = DateTime.Now;
                _values_0.Add(new DateTimePoint(now, LoadCellData.Channel0));
                _values_1.Add(new DateTimePoint(now, LoadCellData.Channel1));
                _values_2.Add(new DateTimePoint(now, LoadCellData.Channel2));
                _values_3.Add(new DateTimePoint(now, LoadCellData.Channel3));
                _values_4.Add(new DateTimePoint(now, LoadCellData.Channel4));
                _values_5.Add(new DateTimePoint(now, LoadCellData.Channel5));
                _values_6.Add(new DateTimePoint(now, LoadCellData.Channel6));
                _values_7.Add(new DateTimePoint(now, LoadCellData.Channel7));

                if (_values_0.Count > 250) _values_0.RemoveAt(0);
                if (_values_1.Count > 250) _values_1.RemoveAt(0);
                if (_values_2.Count > 250) _values_2.RemoveAt(0);
                if (_values_3.Count > 250) _values_3.RemoveAt(0);
                if (_values_4.Count > 250) _values_4.RemoveAt(0);
                if (_values_5.Count > 250) _values_5.RemoveAt(0);
                if (_values_6.Count > 250) _values_6.RemoveAt(0);
                if (_values_7.Count > 250) _values_7.RemoveAt(0);

                _customAxis.CustomSeparators = GetSeparators();
            }
        }
    }

    private static double[] GetSeparators()
    {
        var now = DateTime.Now;

        return
        [
            now.AddSeconds(-20).Ticks,
                    now.AddSeconds(-15).Ticks,
                    now.AddSeconds(-10).Ticks,
                    now.AddSeconds(-5).Ticks,
                    now.Ticks
        ];
    }

    private static string Formatter(DateTime date)
    {
        var secsAgo = (DateTime.Now - date).TotalSeconds;

        return secsAgo < 1
            ? "now"
            : $"{secsAgo:N0}s ago";
    }

}
