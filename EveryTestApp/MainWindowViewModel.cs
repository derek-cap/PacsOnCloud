using CoreWinSubCommonLib;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EveryTestApp
{
    class MainWindowViewModel : BindableObject
    {
        private CornerInfo _cornerInfo = new CornerInfo();
        public CornerInfo CornerInfo
        {
            get { return _cornerInfo; }
        }
        

        private DelegateCommand _oneClickCommand;
        public ICommand OneClickCommand
        {
            get { return _oneClickCommand ?? (_oneClickCommand = new DelegateCommand(async () => await OnOneClicked())); }
        }

        private async Task OnOneClicked()
        {
            CornerInfo.SetText("Clean");
            await Task.Delay(5000);
            CornerInfo.SetText("OneClicked Completed.");
        }
    }

    class CornerInfo : BindableObject
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value, "Text");
            }
        }

        public void SetText(string text)
        {
            Text = text;
        }
    }

}
