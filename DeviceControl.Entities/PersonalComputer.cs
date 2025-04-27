namespace DeviceControl.Entities;

public class PersonalComputer : Device
{
    public string? OperatingSystem { get; set; }
    
    public PersonalComputer(string id, string name, bool isEnabled, string? operatingSystem) : base(id, name, isEnabled)
    {
        if (!CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: P-1", nameof(id));
        }
        
        OperatingSystem = operatingSystem;
    }

    public override void TurnOn()
    {
        if (OperatingSystem is null)
        {
            throw new EmptySystemException();
        }

        base.TurnOn();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        string osStatus = OperatingSystem is null ? "has not OS" : $"has {OperatingSystem}";
        return $"PC {Name} ({Id}) is {enabledStatus} and {osStatus}";
    }

    private bool CheckId(string id) => id.StartsWith("P-");
}