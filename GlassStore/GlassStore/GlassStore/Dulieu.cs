using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
namespace GlassStore
{
    class Dulieu
    {
        public static DataTable zDataAll = null;
        public static DataTable zdatasearch = null;
  

        public static void InitDataTable()
        {
            zDataAll = new DataTable();

            DataColumn zStt = new DataColumn("No");
            zDataAll.Columns.Add(zStt);

            DataColumn zBarcode = new DataColumn("Barcode");
            zDataAll.Columns.Add(zBarcode);

            DataColumn zPos = new DataColumn("Pos");
            zDataAll.Columns.Add(zPos);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "D:\\glass24.5.2020\\Book2.xlsx" + ";Extended Properties=Excel 12.0;";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [Sheet1$]", conn);

                myDataAdapter.Fill(zDataAll);

            }


        }        


        public static void Openfilesearch(string NameofFileExcel,string Sheet)
        {
            zdatasearch = new DataTable();

            DataColumn zStt = new DataColumn("No");
            zdatasearch.Columns.Add(zStt);

            DataColumn zBarcode = new DataColumn("Barcode");
            zdatasearch.Columns.Add(zBarcode);

            DataColumn zPos = new DataColumn("Pos");
            zdatasearch.Columns.Add(zPos);

            string PathConn1 = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + NameofFileExcel + " ;Extended Properties=\"Excel 12.0;HDR=YES;\";";
            using (OleDbConnection conn = new OleDbConnection(PathConn1))
            {
                conn.Open();
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [" + Sheet + "$]", conn);

                myDataAdapter.Fill(zdatasearch);
                
            }
        }

       


       
    }
}
