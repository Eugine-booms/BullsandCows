namespace BullsAndCowsWPF.Models
{
    public class GameSettings
    {
        public int NumberLength { get; set; } = 4;
        public int MinDigit { get; set; } = 0;
        public int MaxDigit { get; set; } = 9;
        public bool AllowDuplicates { get; set; } = false;
        public int MaxAttempts { get; set; } = 0;
    }
}
