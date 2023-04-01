using System.Runtime.InteropServices;
using PE_Sharp.PEStatic;

namespace PE_Sharp.PEFormat.PEFile;
unsafe public class PE32File : AbstractPEFile
{
    /// <summary>
    /// Constructs a new PE File and reads a file
    /// </summary>
    /// <param name="filename">The PE file to read</param>
    public PE32File(byte[] bytes, GCHandle pin, byte* p, CoffHeader* coffHeader)
        : base(bytes, pin, p, coffHeader)
    {
        PEFileType = PEFileType.PE32;

        // Get the standard header
        PtrStandardHeader = (OptionalHeaderStandard*)(CoffHeader + 1);
        if (StandardHeader.Magic != 0x20b && StandardHeader.Magic != 0x10b)
            throw new InvalidDataException("Not a PE32+|PE32 format header");

        // Get the windows header
        PtrWindowsHeader = (OptionalHeaderWindows*)(PtrStandardHeader + 1);

        // Get the section headers
        SectionHeaders = (SectionHeader*)((byte*)PtrStandardHeader + CoffHeader->SizeOfOptionalHeader);

        // Find the first used section header
        for (int i = 0; i < CoffHeader->NumberOfSection; i++)
        {
            if (SectionHeaders[i].SizeOfRawData > 0)
            {
                FirstUsedSectionHeader = SectionHeaders + i;
                break;
            }
        }

        // Get the data directories
        DataDirectories = (DataDirectory*)(PtrWindowsHeader + 1);
        DataDirectoryCount = (int)(((byte*)SectionHeaders - (byte*)DataDirectories) / Marshal.SizeOf<DataDirectory>());
    }

    /// <summary>
    /// Gets HeaderStandard and WindowsHeader in IPEFile
    /// </summary>
    public override IOptionalHeaderStandard StandardHeader { get => *PtrStandardHeader; }
    public override IOptionalHeaderWindows WindowsHeader { get => *PtrWindowsHeader; }

    /// <summary>
    /// Gets the optional standard header
    /// </summary>
    public OptionalHeaderStandard* PtrStandardHeader { get; private set; }

    /// <summary>
    /// Gets the optional windows header
    /// </summary>
    public OptionalHeaderWindows* PtrWindowsHeader { get; private set; }

    /// <summary>
    /// Add a new section to the image
    /// </summary>
    /// <returns>A PESectionBuilder</returns>
    public override PESectionBuilder AddSection()
    {
        // Work out the rva and file position of the new section
        uint rva;
        uint filePosition;
        if (_newSections.Count > 0)
        {
            // Close previous section
            var last = _newSections[_newSections.Count - 1];
            last.Close();
            rva = last.RVA + last.VirtualSize;
            filePosition = last.SizeOnDisk;
        }
        else
        {
            // Work out RVA for the new section
            var last = SectionHeaders + (CoffHeader->NumberOfSection - 1);
            rva = last->VirtualAddress + last->VirtualSize;
            filePosition = last->PointerToRawData + last->SizeOfRawData;
        }

        // Round to alignments
        rva = PEUtils.RoundToAlignment(rva, WindowsHeader.SectionAlignment);
        filePosition = PEUtils.RoundToAlignment(filePosition, WindowsHeader.FileAlignment);

        // Create section builder
        var b = new PESectionBuilder(this, rva, filePosition);
        _newSections.Add(b);
        return b;
    }

    /// <summary>
    /// Rewrite the image to a file
    /// </summary>
    /// <param name="filename"></param>
    public override void Write(string filename)
    {
        // Close last section
        if (_newSections.Count > 0)
        {
            var last = _newSections[^1];
            last.Close();
        }

        // Update the sizes
        foreach (var s in _newSections)
        {
            if ((s.Characteristics & SectionFlags.InitializedData) != 0)
                StandardHeader.SizeOfInitializedData += s.SizeOnDisk;
            if ((s.Characteristics & SectionFlags.UninitializedData) != 0)
                StandardHeader.SizeOfUninitializedData += s.SizeOnDisk;
            if ((s.Characteristics & SectionFlags.Code) != 0)
                StandardHeader.SizeOfCode += s.SizeOnDisk;

            WindowsHeader.SizeOfImage += s.SizeOnDisk;
        }

        // Write new section headers into the image header
        for (int i = 0; i < _newSections.Count; i++)
        {
            // Get the section
            var section = _newSections[i];

            // Update header
            var s = SectionHeaders + CoffHeader->NumberOfSection;
            s->NameBytes = section.NameBytes;
            s->VirtualSize = section.VirtualSize;
            s->VirtualAddress = section.RVA;
            s->SizeOfRawData = section.SizeOnDisk;
            s->PointerToRawData = section.FilePosition;
            s->PointerToRelocations = 0;
            s->PointerToLinenumbers = 0;
            s->NumberOfRelocations = 0;
            s->NumberOfLinenumbers = 0;
            s->Characteristics = section.Characteristics;

            // Update the section count
            CoffHeader->NumberOfSection++;
        }

        // Create file
        using var file = File.Create(filename);
        // Write loaded bytes
        file.Write(_bytes);

        // Write new sections
        foreach (var s in _newSections)
        {
            // Update file position
            System.Diagnostics.Debug.Assert(s.FilePosition >= file.Position);
            file.Position = s.FilePosition;

            // Write the section bytes
            file.Write(s.Bytes);

            // Write padding
            var padding = new byte[s.SizeOnDisk - s.Bytes.Length];
            file.Write(padding);
        }
    }
}
