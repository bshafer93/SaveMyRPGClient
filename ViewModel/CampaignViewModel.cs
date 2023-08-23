using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignViewModel
    {
        private readonly GroupModel _group;

        public string GroupID => _group.Id;
        public string Name => _group.Name;
        public string HostEmail => _group.Host_Email;
        public string? Player02Email => _group.P2_Email;
        public string? Player03Email => _group.P3_Email;
        public string? Player04Email => _group.P4_Email;
        public string? LastSaveHash => _group.Last_Save;

        public CampaignViewModel(GroupModel group)
        {
            _group = group;
        }
    }
}
