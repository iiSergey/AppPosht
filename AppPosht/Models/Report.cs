using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPosht.Annotations;
using AppPosht.Properties;
using DotNetDBF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Prism.Commands;
using Prism.Mvvm;

namespace AppPosht.Models
{
    public class Report : BindableBase
    {
        private string _fileNameXls;
        private ReportStatus _status;
        private DateTime _dateReport;
        private ObservableCollection<Piple> _piples;
        public DelegateCommand SaveCommand { get;protected set; }
        public DelegateCommand LoadCommand { get; protected set; }
        public Report([NotNull] string fileNameXls)
        {
            if (string.IsNullOrWhiteSpace(fileNameXls))
                throw new ArgumentException(Resources.Report_Report_Value_cannot_be_null_or_whitespace_,
                    nameof(fileNameXls));
            FileNameXls = fileNameXls;
            Status = ReportStatus.ReportStatusWait;
            Piples = new ObservableCollection<Piple>();
            SaveCommand = new DelegateCommand(() => SaveAsync());
            LoadCommand = new DelegateCommand(() => LoadAsync());
        }

        public Task SaveAsync()
        {
            return Task.Run(() =>
            {
                Status = ReportStatus.ReportStatusSaving;
                string directoryNameOut = Properties.Settings.Default.DirectoryNameOut;
                using (var dbf = new DBFWriter
                {
                    Fields = new[]
                    {
                        new DBFField("ls", NativeDbType.Char, 12),
                        new DBFField("ls_old", NativeDbType.Char, 12),
                        new DBFField("fio", NativeDbType.Char, 50),
                        new DBFField("adress", NativeDbType.Char, 100),
                        new DBFField("paymonth", NativeDbType.Numeric, 2),
                        new DBFField("payyear", NativeDbType.Numeric, 4),
                        new DBFField("sum", NativeDbType.Numeric, 14, 2),
                        new DBFField("date", NativeDbType.Date),
                        new DBFField("eval0", NativeDbType.Numeric, 12, 2),
                        new DBFField("eval1", NativeDbType.Numeric, 12, 2),
                        new DBFField("date_eval1", NativeDbType.Date),
                        new DBFField("rgc_format", NativeDbType.Numeric, 1)
                    },
                    CharEncoding = Encoding.GetEncoding("cp866") //koi8-u
                })
                {
                    foreach (var piple in Piples)
                    {
                        dbf.AddRecord(piple.Number,
                            "",
                            piple.FullName,
                            piple.Adress,
                            piple.DatePay.Month,
                            piple.DatePay.Year,
                            piple.Sum,
                            piple.DatePay,
                            0,
                            0,
                            piple.DatePay,
                            0);

                    }
                    using (
                        Stream fos =
                            File.Open(directoryNameOut + "0500_polgaz_" + DateReport.ToString("dd.MM.yyyy") + ".dbf",
                                FileMode.Create,
                                FileAccess.Write))
                    {
                        dbf.Write(fos);
                    }
                };
                
            Status = ReportStatus.ReportStatusSaved;
            });
        }

        public Task LoadAsync()
        {
            return Task.Run(() =>
            {
                Status = ReportStatus.ReportStatusLoading;
                HSSFWorkbook hssfwb;
                if (!File.Exists(FileNameXls))
                    Status = ReportStatus.ReportStatusError;
                using (var file = new FileStream(FileNameXls, FileMode.Open, FileAccess.Read))
                {
                    hssfwb = new HSSFWorkbook(file);
                }
                var sheet = hssfwb.GetSheetAt(0);

                var date = sheet.GetRow(8).GetCell(4).DateCellValue;
                DateReport = date;
                for (int row = 0; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) == null) continue;
                    var rowdata = sheet.GetRow(row);
                    if (rowdata.LastCellNum < 11) continue;
                    var cellData = rowdata.GetCell(11);
                    if (cellData?.CellType != CellType.String) continue;
                    if (cellData.StringCellValue != "26007300956933") continue;
                    var rowdataPlarnik = sheet.GetRow(row + 1);
                    var cellDataPlarnik = rowdataPlarnik.GetCell(15);
                    var rowdataPlarnik2 = sheet.GetRow(row + 2);
                    var cellDataPlarnik2 = rowdataPlarnik2.GetCell(1);
                    var sum = cellDataPlarnik.NumericCellValue;
                    var rax = cellDataPlarnik2
                        .StringCellValue.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault();
                    var fio = cellDataPlarnik2
                        .StringCellValue.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                        .Skip(1).Take(3).Aggregate((res, next) => res + " " + next);
                    var adr = cellDataPlarnik2
                        .StringCellValue.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                        .Skip(4).TakeWhile(p => !p.Contains("До")).Aggregate((res, next) => res + " " + next);
                    var piple = new Piple(adr, fio, rax, date, sum);

                    Piples.Add(piple);
                }
                Status = ReportStatus.ReportStatusLoaded;
            });
        }

        public string FileNameXls
        {
            get => _fileNameXls;
            protected set => SetProperty(ref _fileNameXls, value);
        }

        public ReportStatus Status
        {
            get => _status;
            protected set => SetProperty(ref _status, value);
        }

        public double Total => _piples.Sum(p => p.Sum) ;

        public DateTime DateReport
        {
            get => _dateReport;
            protected set => SetProperty(ref _dateReport, value);
        }

        public ObservableCollection<Piple> Piples
        {
            get => _piples;
            protected set
            {
                SetProperty(ref _piples, value);
                _piples.CollectionChanged += (s, e) => RaisePropertyChanged(nameof(Total));
            }
        }
    }
}