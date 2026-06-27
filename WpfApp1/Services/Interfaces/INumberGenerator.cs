namespace BullsAndCowsWPF.Services.Interfaces
{
    public interface INumberGenerator
    {
        string GenerateNumber(int length, int minDigit, int maxDigit, bool allowDuplicates);
    }
}
