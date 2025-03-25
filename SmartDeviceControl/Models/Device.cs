namespace SmartDeviceControl.Models;

public class Device
{
    public string Name { get; set; }
    public string Id { get; set; }
    public bool IsOn { get; protected set; }

    public Device(string name, string id)
    {
        Name = name;
        Id = id;
        IsOn = false;
    }

    public virtual void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} is on");
    }

    public void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} is off");
    }

    public override string ToString()
    {
        return $"{Id}, {Name}, Status: {(IsOn ? "Open" : "Close")}";
    }
}