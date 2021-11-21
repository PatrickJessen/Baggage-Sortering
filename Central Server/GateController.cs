using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Central_Server
{
    class GateController
    {
        private Server server;

        public GateController(Server server)
        {
            this.server = server;
        }

        public void OpenCloseGate(string input)
        {
            while (true)
            {
                if (input != "")
                    server.SendMessage(input);
                input = "";
            }
        }
    }
}
