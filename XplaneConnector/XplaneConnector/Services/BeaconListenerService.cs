using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using XplaneConnector.Message;
using XplaneConnector.Models.Network;

namespace XplaneConnector.Services;

public class BeaconListenerService
{
    private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private byte[] _buffer = new byte[1024];
    private EndPoint _localEp = new IPEndPoint(IPAddress.Any, 50_888);
    private EndPoint _remoteEp = new IPEndPoint(IPAddress.Any, 0);

    public event EventHandler<SimulatorBeaconModel>? BeaconReceived;

    public void Run()
    {
        _socket.Bind(_localEp);
        Start();
    }

    private void Start()
    {
        _socket.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _remoteEp, EndReceive, _socket);
    }

    private void OnDataReceived(string data, IPEndPoint source)
    {
        Debug.WriteLine($"{source.Address}:{source.Port} >>> {data}");

        SimulatorBeaconModel? beacon;
        try
        {
            beacon = JsonSerializer.Deserialize<SimulatorBeaconModel>(data);
        }
        catch (JsonException ex)
        {
            return;
        }
        if (beacon is not null)
        {
            WeakReferenceMessenger.Default.Send(new BeaconReceivedMessage(beacon));
        }
    }

    private void EndReceive(IAsyncResult ar)
    {
        Socket? s = ar.AsyncState as Socket;
        if (s is null) return;
        int data = s.EndReceiveFrom(ar, ref _remoteEp);
        if (data >= 0)
        {
            IPEndPoint ep = _remoteEp as IPEndPoint ?? throw new NullReferenceException();
            //var text = Encoding.ASCII.GetString(_buffer);
            OnDataReceived(Encoding.ASCII.GetString(_buffer, 0, data), ep);
        }
        Start();
    }
}
