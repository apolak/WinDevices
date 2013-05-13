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

   public enum SetupDiGetDeviceRegistryProperty_Property
   {
      SPDRP_DEVICEDESC = 0x00000000,
      SPDRP_HARDWAREID = 0x00000001,
      SPDRP_COMPATIBLEIDS = 0x00000002,
      SPDRP_SERVICE = 0x00000004,
      SPDRP_CLASS = 0x00000007,
      SPDRP_CLASSGUID = 0x00000008,
      SPDRP_DRIVER = 0x00000009,
      SPDRP_CONFIGFLAGS = 0x0000000A,
      SPDRP_MFG = 0x0000000B,
      SPDRP_FRIENDLYNAME = 0x0000000C,
      SPDRP_LOCATION_INFORMATION = 0x0000000D,
      SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,
      SPDRP_CAPABILITIES = 0x0000000F,
      SPDRP_UI_NUMBER = 0x00000010,
      SPDRP_UPPERFILTERS = 0x00000011,
      SPDRP_LOWERFILTERS = 0x00000012,
      SPDRP_BUSTYPEGUID = 0x00000013,
      SPDRP_LEGACYBUSTYPE = 0x00000014,
      SPDRP_BUSNUMBER = 0x00000015,
      SPDRP_ENUMERATOR_NAME = 0x00000016,
      SPDRP_SECURITY = 0x00000017,
      SPDRP_SECURITY_SDS = 0x00000018,
      SPDRP_DEVTYPE = 0x00000019,
      SPDRP_EXCLUSIVE = 0x0000001A,
      SPDRP_CHARACTERISTICS = 0x0000001B,
      SPDRP_ADDRESS = 0x0000001C,
      SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,
      SPDRP_DEVICE_POWER_DATA = 0x0000001E,
      SPDRP_REMOVAL_POLICY = 0x0000001F,
      SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,
      SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,
      SPDRP_INSTALL_STATE = 0x00000022,
      SPDRP_LOCATION_PATHS = 0x00000023,
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

      [DllImport("setupapi", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceProperty(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         ref DEVPROPKEY PropertyKey,
         out int PropertyType,
         byte[] PropertyBuffer,
         int PropertyBufferSize,
         IntPtr RequiredSize,
         IntPtr Flags);

      [DllImport("setupapi", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceProperty(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         ref DEVPROPKEY PropertyKey,
         out int PropertyType,
         IntPtr PropertyBuffer,
         IntPtr PropertyBufferSize,
         out int RequiredSize,
         IntPtr Flags);

      [DllImport("setupapi", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceRegistryProperty(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         SetupDiGetDeviceRegistryProperty_Property Property,
         IntPtr PropertyRegDataType,
         IntPtr PropertyBuffer,
         IntPtr PropertyBufferSize,
         out int RequiredSize);

      [DllImport("setupapi", CharSet = CharSet.Unicode, SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool SetupDiGetDeviceRegistryProperty(
         SafeDeviceInfoSetHandle DeviceInfoSet,
         ref SP_DEVINFO_DATA DeviceInfoData,
         SetupDiGetDeviceRegistryProperty_Property Property,
         IntPtr PropertyRegDataType,
         byte[] PropertyBuffer,
         int PropertyBufferSize,
         IntPtr RequiredSize);
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

   [StructLayout(LayoutKind.Sequential)]
   internal struct DEVPROPKEY
   {
      public Guid fmtid;
      public int pid;
   }
}
