namespace WinDevices.Interop
{
   using System;
   using System.Runtime.InteropServices;
   using System.Text;

   [Flags]
   internal enum SetupDiGetClassDevsEx_Flags
   {
      None = 0,
      DIGCF_DEFAULT = 1,
      DIGCF_PRESENT = 2,
      DIGCF_ALLCLASSES = 4,
      DIGCF_PROFILE = 8,
      DIGCF_DEVICEINTERFACE = 16
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

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceInstanceId(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         IntPtr DeviceInstanceId,
         IntPtr DeviceInstanceIdSize,
         out int RequiredSize);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceInstanceId(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         StringBuilder DeviceInstanceId,
         int DeviceInstanceIdSize,
         IntPtr RequiredSize);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceInterfaceDetail(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
         IntPtr DeviceInterfaceDetailData,
         IntPtr DeviceInterfaceDetailDataSize,
         out int RequiredSize,
         IntPtr DeviceInfoData);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceInterfaceDetail(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
         IntPtr DeviceInterfaceDetailData,
         int DeviceInterfaceDetailDataSize,
         IntPtr RequiredSize,
         ref SP_DEVINFO_DATA DeviceInfoData);

      [DllImport("setupapi", CharSet = CharSet.Auto, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceInterfaceDetail(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
         IntPtr DeviceInterfaceDetailData,
         int DeviceInterfaceDetailDataSize,
         IntPtr RequiredSize,
         IntPtr DeviceInfoData);

      [DllImport("setupapi", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiEnumDeviceInfo(
         SafeDeviceInfoSetHandle DeviceInfoSet, int MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);

      [DllImport("setupapi", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiEnumDeviceInterfaces(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         IntPtr DeviceInfoData,
         ref Guid InterfaceClassGuid,
         int MemberIndex,
         ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

      [DllImport("setupapi", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiEnumDeviceInterfaces(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         ref Guid InterfaceClassGuid,
         int MemberIndex,
         ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);
   }

   [StructLayout(LayoutKind.Sequential)]
   internal struct SP_DEVINFO_DATA
   {
      public int cbSize;
      public Guid ClassGuid;
      public int DevInst;
      private IntPtr Reserved;
   }

   [Flags]
   internal enum SP_DEVICE_INTERFACE_DATA_Flags
   {
      None = 0,
      SPINT_ACTIVE = 1,
      SPINT_DEFAULT = 2,
      SPINT_REMOVED = 4
   }

   [StructLayout(LayoutKind.Sequential)]
   internal struct SP_DEVICE_INTERFACE_DATA
   {
      public Int32 cbSize;
      public Guid InterfaceClassGuid;
      public SP_DEVICE_INTERFACE_DATA_Flags Flags;
      private IntPtr Reserved;
   }

   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
   internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
   {
      public int cbSize;
      public char DevicePath;
   }
}
