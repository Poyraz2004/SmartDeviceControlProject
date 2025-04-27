namespace DeviceControl.Entities;

public class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string DeviceType { get; set; }
    public bool IsEnabled { get; set; }
    
    public Device()
    {
    }
    public Device(string id, string name, bool isEnabled)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
    }

    public virtual void TurnOn()
    {
        IsEnabled = true;
    }

    public virtual void TurnOff()
    {
        IsEnabled = false;
    }
}