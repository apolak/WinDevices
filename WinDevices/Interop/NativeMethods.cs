namespace WinDevices.Interop
{
   using System;
   using System.Runtime.InteropServices;

   [Flags]
   internal enum SetupDiGetClassDevsEx_Flags
   {
      None = 0,
      DIGCF_DEFAULT = 0x00000001,
      DIGCF_PRESENT = 0x00000002,
      DIGCF_ALLCLASSES = 0x00000004,
      DIGCF_PROFILE = 0x00000008,
      DIGCF_DEVICEINTERFACE = 0x00000010,
   }

   internal static class NativeMethods
   {
      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern SafeDeviceInfoSetHandle SetupDiCreateDeviceInfoListEx(
         ref Guid ClassGuid,
         IntPtr hwndParent,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern SafeDeviceInfoSetHandle SetupDiCreateDeviceInfoListEx(
         IntPtr ClassGuid,
         IntPtr hwndParent,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevsEx(
         ref Guid ClassGuid,
         string Enumerator,
         IntPtr hwndParent,
         SetupDiGetClassDevsEx_Flags Flags,
         IntPtr DeviceInfoSet,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevsEx(
         IntPtr ClassGuid,
         string Enumerator,
         IntPtr hwndParent,
         SetupDiGetClassDevsEx_Flags Flags,
         IntPtr DeviceInfoSet,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern IntPtr SetupDiGetClassDevsEx(
         ref Guid ClassGuid,
         string Enumerator,
         IntPtr hwndParent,
         SetupDiGetClassDevsEx_Flags Flags,
         SafeDeviceInfoSetHandle DeviceInfoSet,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      public static extern IntPtr SetupDiGetClassDevsEx(
         IntPtr ClassGuid,
         string Enumerator,
         IntPtr hwndParent,
         SetupDiGetClassDevsEx_Flags Flags,
         SafeDeviceInfoSetHandle DeviceInfoSet,
         string MachineName,
         IntPtr Reserved);

      [DllImport("setupapi", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);
   }
}
