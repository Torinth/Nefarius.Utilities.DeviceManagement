﻿using System;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.DeviceManagement.PnP
{
    /// <summary>
    ///     Provides common device interface <see cref="Guid" />s.
    /// </summary>
    public static class DeviceInterfaceIds
    {
        /// <summary>
        ///     An interface exposed on USB host controllers.
        /// </summary>
        public static Guid UsbHostController => Guid.Parse("{3abf6f2d-71c4-462a-8a92-1e6861e6af27}");

        /// <summary>
        ///     An interface exposed on USB hubs.
        /// </summary>
        public static Guid UsbHub => Guid.Parse("{f18a0e88-c30c-11d0-8815-00a0c906bed8}");

        /// <summary>
        ///     An interface exposed on USB devices.
        /// </summary>
        public static Guid UsbDevice => Guid.Parse("{a5dcbf10-6530-11d2-901f-00c04fb951ed}");

        /// <summary>
        ///     An interface exposed on HID devices.
        /// </summary>
        public static Guid HidDevice
        {
            get
            {
                // GUID_DEVINTERFACE_HID exists but this is considered best practice
                HidD_GetHidGuid(out var guid);

                return guid;
            }
        }

        [DllImport("hid.dll", EntryPoint = "HidD_GetHidGuid", SetLastError = true)]
        private static extern void HidD_GetHidGuid(out Guid guid);
    }
}