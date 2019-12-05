using System;
using System.Linq;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;
using Xrm.ReportUtility.Services;

namespace Xrm.ReportUtility
{
    public class ReportServiceChain
    {
        private readonly ReportHandler _handler;

        public ReportServiceChain(string[] args)
        {
            _handler = new TxtFileHandler(null, args);
            _handler = new CsvFileHandler(_handler, args);
            _handler = new XlsxFileHandler(_handler, args);
        }

        public ReportServiceBase GetReportService()
        {
            return _handler.GetReportService();
        }
        
    }
    public abstract class ReportHandler
    {
        private readonly ReportHandler _nextHandler;
        protected string[] args;
        protected abstract ReportServiceBase ReportService { get; }
        protected abstract string FileEnds { get; }
        protected ReportHandler(ReportHandler nextHandler, string[] args)
        {
            _nextHandler = nextHandler;
            this.args = args;
        }

        public ReportServiceBase GetReportService()
        {
            var filename = args[0];
            if (filename.EndsWith(FileEnds))
            {
                return ReportService;
            }

            if (_nextHandler == null)
            {
                throw new NotSupportedException("this extension not supported");
            }

            return _nextHandler.GetReportService();
        }
    }

    public class TxtFileHandler : ReportHandler
    {
        public TxtFileHandler(ReportHandler nextHandler, string[] args) : base(nextHandler, args)
        {
        }

        protected override ReportServiceBase ReportService => new TxtReportService(args);
        protected override string FileEnds => ".txt";
    }
    
    public class CsvFileHandler : ReportHandler
    {
        public CsvFileHandler(ReportHandler nextHandler, string[] args) : base(nextHandler, args)
        {
        }

        protected override ReportServiceBase ReportService => new CsvReportService(args);
        protected override string FileEnds => ".csv";
    }
    
    public class XlsxFileHandler : ReportHandler
    {
        public XlsxFileHandler(ReportHandler nextHandler, string[] args) : base(nextHandler, args)
        {
        }

        protected override ReportServiceBase ReportService => new XlsxReportService(args);
        protected override string FileEnds => ".xlsx";
    }

    public static class Program
    {
        // "Files/table.txt" -data -weightSum -costSum -withIndex -withTotalVolume
        public static void Main(string[] args)
        {
            var service = GetReportService(args);

            var report = service.CreateReport();

            PrintReport(report);

            Console.WriteLine("");
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }

        private static IReportService GetReportService(string[] args)
        {
            //сделала Chain of responsibility, чтобы можно было легко добавить новое расширение или,
            //если мы захотим определять сервер не по концу файла, а по какому-то другому признаку
            var reportChain = new ReportServiceChain(args);
            return reportChain.GetReportService();
//            var filename = args[0];
//
//            if (filename.EndsWith(".txt"))
//            {
//                return new TxtReportService(args);
//            }
//
//            if (filename.EndsWith(".csv"))
//            {
//                return new CsvReportService(args);
//            }
//
//            if (filename.EndsWith(".xlsx"))
//            {
//                return new XlsxReportService(args);
//            }
//
//            throw new NotSupportedException("this extension not supported");
        }

        private static void PrintReport(Report report)
        {
            if (report.Config.WithData && report.Data != null && report.Data.Any())
            {
                var headerRow = "Наименование\tОбъём упаковки\tМасса упаковки\tСтоимость\tКоличество";
                var rowTemplate = "{1,12}\t{2,14}\t{3,14}\t{4,9}\t{5,10}";

                if (report.Config.WithIndex)
                {
                    headerRow = "№\t" + headerRow;
                    rowTemplate = "{0}\t" + rowTemplate;
                }
                if (report.Config.WithTotalVolume)
                {
                    headerRow = headerRow + "\tСуммарный объём";
                    rowTemplate = rowTemplate + "\t{6,15}";
                }
                if (report.Config.WithTotalWeight)
                {
                    headerRow = headerRow + "\tСуммарный вес";
                    rowTemplate = rowTemplate + "\t{7,13}";
                }

                Console.WriteLine(headerRow);

                for (var i = 0; i < report.Data.Length; i++)
                {
                    var dataRow = report.Data[i];
                    Console.WriteLine(rowTemplate, i + 1, dataRow.Name, dataRow.Volume, dataRow.Weight, dataRow.Cost, dataRow.Count, dataRow.Volume * dataRow.Count, dataRow.Weight * dataRow.Count);
                }

                Console.WriteLine();
            }

            if (report.Rows != null && report.Rows.Any())
            {
                Console.WriteLine("Итого:");
                foreach (var reportRow in report.Rows)
                {
                    Console.WriteLine(string.Format("  {0,-20}\t{1}", reportRow.Name, reportRow.Value));
                }
            }
        }
    }
}