using System;

namespace homework_7.States
{
    public class ChooseDeviceState: BaseState
    {
        protected override string MessageError => "You haven't selected a device yet";

        public override void ChooseDevice(CopyMachineContext context, Device device)
        {
            context.Device = device;
            context.State = new ChooseDocumentState();
        }
    }
}