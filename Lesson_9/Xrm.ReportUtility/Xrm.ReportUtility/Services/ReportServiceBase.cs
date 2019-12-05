using System.IO;
using System.Linq;
using Xrm.ReportUtility.Infrastructure;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Services
{
    public class ReportConfigBuilder
    {
        private bool withData { get; set; }

        private bool withIndex { get; set; }
        private bool withTotalVolume { get; set; }
        private bool withTotalWeight { get; set; }

        private bool volumeSum { get; set; }
        private bool weightSum { get; set; }
        private bool costSum { get; set; }
        private bool countSum { get; set; }
//
//        public ReportConfig Build()
//        {
//            
//            return new ReportConfig
//            {
//                WithData = withData,
//                WithIndex = withIndex,
//                WithTotalVolume = withTotalVolume,
//                WithTotalWeight = withTotalWeight,
//                VolumeSum = volumeSum,
//                WeightSum = weightSum,
//                CostSum = costSum,
//                CountSum = countSum
//            };
//        }
        
        public FinalBuilder VolumeSum()
        {
            volumeSum = true;
            return new FinalBuilder(this);
        }
        public FinalBuilder WeightSum()
        {
            weightSum = true;
            return new FinalBuilder(this);
        }
        public FinalBuilder CostSum()
        {
            costSum = true;
            return new FinalBuilder(this);
        }
        public FinalBuilder CountSum()
        {
            countSum = true;
            return new FinalBuilder(this);
        }

        public class FinalBuilder: ReportConfigBuilder
        {
            private ReportConfigBuilder builder;
            public FinalBuilder(ReportConfigBuilder builder)
            {
                this.builder = builder;
            }
            public ReportConfigBuilder WithIndex()
            {
                builder.withIndex = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder WithData()
            {
                builder.withData = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder WithTotalVolume()
            {
                builder.withTotalVolume = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder WithTotalWeight()
            {
                builder.withTotalWeight = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder WeightSum()
            {
                builder.weightSum = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder CostSum()
            {
                builder.costSum = true;
                return new FinalBuilder(this);
            }
            public ReportConfigBuilder CountSum()
            {
                builder.countSum = true;
                return new FinalBuilder(this);
            }
            
            public ReportConfig Build()
            {
                return new ReportConfig
                {
                    WithData = builder.withData,
                    WithIndex = builder.withIndex,
                    WithTotalVolume = builder.withTotalVolume,
                    WithTotalWeight = builder.withTotalWeight,
                    VolumeSum = builder.volumeSum,
                    WeightSum = builder.weightSum,
                    CostSum = builder.costSum,
                    CountSum = builder.countSum
                };
            }
        }
    }
    public abstract class ReportServiceBase : IReportService
    {
        private readonly string[] _args;

        protected ReportServiceBase(string[] args)
        {
            _args = args;
        }

        public Report CreateReport()
        {
            var config = ParseConfig();
            var dataTransformer = DataTransformerCreator.CreateTransformer(config);

            var fileName = _args[0];
            var text = File.ReadAllText(fileName);
            var data = GetDataRows(text);
            return dataTransformer.TransformData(data);
        }

        private ReportConfig ParseConfig()
        {
            //ввела билдер, чтобы обязательно был указан один из флагов «volumeSum», «weightSum», «costSum», «countSum»
            //если одного из обязательных флагов не будет, то finalBuilder не проинициализируется
            var builder = new ReportConfigBuilder();
            ReportConfigBuilder.FinalBuilder finalBuilder = null;
            if (_args.Contains("-volumeSum"))
            {
                finalBuilder = builder.VolumeSum();
            }
            if (_args.Contains("-weightSum"))
            {
                finalBuilder = builder.WeightSum();
            }
            
            if (_args.Contains("-costSum"))
            {
                finalBuilder = builder.CostSum();
            }
            
            if (_args.Contains("-countSum"))
            {
                finalBuilder = builder.CountSum();
            }
            if (_args.Contains("-data"))
            {
                finalBuilder.CostSum();
            }
            if (_args.Contains("-withIndex"))
            {
                finalBuilder.WithIndex();
            }
            if (_args.Contains("-withTotalVolume"))
            {
                finalBuilder.WithTotalVolume();
            }
            if (_args.Contains("-withTotalWeight"))
            {
                finalBuilder.WithTotalWeight();
            }

            return finalBuilder.Build();
//            return new ReportConfig
//            {
//                WithData = _args.Contains("-data"),
//
//                WithIndex = _args.Contains("-withIndex"),
//                WithTotalVolume = _args.Contains("-withTotalVolume"),
//                WithTotalWeight = _args.Contains("-withTotalWeight"),
//
//                VolumeSum = _args.Contains("-volumeSum"),
//                WeightSum = _args.Contains("-weightSum"),
//                CostSum = _args.Contains("-costSum"),
//                CountSum = _args.Contains("-countSum")
//            };
        }

        protected abstract DataRow[] GetDataRows(string text);
    }
}
