using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace SreeVisalamChitFundLtd_phase1
{
    public partial class FrmYearEndingBooklet : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                Excel.Range chartRange;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
               // xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            }
            catch (Exception err) { }
        }
    }
}