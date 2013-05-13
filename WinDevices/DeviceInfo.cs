namespace WinDevices
{
   using System;
   using System.Collections.Generic;
   using System.Runtime.InteropServices;
   using WinDevices.Interop;

   public sealed class DeviceInfo
   {
      private readonly DeviceInfoSet deviceInfoSet;
      private readonly Guid setupClassGuid;
      private readonly int deviceInstanceHandle;
      private readonly string deviceInstanceId;

      internal DeviceInfo(
         DeviceInfoSet deviceInfoSet,
         Guid setupClassGuid,
         int deviceInstanceHandle,
         string deviceInstanceId)
      {
         this.deviceInfoSet = deviceInfoSet;
         this.setupClassGuid = setupClassGuid;
         this.deviceInstanceHandle = deviceInstanceHandle;
         this.deviceInstanceId = deviceInstanceId;
      }

      public string DeviceInstanceId
      {
         get { return this.deviceInstanceId; }
      }

      public Guid SetupClassGuid
      {
         get { return this.setupClassGuid; }
      }

      public int DeviceInstanceHandle
      {
         get { return this.deviceInstanceHandle; }
      }

      public IEnumerable<DeviceInterfaceInfo> EnumerateDeviceInterfaces(Guid interfaceClassGuid)
      {
         IEnumerable<DeviceInterfaceInfo> deviceInterfaces =
            this.deviceInfoSet.EnumerateDeviceInterfaces(interfaceClassGuid, this);
         return deviceInterfaces;
      }

      internal static DeviceInfo FromDeviceInfoData(
         DeviceInfoSet deviceInfoSet, SP_DEVINFO_DATA deviceInfoData)
      {
         DeviceInfo deviceInfo = deviceInfoSet.CreateDeviceInfo(
            deviceInfoData.ClassGuid, deviceInfoData.DevInst);
         return deviceInfo;
      }

      internal SP_DEVINFO_DATA ToDeviceInfoData()
      {
         var deviceInfoData = new SP_DEVINFO_DATA
         {
            cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA)),
            ClassGuid = this.setupClassGuid,
            DevInst = this.deviceInstanceHandle,
         };
         return deviceInfoData;
      }
   }
}
