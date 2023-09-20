using SaveMyRPGClient.View;

namespace SaveMyRPGClient.ViewModel
{
    public class ShowJoinGroupService
    {

        public CampaignListViewModel _clvm;

        public ShowJoinGroupService(CampaignListViewModel clvm)
        {
            _clvm = clvm;
        }
        public void ShowDialog()
        {
            var createJoinGroupView = new JoinGroupView();
            createJoinGroupView.DataContext = new JoinGroupViewModel(_clvm);
            createJoinGroupView.ShowDialog();
        }
    }
}
