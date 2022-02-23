namespace BullsAndCowsWPF.Services.Interfaces
{
    public interface IUserDialog
    {
        bool ConfirmInformation_YesNo(string information, string title);
        void Error_OK(string title, string message);
        bool Error_YesNo(string information, string title);
        void Information_OK(string title, string message);
        void Warning_OK(string title, string message);
        bool Warninng_YesNo(string information, string title);
    }
}