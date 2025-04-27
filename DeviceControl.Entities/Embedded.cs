using System.Text.RegularExpressions;

namespace DeviceControl.Entities;

public class Embedded : Device
{
    public string NetworkName { get; set; }
    private string _ipAddress;
    private bool _isConnected = false;

    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            Regex ipRegex = new Regex(@"^((25[0-5]|(2[0-4]\d|1\d\d|[1-9]?\d))\.){3}(25[0-5]|(2[0-4]\d|1\d\d|[1-9]?\d))$");
            if (ipRegex.IsMatch(value))
            {
                _ipAddress = value;
            }
            else
            {
                throw new ArgumentException("Wrong IP address format.", nameof(value));
            }
        }
    }
    
    public Embedded(string id, string name, bool isEnabled, string ipAddress, string networkName) : base(id, name, isEnabled)
    {
        if (!CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: ED-1", nameof(id));
        }

        IpAddress = ipAddress;
        NetworkName = networkName;
    }

    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }

    public override void TurnOff()
    {
        _isConnected = false;
        base.TurnOff();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        return $"Embedded device {Name} ({Id}) is {enabledStatus} and has IP address {IpAddress}";
    }

    private void Connect()
    {
        if (NetworkName.Contains("MD Ltd."))
        {
            _isConnected = true;
        }
        else
        {
            throw new ConnectionException();
        }
    }
    
    private bool CheckId(string id) => id.StartsWith("ED-");
}