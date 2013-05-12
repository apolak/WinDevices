namespace WinDevices.Interop
{
   using System;
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

      protected override bool ReleaseHandle()
      {
         return NativeMethods.SetupDiDestroyDeviceInfoList(this.handle);
      }
   }
}