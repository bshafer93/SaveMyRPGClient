using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignViewModel
    {
        private readonly GroupModel _group;
        public CampaignListViewModel _clvm;

        public string GroupID => _group.Id;
        public string Name => _group.Name;
        public string HostEmail => _group.Host_Email;
        public string? Player02Email => _group.P2_Email;
        public string? Player03Email => _group.P3_Email;
        public string? Player04Email => _group.P4_Email;
        public string? LastSaveHash => _group.Last_Save;

        public SelectGroupCommand SelectGroupCMD { get; set; }
        public CampaignViewModel(GroupModel group, CampaignListViewModel clvm)
        {
            _group = group;
            _clvm = clvm;
            SelectGroupCMD = new SelectGroupCommand(this);
        }
    }
}
