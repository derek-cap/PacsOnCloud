using System;
using System.Collections.Generic;
using System.Text;

namespace Lords.DataModel
{
    public class Palace : Building
    {
        protected override void UpdateAppreciaitons()
        {
            Appreciation appreciation = AppreciationDictionary.GetAppreciation(1000);
            _appreciations.Add(appreciation);
        }
    }
}
