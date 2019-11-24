using System;

namespace homework_7.States
{
    public class PutMoneyState: BaseState
    {
        protected override string MessageError => "You haven't deposited the money yet";

        public override void PutMoney(CopyMachineContext context, int money)
        {
            context.Money = money;
            context.State = new ChooseDeviceState();
        }
    }
}