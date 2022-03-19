
using System.Collections.Generic;

namespace BullsAndCowsWPF.Services.Interfaces
{
    internal interface IUserFileDialog
    {
        bool OpenFile(string title, out string selectdFile, string filter = "Все файлы (*.*)|*.*");
        bool OpenFiles(string title, out IEnumerable<string> selectdFiles, string filter = "Все файлы (*.*)|*.*");
        bool SaveFile(string title, out string selectdFile, string defaultFileName = null, string filter = "Все файлы (*.*)|*.*");
    }
}