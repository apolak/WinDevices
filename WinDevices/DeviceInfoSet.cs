namespace WinDevices
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel;
   using System.Runtime.CompilerServices;
   using System.Runtime.InteropServices;
   using System.Text;
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
         string deviceInstanceId, DeviceAdditionOptions options)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(
            null, deviceInstanceId, options);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromSetupClassOptions(
         Guid setupClassGuid, string enumerator, DeviceAdditionOptions options)
      {
         var handle = SafeDeviceInfoSetHandle.FromSetupClassOptions(
            setupClassGuid, enumerator, options);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromAnySetupClassOptions(
         string enumerator, DeviceAdditionOptions options)
      {
         var handle = SafeDeviceInfoSetHandle.FromSetupClassOptions(
            null, enumerator, options);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromInterfaceClassOptions(
         Guid setupClassGuid, Guid interfaceClassGuid, DeviceAdditionOptions options)
      {
         var handle = SafeDeviceInfoSetHandle.Create(setupClassGuid);
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
               IntPtr.Zero,
               IntPtr.Zero) == new IntPtr(NativeConstants.INVALID_HANDLE_VALUE))
         {
            throw new Win32Exception();
         }
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromInterfaceClassOptions(
         Guid interfaceClassGuid, DeviceAdditionOptions options)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(
            interfaceClassGuid, null, options);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public static DeviceInfoSet FromAnyInterfaceClassOptions(
         DeviceAdditionOptions options, string machineName)
      {
         var handle = SafeDeviceInfoSetHandle.FromInterfaceClassOptions(null, null, options);
         var deviceInfoSet = new DeviceInfoSet(handle, true);
         return deviceInfoSet;
      }

      public IEnumerable<DeviceInfo> EnumerateDevices()
      {
         var deviceInfoData = new SP_DEVINFO_DATA { cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA)) };
         int index = 0;
         while (NativeMethods.SetupDiEnumDeviceInfo(this.handle, index, ref deviceInfoData))
         {
            var deviceInfo = DeviceInfo.FromDeviceInfoData(this, deviceInfoData);
            yield return deviceInfo;
            index++;
         }
         if (Marshal.GetLastWin32Error() != NativeConstants.ERROR_NO_MORE_ITEMS)
            throw new Win32Exception();
      }

      public IEnumerable<DeviceInterfaceInfo> EnumerateDeviceInterfaces(Guid interfaceClassGuid)
      {
         var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA
         {
            cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA))
         };
         int index = 0;
         while (
            NativeMethods.SetupDiEnumDeviceInterfaces(
               this.handle,
               IntPtr.Zero,
               ref interfaceClassGuid,
               index,
               ref deviceInterfaceData))
         {
            var deviceInterface = DeviceInterfaceInfo.FromDeviceInterfaceData(
               this, deviceInterfaceData, null);
            yield return deviceInterface;
            index++;
         }
         if (Marshal.GetLastWin32Error() != NativeConstants.ERROR_NO_MORE_ITEMS)
            throw new Win32Exception();
      }

      internal IEnumerable<DeviceInterfaceInfo> EnumerateDeviceInterfaces(
         Guid interfaceClassGuid, DeviceInfo device)
      {
         SP_DEVINFO_DATA deviceInfoData = device.ToDeviceInfoData();
         var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA
         {
            cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA)) 
         };
         int index = 0;
         while (
            NativeMethods.SetupDiEnumDeviceInterfaces(
               this.handle,
               ref deviceInfoData,
               ref interfaceClassGuid,
               index,
               ref deviceInterfaceData))
         {
            var deviceInterface = DeviceInterfaceInfo.FromDeviceInterfaceData(
               this, deviceInterfaceData, device);
            yield return deviceInterface;
            index++;
         }
         if (Marshal.GetLastWin32Error() != NativeConstants.ERROR_NO_MORE_ITEMS)
            throw new Win32Exception();
      }

      internal DeviceInfo CreateDeviceInfo(Guid setupClassGuid, int deviceInstanceHandle)
      {
         var deviceInfoData = new SP_DEVINFO_DATA
         {
            cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA)),
            ClassGuid = setupClassGuid,
            DevInst = deviceInstanceHandle
         };
         int requiredSize;
         if (
            !NativeMethods.SetupDiGetDeviceInstanceId(
               this.handle,
               ref deviceInfoData,
               IntPtr.Zero,
               IntPtr.Zero,
               out requiredSize)
            && (Marshal.GetLastWin32Error() != NativeConstants.ERROR_INSUFFICIENT_BUFFER))
         {
            throw new Win32Exception();
         }
         var buffer = new StringBuilder(requiredSize);
         if (
            !NativeMethods.SetupDiGetDeviceInstanceId(
               this.handle,
               ref deviceInfoData,
               buffer,
               buffer.Capacity,
               IntPtr.Zero))
         {
            throw new Win32Exception();
         }
         var deviceInfo = new DeviceInfo(
            this, setupClassGuid, deviceInstanceHandle, buffer.ToString());
         return deviceInfo;
      }

      internal DeviceInterfaceInfo CreateDeviceInterfaceInfo(
         Guid interfaceClassGuid, DeviceInterfaceStatus status, DeviceInfo deviceInfo)
      {
         var deviceInterfaceData = new SP_DEVICE_INTERFACE_DATA
         {
            cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DATA)),
            InterfaceClassGuid = interfaceClassGuid,
            Flags = (SP_DEVICE_INTERFACE_DATA_Flags)status
         };
         int requiredSize;
         if (
            !NativeMethods.SetupDiGetDeviceInterfaceDetail(
               this.handle,
               ref deviceInterfaceData,
               IntPtr.Zero,
               IntPtr.Zero,
               out requiredSize,
               IntPtr.Zero)
            && (Marshal.GetLastWin32Error() != NativeConstants.ERROR_INSUFFICIENT_BUFFER))
         {
            throw new Win32Exception();
         }
         IntPtr buffer = IntPtr.Zero;
         RuntimeHelpers.PrepareConstrainedRegions();
         try
         {
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
            }
            finally
            {
               // RELIABILITY Unmanaged memory allocation and handle assignment performed within a constrained execution region.
               buffer = Marshal.AllocHGlobal(requiredSize);
            }
            var deviceInterfaceDetailData = new SP_DEVICE_INTERFACE_DETAIL_DATA
            {
               cbSize = Marshal.SizeOf(typeof(SP_DEVICE_INTERFACE_DETAIL_DATA))
            };
            Marshal.StructureToPtr(deviceInterfaceDetailData, buffer, false);
            bool success;
            if (deviceInfo == null)
            {
               var deviceInfoData = new SP_DEVINFO_DATA
               {
                  cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA))
               };
               success = NativeMethods.SetupDiGetDeviceInterfaceDetail(
                  this.handle,
                  ref deviceInterfaceData,
                  buffer,
                  requiredSize,
                  IntPtr.Zero,
                  ref deviceInfoData);
               if (success)
                  deviceInfo = DeviceInfo.FromDeviceInfoData(this, deviceInfoData);
            }
            else
            {
               success = NativeMethods.SetupDiGetDeviceInterfaceDetail(
                  this.handle,
                  ref deviceInterfaceData,
                  buffer,
                  requiredSize,
                  IntPtr.Zero,
                  IntPtr.Zero);
            }
            if (!success)
               throw new Win32Exception();
            int offset = Marshal.OffsetOf(typeof(SP_DEVICE_INTERFACE_DETAIL_DATA), "DevicePath").ToInt32();
            string devicePath = Marshal.PtrToStringAuto(buffer + offset);
            var interfaceInfo = new DeviceInterfaceInfo(
               this, interfaceClassGuid, status, devicePath, deviceInfo);
            return interfaceInfo;
         }
         finally
         {
            // RELIABILITY Critical resources released in a constrained execution region.
            if (buffer != IntPtr.Zero)
               Marshal.FreeHGlobal(buffer);
         }
      }
   }
}
