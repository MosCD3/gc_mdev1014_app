using System;
using firstapp.ENUMS;

namespace firstapp.Models
{
    public class ServerResponseObject
    {
        public ServerReplyStatus status;
        public string error;
        public string message;
        public string data;
    }
}
