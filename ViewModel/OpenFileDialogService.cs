using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using SaveMyRPGClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
