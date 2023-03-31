using System.Runtime.InteropServices;
using System.Text;

namespace PE_Sharp.PEFormat;
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CoffHeader
{
    public ushort Machine;
    public ushort NumberOfSection;
    public uint TimeDateStamp;
    public uint PointerToSymbolTable;
    public uint NumberOfSymbols;
    public ushort SizeOfOptionalHeader;
    public ushort Characteristics;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct OptionalHeaderStandard: IOptionalHeaderStandard
{
    ushort magic;
    byte majorLinkerVersion;
    byte minorLinkerVersion;
    uint sizeOfCode;
    uint sizeOfInitializedData;
    uint sizeOfUninitializedData;
    uint addressOfEntryPoint;
    uint baseOfCode;
    uint baseOfData;

    public ushort Magic { get => magic; set => magic = value; }
    public byte MajorLinkerVersion { get => majorLinkerVersion; set => majorLinkerVersion = value; }
    public byte MinorLinkerVersion { get => minorLinkerVersion; set => minorLinkerVersion = value; }
    public uint SizeOfCode { get => sizeOfCode; set => sizeOfCode = value; }
    public uint SizeOfInitializedData { get => sizeOfInitializedData; set => sizeOfInitializedData = value; }
    public uint SizeOfUninitializedData { get => sizeOfUninitializedData; set => sizeOfUninitializedData = value; }
    public uint AddressOfEntryPoint { get => addressOfEntryPoint; set => addressOfEntryPoint = value; }
    public uint BaseOfCode { get => baseOfCode; set => baseOfCode = value; }
    public uint BaseOfData { get => baseOfData; set => baseOfData = value; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct OptionalHeaderStandardPlus: IOptionalHeaderStandard
{
    ushort magic;
    byte majorLinkerVersion;
    byte minorLinkerVersion;
    uint sizeOfCode;
    uint sizeOfInitializedData;
    uint sizeOfUninitializedData;
    uint addressOfEntryPoint;
    uint baseOfCode;

    public ushort Magic { get => magic; set => magic = value; }
    public byte MajorLinkerVersion { get => majorLinkerVersion; set => majorLinkerVersion = value; }
    public byte MinorLinkerVersion { get => minorLinkerVersion; set => minorLinkerVersion = value; }
    public uint SizeOfCode { get => sizeOfCode; set => sizeOfCode = value; }
    public uint SizeOfInitializedData { get => sizeOfInitializedData; set => sizeOfInitializedData = value; }
    public uint SizeOfUninitializedData { get => sizeOfUninitializedData; set => sizeOfUninitializedData = value; }
    public uint AddressOfEntryPoint { get => addressOfEntryPoint; set => addressOfEntryPoint = value; }
    public uint BaseOfCode { get => baseOfCode; set => baseOfCode = value; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct OptionalHeaderWindows: IOptionalHeaderWindows
{
    uint imageBase;
    uint sectionAlignment;
    uint fileAlignment;
    ushort majorOperatingSystemVersion;
    ushort minorOperatingSystemVersion;
    ushort majorImageVersion;
    ushort minorImageVersion;
    ushort majorSubsystemVersion;
    ushort minorSubsystemVersion;
    uint win32VersionValue;
    uint sizeOfImage;
    uint sizeOfHeaders;
    uint checkSum;
    ushort subsystem;
    ushort dllCharacteristics;
    uint sizeOfStackReserve;
    uint sizeOfStackCommit;
    uint sizeOfHeapReserve;
    uint sizeOfHeapCommit;
    uint loaderFlags;
    uint numberOfRvaAndSizes;

    public uint ImageBase { get => imageBase; set => imageBase = value; }
    public uint SectionAlignment { get => sectionAlignment; set => sectionAlignment = value; }
    public uint FileAlignment { get => sectionAlignment; set => sectionAlignment = value; }
    public ushort MajorOperatingSystemVersion { get => majorOperatingSystemVersion; set => majorOperatingSystemVersion = value; }
    public ushort MinorOperatingSystemVersion { get => minorOperatingSystemVersion; set => minorOperatingSystemVersion = value; }
    public ushort MajorImageVersion { get => majorImageVersion; set => majorImageVersion = value; }
    public ushort MinorImageVersion { get => minorImageVersion; set => minorImageVersion = value; }
    public ushort MajorSubsystemVersion { get => majorSubsystemVersion; set => majorSubsystemVersion = value; }
    public ushort MinorSubsystemVersion { get => minorSubsystemVersion; set => minorSubsystemVersion = value; }
    public uint Win32VersionValue { get => win32VersionValue; set => win32VersionValue = value; }
    public uint SizeOfImage { get => sizeOfImage; set => sizeOfImage = value; }
    public uint SizeOfHeaders { get => sizeOfHeaders; set => sizeOfHeaders = value; }
    public uint CheckSum { get => checkSum; set => checkSum = value; }
    public ushort Subsystem { get => subsystem; set => subsystem = value; }
    public ushort DllCharacteristics { get => dllCharacteristics; set => dllCharacteristics = value; }
    public uint SizeOfStackReserve { get => sizeOfStackReserve; set => sizeOfStackReserve = value; }
    public uint SizeOfStackCommit { get => sizeOfStackCommit; set => sizeOfStackCommit = value; }
    public uint SizeOfHeapReserve { get => sizeOfHeapReserve; set => sizeOfHeapReserve = value; }
    public uint SizeOfHeapCommit { get => sizeOfHeapCommit; set => sizeOfHeapCommit = value; }
    public uint LoaderFlags { get => loaderFlags; set => loaderFlags = value; }
    public uint NumberOfRvaAndSizes { get => numberOfRvaAndSizes; set => numberOfRvaAndSizes = value; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct OptionalHeaderWindowsPlus: IOptionalHeaderWindows
{
    ulong imageBase;
    uint sectionAlignment;
    uint fileAlignment;
    ushort majorOperatingSystemVersion;
    ushort minorOperatingSystemVersion;
    ushort majorImageVersion;
    ushort minorImageVersion;
    ushort majorSubsystemVersion;
    ushort minorSubsystemVersion;
    uint win32VersionValue;
    uint sizeOfImage;
    uint sizeOfHeaders;
    uint checkSum;
    ushort subsystem;
    ushort dllCharacteristics;
    ulong sizeOfStackReserve;
    ulong sizeOfStackCommit;
    ulong sizeOfHeapReserve;
    ulong sizeOfHeapCommit;
    uint loaderFlags;
    uint numberOfRvaAndSizes;

    public ulong ImageBase { get => imageBase; set => imageBase = value; }
    public uint SectionAlignment { get => sectionAlignment; set => sectionAlignment = value; }
    public uint FileAlignment { get => sectionAlignment; set => sectionAlignment = value; }
    public ushort MajorOperatingSystemVersion { get => majorOperatingSystemVersion; set => majorOperatingSystemVersion = value; }
    public ushort MinorOperatingSystemVersion { get => minorOperatingSystemVersion; set => minorOperatingSystemVersion = value; }
    public ushort MajorImageVersion { get => majorImageVersion; set => majorImageVersion = value; }
    public ushort MinorImageVersion { get => minorImageVersion; set => minorImageVersion = value; }
    public ushort MajorSubsystemVersion { get => majorSubsystemVersion; set => majorSubsystemVersion = value; }
    public ushort MinorSubsystemVersion { get => minorSubsystemVersion; set => minorSubsystemVersion = value; }
    public uint Win32VersionValue { get => win32VersionValue; set => win32VersionValue = value; }
    public uint SizeOfImage { get => sizeOfImage; set => sizeOfImage = value; }
    public uint SizeOfHeaders { get => sizeOfHeaders; set => sizeOfHeaders = value; }
    public uint CheckSum { get => checkSum; set => checkSum = value; }
    public ushort Subsystem { get => subsystem; set => subsystem = value; }
    public ushort DllCharacteristics { get => dllCharacteristics; set => dllCharacteristics = value; }
    public ulong SizeOfStackReserve { get => sizeOfStackReserve; set => sizeOfStackReserve = value; }
    public ulong SizeOfStackCommit { get => sizeOfStackCommit; set => sizeOfStackCommit = value; }
    public ulong SizeOfHeapReserve { get => sizeOfHeapReserve; set => sizeOfHeapReserve = value; }
    public ulong SizeOfHeapCommit { get => sizeOfHeapCommit; set => sizeOfHeapCommit = value; }
    public uint LoaderFlags { get => loaderFlags; set => loaderFlags = value; }
    public uint NumberOfRvaAndSizes { get => numberOfRvaAndSizes; set => numberOfRvaAndSizes = value; }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct DataDirectory
{
    public uint VirtualAddress;
    public uint Size;
}

public enum DataDirectoryIndex
{
    ExportTable,
    ImportTable,
    ResourceTable,
    ExceptionTable,
    CertificateTable,
    BaseRelocationTable,
    Debug,
    Architecture,
    GlobalPtr,
    TLSTable,
    LoadConfigTable,
    BoundImport,
    IAT,
    DelayImportDescriptor,
    CLRRuntimeHeader,
    Reserved,
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct SectionHeader
{
    public ulong NameBytes;
    public uint VirtualSize;
    public uint VirtualAddress;
    public uint SizeOfRawData;
    public uint PointerToRawData;
    public uint PointerToRelocations;
    public uint PointerToLinenumbers;
    public ushort NumberOfRelocations;
    public ushort NumberOfLinenumbers;
    public uint Characteristics;

    public string Name
    {
        get
        {
            var sb = new StringBuilder();
            var t = NameBytes;
            while (t != 0)
            {
                sb.Append((char)(t & 0xFF));
                t >>= 8;
            }
            return sb.ToString();
        }
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct ExportDirectoryTable
{
    public uint Flags;
    public uint TimeDateStamp;
    public ushort MajorVersion;
    public ushort MinorVersion;
    public uint NameRVA;
    public uint OrdinalBase;
    public uint AddressTableEntries;
    public uint NumberOfNamePointers;
    public uint ExportAddressTableRVA;
    public uint NamePointerRVA;
    public uint OrdinalTableRVA;
}

public static class SectionFlags
{
    public const uint Code = 0x00000020;
    public const uint InitializedData = 0x00000040;
    public const uint UninitializedData = 0x00000080;
    public const uint MemRead = 0x40000000;
    public const uint MemWrite = 0x80000000;
}

public interface IOptionalHeaderStandard
{
    ushort Magic { get; set; }
    byte MajorLinkerVersion { get; set; }
    byte MinorLinkerVersion { get; set; }
    uint SizeOfCode { get; set; }
    uint SizeOfInitializedData { get; set; }
    uint SizeOfUninitializedData { get; set; }
    uint AddressOfEntryPoint { get; set; }
    uint BaseOfCode { get; set; }
}

public interface IOptionalHeaderWindows
{
    uint SectionAlignment { get; set; }
    uint FileAlignment { get; set; }
    ushort MajorOperatingSystemVersion { get; set; }
    ushort MinorOperatingSystemVersion { get; set; }
    ushort MajorImageVersion { get; set; }
    ushort MinorImageVersion { get; set; }
    ushort MajorSubsystemVersion { get; set; }
    ushort MinorSubsystemVersion { get; set; }
    uint Win32VersionValue { get; set; }
    uint SizeOfImage { get; set; }
    uint SizeOfHeaders { get; set; }
    uint CheckSum { get; set; }
    ushort Subsystem { get; set; }
    ushort DllCharacteristics { get; set; }
    uint LoaderFlags { get; set; }
    uint NumberOfRvaAndSizes { get; set; }
}