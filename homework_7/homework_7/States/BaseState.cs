using System;

namespace homework_7.States
{
    public abstract class BaseState: IState
    {
        protected virtual string MessageError { get; set; }
        public virtual void PutMoney(CopyMachineContext context, int money)
        {
            throw new Exception(MessageError);
        }

        public virtual void ChooseDevice(CopyMachineContext context, Device device)
        {
            throw new Exception(MessageError);
        }

        public virtual void ChooseDocument(CopyMachineContext context, string document)
        {
            throw new Exception(MessageError);
        }

        public virtual void PrintDocument(CopyMachineContext context)
        {
            throw new Exception(MessageError);
        }

        public virtual int TakeChange(CopyMachineContext context)
        {
            context.State = new PutMoneyState();
            return 0;
        }
    }
}