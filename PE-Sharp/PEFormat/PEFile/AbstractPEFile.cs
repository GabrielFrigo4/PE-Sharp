using System.Runtime.InteropServices;
using System.Text;
using PE_Sharp.PEStatic;

namespace PE_Sharp.PEFormat.PEFile;
unsafe public abstract class AbstractPEFile: IPEFile
{
    /// <summary>
    /// Constructs a new PE File and reads a file
    /// </summary>
    /// <param name="filename">The PE file to read</param>
    public AbstractPEFile(byte[] bytes, GCHandle pin, byte* p, CoffHeader* coffHeader)
    {
        // Read and lock data
        _bytes = bytes;
        _pin = pin;
        _p = p;

        // Set CoffHeader
        CoffHeader = coffHeader;
    }

    /// <summary>
    /// Dispose this object
    /// </summary>
    public void Dispose()
    {
        if (_p != null)
        {
            _pin.Free();
            _p = null;
        }
    }

    /// <summary>
    /// Gets a byte pointer to the loaded image data
    /// </summary>
    public byte* Base => _p;

    /// <summary>
    /// Gets the Coff header
    /// </summary>
    public CoffHeader* CoffHeader { get; private protected set; }

    /// <summary>
    /// Gets the section headers
    /// </summary>
    public SectionHeader* SectionHeaders { get; private protected set; }

    /// <summary>
    /// Gets the first used section header
    /// </summary>
    /// <remarks>
    /// Used to calculate how much room between the headers and the
    /// first section and therefore how many section headers can be
    /// inserted without having to rejig everything.
    /// </remarks>
    public SectionHeader* FirstUsedSectionHeader { get; private protected set; }

    public SectionHeader* FindSection(string name)
    {
        for (int i = 0; i < CoffHeader->NumberOfSection; i++)
        {
            if (SectionHeaders[i].Name == name)
                return SectionHeaders + i;
        }
        return null;
    }

    /// <summary>
    /// Gets the data directories
    /// </summary>
    public DataDirectory* DataDirectories { get; private protected set; }

    /// <summary>
    /// Gets the data directory count
    /// </summary>
    public int DataDirectoryCount { get; private protected set; }

    /// <summary>
    /// Give an RVA, work out which section it's in
    /// </summary>
    /// <param name="rva">The rva</param>
    /// <returns>The section header, or null</returns>
    public SectionHeader* SectionForRVA(uint rva)
    {
        for (int i = 0; i < CoffHeader->NumberOfSection; i++)
        {
            if (rva >= SectionHeaders[i].VirtualAddress &&
                rva < SectionHeaders[i].VirtualAddress + SectionHeaders[i].VirtualSize)
                return SectionHeaders + i;
        }
        return null;
    }

    /// <summary>
    /// Given an RVA returns a pointer to the image byte at that address
    /// </summary>
    /// <param name="rva">The RVA</param>
    /// <returns>A pointer to the data</returns>
    public byte* GetRVA(uint rva)
    {
        var sect = SectionForRVA(rva);
        if (sect == null)
            return null;

        return _p + sect->PointerToRawData + rva - sect->VirtualAddress;
    }

    /// <summary>
    /// Reads a null terminated string from the image data
    /// </summary>
    /// <param name="rva">RVA of the string</param>
    /// <returns>A string</returns>
    public string ReadString(uint rva)
    {
        // Hacky, ansi only...
        var ptr = (sbyte*)GetRVA(rva);
        var sb = new StringBuilder();
        while (*ptr != '\0')
        {
            sb.Append((char)*ptr++);
        }
        return sb.ToString();
    }

    public abstract PESectionBuilder AddSection();
    public abstract void Write(string filename);

    public virtual PE32File? GetPE32File()
    {
        return null;
    }

    public virtual PE32PlusFile? GetPE32PlusFile()
    {
        return null;
    }

    public PEFileType PEFileType { get; private protected set; }
    public abstract IOptionalHeaderStandard StandardHeader { get; }
    public abstract IOptionalHeaderWindows WindowsHeader { get; }

    /// <summary>
    /// Gets the total length of the PE file
    /// </summary>
    public int Length => _bytes.Length;

    // Loaded image data
    private protected byte[] _bytes;
    private protected GCHandle _pin;
    private protected byte* _p;
    private protected List<PESectionBuilder> _newSections = new();
}
