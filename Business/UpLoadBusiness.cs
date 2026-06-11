using Frame;
using LT.Common.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UpLoadBusiness
    {
        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void Restart()
        {

        }

        public virtual void Pause()
        {

        }

        public virtual BusinessConfig GetConfig()
        {
            return null;
        }

        public virtual void SetConfig(BusinessConfig config)
        {
            
        }

        public PlcCommunicator PlcCommunicator = new PlcCommunicator();
    }
}
