namespace Example__05.Homework
{
    public class ClientDecoratorBuilder
    {
        private IClient client;
        public ClientDecoratorBuilder(IClient client)
        {
            this.client = client;
        }

        public ClientDecoratorBuilder WithHidingUsers()
        {
            client = new HidingUsersDecorator(client);
            return this;
        }

        public ClientDecoratorBuilder WithLogger()
        {
            client = new CipherDecorator(client);
            return this;
        }

        public IClient Build()
        {
            return client;
        }
    }
}