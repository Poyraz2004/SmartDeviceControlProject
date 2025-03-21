using SmartDeviceControl.Interfaces;
using SmartDeviceControl.Exceptions;

namespace SmartDeviceControl.Models;

public class SmartWatch : Device, IPowerNotifier
{
    private int batteryPercentage;

    public int BatteryPercentage
    {
        get => batteryPercentage;
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Battery percentage must be between 0 and 100.");
            }
            batteryPercentage = value;

            if (batteryPercentage < 20)
                NotifyLowBattery();
        }
    }

    public SmartWatch(string name, string id, int batteryPercentage) : base(name, id)
    {
        BatteryPercentage = batteryPercentage;
    }

    public override void TurnOn()
    {
        
        if (BatteryPercentage < 11)
        {
            throw new EmptyBatteryException();  
        }

       
        Console.WriteLine($"{Name} is turning on...");
        batteryPercentage -= 10;
        
        base.TurnOn();
    }

    public void NotifyLowBattery()
    {
        Console.WriteLine($"{Name} battery is too low: {BatteryPercentage}%");
    }
}