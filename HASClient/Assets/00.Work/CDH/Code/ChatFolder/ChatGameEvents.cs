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

    public class ChatGameEvents
    {
        public static readonly ChatOpenEvent openEvt = new();
        public static readonly ChatCloseEvent closeEvt = new();
        public static readonly ChattingEvent chattingEvt = new();
        public static readonly ChattingSendEvent chatSendEvt = new();
    }

    public class ChatOpenEvent : GameEvent
    {

    }
    public class ChatCloseEvent : GameEvent
    {
        public float timer { get; set; }
    }
    public class ChattingEvent : GameEvent
    {

    }
    public class ChattingSendEvent : GameEvent
    {

    }
}
