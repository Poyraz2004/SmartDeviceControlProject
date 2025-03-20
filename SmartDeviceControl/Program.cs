using System;
using SmartDeviceControl.Models;
using SmartDeviceControl.Exceptions;  // Add this to avoid ambiguity

class Program
{
    static void Main()
    {
        try
        {
            // PersonalComputer without an Operating System
            PersonalComputer pc2 = new PersonalComputer(id: "PC-2", name: "Lenovo", ipAddress: "192.168.1.101");
            Console.WriteLine(pc2.ToString());
            pc2.TurnOn(); // EmptySystemException will be thrown
        }
        catch (SmartDeviceControl.Exceptions.InvalidDataException ex)  // Using full namespace for clarity
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (SmartDeviceControl.Exceptions.EmptySystemException ex)  // Using full namespace for clarity
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
        }
    }
}