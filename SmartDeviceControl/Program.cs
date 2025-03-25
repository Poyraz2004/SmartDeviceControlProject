using SmartDeviceControl.Managers;
using SmartDeviceControl.Models;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";
        DeviceManager deviceManager = new DeviceManager(filePath);

        Console.WriteLine("Listing all devices from input.txt...");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nAdding a new device (SW-2, Galaxy Watch, 45%)...");
        SmartWatch newWatch = new SmartWatch("Galaxy Watch", "SW-2", 45);
        deviceManager.AddDevice(newWatch);
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nRemoving a device (P-1, LinuxPC)...");
        deviceManager.RemoveDevice("P-1");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nUpdating device name (P-2 -> Workstation-T440)...");
        deviceManager.EditDevice("P-2", "Workstation-T440");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nTurning on a device (SW-2, Galaxy Watch)");
        try
        {
            deviceManager.TurnOnDevice("SW-2");  
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nTurning on a device (ED-1, Pi3)");
        try
        {
            deviceManager.TurnOnDevice("ED-1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nTurning off a device (SW-1, Apple Watch SE2)");
        deviceManager.TurnOffDevice("SW-1");
        deviceManager.ShowAllDevices();

        Console.WriteLine("\nSaving updated data back to input.txt");
        deviceManager.SaveDataToFile();

        Console.WriteLine("\nProgram execution completed!");
    }
}