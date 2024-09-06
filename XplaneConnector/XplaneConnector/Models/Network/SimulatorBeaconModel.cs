using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace XplaneConnector.Models.Network;

public partial class SimulatorBeaconModel : ObservableObject
{
    [ObservableProperty]
    private string _author = null!;

    [ObservableProperty]
    private string _description = null!;

    [ObservableProperty]
    private string _iPAddress = null!;

    [ObservableProperty]
    private int _listeningPort;

    [ObservableProperty]
    private string _operation = null!;

    [ObservableProperty]
    private string _simulator = null!;

    [ObservableProperty]
    private int _simulatorSDKVersion;

    [ObservableProperty]
    private int _simulatorVersion;

    [ObservableProperty]
    private int _time;

    partial void OnTimeChanged(int value)
    {
        TimeReadable = DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
    }

    [ObservableProperty]
    private DateTime _timeReadable;
}
