using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Linq;
using XplaneConnector.Message;
using XplaneConnector.Models.Network;
using XplaneConnector.Services;

namespace XplaneConnector.ViewModels;

public partial class SimulatorViewModel : ViewModelBase, IRecipient<BeaconReceivedMessage>
{
    BeaconListenerService beaconListenerService;


    public SimulatorViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        beaconListenerService = new BeaconListenerService();
        beaconListenerService.Run();
    }


    [ObservableProperty]
    ObservableCollection<SimulatorBeaconModel> _beacons = [];

    public void Receive(BeaconReceivedMessage message)
    {
        var target = Beacons.FirstOrDefault(b => b.IPAddress == message.Value.IPAddress && b.ListeningPort == message.Value.ListeningPort);
        if(target is not null)
        {
            target.Author = message.Value.Author;
            target.Description = message.Value.Description;
            target.SimulatorVersion =  message.Value.SimulatorVersion;
            target.SimulatorSDKVersion = message.Value.SimulatorSDKVersion;
            target.Time= message.Value.Time;
        }
        else
        {
            Beacons.Add(message.Value);
        }
    }
}
