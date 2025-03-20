using System;
using SmartDeviceControl.Managers;
using SmartDeviceControl.Models;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";
        DeviceManager deviceManager = new DeviceManager(filePath);

        Console.WriteLine("📌 Listing all devices from input.txt...");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n✅ Adding a new device (SW-2, Galaxy Watch, 45%)...");
        SmartWatch newWatch = new SmartWatch("Galaxy Watch", "SW-2", 45);
        deviceManager.AddDevice(newWatch);
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n❌ Removing a device (P-1, LinuxPC)...");
        deviceManager.RemoveDevice("P-1");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n✏️ Updating device name (P-2 -> Workstation-T440)...");
        deviceManager.EditDevice("P-2", "Workstation-T440");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n⚡ Turning on a device (ED-1, Pi3)...");
        deviceManager.TurnOnDevice("ED-1");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n🛑 Turning off a device (SW-1, Apple Watch SE2)...");
        deviceManager.TurnOffDevice("SW-1");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\n💾 Saving updated data back to input.txt...");
        deviceManager.SaveDataToFile();

        Console.WriteLine("\n🚀 Program execution completed!");
    }
}