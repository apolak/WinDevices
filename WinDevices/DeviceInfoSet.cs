namespace WinDevices
{
   using System;
   using System.ComponentModel;
   using WinDevices.Interop;

   [Flags]
   public enum DeviceAdditionOptions
   {
      None = 0,
      SupportsDefaultInterface = 1,
      Active = 2,
      CurrentProfile = 8
   }

   public sealed class DeviceInfoSet
   {
      private readonly SafeDeviceInfoSetHandle handle;
      private readonly bool ownsHandle;

      public DeviceInfoSet(SafeDeviceInfoSetHandle handle, bool ownsHandle = true)
      {
         this.handle = handle;
         this.ownsHandle = ownsHandle;
      }

      public static DeviceInfoSet FromDeviceOptions(
         string deviceInstanceId, DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(
            null, deviceInstanceId, options, machineName);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromSetupClassOptions(
         Guid setupClassGuid, string enumerator, DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromSetupClassOptions(
            setupClassGuid, enumerator, options, machineName);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromAnySetupClassOptions(
         string enumerator, DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromSetupClassOptions(
            null, enumerator, options, machineName);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromInterfaceClassOptions(
         Guid setupClassGuid,
         Guid interfaceClassGuid,
         DeviceAdditionOptions options,
         string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.Create(setupClassGuid, machineName);
         var flags = SetupDiGetClassDevsEx_Flags.DIGCF_DEVICEINTERFACE;
         if ((options & DeviceAdditionOptions.SupportsDefaultInterface) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_DEFAULT;
         if ((options & DeviceAdditionOptions.Active) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_PRESENT;
         if ((options & DeviceAdditionOptions.CurrentProfile) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_PROFILE;
         if (
            NativeMethods.SetupDiGetClassDevsEx(
               ref interfaceClassGuid,
               null,
               IntPtr.Zero,
               flags,
               handle,
               machineName,
               IntPtr.Zero) == new IntPtr(NativeConstants.INVALID_HANDLE_VALUE))
         {
            throw new Win32Exception();
         }
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromInterfaceClassOptions(
         Guid interfaceClassGuid, DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(
            interfaceClassGuid, null, options, machineName);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromAnyInterfaceClassOptions(
         DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(
            null, null, options, machineName);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }
   }
}
