using System.Collections.Generic;

namespace Example__05.Homework
{
    public class Client : IClient
    {
        public void SendMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }

        public List<IMessage> GetMessages()
        {
            throw new System.NotImplementedException();
        }
    }
}