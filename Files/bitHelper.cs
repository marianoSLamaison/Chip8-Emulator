class BitHelper
{
    public ushort GetMaskValue(ushort data, ushort mask, ushort displacemt) => (ushort)(data & (mask << displacemt));
    public bool ResultsInCarry(ushort reg1, ushort reg2) => (reg1 & reg2) != 0;
    public bool SubtractionNeedsBorrows(ushort reg1, ushort reg2) =>( (reg1 ^ reg2) & (~reg1) )!= 0;
}