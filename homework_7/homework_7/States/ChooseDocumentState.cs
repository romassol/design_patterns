namespace homework_7.States
{
    public class ChooseDocumentState: BaseState
    {
        protected override string MessageError => "You haven't selected a document yet";

        public override void ChooseDocument(CopyMachineContext context,  string document)
        {
            context.Document = document;
            context.State = new PrintDocumentState();
        }
        
        public override int TakeChange(CopyMachineContext context)
        {
            var change = context.Money;
            context.Money = 0;
            context.Device = Device.NotSelected;
            context.Document = "";
            context.State = new PrintDocumentState();
            return change;
        }
    }
}