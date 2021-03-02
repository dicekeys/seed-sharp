using System;
using System.Linq;
using Windows.Devices.Enumeration;
using Windows.Devices.HumanInterfaceDevice;
using Windows.Storage;
using Windows.UI.Core;

namespace SeedSharp
{
    class HIDDevices
    {
        // Find HID devices.
        public static async void Enumerate()
        {
            // Microsoft Input Configuration Device.
            ushort vendorId = 0x0483;
            ushort productId = 0xA2CA;
            ushort usagePage = 0xf1d0;
            ushort usageId = 0x0001; // 20 input report, 21, output report

            // Create the selector.
            string selector =
                HidDevice.GetDeviceSelector(usagePage, usageId, vendorId, productId);

            // Enumerate devices using the selector.
            //var devices = await DeviceInformation.FindAllAsync(selector);
            var devicesInfo = await DeviceInformation.FindAllAsync(selector);

            foreach (var deviceInfo in devicesInfo) {
                Console.WriteLine($"Device: {deviceInfo.Id}, name:{deviceInfo.Name}, enabled:{deviceInfo.IsEnabled}, kind: {deviceInfo.Kind}");

                var das = DeviceAccessInformation.CreateFromId(deviceInfo.Id);
                Console.WriteLine($"DeviceAccessInforamtion.currentStatus=={das?.CurrentStatus}");

                // Do your stuff here
                HidDevice device = await HidDevice.FromIdAsync(deviceInfo.Id, FileAccessMode.ReadWrite);

                if (device == null)
                {
                    Console.WriteLine($"HidDevice.FromIdAsync({deviceInfo.Id}, {FileAccessMode.ReadWrite})=={device}");
                }
                else
                {
                    Console.WriteLine($"Device connected {device?.ProductId}");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
