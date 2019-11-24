namespace homework_7.States
{
    public class CopyMachineContext
    {
        public int Money { get; set; }
        public Device Device { get; set; }
        public string Document { get; set; }
        public int Price { get; }

        public IState State { get; set; }

        public CopyMachineContext()
        {
            Price = 10;
            State = new PutMoneyState();
        }

        public void PutMoney(int money)
        {
            State.PutMoney(this, money);
        }

        public void ChooseDevice(Device device)
        {
            State.ChooseDevice(this, device);
        }

        public void ChooseDocument(string document)
        {
            State.ChooseDocument(this, document);
        }
        
        public void PrintDocument()
        {
            State.PrintDocument(this);
        }
        
        public int TakeChange()
        {
            return State.TakeChange(this);
        }
    }

    public enum Device
    {
        FlashCard,
        WiFi,
        NotSelected
    }
}