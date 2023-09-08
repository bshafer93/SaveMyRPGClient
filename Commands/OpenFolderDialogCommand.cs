using Microsoft.WindowsAPICodePack.Dialogs;
using SaveMyRPGClient.ViewModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            var folder_dialog = new CommonOpenFileDialog();
            folder_dialog.IsFolderPicker = true;
            folder_dialog.ShowDialog();
            cgvm.SavePath = folder_dialog.FileName;

        }

    }
}
