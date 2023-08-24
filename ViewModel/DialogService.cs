﻿using SaveMyRPGClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{


    public class DialogService : IDialogService 
    {
        public CampaignListViewModel _clvm;
        public DialogService(CampaignListViewModel clvm) { 
            _clvm = clvm;
        }
        public void ShowDialog() {
            var createGroupWindow = new CreateGroupView();
            createGroupWindow.DataContext = new CreateGroupViewModel(_clvm);
            createGroupWindow.ShowDialog();
        }
    }
}
