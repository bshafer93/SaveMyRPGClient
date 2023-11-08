using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignViewModel : ViewModelBase
    {
        private readonly GroupModel _group;
        public CampaignListViewModel _clvm;
        private SaveListViewModel _savesListVM;
        public int listIndex;
        public string GroupID => _group.Id;
        public string Name => _group.Name;
        public string HostEmail => _group.Host_Email;
        public string? Player02Email => _group.P2_Email;
        public string? Player03Email => _group.P3_Email;
        public string? Player04Email => _group.P4_Email;
        public string? LastSaveHash => _group.Last_Save;

        public SelectGroupCommand SelectGroupCMD { get; set; }
        public SaveListViewModel SaveListVM
        {
            get
            {
                return _savesListVM;
            }
            set
            {
                _savesListVM = value;
                OnPropertyChanged(nameof(SaveListVM));
            }
        }
        public CampaignViewModel(int index,GroupModel group, CampaignListViewModel clvm)
        {
            listIndex=index;
            _group = group;
            _clvm = clvm;
            SelectGroupCMD = new SelectGroupCommand(this);
            SaveListVM = new SaveListViewModel(GroupID, Name,this);
        }

        public void updateSaveList() {

            SaveListVM = new SaveListViewModel(GroupID,Name,this);


        }
    }
}
