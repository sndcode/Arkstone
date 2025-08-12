using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Arkstone
{
    class classMemory
    {
        /////////////////////
        /////DLL Imports/////
        /////////////////////
        [DllImport("kernel32.dll")]
        private static extern Int32 WriteProcessMemory(IntPtr Handle, int Address, byte[] buffer, int Size, int BytesWritten = 0);
        [DllImport("kernel32.dll")]
        private static extern Int32 ReadProcessMemory(IntPtr Handle, int Address, byte[] buffer, int Size, int BytesRead = 0);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);


        /////////////////////
        //////Variables//////
        /////////////////////
        public static IntPtr pHandle;  //Process Handle

        static string ExeName = frmTrainer.ExeName; //Use the exename definied in the Trainer form


        /////////////////////
        //Process Functions//
        /////////////////////

        public static IntPtr GetProcessHandle() //Find Process
        {
            try
            {
                Process[] ProcList = Process.GetProcessesByName("Wow");
                pHandle = ProcList[0].Handle;
                return pHandle;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public static int GetBaseAddress(string ProcessName, string ModuleName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ProcessName);
                ProcessModuleCollection modules = processes[0].Modules;
                ProcessModule RequestedModuleAddress = null;

                foreach (ProcessModule i in modules)
                {
                    if (i.ModuleName == ModuleName)
                    {
                        RequestedModuleAddress = i;
                    }
                }

                return RequestedModuleAddress.BaseAddress.ToInt32();
            }
            catch
            {
                return 0;
            }
        }

        public static int GetPointerAddress(int Pointer, int[] Offset)
        {
            byte[] Buffer = new byte[4];

            ReadProcessMemory(GetProcessHandle(), Pointer, Buffer, Buffer.Length);

            for (int x = 0; x < (Offset.Length - 1); x++)
            {
                Pointer = BitConverter.ToInt32(Buffer, 0) + Offset[x];
                ReadProcessMemory(GetProcessHandle(), Pointer, Buffer, Buffer.Length);
            }

            Pointer = BitConverter.ToInt32(Buffer, 0) + Offset[Offset.Length - 1];

            return Pointer;
        }


        /////////////////////
        //Writing Functions//
        /////////////////////
        public static void WriteBytes(int Address, byte[] Bytes) //Write Byte Array
        {
            WriteProcessMemory(GetProcessHandle(), Address, Bytes, Bytes.Length);
        }

        public static void WriteFloat(int Address, float Value) //Write a Float
        {
            WriteProcessMemory(GetProcessHandle(), Address, BitConverter.GetBytes(Value), 4);
        }

        public static void WriteDouble(int Address, double Value) //Write a Double
        {
            WriteProcessMemory(GetProcessHandle(), Address, BitConverter.GetBytes(Value), 8);
        }

        public static void WriteInt(int Address, int Value) //Write an Integer (4 Bytes)
        {
            WriteProcessMemory(GetProcessHandle(), Address, BitConverter.GetBytes(Value), 4);
        }

        public static void WriteString(int Address, string String) //Write String (ASCII)
        {
            byte[] Buffer = new ASCIIEncoding().GetBytes(String);
            WriteProcessMemory(GetProcessHandle(), Address, Buffer, Buffer.Length);
        }


        /////////////////////
        //Reading Functions//
        /////////////////////
        public static byte[] ReadBytes(int Address, int Length) //Read Bytes
        {
            byte[] Buffer = new byte[Length];
            ReadProcessMemory(GetProcessHandle(), Address, Buffer, Length);
            return Buffer;
        }

        public static float ReadFloat(int Address) //Read Float
        {
            byte[] Buffer = new byte[4];
            ReadProcessMemory(GetProcessHandle(), Address, Buffer, 4);
            return BitConverter.ToSingle(Buffer, 0);
        }

        public static double ReadDouble(int Address) //Read Double
        {
            byte[] Buffer = new byte[8];
            ReadProcessMemory(GetProcessHandle(), Address, Buffer, 8);
            return BitConverter.ToDouble(Buffer, 0);
        }

        public static int ReadInt(int Address) //Read Integer (4 Bytes)
        {
            byte[] Buffer = new byte[4];
            ReadProcessMemory(GetProcessHandle(), Address, Buffer, 4);
            return BitConverter.ToInt32(Buffer, 0);
        }

        public static string ReadString(int Address, int Size) //Read String (ASCII)
        {
            byte[] Buffer = new byte[Size];
            ReadProcessMemory(GetProcessHandle(), Address, Buffer, Size);
            return Encoding.ASCII.GetString(Buffer).TrimEnd('\0');
        }
    }
}
