using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TCPTracker
{
    public abstract class Observer
    {
        public abstract Task Update(PingReply reply, IPAddress IP);
    }

    public abstract class Observable
    {
        private List<Observer> _observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }

        public async Task Notify(PingReply reply, IPAddress IP)
        {
            foreach (Observer o in _observers)
            {
                await o.Update(reply, IP);
            }
        }
    }
}
