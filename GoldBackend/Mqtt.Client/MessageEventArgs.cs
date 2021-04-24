using System;

namespace Mqtt.Client
{
    public class MessageEventArgs : EventArgs
    {
        private readonly string _message;

        public string Message => _message;

        public MessageEventArgs(string message)
        {
            _message = message;
        }
    }
}
