using Framework.Config;
using Npoi.Mapper;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;

namespace Framework.Utils
{
    public class ExcelDataHelper
    {
        public static List<Ttype> ReadExcel<Ttype>(string filePath, string sheetName) where Ttype : class
        {
            var localPath = FW.BaseDir + filePath;
            IWorkbook workbook;
            using (var file = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(file);
            }

            var importer = new Mapper(workbook);
            var items = importer.Take<Ttype>(sheetName);
            var list = new List<Ttype>();
            foreach (var item in items)
            {
                var row = item.Value;
                if (row != null)
                {
                    list.Add(row);
                }
            }
            return list;
        }

    }
}
