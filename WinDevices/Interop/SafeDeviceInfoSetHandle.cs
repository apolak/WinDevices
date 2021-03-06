﻿namespace WinDevices.Interop
{
   using System;
   using System.ComponentModel;
   using Microsoft.Win32.SafeHandles;

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

      internal static SafeDeviceInfoSetHandle Create(Guid? setupClassGuid)
      {
         SafeDeviceInfoSetHandle handle;
         if (setupClassGuid == null)
         {
            handle = NativeMethods.SetupDiCreateDeviceInfoListEx(
               IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
         }
         else
         {
            var guid = (Guid)setupClassGuid;
            handle = NativeMethods.SetupDiCreateDeviceInfoListEx(
               ref guid, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
         }
         if (handle.IsInvalid)
            throw new Win32Exception();
         return handle;
      }

      internal static SafeDeviceInfoSetHandle FromSetupClassOptions(
         Guid? setupClassGuid,
         string enumerator,
         DeviceAdditionOptions options)
      {
         return FromSetupOrInterfaceClassOptions(
            setupClassGuid,
            enumerator,
            SetupDiGetClassDevsEx_Flags.None,
            options);
      }

      internal static SafeDeviceInfoSetHandle FromInterfaceClassOptions(
         Guid? interfaceClassGuid,
         string deviceInstanceId,
         DeviceAdditionOptions options)
      {
         var flags = SetupDiGetClassDevsEx_Flags.DIGCF_DEVICEINTERFACE;
         if ((options & DeviceAdditionOptions.SupportsDefaultInterface) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_DEFAULT;
         return FromSetupOrInterfaceClassOptions(
            interfaceClassGuid,
            deviceInstanceId,
            flags,
            options);
      }

      private static SafeDeviceInfoSetHandle FromSetupOrInterfaceClassOptions(
         Guid? setupOrInterfaceClassGuid,
         string enumeratorOrDeviceInstanceId,
         SetupDiGetClassDevsEx_Flags flags,
         DeviceAdditionOptions options)
      {
         if ((options & DeviceAdditionOptions.Active) != 0)
            flags |= SetupDiGetClassDevsEx_Flags.DIGCF_PRESENT;
         if ((options & DeviceAdditionOptions.CurrentProfile) != 0)
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
               IntPtr.Zero,
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
               IntPtr.Zero,
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