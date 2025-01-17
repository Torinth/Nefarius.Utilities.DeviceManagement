<img src="assets/NSS-128x128.png" align="right" />

# Nefarius.Utilities.DeviceManagement

[![Build status](https://ci.appveyor.com/api/projects/status/x6ylnh2c6p3l12pw?svg=true)](https://ci.appveyor.com/project/nefarius/nefarius-utilities-devicemanagement) ![Requirements](https://img.shields.io/badge/Requires-.NET%20Standard%202.0-blue.svg) [![Nuget](https://img.shields.io/nuget/v/Nefarius.Utilities.DeviceManagement)](https://www.nuget.org/packages/Nefarius.Utilities.DeviceManagement/) [![Nuget](https://img.shields.io/nuget/dt/Nefarius.Utilities.DeviceManagement)](https://www.nuget.org/packages/Nefarius.Utilities.DeviceManagement/)

Managed wrappers around SetupAPI, Cfgmgr32, NewDev and DrvStore native APIs on Windows.

## Documentation

[Link to API docs](docs/index.md).

## Examples

Some usage examples of the core library features are presented below.

### Enumerate all USB devices

The `Devcon` utility class offers helper methods to find devices.

```csharp
var instance = 0;
// enumerate all devices that export the GUID_DEVINTERFACE_USB_DEVICE interface
while (Devcon.FindByInterfaceGuid(Guid.Parse("{a5dcbf10-6530-11d2-901f-00c04fb951ed}"), out var path,
           out var instanceId, instance++))
{
    Console.WriteLine($"Path: {path}, InstanceId: {instanceId}");

    var usbDevice = PnPDevice
        .GetDeviceByInterfaceId(path)
        .ToUsbPnPDevice();

    Console.WriteLine($"Got USB device {usbDevice.InstanceId}");
}
```

### Listen for new and removed USB devices

One or more instances of the `DeviceNotificationListener` can be used to listen for plugin and unplug events of various devices. This class has no dependency on WinForms or WPF and works in Console Applications and Windows Services alike.

```csharp
var listener = new DeviceNotificationListener();

listener.DeviceArrived += Console.WriteLine;
listener.DeviceRemoved += Console.WriteLine;

// start listening for plugins or unplugs of GUID_DEVINTERFACE_USB_DEVICE interface devices
listener.StartListen(Guid.Parse("{a5dcbf10-6530-11d2-901f-00c04fb951ed}"));
```

### Get all driver packages in the Driver Store

```csharp
var allDriverPackages = DriverStore.ExistingDrivers.ToList();
```

### Remove all copies of `mydriver.inf` from the Driver Store

```csharp
foreach (var driverPackage in allDriverPackages.Where(p => p.Contains("mydriver.inf", StringComparison.OrdinalIgnoreCase)))
{
    DriverStore.RemoveDriver(driverPackage);
}
```

## Sources & 3rd party credits

- [ManagedDevcon](https://github.com/nefarius/ManagedDevcon)
- [ViGEm.Management](https://github.com/ViGEm/ViGEm.Management)
- [Driver Store Explorer [RAPR]](https://github.com/lostindark/DriverStoreExplorer)
- [Driver Updater](https://github.com/WOA-Project/DriverUpdater)
- [XMLDoc2Markdown](https://charlesdevandiere.github.io/xmldoc2md/)
