using System.Runtime.InteropServices;
using PE_Sharp.PEFormat;
using PE_Sharp.PEFormat.PEFile;

namespace PE_Sharp.PEStatic;
unsafe public class PEFileUtils
{
    public unsafe static IPEFile GetPEFile(string filename)
    {
        // Read and lock data
        byte[] _bytes = File.ReadAllBytes(filename);
        GCHandle _pin = GCHandle.Alloc(_bytes, GCHandleType.Pinned);
        byte* _p = (byte*)_pin.AddrOfPinnedObject();

        // Check PE signature
        var PEOffset = *(uint*)(_p + 0x3c);
        var PESignature = *(uint*)(_p + PEOffset);
        if (PESignature != 0x00004550) //"PE\0\0"
        {
            throw new InvalidDataException("PE signature not found");
        }

        // Get the coff header
        CoffHeader* _coffHeader = (CoffHeader*)(_p + PEOffset + 4);
        if (_coffHeader->SizeOfOptionalHeader == 0)
            throw new InvalidDataException("Optional header missing");

        // Get Version of PE32|PE32+
        ushort* magicNumber = (ushort*)(_coffHeader + 1);
        if (*magicNumber == 0x10b)
        {
            return new PE32File(_bytes, _pin, _p, _coffHeader);
        }
        else if (*magicNumber == 0x20b)
        {
            return new PE32PlusFile(_bytes, _pin, _p, _coffHeader);
        }
        else
        {
            throw new InvalidDataException("Not a PE32+|PE32 format header");
        }
    }

    static PEFileUtils()
    {
        // Sanity checks that we've got the structures declared correctly
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<CoffHeader>() == 20);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<OptionalHeaderStandard>() == 28);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<OptionalHeaderStandardPlus>() == 24);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<OptionalHeaderWindows>() == 68);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<OptionalHeaderWindowsPlus>() == 88);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<OptionalHeaderWindowsPlus>() == 88);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<SectionHeader>() == 40);
        System.Diagnostics.Debug.Assert(Marshal.SizeOf<ExportDirectoryTable>() == 40);
    }
}

public enum PEFileType
{
    PE32,
    PE32Plus,
}