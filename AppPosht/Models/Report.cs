using System;
using System.Collections.Generic;
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
using Prism.Mvvm;

namespace AppPosht.Models
{
    public class Report : BindableBase
    {
        public Report([NotNull] string fileNameXls)
        {
            if (string.IsNullOrWhiteSpace(fileNameXls))
                throw new ArgumentException(Resources.Report_Report_Value_cannot_be_null_or_whitespace_,
                    nameof(fileNameXls));
            FileNameXls = fileNameXls;
            Status = ReportStatus.Wait;
            Piples = new ObservableCollection<Piple>();
        }

        public Task SaveAsync()
        {
            return Task.Run(() =>
            {
                Status = ReportStatus.Saving;
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
                    CharEncoding = Encoding.GetEncoding("koi8-u")
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
                }
                Status = ReportStatus.Saved;
            });
        }

        public Task LoadAsync()
        {
            return Task.Run(() =>
            {
                Status = ReportStatus.Loading;
                HSSFWorkbook hssfwb;
                if (!File.Exists(FileNameXls))
                    Status = ReportStatus.Error;
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
                    //     RiListBox.Items.Add("platnik " + sum + " -- " + rax + " -- " + fio);
                    var piple = new Piple(adr, fio, rax, date, sum);
                    Piples.Add(piple);
                }
                Status = ReportStatus.Loaded;
            });
        }

        public string FileNameXls { get; protected set; }
        public ReportStatus Status { get; protected set; }
        public double Sum => Piples?.Sum(p => p.Sum) ?? 0.0;
        public DateTime DateReport { get; protected set; }
        public ObservableCollection<Piple> Piples { get; protected set; }

        public enum ReportStatus
        {
            Wait,
            Loading,
            Loaded,
            Saving,
            Saved,
            Error
        }
    }
}