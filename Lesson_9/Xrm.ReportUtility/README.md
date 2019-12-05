####Реализации шаблонов

1. Chain of responsibility - в Program.cs в методе GetReportService - вместо цепочки if`ов сделала цепочку ответственности, благодаря этому если не подошел txt, например, в цепочки пойдет дальше к csv, и так далее по всем расширениям. Мы можем удобно добавлять новые расширения или, например, определять сервер отчетов не по концу файла, а по какому-то дургому признаку.
2. Builder -  в Services/ReportServiceBase.cs в методе ParseConfig - благодаря билдеру теперь обязательно указать один из флагов «volumeSum», «weightSum», «costSum», «countSum», если одного из обязательных флагов не будет, то finalBuilder не проинициализируется.
3. Chain of responsibility - в Infrastructure/DataTransformerCreator.cs в методе CreateTransformer - сделала цепочку ответственности, чтобы в зависимости от конфига возвращался нужный трансформер.

#### Найденные реализованные шаблоны

1. Декоратор - в классах: CostSumReportTransformer, CountSumReportTransformer, VolumeSumReportTransformer, WeightSumReportTransfomer, WithDataReportTransformer - так как это все наследники абстрактного класса ReportServiceTransformerBase, и потом в каждом из наследников после TransformData report как-то еще изменяется - добавляются новые строки, например.