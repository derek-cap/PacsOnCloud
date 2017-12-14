using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    public enum RegisterStatus { Ok, Fail }

    public interface IHelper
    {
        event EventHandler<RegisterStatus> Handler;
        //
        RegisterStatus GetStatus();
    }

    public class Helper : IHelper, IDisposable
    {
        public event EventHandler<RegisterStatus> Handler;

        public RegisterStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        Timer timer;

        void TimerTick()
        {
            RegisterStatus s = RegisterStatus.Ok;
            Handler?.Invoke(this, s);
        }

        protected void SetOverTime()
        {

        }

        public void Dispose()
        {
            SetOverTime();
        }
    }
}
