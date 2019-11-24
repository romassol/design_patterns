using System;

namespace homework_7.States
{
    public class PrintDocumentState: BaseState
    {
        protected override string MessageError => "You haven't printed the document yet";

        public override void PrintDocument(CopyMachineContext context)
        {
            if (context.Money - context.Price > 0)
            {
                context.Money -= context.Price;
                context.State = new ChooseDocumentState();
            }
            else
                throw new Exception("Not enough money");
            
        }
    }
}