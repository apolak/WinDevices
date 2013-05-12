namespace WinDevices.Interop
{
   using System;
   using System.ComponentModel;
   using Microsoft.Win32.SafeHandles;

   [Flags]
   internal enum DeviceInfoSetAdditionOptions
   {
      None = 0,
      OnlyDevicesWithDefaultInterface = 0x00000001,
      OnlyActiveDevices = 0x00000002,
      OnlyDevicesInCurrentProfile = 0x00000008
   }

   public sealed class SafeDeviceInfoSetHandle : SafeHandleZeroOrMinusOneIsInvalid
   {
      private SafeDeviceInfoSetHandle()
         : base(true)
      {
      }

      public SafeDeviceInfoSetHandle(IntPtr handle, bool ownsHandle)
         : base(ownsHandle)
      {
         this.SetHandle(handle);
      }

      internal static SafeDeviceInfoSetHandle FromSetupClassOptions(
         Guid? setupClassGuid,
         string enumerator,
         DeviceInfoSetAdditionOptions options,
         string machineName)
      {
         return CreateInternal(
            setupClassGuid,
            enumerator,
            SetupDiGetClassDevsEx_Flags.None,
            options,
            machineName);
      }

      internal static SafeDeviceInfoSetHandle FromInterfaceClassOptions(
         Guid? interfaceClassGuid,
         string deviceInstanceId,
         DeviceInfoSetAdditionOptions options,
         string machineName)
      {
         var flags = SetupDiGetClassDevsEx_Flags.DIGCF_DEVICEINTERFACE;
         if ((options & DeviceInfoSetAdditionOptions.OnlyDevicesWithDefaultInterface) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_DEFAULT;
         return CreateInternal(
            interfaceClassGuid,
            deviceInstanceId,
            flags,
            options,
            machineName);
      }

      private static SafeDeviceInfoSetHandle CreateInternal(
         Guid? setupOrInterfaceClassGuid,
         string enumeratorOrDeviceInstanceId,
         SetupDiGetClassDevsEx_Flags flags,
         DeviceInfoSetAdditionOptions options,
         string machineName)
      {
         if ((options & DeviceInfoSetAdditionOptions.OnlyActiveDevices) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_PRESENT;
         if ((options & DeviceInfoSetAdditionOptions.OnlyDevicesInCurrentProfile) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_PROFILE;
         SafeDeviceInfoSetHandle handle;
         if (setupOrInterfaceClassGuid == null)
         {
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_ALLCLASSES;
            handle = NativeMethods.SetupDiGetClassDevsEx(
               IntPtr.Zero,
               enumeratorOrDeviceInstanceId,
               IntPtr.Zero,
               flags,
               IntPtr.Zero,
               machineName,
               IntPtr.Zero);
         }
         else
         {
            var guid = (Guid)setupOrInterfaceClassGuid;
            handle = NativeMethods.SetupDiGetClassDevsEx(
               ref guid,
               enumeratorOrDeviceInstanceId,
               IntPtr.Zero,
               flags,
               IntPtr.Zero,
               machineName,
               IntPtr.Zero);
         }
         if (handle.IsInvalid)
            throw new Win32Exception();
         return handle;
      }
      
      protected override bool ReleaseHandle()
      {
         return NativeMethods.SetupDiDestroyDeviceInfoList(this.handle);
      }
   }
}