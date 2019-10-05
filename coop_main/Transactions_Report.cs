﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
namespace coop_main
{
    public partial class Transactions_Report : Form
    {
        List<String> custId = new List<String>();
        DatabaseStuff dbhandler = new DatabaseStuff();
        String customerId;
        String empID = "1";
        int what = 1;

        DataTable t;
        public Transactions_Report(String empID)
        {
            InitializeComponent();
            this.empID = empID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportsForm f = new ReportsForm(empID);
            f.Show();
            this.Close();
        }

        private void Transactions_Report_Load(object sender, EventArgs e)
        {
            label6.Text = Properties.Settings.Default.username.ToUpper();
            //if (empID.Trim() == "" || empID == "0")
            //{
            empID = Properties.Settings.Default.userid;
            //}


            txt_search.GotFocus += txt_search_GotFocus;
            txt_search.LostFocus += txt_search_LostFocus;
        }

        private void Transactions_Report_Shown(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                button1.PerformClick();
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }



        #region misc
        void txt_search_LostFocus(object sender, EventArgs e)
        {
            if (txt_search.Text.Trim() == "")
            {
                txt_search.Text = "Type Keyword to Search";
            }
        }

        void txt_search_GotFocus(object sender, EventArgs e)
        {
           // if (txt_search.Text.Trim() == "Type Keyword to Search")
           // {
                txt_search.Text = "";
          //  }
        }



        private void selectMember()
        {
            if (listBox1.Items.Count > 0)
            {

                txt_search.Text = listBox1.SelectedItem.ToString();

                if (listBox1.SelectedIndex == 0)
                {
                    customerId = custId[0].ToString();
                }
                else if (listBox1.SelectedIndex > 0)
                {
                    customerId = custId[listBox1.SelectedIndex].ToString();
                }

                btn_search.PerformClick();
            }

        }

        #endregion

        private void txt_search_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
            {
                if (txt_search.Text.Trim() != "")
                {

                    dbhandler.searchMemberEmployee(txt_search.Text.Replace("'", "`"), listBox1, custId);
                }
                else
                {
                    listBox1.Visible = false;

                }
            }
        }

        private void txt_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (listBox1.Items.Count > 0)
                {
                    selectMember();
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (listBox1.Items.Count > 0 && (listBox1.SelectedIndex + 1) < listBox1.Items.Count)
                {

                    listBox1.SelectedIndex = listBox1.SelectedIndex + 1;

                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (listBox1.Items.Count > 0 && (listBox1.SelectedIndex + 1) > 1)
                {

                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;

                }
            }
        }

        private void ch_all_CheckedChanged(object sender, EventArgs e)
        {
            if (!ch_all.Checked)
            {
                ch_startDate.Checked = true;
                ch_endDate.Checked = true;
            }
            else
            {
                ch_startDate.Checked = false;
                ch_endDate.Checked = false;
            }

            refreshDataGrid();
        }


