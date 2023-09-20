using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class OpenFileDialogCommand : AsyncCommand
    {
        public CreateGroupViewModel cgvm { get; set; }
        public OpenFileDialogCommand(CreateGroupViewModel vm)
        {
            cgvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }


        public override async Task ExecuteAsync()
        {

            string fileName = new OpenFileDialogService().OpenFileDialog();
            cgvm.SavePath = fileName;

        }
    }
}
