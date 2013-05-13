namespace WinDevices
{
   using System;
   using WinDevices.Interop;

   [Flags]
   public enum DeviceInterfaceStatus
   {
      Active = 1,
      DefaultInterface = 2,
      Removed = 4
   }

   public sealed class DeviceInterfaceInfo
   {
      private readonly DeviceInfoSet deviceInfoSet;
      private readonly Guid interfaceClassGuid;
      private readonly DeviceInterfaceStatus status;
      private readonly string devicePath;
      private readonly DeviceInfo device;

      internal DeviceInterfaceInfo(
         DeviceInfoSet deviceInfoSet,
         Guid interfaceClassGuid,
         DeviceInterfaceStatus status,
         string devicePath,
         DeviceInfo device)
      {
         this.deviceInfoSet = deviceInfoSet;
         this.interfaceClassGuid = interfaceClassGuid;
         this.status = status;
         this.devicePath = devicePath;
         this.device = device;
      }

      public bool IsActive
      {
         get { return (this.status & DeviceInterfaceStatus.Active) != 0; }
      }

      public bool IsDefaultInterface
      {
         get { return (this.status & DeviceInterfaceStatus.DefaultInterface) != 0; }
      }

      public bool IsRemoved
      {
         get { return (this.status & DeviceInterfaceStatus.Removed) != 0; }
      }

      public Guid InterfaceClassGuid
      {
         get { return this.interfaceClassGuid; }
      }

      public DeviceInterfaceStatus Status
      {
         get { return this.status; }
      }

      public string DevicePath 
      {
         get { return this.devicePath; }
      }

      public DeviceInfo Device
      {
         get { return this.device; }
      }

      internal static DeviceInterfaceInfo FromDeviceInterfaceData(
         DeviceInfoSet deviceInfoSet, SP_DEVICE_INTERFACE_DATA deviceInterfaceData, DeviceInfo device)
      {
         DeviceInterfaceInfo deviceInterfaceInfo = deviceInfoSet.CreateDeviceInterfaceInfo(
            deviceInterfaceData.InterfaceClassGuid,
            (DeviceInterfaceStatus)deviceInterfaceData.Flags,
            device);
         return deviceInterfaceInfo;
      }
   }
}
