﻿using PE_Sharp.PEStatic;

namespace PE_Sharp.PEFormat.PEFile;
unsafe public interface IPEFile: IDisposable
{
    SectionHeader* FindSection(string name);
    byte* GetRVA(uint rva);
    string ReadString(uint rva);
    PESectionBuilder AddSection();
    void Write(string filename);
    PE32File? GetPE32File();
    PE32PlusFile? GetPE32PlusFile();

    DataDirectory* DataDirectories { get; }
    int DataDirectoryCount { get; }
    PEFileType PEFileType { get; }
    IOptionalHeaderStandard StandardHeader { get; }
    IOptionalHeaderWindows WindowsHeader { get; }
}
