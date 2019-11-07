using System.Collections.Generic;
using System.Linq;

namespace Example__05.Homework
{
    public class ClientDecoratorBase : IClient
    {
        protected readonly IClient Decoratee;
        
        protected ClientDecoratorBase(IClient client)
        {
            Decoratee = client;
        }
        
        public void SendMessage(IMessage message)
        {
            OnBeforeSend(message);
            Decoratee.SendMessage(message);
        }

        public List<IMessage> GetMessages()
        {
            var messages = Decoratee.GetMessages();
            return messages.Select(OnAfterGettingMessages).ToList();
        }
        
        protected virtual void OnBeforeSend(IMessage message)
        {
            
        }

        protected virtual IMessage OnAfterGettingMessages(IMessage message)
        {
            return message;
        }
    }
    
    public class HidingUsersDecorator : ClientDecoratorBase
    {
        public HidingUsersDecorator(IClient client) : base(client)
        {
        }

        protected override IMessage OnAfterGettingMessages(IMessage message)
        {
            message.author = "123";
            message.receiver = "454";
            return message;
        }
    }
    
    public class CipherDecorator : ClientDecoratorBase
    {
        public CipherDecorator(IClient client) : base(client)
        {
        }

        protected override void OnBeforeSend(IMessage message)
        {
            message.text = "<encrypt>" + message.text + "<encrypt/>";
        }
        
        protected override IMessage OnAfterGettingMessages(IMessage message)
        {
            message.text = message.text.Substring(9, message.text.Length);
            return message;
        }
    }
}