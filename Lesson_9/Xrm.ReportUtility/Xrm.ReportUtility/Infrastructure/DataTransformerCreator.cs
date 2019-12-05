using Xrm.ReportUtility.Infrastructure.Transformers;
using Xrm.ReportUtility.Infrastructure.Transformers.Abstract;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Infrastructure
{
    public class DataTransformerChain
    {
        private readonly TransformerHandler _handler;

        public DataTransformerChain(ReportConfig config)
        {
            _handler = new WithDataHandler(null, config);
            _handler = new VolumeSumHandler(_handler, config);
            _handler = new WeightSumHandler(_handler, config);
            _handler = new CostSumHandler(_handler, config);
            _handler = new CountSumHandler(_handler, config);
        }

        public IDataTransformer GetReportServiceTransformer()
        {
            return _handler.GetReportServiceTransformer();
        }
        
    }
    public abstract class TransformerHandler
    {
        private readonly TransformerHandler _nextHandler;
        protected IDataTransformer service;
        protected abstract ReportServiceTransformerBase _reportServiceTransformerBase { get; }

        protected TransformerHandler(TransformerHandler nextHandler, ReportConfig config)
        {
            service = new DataTransformer(config);
            _nextHandler = nextHandler;
        }

        public IDataTransformer GetReportServiceTransformer()
        {
            return _nextHandler == null ? service : _nextHandler.GetReportServiceTransformer();
        }
    }

    public class WithDataHandler: TransformerHandler
    {
        public WithDataHandler(TransformerHandler nextHandler, ReportConfig config) : base(nextHandler, config)
        {
        }

        protected override ReportServiceTransformerBase _reportServiceTransformerBase => new WithDataReportTransformer(service);
    }
    
    public class VolumeSumHandler: TransformerHandler
    {
        public VolumeSumHandler(TransformerHandler nextHandler, ReportConfig config) : base(nextHandler, config)
        {
        }

        protected override ReportServiceTransformerBase _reportServiceTransformerBase => new VolumeSumReportTransformer(service);
    }
    
    public class WeightSumHandler: TransformerHandler
    {
        public WeightSumHandler(TransformerHandler nextHandler, ReportConfig config) : base(nextHandler, config)
        {
        }

        protected override ReportServiceTransformerBase _reportServiceTransformerBase => new WeightSumReportTransfomer(service);
    }
    
    public class CostSumHandler: TransformerHandler
    {
        public CostSumHandler(TransformerHandler nextHandler, ReportConfig config) : base(nextHandler, config)
        {
        }

        protected override ReportServiceTransformerBase _reportServiceTransformerBase => new CostSumReportTransformer(service);
    }
    
    public class CountSumHandler: TransformerHandler
    {
        public CountSumHandler(TransformerHandler nextHandler, ReportConfig config) : base(nextHandler, config)
        {
        }

        protected override ReportServiceTransformerBase _reportServiceTransformerBase => new CountSumReportTransformer(service);
    }
    
    public static class DataTransformerCreator
    {
        public static IDataTransformer CreateTransformer(ReportConfig config)
        {
            var dataTransformerChain = new DataTransformerChain(config);
            return dataTransformerChain.GetReportServiceTransformer();
//            IDataTransformer service = new DataTransformer(config);
//
//            if (config.WithData)
//            {
//                service = new WithDataReportTransformer(service);
//            }
//
//            if (config.VolumeSum)
//            {
//                service = new VolumeSumReportTransformer(service);
//            }
//
//            if (config.WeightSum)
//            {
//                service = new WeightSumReportTransfomer(service);
//            }
//
//            if (config.CostSum)
//            {
//                service = new CostSumReportTransformer(service);
//            }
//
//            if (config.CountSum)
//            {
//                service = new CountSumReportTransformer(service);
//            }
//
//            return service;
        }
    }
}