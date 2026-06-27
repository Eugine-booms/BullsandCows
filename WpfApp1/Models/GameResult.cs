namespace BullsAndCowsWPF.Models
{
    public class GameResult
    {
        public string Guess { get; set; }
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public int AttemptNumber { get; set; }
        public string ResultText => $"{Guess} — Быков: {Bulls}, Коров: {Cows}";
    }
}
