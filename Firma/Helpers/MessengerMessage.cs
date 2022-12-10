using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Helpers
{
    public class MessengerMessage<SenderType, ResponseType>
    {
        public SenderType Sender { get; set; }
        public ResponseType Response { get; set; }
        public string Message { get; set; }

    }
}
