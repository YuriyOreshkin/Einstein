using NPOI.POIFS.NIO;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public class NPOIExcel : IExcel
    {
        private string template_path;
        public NPOIExcel(string _template_path) 
        {
            template_path = _template_path;
        }
        public void Export(string filename, string template, List<Sheet> sheets)
        {
            template =Path.Combine(template_path, template);
            if (File.Exists(template))
            {
                XSSFWorkbook workbook;

                using (FileStream file = new FileStream(template, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }
                for (int i = 0; i < sheets.Count; i++)
                {
                    ISheet Sheet = workbook.GetSheetAt(i);
                    ExportSheet(Sheet, sheets[i]);
                }
                //Write Excel to disc
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                using (var file = new FileStream(filename, FileMode.Create))
                {
                    workbook.Write(file);
                }
            }
            else
            {
                throw new Exception(String.Format("Файл шаблона {0} не найден!",template));
            }
        }

        private void ExportSheet(ISheet sheet, Sheet data) 
        {
            int StartRowIndex = data.startRow;
          
            foreach (var row in data.rows)
            {
                IRow CurrentRow = sheet.CreateRow(StartRowIndex);
                int CellIndex = 0;

                foreach (var cell in row)
                {
                    CreateCell(CurrentRow, CellIndex, cell.Value);
                    CellIndex++;
                }

                StartRowIndex++;
            }

        }

        private void CreateCell(IRow CurrentRow, int CellIndex, object value)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);


            if (value != null)
            {
                if (value.GetType().Equals(typeof(string)))
                {
                    Cell.SetCellType(CellType.String);
                    Cell.SetCellValue(value.ToString());

                }
                else if (value.GetType().Equals(typeof(decimal)) || value.GetType().Equals(typeof(int)) || value.GetType().Equals(typeof(short)) || value.GetType().Equals(typeof(long)) || value.GetType().Equals(typeof(short?)))
                {

                    Cell.SetCellType(CellType.Numeric);
                    //if (value.GetType().Equals(typeof(decimal)))
                    //{
                    //    Cell.CellStyle.DataFormat = CurrentRow.Sheet.Workbook.CreateDataFormat().GetFormat("#,##0.00");
                    //   // Cell.CellStyle = numStyle;
                    //}
                    Cell.SetCellValue(Double.Parse(value.ToString()));

                }
                else if (value.GetType().Equals(typeof(DateTime)))
                {
                    if (((DateTime)value).Hour == 0 && ((DateTime)value).Minute == 0 && ((DateTime)value).Second == 0)
                    {
                        Cell.SetCellValue(((DateTime)value).ToString("dd.MM.yyyy"));
                    }
                    else
                    {
                        Cell.SetCellValue(((DateTime)value).ToString("dd.MM.yyyy HH:mm"));
                    }
                }
                else
                {
                    Cell.SetCellValue(value.ToString());
                }
            }

        }


    }

    public class Sheet
    {
        public int startRow { get; set; }
        public List<Dictionary<int, object>> rows { get; set; }
    }
}
