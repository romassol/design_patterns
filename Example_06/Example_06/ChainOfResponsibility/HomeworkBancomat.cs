using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Example_06.ChainOfResponsibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }
    
    public interface IBanknote
    {
        CurrencyType Currency { get; }
        int Value { get; }
    }
    
    public class Banknote : IBanknote
    {
        public CurrencyType Currency { get; }
        public int Value { get; }

        public Banknote(CurrencyType currency, int value)
        {
            Currency = currency;
            Value = value;
        }
    }

    public class HomeworkBancomat
    {
        private readonly BanknoteHandler _handler;
        public Dictionary<string, CurrencyType> StrCurrencyToEnum = 
            new Dictionary<string, CurrencyType>()
            {
                {"руб", CurrencyType.Ruble},
                {"$", CurrencyType.Dollar},
                {"eur", CurrencyType.Eur}
            };

        

        public HomeworkBancomat()
        {
            _handler = new TenRubleHandler(null);
            _handler = new FiftyRubleHandler(_handler);
            _handler = new HundredRubleHandler(_handler);
            _handler = new FiveHundredRubleHandler(_handler);
            _handler = new ThousandRubleHandler(_handler);
            _handler = new FiveThousandRubleHandler(_handler);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
            _handler = new TenEurHandler(_handler);
            _handler = new TwentyEurHandler(_handler);
            _handler = new FiftyEurHandler(_handler);
            _handler = new HundredEurHandler(_handler);
            _handler = new TwoHundredEurHandler(_handler);
            _handler = new FiveHundredEurHandler(_handler);
        }
        
        private Tuple<CurrencyType, int> ParseBanknote(string banknote)
        {
            var regex = new Regex(@"(\d+)\s?(руб|\$|eur)");
            var match = regex.Match(banknote);
            if (!match.Success) throw new ArgumentException("You send invalid banknote");
            var value = int.Parse(match.Groups[1].Value);
            var currency = match.Groups[2].Value;
            return new Tuple<CurrencyType, int> (StrCurrencyToEnum[currency], value);
        }

        public List<Tuple<IBanknote, int>> Cash(string banknote)
        {
            var banknotesCount = new List<Tuple<IBanknote, int>>();
            var inputValue = ParseBanknote(banknote);
            return _handler.Cash(banknotesCount, inputValue.Item1, inputValue.Item2);
        }
        
    }
    
    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;
        protected abstract Banknote Banknote { get; }

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public List<Tuple<IBanknote, int>> Cash(
            List<Tuple<IBanknote, int>> banknotesCount,
            CurrencyType currency, int currentValue
            )
        {
            if (currency == Banknote.Currency)
            {
                var count = currentValue / Banknote.Value;
                currentValue -= count * Banknote.Value;
                if (count > 0)
                    banknotesCount.Add(new Tuple<IBanknote, int>(Banknote, count));
            }
            if (currentValue == 0)
                return banknotesCount;

            if (_nextHandler == null)
            {
                Console.WriteLine("It is impossible to cash the transferred amount");
                return new List<Tuple<IBanknote, int>>();
            }
            return _nextHandler.Cash(banknotesCount, currency, currentValue);
        }
    }

    public class FiveHundredEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 500);

        public FiveHundredEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TwoHundredEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 200);

        public TwoHundredEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class HundredEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 100);

        public HundredEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiftyEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 50);

        public FiftyEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TwentyEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 20);

        public TwentyEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TenEurHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Eur, 10);

        public TenEurHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class HundredDollarHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Dollar, 100);

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiftyDollarHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Dollar, 50);

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TenDollarHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Dollar, 10);

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiveThousandRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 5000);

        public FiveThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class ThousandRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 1000);

        public ThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiveHundredRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 500);

        public FiveHundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class HundredRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 100);

        public HundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class FiftyRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 50);

        public FiftyRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }

    public class TenRubleHandler : BanknoteHandler
    {
        protected override Banknote Banknote => new Banknote(CurrencyType.Ruble, 10);

        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }
    }
}