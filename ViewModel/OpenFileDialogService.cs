using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

namespace SaveMyRPGClient.ViewModel
{
    public class OpenFileDialogService
    {

        public OpenFileDialogService()
        {

        }
        public string OpenFileDialog()
        {
            var fileDialogWindow = new OpenFileDialog();
            fileDialogWindow.ShowDialog();

            if (!fileDialogWindow.FileName.IsNullOrEmpty())
            {
                return fileDialogWindow.FileName;
            }

            return "";

        }
    }
}
