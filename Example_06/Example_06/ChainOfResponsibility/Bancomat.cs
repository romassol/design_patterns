//using System.Collections;
//
//namespace Example_06.ChainOfResponsibility
//{
//    public enum CurrencyType
//    {
//        Eur,
//        Dollar,
//        Ruble
//    }
//
//    public interface IBanknote
//    {
//        CurrencyType Currency { get; }
//        string Value { get; }
//    } 
//
//    public class Bancomat
//    {
//        private readonly BanknoteHandler _handler;
//
//        public Bancomat()
//        {
//            _handler = new TenRubleHandler(null);
//            _handler = new TenDollarHandler(_handler);
//            _handler = new FiftyDollarHandler(_handler);
//            _handler = new HundredDollarHandler(_handler);
//        }
//
//        public bool Validate(string banknote)
//        {
//            return _handler.Validate(banknote);
//        } 
//    }
//
//    public abstract class BanknoteHandler
//    {
//        private readonly BanknoteHandler _nextHandler;
//
//        protected BanknoteHandler(BanknoteHandler nextHandler)
//        {
//            _nextHandler = nextHandler;
//        }
//
//        public virtual bool Validate(string banknote)
//        {
//            return _nextHandler != null && _nextHandler.Validate(banknote);
//        }
//    }
//
//    public class TenRubleHandler : BanknoteHandler
//    {
//        public override bool Validate(string banknote)
//        {
//            if (banknote.Equals("10 Рублей"))
//            {
//                return true;
//            }
//            return base.Validate(banknote);
//        }
//
//        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
//        { }
//    }
//
//    public abstract class DollarHandlerBase : BanknoteHandler
//    {
//        public override bool Validate(string banknote)
//        {
//            if (banknote.Equals($"{Value}$"))
//            {
//                return true;
//            }
//            return base.Validate(banknote);
//        }
//
//        protected abstract int Value { get; }
//
//        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
//        {
//        }
//    }
//
//    public class HundredDollarHandler : DollarHandlerBase
//    {
//        protected override int Value => 100;
//
//        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
//        { }
//    }
//
//    public class FiftyDollarHandler : DollarHandlerBase
//    {
//        protected override int Value => 50;
//
//        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
//        { }
//    }
//
//    public class TenDollarHandler : DollarHandlerBase
//    {
//        protected override int Value => 10;
//
//        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
//        { }
//    }
//}
