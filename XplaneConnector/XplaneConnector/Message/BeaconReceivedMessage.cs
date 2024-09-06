using CommunityToolkit.Mvvm.Messaging.Messages;
using XplaneConnector.Models.Network;

namespace XplaneConnector.Message;

public class BeaconReceivedMessage(SimulatorBeaconModel msg) 
    : ValueChangedMessage<SimulatorBeaconModel>(msg)
{
}
