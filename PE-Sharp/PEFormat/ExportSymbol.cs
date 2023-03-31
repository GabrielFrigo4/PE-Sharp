namespace PE_Sharp.PEFormat;
public struct ExportSymbol
{
    public const uint EnableValue = 1; 
    public const uint DisableValue = 0;

    public string Name { get; set; }
    public uint? Value { get; set; }

    public ExportSymbol(string name)
    {
        Name = name;
    }
    
    public ExportSymbol(string name, uint value)
    {
        Name = name;
        Value = value;
    }
}
