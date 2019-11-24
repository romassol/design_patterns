using System;
using homework_7.States;

namespace homework_7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var copyContext1 = new CopyMachineContext();
            copyContext1.PutMoney(30);
            copyContext1.ChooseDevice(Device.FlashCard);
            copyContext1.ChooseDocument("lol");
            copyContext1.PrintDocument();
            copyContext1.ChooseDocument("kek");
            copyContext1.PrintDocument();
            Console.WriteLine(copyContext1.TakeChange());
            
            var copyContext2 = new CopyMachineContext();
            copyContext2.PutMoney(5);
            copyContext2.ChooseDevice(Device.FlashCard);
            copyContext2.ChooseDocument("lol");
            try
            {
                copyContext2.PrintDocument();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}