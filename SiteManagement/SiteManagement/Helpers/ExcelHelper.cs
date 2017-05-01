using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using System.Data;
using System.Reflection;

namespace SiteManagement.Helpers
{
    public class ExcelHelper
    {
        public string FileName { get; set; }
        public ExcelPackage ExcelApplication => _excelApplication ?? (_excelApplication = new ExcelPackage(new FileInfo(FileName)));
        private ExcelPackage _excelApplication;

        public ExcelWorkbook Workbook => _excelApplication.Workbook;
        public ExcelWorksheet Worksheet { get; set; }

        #region value
        public static void SetValue(ExcelRange rng, object value)
        {
            rng.Value = value;
        }
        public static void SetValue(ExcelWorksheet ws, int row, int column, object value)
        {
            SetValue(ws.Cells[row, column], value);
        }
        public static void SetValue(ExcelWorksheet ws, string address, object value)
        {
            SetValue(ws.Cells[address], value);
        }
        public static object GetValue(ExcelRange rng)
        {
            return rng.Value;
        }
        public static object GetValue(ExcelWorksheet ws, int row, int column)
        {
            return GetValue(ws.Cells[row, column]) ?? "";
        }
        public static object GetValue(ExcelWorksheet ws, string address)
        {
            return GetValue(ws.Cells[address]) ?? "";
        }
        #endregion

        #region Number format
        public static void SetFormat(ExcelWorksheet ws, int row, int column, string format)
        {
            SetFormat(ws.Cells[row, column], format);
        }

        public static void SetFormat(ExcelWorksheet ws, string address, string format)
        {
            SetFormat(ws.Cells[address], format);
        }

        public static void SetFormat(ExcelRange rng, string format)
        {
            rng.Style.Numberformat.Format = format;
        }
        #endregion

        #region Formula
        public static void SetFormula(ExcelRange rng, string formula)
        {
            rng.Formula = formula;
        }
        public static void SetFormula(ExcelWorksheet ws, int row, int column, string formula)
        {
            SetFormula(ws.Cells[row, column], formula);
        }
        public static void SetFormula(ExcelWorksheet ws, string address, string formula)
        {
            SetFormula(ws.Cells[address], formula);
        }

        public static string GetFormula(ExcelRange rng)
        {
            return rng.Formula;
        }
        public static string GetFormula(ExcelWorksheet ws, int row, int column)
        {
            return GetFormula(ws.Cells[row, column]);
        }
        public static string GetFormula(ExcelWorksheet ws, string address)
        {
            return GetFormula(ws.Cells[address]);
        }
        #endregion

        #region Address
        public static string GetAddress(ExcelRange rng)
        {
            return rng.Address;
        }
        public static string GetAddress(ExcelWorksheet ws, int row, int column)
        {
            return GetAddress(ws.Cells[row, column]);
        }
        public static string GetAddress(ExcelWorksheet ws, int row1, int col1, int row2, int col2)
        {
            return GetAddress(GetRange(ws, row1, col1, row2, col2));
        }
        public static int[] GetCoordinates(ExcelRange rng)
        {
            return new[] { rng.Rows, rng.Columns };
        }
        public static int[] GetCoordinates(ExcelWorksheet ws, string address)
        {
            return GetCoordinates(ws.Cells[address]);
        }
        #endregion Address

        #region Color

        public static void SetColor(ExcelRange rng, string color)
        {
            rng.Style.Font.Color.SetColor(ColorTranslator.FromHtml(color));
        }

        public static void SetColor(ExcelWorksheet ws, int row, int column, string color)
        {
            var rng = ws.Cells[row, column];
            SetColor(rng, color);
        }
        #endregion

