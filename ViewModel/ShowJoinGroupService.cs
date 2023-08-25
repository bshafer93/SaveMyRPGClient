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
