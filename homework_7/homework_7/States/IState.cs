namespace homework_7.States
{
    public interface IState
    {
        void PutMoney(CopyMachineContext context, int money);
        void ChooseDevice(CopyMachineContext context, Device device);
        void ChooseDocument(CopyMachineContext context, string document);
        void PrintDocument(CopyMachineContext context);
        int TakeChange(CopyMachineContext context);
    }
}