static class BitHelper
{
    public static ushort GetMaskValue(ushort data, ushort mask, ushort displacemt) => (ushort)((data & (mask << displacemt)) >> displacemt );
    public static bool ResultsInCarry(ushort reg1, ushort reg2) => (reg1 & reg2) != 0;
    public static bool SubtractionNeedsBorrows(ushort reg1, ushort reg2) =>( (reg1 ^ reg2) & (~reg1) )!= 0;
}