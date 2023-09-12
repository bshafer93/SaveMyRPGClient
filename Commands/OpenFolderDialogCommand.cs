using SaveMyRPGClient.ViewModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace SaveMyRPGClient.Commands
{
    public class OpenFolderDialogCommand : AsyncCommand
    {
        public CreateGroupViewModel cgvm { get; set; }
        public OpenFolderDialogCommand(CreateGroupViewModel vm)
        {
            cgvm = vm;
        }

        public override bool CanExecute()
        {
            return true;
        }


        public override async Task ExecuteAsync()
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();  
            var result = openFileDlg.ShowDialog();  
            if (result.ToString() != string.Empty)  
            {
                cgvm.SavePath = openFileDlg.SelectedPath;
                return;
            }
            cgvm.SavePath = "";


        }

    }
}
