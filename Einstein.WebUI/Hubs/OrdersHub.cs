using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Einstein.WebUI.Hubs
{
   
    public class OrdersHub : Hub
    {
       
      
        public override Task OnConnected()
        {

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            return base.OnDisconnected(stopCalled);
        }
    }
}