        private void refreshDataGrid()
        {
            if (what == 1)
            {
                 button1.PerformClick();
            }
            else if (what == 2)
            {
                button3.PerformClick();
            }
            else if (what == 3)
            {
                 button4.PerformClick();

            }
            else if (what == 4)
            {
                 button5.PerformClick();
            }
            else if (what == 5)
            {
                 button6.PerformClick();
            }
            else if (what == 6)
            {
                 button7.PerformClick();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        #region printables
        private void populateprintAllTransaction()
        {
            t = new DataTable();
            t.TableName = "ds_allTransaction";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("type");
            t.Columns.Add("amt");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAll.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allTransaction", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }
        private void populateprintAllTransactionPrMember()
        {
            t = new DataTable();
            t.TableName = "ds_allTransaction";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("type");
            t.Columns.Add("amt");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAllPerMember.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allTransaction", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }

        private void populateprintAllLoanTransactionr()
        {
            t = new DataTable();
            t.TableName = "ds_allTransaction";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("type");
            t.Columns.Add("amt");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAllPerLoans.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allTransaction", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }


        private void populateprintAllLoanTransactionrPerMem()
        {
            t = new DataTable();
            t.TableName = "ds_allTransaction";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("type");
            t.Columns.Add("amt");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAllPerLoansPerMem.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allTransaction", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }

        private void populateprintAllPurchasesTransactionrPerMem()
        {
            t = new DataTable();
            t.TableName = "ds_allpurchOfMem";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("no_item");
            t.Columns.Add("amt");
            t.Columns.Add("proc_by");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                dtrw[5] = dataGridView1.Rows[rw.Index].Cells[5].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAllPurchases.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allpurchOfMem", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }


        private void populateprintAllPurchasesTransactionrPerMem2()
        {
            t = new DataTable();
            t.TableName = "ds_allpurchOfMem";
            t.Columns.Add("no");
            t.Columns.Add("date");
            t.Columns.Add("no_item");
            t.Columns.Add("amt");
            t.Columns.Add("proc_by");
            t.Columns.Add("details");
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                DataRow dtrw = t.NewRow();
                dtrw[0] = dataGridView1.Rows[rw.Index].Cells[0].FormattedValue.ToString();
                dtrw[1] = dataGridView1.Rows[rw.Index].Cells[1].FormattedValue.ToString();
                dtrw[2] = dataGridView1.Rows[rw.Index].Cells[2].FormattedValue.ToString();
                dtrw[3] = dataGridView1.Rows[rw.Index].Cells[3].FormattedValue.ToString();
                dtrw[4] = dataGridView1.Rows[rw.Index].Cells[4].FormattedValue.ToString();
                dtrw[5] = dataGridView1.Rows[rw.Index].Cells[5].FormattedValue.ToString();
                t.Rows.Add(dtrw);
            }
            reportViewer1.LocalReport.ReportPath = @"ReportTransAllPurchasesPerMem.rdlc";
            ReportDataSource rds = new ReportDataSource("ds_allpurchOfMem", t);
            reportViewer1.LocalReport.DataSources.Clear(); // Clear the Previous DataSource of ReportViewer
            reportViewer1.LocalReport.DataSources.Add(rds); //bind ReportViewer1 to the new datasource(Which you wish)
            reportViewer1.LocalReport.Refresh(); // Refresh the ReportViewer Control, ReportViewer1 in this case
            reportViewer1.RefreshReport();

        }
        #endregion

        private void printNow() {
            if (what == 1)
            {
                populateprintAllTransaction();
            }
            else if (what == 2)
            {
                populateprintAllTransactionPrMember();
            }
            else if (what == 3)
            {
                populateprintAllLoanTransactionr();

            }
            else if (what == 4)
            {
                populateprintAllLoanTransactionrPerMem();
            }
            else if (what == 5)
            {
                populateprintAllPurchasesTransactionrPerMem();
  
            }
            else if (what == 6)
            {
                populateprintAllPurchasesTransactionrPerMem2();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //all trans
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                what = 1;
                listBox1.Visible = false;
                if (!ch_all.Checked)
                {
                    dbhandler.getALLTransactions(dataGridView1, "0", true, dateTimePicker1.Value, dateTimePicker2.Value);
                }
                else if (ch_all.Checked)
                {
                    dbhandler.getALLTransactions(dataGridView1, "0", false, dateTimePicker1.Value, dateTimePicker2.Value);
                }

                customerId = "";
            }
            catch (Exception xx)
            {
                MessageBox.Show(xx.ToString());
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //all trans per mem
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                txt_search.Text = "Type Member's Name to Search";
                what = 2;
                listBox1.Visible = false;
                if (!ch_all.Checked)
                {
                    dbhandler.getALLTransactions(dataGridView1, customerId, true, dateTimePicker1.Value, dateTimePicker2.Value);
                }
                else if (ch_all.Checked)
                {
                    dbhandler.getALLTransactions(dataGridView1, customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
                }
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }

           

            toolTip1.ToolTipTitle = "Important Tip!";
            toolTip1.Show("Use the Searchbar to Search to view \n trasanctions of a member.", txt_search, 3000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // all loans
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                what = 3;
                listBox1.Visible = false;
                if (!ch_all.Checked)
                {
                    dbhandler.getLoanMoneyOFMember(dataGridView1, "0", true, dateTimePicker1.Value, dateTimePicker2.Value);

                }
                else if (ch_all.Checked)
                {
                    dbhandler.getLoanMoneyOFMember(dataGridView1, "0", false, dateTimePicker1.Value, dateTimePicker2.Value);

                }
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
  
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txt_search.Text = "Type Member's Name to Search";
           
            // all loans per mem
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                what = 4;
                listBox1.Visible = false;
                if (!ch_all.Checked)
                {
                    dbhandler.getLoanMoneyOFMember(dataGridView1, customerId, true, dateTimePicker1.Value, dateTimePicker2.Value);
                }
                else if (ch_all.Checked)
                {
                    dbhandler.getLoanMoneyOFMember(dataGridView1, customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
                }
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }

            toolTip1.ToolTipTitle = "Important Tip!";
            toolTip1.Show("Use the Searchbar to Search to view \n All Loans of a member.", txt_search, 0);
            toolTip1.Show("Type the Member Name in Searchbar\n To view all loans of the member.", txt_search, 3000);


        }

        private void button6_Click(object sender, EventArgs e)
        {
            // all purch
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                what = 5;
                listBox1.Visible = false;
                if (!ch_all.Checked)
                {
                    dbhandler.getALLPurchaseOfMember(dataGridView1, "0", true, dateTimePicker1.Value, dateTimePicker2.Value);

                }
                else if (ch_all.Checked)
                {
                    dbhandler.getALLPurchaseOfMember(dataGridView1, "0", false, dateTimePicker1.Value, dateTimePicker2.Value);

                }
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
    
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txt_search.Text = "Type Member's Name to Search";
          
            // all purch per mem
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();

                what = 6;
                listBox1.Visible = false;
                dbhandler.getALLPurchaseOfMember(dataGridView1,customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
                if (!ch_all.Checked)
                {
                    dataGridView1.Columns.Clear(); //quick and dirty fix
                    dataGridView1.Columns.Add("c13", "OR No.");
                    dataGridView1.Columns.Add("c2", "Date");
                    dataGridView1.Columns.Add("c3", "No. of Items");
                    dataGridView1.Columns.Add("c4", "Amount");
                    dataGridView1.Columns.Add("c5", "Processed by");
                    dataGridView1.Columns.Add("c66", "Details");

                }
                else if (ch_all.Checked)
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("c13", "OR No.");
                    dataGridView1.Columns.Add("c2", "Date");
                    dataGridView1.Columns.Add("c3", "No. of Items");
                    dataGridView1.Columns.Add("c4", "Amount");
                    dataGridView1.Columns.Add("c5", "Processed by");
                    dataGridView1.Columns.Add("c66", "Details");

                }
            }
            catch (Exception)
            {
                message mes = new message("ERROR: Cannot connect to the server\n Pls. Check your network connection");
                mes.ShowDialog(mes.Parent);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
 
            

            toolTip1.ToolTipTitle = "Important Tip!";
            toolTip1.Show("Use the Searchbar to Search to view \n All Loans of a member.", txt_search, 0);
            toolTip1.Show("Type the Member Name in Searchbar\n To view all Purchases of the member.", txt_search, 3000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var a = DateTime.Now;
            label5.Text = "Today is " + a.ToString("MM-dd-yyyy") + " " + a.ToLongTimeString();
        }

        private void btn_ePDF_Click(object sender, EventArgs e)
        {
        
            printmessage ms = new printmessage();
            DialogResult res = ms.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                try {
                    this.UseWaitCursor = true;
                    Cursor.Current = Cursors.WaitCursor;
                    Application.DoEvents();
                    printNow();
                }
                catch { }
                finally {
                    this.UseWaitCursor = false;
                }
              
                OpenFileDialog ds = new OpenFileDialog();
                SaveFileDialog s = new SaveFileDialog();

                if (what == 1)
                {
                    s.FileName = "All Transaction Report" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                else if (what == 2)
                {
                    s.FileName = "All Transaction Per Member Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                else if (what == 3)
                {
                    s.FileName = "All Loans Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                else if (what == 4)
                {
                    s.FileName = "All Loans Per Member Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                else if (what == 5)
                {
                    s.FileName = "All Purchases Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                else if (what == 6)
                {
                    s.FileName = "All Purchases Per Member Report" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
                }
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);


                s.ShowDialog();
                using (System.IO.FileStream fs = new System.IO.FileStream(s.FileName, System.IO.FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);

                }

            }
        }

        private void btn_eExcel_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                printNow();
            }
            catch { }
            finally
            {
                this.UseWaitCursor = false;
            }


            OpenFileDialog ds = new OpenFileDialog();
            SaveFileDialog s = new SaveFileDialog();

            if (what == 1)
            {
                s.FileName = "All Transaction Report" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (what == 2)
            {
                s.FileName = "All Transaction Per Member Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (what == 3)
            {
                s.FileName = "All Loans Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (what == 4)
            {
                s.FileName = "All Loans Per Member Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (what == 5)
            {
                s.FileName = "All Purchases Report " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (what == 6)
            {
                s.FileName = "All Purchases Per Member Report" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = reportViewer1.LocalReport.Render(
                "EXCEL", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);


            s.ShowDialog();
            using (System.IO.FileStream fs = new System.IO.FileStream(s.FileName, System.IO.FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);

            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                printNow();
            }
            catch { }
            finally
            {
                this.UseWaitCursor = false;
            }
            printmessageR print = new printmessageR();
            DialogResult res = print.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                PrinterSettings set = new PrinterSettings();
                set.DefaultPageSettings.Landscape = true;
                set.DefaultPageSettings.Margins = new Margins(25, 25, 25, 25);
                set.DefaultPageSettings.PrinterSettings.Copies = 1;
                reportViewer1.PrintDialog(set);
            }
            else
            {

            }
        }

        private void ch_startDate_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_startDate.Checked){
                ch_endDate.Checked = true;
                ch_all.Checked = false;
            } else{
                ch_all.Checked = true;
                ch_endDate.Checked = false;
            }
            refreshDataGrid();
        }

        private void ch_endDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_endDate.Checked)
            {
                ch_startDate.Checked = true;
                ch_all.Checked = false;
            }
            else
            {
                ch_all.Checked = true;
                ch_startDate.Checked = false;
            }
            refreshDataGrid();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            logout logout = new logout();

            DialogResult res = logout.ShowDialog(this);

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                login log = new login();
                log.Show();
                this.Close();

            }
        }


        //
        #region toolstrip
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1 fo = new Form1(empID);
            fo.Show();
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            logout logout = new logout();

            DialogResult res = logout.ShowDialog(this);

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                login log = new login();
                log.Show();
                this.Close();

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            logout logout = new logout();
            DialogResult res = logout.ShowDialog(this);
            if (res == System.Windows.Forms.DialogResult.OK)
            {

                this.Close();
                Application.Exit();

            }
        }

        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Globals.arrayModule[1] == "1")
            {
                members_reg f1;
                f1 = new members_reg(empID);
                f1.Show();

                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void memberRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[2] == "1")
            {
                members_record memrecord;
                memrecord = new members_record(empID, "1");
                memrecord.Show();

                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void openRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[4] == "1")
            {
                open_register f1 = new open_register(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void savedSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[5] == "1")
            {
                saved_sales f1 = new saved_sales(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void refundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[6] == "1")
            {
                refunds f1 = new refunds(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void endOfTheDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[7] == "1")
            {
                end_of_the_day day = new end_of_the_day(empID);
                day.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void endOfTheWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[8] == "1")
            {
                end_of_the_week f1 = new end_of_the_week(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[9] == "1")
            {
                transaction_history f1 = new transaction_history(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void enterItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[11] == "1")
            {
                enter_items items;
                items = new enter_items(empID);
                items.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[12] == "1")
            {
                stocks st;
                st = new stocks(empID);
                st.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void stockRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[13] == "1")
            {
                stock_request f1 = new stock_request(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void stockReceiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[14] == "1")
            {
                stock_receiving f1 = new stock_receiving(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[15] == "1")
            {
                stock_return st_r = new stock_return(empID);
                st_r.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void itemListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[16] == "1")
            {
                Itemlist list;
                list = new Itemlist(empID);
                list.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void inventoryReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[17] == "1")
            {
                inventory_reports f = new inventory_reports(empID);
                f.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void moneyTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[19] == "1")
            {
                loans l = new loans(empID);
                l.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void depositMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[20] == "1")
            {
                initial_deposit f1 = new initial_deposit(empID);
                DialogResult res = f1.ShowDialog(this);
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void withdrawMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            withdraw_money money = new withdraw_money(empID);
            money.ShowDialog(this);
        }

        private void loanMoneyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[21] == "1")
            {
                loan_money f1 = new loan_money(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }


        private void loanPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[22] == "1")
            {
                loan_payment f1 = new loan_payment(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }


        private void moneyCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[23] == "1")
            {
                money_counter count = new money_counter(empID);
                count.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }


        private void manageMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[25] == "1")
            {
                members_record m = new members_record(empID, "2");
                m.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void manageProductInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[26] == "1")
            {
                inventory_products pr = new inventory_products(empID, "2");
                pr.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void manageSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[27] == "1")
            {
                Sales_Report rep = new Sales_Report(empID);
                rep.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void managePurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[28] == "1")
            {
                Purchases_Report rep = new Purchases_Report(empID);
                rep.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void manageLoansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[29] == "1")
            {
                Loans_Report reo = new Loans_Report(empID);
                reo.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void manageTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[30] == "1")
            {
                Transactions_Report re = new Transactions_Report(empID);
                re.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void manageEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[32] == "1")
            {
                List_Reports r = new List_Reports(empID);
                r.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void userAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[35] == "1")
            {
                user_accounts f1 = new user_accounts(empID);
                f1.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void auditTrailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[36] == "1")
            {
                audit_trail ad = new audit_trail(empID);
                ad.Show();
                this.Close();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void loanCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[37] == "1")
            {
                loan_calculator s = new loan_calculator();
                s.ShowDialog();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {
            if (Globals.arrayModule[37] == "1")
            {
                loan_calculator s = new loan_calculator();
                s.ShowDialog();
            }
            else
            {
                message mes = new message("You don't have sufficient rights to access this module.");
                mes.ShowDialog(this);
            }
        }

        #endregion

      

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (what == 1 || what == 2) {
                dbhandler.getALLTransactions(dataGridView1, customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
            }
            else if (what == 3 || what == 4) {
                dbhandler.getLoanMoneyOFMember(dataGridView1, customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
            }
            else if(what == 5 || what == 6){
                dbhandler.getALLPurchaseOfMember(dataGridView1, customerId, false, dateTimePicker1.Value, dateTimePicker2.Value);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                selectMember();
            }
            listBox1.Visible = false;

           
        }

      

    










    }
}
