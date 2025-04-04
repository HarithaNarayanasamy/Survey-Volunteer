using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DevExpress.Web.ASPxGridView;
using SVCF_BusinessAccessLayer;
using SVCF_TransactionLayer;
using SVCF_DataAccessLayer;
namespace SreeVisalamChitFundLtd_phase1
{
    public partial class _DashBoard : System.Web.UI.Page
    {
        #region Object
        BusinessLayer balayer = new BusinessLayer();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtFromDate.Text =  DateTime.Now.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = "31/03/2009";
                txtToDate.Text = "31/05/2013";
                select();
                grid.DataBind();
                setStats(balayer.indiandateToMysqlDate(txtFromDate.Text), balayer.indiandateToMysqlDate(txtToDate.Text));
                lblledgerADVANCES.Text = "Advances on: " + txtToDate.Text;
                lblledgerBANKS.Text = "Banks on: " + txtToDate.Text;
                lblledgerBRANCHES.Text = "Branches on: " + txtToDate.Text;
                lblledgerCASH.Text = "Cash on: " + txtToDate.Text;
                lblledgerCHITS.Text = "Chits on: " + txtToDate.Text;
                lblledgerDECREEDEBTORS.Text = "Decree Debtors on: " + txtToDate.Text;
                lblledgerFOREMANCHITS.Text = "Foreman Chits on: " + txtToDate.Text;
                lblledgerINVESTMENTS.Text = "Investments on: " + txtToDate.Text;
                lblledgerLOANS.Text = "Loans on: " + txtToDate.Text;
                lblledgerOTHERITEMS.Text = "Other Items on: " + txtToDate.Text;
                lblledgerPROFITANDLOSS.Text = "Profit and Loss on: " + txtToDate.Text;
                lblledgerSTAMPS.Text = "Stamps on: " + txtToDate.Text;
            }
            else
            {
                lblledgerADVANCES.Text = "Advances on: " + txtToDate.Text;
                lblledgerBANKS.Text = "Banks on: " + txtToDate.Text;
                lblledgerBRANCHES.Text = "Branches on: " + txtToDate.Text;
                lblledgerCASH.Text = "Cash on: " + txtToDate.Text;
                lblledgerCHITS.Text = "Chits on: " + txtToDate.Text;
                lblledgerDECREEDEBTORS.Text = "Decree Debtors on: " + txtToDate.Text;
                lblledgerFOREMANCHITS.Text = "Foreman Chits on: " + txtToDate.Text;
                lblledgerINVESTMENTS.Text = "Investments on: " + txtToDate.Text;
                lblledgerLOANS.Text = "Loans on: " + txtToDate.Text;
                lblledgerOTHERITEMS.Text = "Other Items on: " + txtToDate.Text;
                lblledgerPROFITANDLOSS.Text = "Profit and Loss on: " + txtToDate.Text;
                lblledgerSTAMPS.Text = "Stamps on: " + txtToDate.Text;
            }
        }

        protected void BtnStatisticsGo_Click(object sender, EventArgs e)
        {
            setStats(balayer.indiandateToMysqlDate(txtFromDate.Text), balayer.indiandateToMysqlDate(txtToDate.Text));
            select();
            grid.DataBind();
        }

        protected void select()
        {
            //AccessDataSource1.SelectCommand = @"SELECT voucher.RootID,`headstree`.`Node`,sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) where voucher.BranchID=" +balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by voucher.RootID ";
            AccessDataSource1.SelectCommand = @"SELECT voucher.RootID,`headstree`.`Node`,sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) as `Credit`,sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) as `Debit`,(case when (sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end ))>sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end ))) then sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end ))-sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end )) else 0.00 end ) as `Credit Balance`,(case when (sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end ))>sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end ))) then sum((case when voucher.Voucher_Type='D' then voucher.Amount else 0.00 end ))-sum((case when voucher.Voucher_Type='C' then voucher.Amount else 0.00 end )) else 0.00 end ) as `Debit Balance` FROM `svcf`.`voucher` join headstree on (voucher.RootID=headstree.NodeID) where voucher.BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and voucher.ChoosenDate between '" + balayer.indiandateToMysqlDate(txtFromDate.Text) + "' and '" + balayer.indiandateToMysqlDate(txtToDate.Text) + "' group by voucher.RootID";
        }

        protected void ResetStats()
        {

            lblBranchCr.Text = "Cr. " + "0";
            lblBranchDr.Text = "Dr. " + "0";
            lblledgerBRANCHESCRBDRB.Text = "0.00";
            lblledgerBRANCHESCRBDRB.CssClass = "down";
            //Branch

            lblCHITSCr.Text = "Cr. " + "0";
            lblCHITSDr.Text = "Dr. " + "0";
            lblledgerCHITSCRBDRB.Text = "0.00";
            lblledgerCHITSCRBDRB.CssClass = "down";
            //Chit

            lblBankCr.Text = "Cr. " + "0";
            lblBankDr.Text = "Dr. " + "0";
            lblledgerBANKSCRBDRB.Text = "0.00";
            lblledgerBANKSCRBDRB.CssClass = "down";
            //Bank

            lblInvestMentsCr.Text = "Cr. " + "0";
            lblInvestMentsDr.Text = "Dr. " + "0";
            lblledgerINVESTMENTSCRBDRB.Text = "0.00";
            lblledgerINVESTMENTSCRBDRB.CssClass = "down";
            //Investments

            lblOTHERITEMScr.Text = "Cr. " + "0";
            lblOTHERITEMSdr.Text = "Dr. " + "0";
            lblledgerOTHERITEMSCRBDRB.Text = "0.00";
            lblledgerOTHERITEMSCRBDRB.CssClass = "down";
            //Otheritems

            lblFOREMANCHITSCr.Text = "Cr. " + "0";
            lblFOREMANCHITSDr.Text = "Dr. " + "0";
            lblledgerFOREMANCHITSCRBDRB.Text = "0.00";
            lblledgerFOREMANCHITSCRBDRB.CssClass = "down";
            //Foremanchits

            lblDECREEDEBTORSCr.Text = "Cr. " + "0";
            lblDECREEDEBTORSDr.Text = "Dr. " + "0";
            lblledgerDECREEDEBTORSCRBDRB.Text = "0.00";
            lblledgerDECREEDEBTORSCRBDRB.CssClass = "down";
            //Decreedebtors

            lblLoanCr.Text = "Cr. " + "0";
            lblLoanDr.Text = "Dr. " + "0";
            lblledgerLOANSCRBDRB.Text = "0.00";
            lblledgerLOANSCRBDRB.CssClass = "down";
            //Loans

            lblSUNDRIESANDADVANCEScr.Text = "Cr. " + "0";
            lblSUNDRIESANDADVANCESDr.Text = "Dr. " + "0";
            lblledgerADVANCESCRBDRB.Text = "0.00";
            lblledgerADVANCESCRBDRB.CssClass = "down";
            //SundriesAndAdvances

            lblSTAMPSAndSTAMPPAPERSCr.Text = "Cr. " + "0";
            lblSTAMPSAndSTAMPPAPERSDr.Text = "Dr. " + "0";
            lblledgerSTAMPSCRBDRB.Text = "0.00";
            lblledgerSTAMPSCRBDRB.CssClass = "down";
            //StampsAndStampPapers

            lblPROFITANDLOSSACCOUNTCr.Text = "Cr. " + "0";
            lblPROFITANDLOSSACCOUNTDr.Text = "Dr. " + "0";
            lblledgerPROFITANDLOSSCRBDRB.Text = "0.00";
            lblledgerPROFITANDLOSSCRBDRB.CssClass = "down";
            //ProfitAndLoss

            lblCashCr.Text = "Cr. " + "0";
            lblCashDr.Text = "Dr. " + "0";
            lblledgerCASHCRBDRB.Text = "0.00";
            lblledgerCASHCRBDRB.CssClass = "down";
            //Cash
        }

        protected void setStats(string Fromdate, string Todate)
        {
            ResetStats();
            DataTable dtStats = balayer.GetDataTable(" SELECT RootID,sum((case when Voucher_Type='C' then Amount else 0.00 end ))as `Credit Amount`,sum((case when Voucher_Type='D' then Amount else 0.00 end ))as `Debit Amount` FROM `svcf`.`voucher` where BranchID=" + balayer.ToobjectstrEvenNull(Session["Branchid"]) + " and ChoosenDate between '" + Fromdate + "' and '" + Todate + "' group by RootID");

            for (int iR = 0; iR < dtStats.Rows.Count; iR++)
            {
                if (dtStats.Rows[iR][0].ToString() == "1")
                {
                    lblBranchCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblBranchDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");
                    Decimal lbcrBranch = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrBranch = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrBranch) > Convert.ToDecimal(lbdrBranch))
                    {
                        lblledgerBRANCHESCRBDRB.CssClass = "up";
                        lblledgerBRANCHESCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrBranch) - Convert.ToDecimal(lbdrBranch));
                    }
                    else if (Convert.ToDecimal(lbcrBranch) < Convert.ToDecimal(lbdrBranch))
                    {
                        lblledgerBRANCHESCRBDRB.CssClass = "down";
                        lblledgerBRANCHESCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrBranch) - Convert.ToDecimal(lbcrBranch));

                    }
                    else
                    {
                        lblledgerBRANCHESCRBDRB.CssClass = "down";
                        lblledgerBRANCHESCRBDRB.Text = "0.00";
                    }

                    //Branch
                }
                else if (dtStats.Rows[iR][0].ToString() == "5")
                {
                    lblCHITSCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblCHITSDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrChit = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrChit = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrChit) > Convert.ToDecimal(lbdrChit))
                    {
                        lblledgerCHITSCRBDRB.CssClass = "up";
                        lblledgerCHITSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrChit) - Convert.ToDecimal(lbdrChit));
                    }
                    else if (Convert.ToDecimal(lbcrChit) < Convert.ToDecimal(lbdrChit))
                    {
                        lblledgerCHITSCRBDRB.CssClass = "down";
                        lblledgerCHITSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrChit) - Convert.ToDecimal(lbcrChit));

                    }
                    else
                    {
                        lblledgerCHITSCRBDRB.CssClass = "down";
                        lblledgerCHITSCRBDRB.Text = "0.00";
                    }

                    //Chit
                }
                else if (dtStats.Rows[iR][0].ToString() == "3")
                {
                    lblBankCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblBankDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrBank = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrBank = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrBank) > Convert.ToDecimal(lbdrBank))
                    {
                        lblledgerBANKSCRBDRB.CssClass = "down";
                        lblledgerBANKSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrBank) - Convert.ToDecimal(lbdrBank));
                    }
                    else if (Convert.ToDecimal(lbcrBank) < Convert.ToDecimal(lbdrBank))
                    {
                        lblledgerBANKSCRBDRB.CssClass = "up";
                        lblledgerBANKSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrBank) - Convert.ToDecimal(lbcrBank));

                    }
                    else
                    {
                        lblledgerBANKSCRBDRB.CssClass = "down";
                        lblledgerBANKSCRBDRB.Text = "0.00";
                    }

                    //Bank
                }
                else if (dtStats.Rows[iR][0].ToString() == "2")
                {
                    lblInvestMentsCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblInvestMentsDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrInvestment = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrInvestment = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrInvestment) > Convert.ToDecimal(lbdrInvestment))
                    {
                        lblledgerINVESTMENTSCRBDRB.CssClass = "down";
                        lblledgerINVESTMENTSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrInvestment) - Convert.ToDecimal(lbdrInvestment));
                    }
                    else if (Convert.ToDecimal(lbcrInvestment) < Convert.ToDecimal(lbdrInvestment))
                    {
                        lblledgerINVESTMENTSCRBDRB.CssClass = "up";
                        lblledgerINVESTMENTSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrInvestment) - Convert.ToDecimal(lbcrInvestment));

                    }
                    else
                    {
                        lblledgerINVESTMENTSCRBDRB.CssClass = "down";
                        lblledgerINVESTMENTSCRBDRB.Text = "0.00";
                    }

                    //Investments
                }
                else if (dtStats.Rows[iR][0].ToString() == "4")
                {
                    lblOTHERITEMScr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblOTHERITEMSdr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrOtherItems = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrOtherItems = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrOtherItems) > Convert.ToDecimal(lbdrOtherItems))
                    {
                        lblledgerOTHERITEMSCRBDRB.CssClass = "up";
                        lblledgerOTHERITEMSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrOtherItems) - Convert.ToDecimal(lbdrOtherItems));
                    }
                    else if (Convert.ToDecimal(lbcrOtherItems) < Convert.ToDecimal(lbdrOtherItems))
                    {
                        lblledgerOTHERITEMSCRBDRB.CssClass = "down";
                        lblledgerOTHERITEMSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrOtherItems) - Convert.ToDecimal(lbcrOtherItems));

                    }
                    else
                    {
                        lblledgerOTHERITEMSCRBDRB.CssClass = "down";
                        lblledgerOTHERITEMSCRBDRB.Text = "0.00";
                    }

                    //Otheritems
                }
                else if (dtStats.Rows[iR][0].ToString() == "6")
                {
                    lblFOREMANCHITSCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblFOREMANCHITSDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrForemanChits = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrForemanChits = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrForemanChits) > Convert.ToDecimal(lbdrForemanChits))
                    {
                        lblledgerFOREMANCHITSCRBDRB.CssClass = "up";
                        lblledgerFOREMANCHITSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrForemanChits) - Convert.ToDecimal(lbdrForemanChits));
                    }
                    else if (Convert.ToDecimal(lbcrForemanChits) < Convert.ToDecimal(lbdrForemanChits))
                    {
                        lblledgerFOREMANCHITSCRBDRB.CssClass = "down";
                        lblledgerFOREMANCHITSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrForemanChits) - Convert.ToDecimal(lbcrForemanChits));

                    }
                    else
                    {
                        lblledgerFOREMANCHITSCRBDRB.CssClass = "down";
                        lblledgerFOREMANCHITSCRBDRB.Text = "0.00";
                    }


                    //Foremanchits
                }
                else if (dtStats.Rows[iR][0].ToString() == "7")
                {
                    lblDECREEDEBTORSCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblDECREEDEBTORSDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrDecreeDebtors = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrDecreeDebtors = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrDecreeDebtors) > Convert.ToDecimal(lbdrDecreeDebtors))
                    {
                        lblledgerDECREEDEBTORSCRBDRB.CssClass = "up";
                        lblledgerDECREEDEBTORSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrDecreeDebtors) - Convert.ToDecimal(lbdrDecreeDebtors));
                    }
                    else if (Convert.ToDecimal(lbcrDecreeDebtors) < Convert.ToDecimal(lbdrDecreeDebtors))
                    {
                        lblledgerDECREEDEBTORSCRBDRB.CssClass = "down";
                        lblledgerDECREEDEBTORSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrDecreeDebtors) - Convert.ToDecimal(lbcrDecreeDebtors));

                    }
                    else
                    {
                        lblledgerDECREEDEBTORSCRBDRB.CssClass = "down";
                        lblledgerDECREEDEBTORSCRBDRB.Text = "0.00";
                    }

                    //Decreedebtors
                }
                else if (dtStats.Rows[iR][0].ToString() == "8")
                {
                    lblLoanCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblLoanDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrLoans = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrLoans = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrLoans) > Convert.ToDecimal(lbdrLoans))
                    {
                        lblledgerLOANSCRBDRB.CssClass = "down";
                        lblledgerLOANSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrLoans) - Convert.ToDecimal(lbdrLoans));
                    }
                    else if (Convert.ToDecimal(lbcrLoans) < Convert.ToDecimal(lbdrLoans))
                    {
                        lblledgerLOANSCRBDRB.CssClass = "up";
                        lblledgerLOANSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrLoans) - Convert.ToDecimal(lbcrLoans));

                    }
                    else
                    {
                        lblledgerLOANSCRBDRB.CssClass = "down";
                        lblledgerLOANSCRBDRB.Text = "0.00";
                    }

                    //Loans
                }
                else if (dtStats.Rows[iR][0].ToString() == "9")
                {
                    lblSUNDRIESANDADVANCEScr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblSUNDRIESANDADVANCESDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrAdvances = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrAdvances = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrAdvances) > Convert.ToDecimal(lbdrAdvances))
                    {
                        lblledgerADVANCESCRBDRB.CssClass = "down";
                        lblledgerADVANCESCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrAdvances) - Convert.ToDecimal(lbdrAdvances));
                    }
                    else if (Convert.ToDecimal(lbcrAdvances) < Convert.ToDecimal(lbdrAdvances))
                    {
                        lblledgerADVANCESCRBDRB.CssClass = "up";
                        lblledgerADVANCESCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrAdvances) - Convert.ToDecimal(lbcrAdvances));

                    }
                    else
                    {
                        lblledgerADVANCESCRBDRB.CssClass = "down";
                        lblledgerADVANCESCRBDRB.Text = "0.00";
                    }

                    //SundriesAndAdvances
                }
                else if (dtStats.Rows[iR][0].ToString() == "10")
                {
                    lblSTAMPSAndSTAMPPAPERSCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblSTAMPSAndSTAMPPAPERSDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrStamps = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrStamps = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrStamps) > Convert.ToDecimal(lbdrStamps))
                    {
                        lblledgerSTAMPSCRBDRB.CssClass = "down";
                        lblledgerSTAMPSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrStamps) - Convert.ToDecimal(lbdrStamps));
                    }
                    else if (Convert.ToDecimal(lbcrStamps) < Convert.ToDecimal(lbdrStamps))
                    {
                        lblledgerSTAMPSCRBDRB.CssClass = "up";
                        lblledgerSTAMPSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrStamps) - Convert.ToDecimal(lbcrStamps));
                    }
                    else
                    {
                        lblledgerSTAMPSCRBDRB.CssClass = "down";
                        lblledgerSTAMPSCRBDRB.Text = "0.00";
                    }
                    //StampsAndStampPapers
                }
                else if (dtStats.Rows[iR][0].ToString() == "11")
                {
                    lblPROFITANDLOSSACCOUNTCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblPROFITANDLOSSACCOUNTDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrProfitAndLoss = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrProfitAndLoss = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrProfitAndLoss) > Convert.ToDecimal(lbdrProfitAndLoss))
                    {
                        lblledgerPROFITANDLOSSCRBDRB.CssClass = "up";
                        lblledgerPROFITANDLOSSCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrProfitAndLoss) - Convert.ToDecimal(lbdrProfitAndLoss));
                    }
                    else if (Convert.ToDecimal(lbcrProfitAndLoss) < Convert.ToDecimal(lbdrProfitAndLoss))
                    {
                        lblledgerPROFITANDLOSSCRBDRB.CssClass = "down";
                        lblledgerPROFITANDLOSSCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrProfitAndLoss) - Convert.ToDecimal(lbcrProfitAndLoss));
                    }
                    else
                    {
                        lblledgerPROFITANDLOSSCRBDRB.CssClass = "down";
                        lblledgerPROFITANDLOSSCRBDRB.Text = "0.00";
                    }

                    //ProfitAndLoss
                }
                else if (dtStats.Rows[iR][0].ToString() == "12")
                {
                    lblCashCr.Text = "Cr. " + dtStats.Rows[iR][1].ToString().Replace(".00", "");
                    lblCashDr.Text = "Dr. " + dtStats.Rows[iR][2].ToString().Replace(".00", "");

                    Decimal lbcrCash = Convert.ToDecimal(dtStats.Rows[iR][1].ToString().Replace(".00", ""));
                    Decimal lbdrCash = Convert.ToDecimal(dtStats.Rows[iR][2].ToString().Replace(".00", ""));
                    if (Convert.ToDecimal(lbcrCash) > Convert.ToDecimal(lbdrCash))
                    {
                        lblledgerCASHCRBDRB.CssClass = "down";
                        lblledgerCASHCRBDRB.Text = "Cr. Bal: " + (Convert.ToDecimal(lbcrCash) - Convert.ToDecimal(lbdrCash));
                    }
                    else if (Convert.ToDecimal(lbcrCash) < Convert.ToDecimal(lbdrCash))
                    {
                        lblledgerCASHCRBDRB.CssClass = "up";
                        lblledgerCASHCRBDRB.Text = "Dr. Bal: " + (Convert.ToDecimal(lbdrCash) - Convert.ToDecimal(lbcrCash));
                    }
                    else
                    {
                        lblledgerCASHCRBDRB.CssClass = "down";
                        lblledgerCASHCRBDRB.Text = "0.00";
                    }
                    //Cash
                }
            }
        }

        protected void ASPxGridView1_SummaryDisplayText(object sender, ASPxGridViewSummaryDisplayTextEventArgs e)
        {
            if (e.Item.FieldName == "Credit")
                e.Text = string.Format("Cr.{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit")
                e.Text = string.Format("Dr.{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Credit Balance")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
            if (e.Item.FieldName == "Debit Balance")
                e.Text = string.Format("{0}", Convert.ToDouble(e.Value));
        }

        protected void lbBranch_click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Branch Ledger";
            dxPopup.ContentUrl = "BranchLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbInvestMents_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Investment Ledger";
            dxPopup.ContentUrl = "InvestmentLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbBank_ClicK(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Bank Ledger";
            dxPopup.ContentUrl = "BanksLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }
        protected void lbOTHERITEMS_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Other Items Ledger";
            dxPopup.ContentUrl = "OtherItemsLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbCHITS_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Chits Ledger";
            dxPopup.ContentUrl = "ChitLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lblOREMANCHITS_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Foreman Chits Ledger";
            dxPopup.ContentUrl = "ForemanChitsLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbDECREEDEBTORS_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Decree Debtors Ledger";
            dxPopup.ContentUrl = "DecreeDetorsLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbLoan_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Loans Ledger";
            dxPopup.ContentUrl = "LoansLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbSUNDRIESANDADVANCES_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Sundries And Advances Ledger";
            dxPopup.ContentUrl = "SudriesAndAdvancesLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbSTAMPSAndSTAMPPAPERS_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Stamps Ledger";
            dxPopup.ContentUrl = "StampsLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }

        protected void lbPROFITANDLOSSACCOUNT_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Profit And Loss Ledger";
            dxPopup.ContentUrl = "ProfitAndLossLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }
        protected void lbCashCr_Click(object sender, EventArgs e)
        {
            dxPopup.HeaderText = "Cash Ledger";
            dxPopup.ContentUrl = "CashLedgerNew.aspx?fromDate=" + txtFromDate.Text + "&toDate=" + txtToDate.Text + "";
            dxPopup.CloseButtonImage.Url = "Images\\Close.gif";
            dxPopup.ShowOnPageLoad = true;
        }
    }
}
