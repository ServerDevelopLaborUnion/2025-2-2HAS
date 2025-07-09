using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._00.Work.CDH.Code.ChatFolder
{
    public class ChatRecvEventHandler : GameEvent
    {
        public string pName { get; set; }
        public string message { get; set; }
    }

}
