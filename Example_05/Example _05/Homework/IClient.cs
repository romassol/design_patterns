using System.Collections.Generic;

namespace Example__05.Homework
{
    public interface IClient
    {
        void SendMessage(IMessage message);
        List<IMessage> GetMessages();
    }
}