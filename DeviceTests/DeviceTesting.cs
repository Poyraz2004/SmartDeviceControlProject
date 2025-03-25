using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SmartDeviceControl.Managers;
using SmartDeviceControl.Models;
using SmartDeviceControl.Exceptions;
using Assert = NUnit.Framework;

namespace SmartDeviceControl.Tests
{
    [TestFixture]
    public class DeviceTesting
    {
        private string testFilePath;
        private DeviceManager deviceManager;

        
        public void Setup()
        {
            testFilePath = "input.txt"; 
            File.WriteAllLines(testFilePath, new[]
            {
                "SW-1,Apple Watch SE2,true,27%",
                "P-1,LinuxPC,false,Linux Mint",
                "P-2,ThinkPad T440,false",
                "ED-1,Pi3,192.168.1.44,MD Ltd.Wifi-1",
                "ED-2,Pi4,192.168.1.45,eduroam",
                "ED-3,Pi4,whatisIP,MyWifiName"
            });

            deviceManager = new DeviceManager(testFilePath);
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
                File.Delete(testFilePath);
        }

        [Test]
        public void Test_AddDevice_ShouldAddDevice()
        {
            var newDevice = new SmartWatch("Galaxy Watch", "SW-2", 50);
            deviceManager.AddDevice(newDevice);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                deviceManager.ShowAllDevices();
                ClassicAssert.IsTrue(sw.ToString().Contains("Galaxy Watch"));
            }
        }

        [Test]
        public void Test_RemoveDevice_ShouldRemoveDevice()
        {
            deviceManager.RemoveDevice("SW-1");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                deviceManager.ShowAllDevices();
                ClassicAssert.IsFalse(sw.ToString().Contains("Apple Watch SE2"));
            }
        }

        [Test]
        public void Test_EditDevice_ShouldChangeName()
        {
            deviceManager.EditDevice("P-1", "NewLinuxPC");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                deviceManager.ShowAllDevices();
                ClassicAssert.IsTrue(sw.ToString().Contains("NewLinuxPC"));
                ClassicAssert.IsFalse(sw.ToString().Contains("LinuxPC"));
            }
        }

        [Test]
        public void Test_TurnOnDevice_ShouldTurnOn()
        {
            deviceManager.TurnOnDevice("P-1");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                deviceManager.ShowAllDevices();
                ClassicAssert.IsTrue(sw.ToString().Contains("Status: Open"));
            }
        }

        [Test]
        public void Test_TurnOffDevice_ShouldTurnOff()
        {
            deviceManager.TurnOffDevice("SW-1");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                deviceManager.ShowAllDevices();
                ClassicAssert.IsTrue(sw.ToString().Contains("Status: Close"));
            }
        }

        [Test]
        public void Test_SaveData_ShouldWriteToFile()
        {
            deviceManager.EditDevice("P-2", "NewThinkPad");
            deviceManager.SaveDataToFile();

            string[] lines = File.ReadAllLines(testFilePath);
            ClassicAssert.IsTrue(lines[2].Contains("NewThinkPad"));
        }
    }
}
