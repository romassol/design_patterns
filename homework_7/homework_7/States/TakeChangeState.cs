namespace homework_7.States
{
    public class TakeChangeState: BaseState
    {
        protected override string MessageError => "You haven't printed the document yet";

        public override int TakeChange(CopyMachineContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}