using SmartDeviceControl.Managers; 
using SmartDeviceControl.Models;
using SmartDeviceControl.Exceptions;


class Program
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("Welcome to the Device Management System!");
            Console.WriteLine("==============================================");
            
            
            DeviceManager deviceManager = new("input.txt");
            Console.WriteLine("Devices have been loaded from the file.");

            
            Console.WriteLine("\nDevices presented after file read:");
            deviceManager.ShowAllDevices();
            
            
            Console.WriteLine("\nCreating a new computer (ThinkPad T440) and adding it to the device store...");
            PersonalComputer computer = new("P-2", "ThinkPad T440", "127.0.0.1");
            deviceManager.AddDevice(computer);
            Console.WriteLine("New device added to the store.");
            
            
            Console.WriteLine("\nLet's try to enable this PC (P-2)...");

            try
            {
                deviceManager.TurnOnDevice("P-2");
            }
            catch (EmptySystemException ex)  
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please install an OS before turning on the PC.");
            }
            
            
            Console.WriteLine("\nInstalling 'Arch Linux' OS on the ThinkPad...");
            deviceManager.EditDevice("P-2", "ThinkPad T440 Updated");
            Console.WriteLine("Operating system installed and PC details updated.");
            
            
            Console.WriteLine("\nNow, let's turn on the PC again...");
            deviceManager.TurnOnDevice("P-2");
            
            
            Console.WriteLine("\nTurning off the PC...");
            deviceManager.TurnOffDevice("P-2");

            
            Console.WriteLine("\nDeleting the PC (P-2) from the device store...");
            deviceManager.RemoveDevice("P-2");
            
            
            Console.WriteLine("\nDevices presented after all operations:");
            deviceManager.ShowAllDevices();
            
            Console.WriteLine("\n==============================================");
            Console.WriteLine("Operations completed successfully!");
            Console.WriteLine("==============================================");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occurred during the operations:");
            Console.WriteLine(ex.Message);
        }
    }
}