        #region Background color
        public static void SetBackgroundColor(ExcelRange rng, string color)
        {
            rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            rng.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(color));
        }
        public static void SetBackgroundColor(ExcelWorksheet ws, int row, int column, string color)
        {
            SetBackgroundColor(ws, ws.Cells[row, column].Address, color);
        }
        public static void SetBackgroundColor(ExcelWorksheet ws, string address, string color)
        {
            SetBackgroundColor(ws.Cells[address], color);
        }
        public static void SetBackgroundColor(ExcelWorksheet ws, int row1, int col1, int row2, int col2, string color)
        {
            var rng = GetRange(ws, row1, col1, row2, col2);
            rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            rng.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(color));
        }
        #endregion

        #region Bold
        public static void SetBold(ExcelWorksheet ws, ExcelRange rng, bool bold)
        {
            rng.Style.Font.Bold = bold;
        }
        public static void SetBold(ExcelWorksheet ws, int row, int column, bool bold)
        {
            SetBold(ws, GetAddress(ws, row, column), bold);
        }

        public static void SetBold(ExcelWorksheet ws, int row1, int col1, int row2, int col2, bool bold)
        {
            SetBold(ws, GetRange(ws, row1, col1, row2, col2), bold);
        }
        public static void SetBold(ExcelWorksheet ws, string address, bool bold)
        {
            ws.Cells[address].Style.Font.Bold = bold;
        }
        #endregion

        #region horizontal aligniament
        public static void SetAligniament(ExcelWorksheet ws, ExcelRange rng, OfficeOpenXml.Style.ExcelHorizontalAlignment aligniament)
        {
            rng.Style.HorizontalAlignment = aligniament;
        }
        public static void SetAligniament(ExcelWorksheet ws, string address, OfficeOpenXml.Style.ExcelHorizontalAlignment aligniament)
        {
            ws.Cells[address].Style.HorizontalAlignment = aligniament;
        }

        public static void SetAligniament(ExcelWorksheet ws, int row, int column, OfficeOpenXml.Style.ExcelHorizontalAlignment aligniament)
        {
            ws.Cells[GetAddress(ws, row, column)].Style.HorizontalAlignment = aligniament;
        }

        #endregion

        public static int GetRowsCount(ExcelWorksheet ws)
        {
            return ws.Dimension.End.Row;
        }

        public static int GetColumnsCount(ExcelWorksheet ws)
        {
            return ws.Dimension.End.Column + 1;
        }

        public void SaveAs(string fileName)
        {
            var fileDirectory = Path.GetDirectoryName(fileName);
            if (fileDirectory != null && !Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);
            else if (File.Exists(fileName))
                File.Delete(fileName);
            _excelApplication.SaveAs(new FileInfo(fileName));
        }

        public static string GetRangeAddress(ExcelWorksheet ws, int row1, int col1, int row2, int col2)
        {
            return $"{GetAddress(ws, row1, col1)}:{GetAddress(ws, row2, col2)}";
        }

        public static ExcelRange GetRange(ExcelWorksheet ws, int row1, int col1, int row2, int col2)
        {
            return ws.Cells[GetRangeAddress(ws, row1, col1, row2, col2)];
        }

        public static string GetColumnLetter(ExcelWorksheet ws, int row, int col)
        {
            var address = ws.Cells[row, col].Address;
            var arr = address.TakeWhile(c => !Char.IsDigit(c));
            return new string(arr.ToArray());
        }

        public static void SetColumnSize(ExcelWorksheet ws, int column, double size)
        {
            ws.Column(column).Width = size;
        }

        public static void AutoResizeColumn(ExcelWorksheet ws, int column)
        {
            ws.Column(column).AutoFit();
        }

        public static List<ExcelDTO> GetData(string path)
        {
            var result = new List<ExcelDTO>();
            var pck = new OfficeOpenXml.ExcelPackage();
            pck.Load(File.OpenRead(path));
            var ws = pck.Workbook.Worksheets.First();

            bool hasHeader = true;

            var ExcelDTOType = typeof(ExcelDTO);
            var excelDtoProps = ExcelDTOType.GetProperties();
            var fields = new List<string>();

            foreach (var prop in excelDtoProps)
            {
                var groupAttribute = (ExcelFieldAttribute)prop.GetCustomAttribute(typeof(ExcelFieldAttribute));
                fields.Add(groupAttribute.FieldName);
            }

            var startRow = hasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var item = new ExcelDTO()
                {
                    PosNr = ws.Cells[rowNum, 1].Text,
                    Group = ws.Cells[rowNum, 2].Text,
                    KurzUndLangtext = ws.Cells[rowNum, 3].Text,
                    Menge = ws.Cells[rowNum, 4].Text,
                    Enih = ws.Cells[rowNum, 5].Text,
                    LEPiBlue = ws.Cells[rowNum, 6].Text
                };
                result.Add(item);
            }
            pck.Dispose();
            return result;
        }

        public static List<LvDto> GetLvData(string path)
        {
            var result = new List<LvDto>();
            var pck = new OfficeOpenXml.ExcelPackage();
            using (var file = File.OpenRead(path))
            {
                pck.Load(file);
            }
            var ws = pck.Workbook.Worksheets.First();

            bool hasHeader = true;

            //var ExcelDTOType = typeof(LvDto);
            //var excelDtoProps = ExcelDTOType.GetProperties();
            //var fields = new List<string>();

            //foreach (var prop in excelDtoProps)
            //{
            //    var groupAttribute = (ExcelFieldAttribute)prop.GetCustomAttribute(typeof(ExcelFieldAttribute));
            //    fields.Add(groupAttribute.FieldName);
            //}

            var startRow = hasHeader ? 2 : 1;
            for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                var item = new LvDto()
                {
                    PosNr = ws.Cells[rowNum, 1].Text.Trim(),
                    Gruppe = ws.Cells[rowNum, 2].Text.Trim(),
                    Bezeichnung = ws.Cells[rowNum, 3].Text.Trim(),
                    Menge = ws.Cells[rowNum, 4].Text.Trim(),
                    Enih = ws.Cells[rowNum, 5].Text.Trim(),
                    Ep = ws.Cells[rowNum, 6].Text.Trim(),
                    Gp = ws.Cells[rowNum, 7].Text.Trim()
                };
                result.Add(item);
            }
            pck.Dispose();
            return result;
        }
    }
}
