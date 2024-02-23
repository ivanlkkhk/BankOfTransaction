using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using BankOfBIT_KL.Data;
using BankOfBIT_KL.Models;
using System.Data.SqlClient;

namespace WindowsBanking
{
    public partial class ProcessTransaction : Form
    {
        ConstructorData constructorData;
        BankOfBIT_KLContext db = new BankOfBIT_KLContext();


        /// Form can only be opened with a Constructor Data object
        /// containing client and account details.
        /// </summary>
        /// <param name="constructorData">Populated Constructor data object.</param>
        public ProcessTransaction(ConstructorData constructorData)
        {
            //Given, more code to be added.
            InitializeComponent();
            this.constructorData = constructorData;

            clientBindingSource.DataSource = constructorData.Client;
            bankAccountBindingSource.DataSource = constructorData.BankAccount;
        }

        /// <summary>
        /// Return to the Client Data form passing specific client and 
        /// account information within ConstructorData.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClientData client = new ClientData(constructorData);
            client.MdiParent = this.MdiParent;
            client.Show();
            this.Close();
        }

        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void ProcessTransaction_Load(object sender, EventArgs e)
        {
            List<int> exceptTransactionTypes = new List<int> { (int)TransactionTypeValues.TRANSFER_RECIPIENT, (int)TransactionTypeValues.INTEREST };
            this.Location = new Point(0,0);
            accountNumberMaskedTextBox.Mask = BusinessRules.AccountFormat(constructorData.BankAccount.Description);

            try
            {
                /*** LINQ - Using NOT IN ***/
                //IQueryable<TransactionType> transactionTypes = from tt in db.TransactionTypes 
                //                                               where !exceptTransactionTypes.Any(ett => ett == tt.TransactionTypeId) 
                //                                               select tt;
                IQueryable<TransactionType> transactionTypes = db.TransactionTypes
                                                                .Where(tt => !exceptTransactionTypes.Contains(tt.TransactionTypeId));

                transactionTypeBindingSource.DataSource = transactionTypes.ToList();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Message");
            }

            PayeeControlsEnable(false);
            lnkUpdate.Enabled = true;
            lblNoAdditionalAccounts.Visible = false;
        }

        /// <summary>
        /// Transaction Type ComboBox SelectedIndexChanged event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TransactionType transactionType = (TransactionType)descriptionComboBox.SelectedItem;

            if (transactionType != null) {
                switch (transactionType.TransactionTypeId)
                {
                    case (int)TransactionTypeValues.DEPOSIT:
                    case (int)TransactionTypeValues.WITHDRAWAL:
                        PayeeControlsEnable(false);
                        lblNoAdditionalAccounts.Visible = false;
                        lnkUpdate.Enabled = true;
                        break;
                    case (int)TransactionTypeValues.BILL_PAYMENT:
                        PayeeDataBillPaymentBind();
                        break;
                    case (int)TransactionTypeValues.TRANSFER:
                        PayeeDataTransferBind();
                        break;
                }
            }
        }

        /// <summary>
        /// Binding from Account's data on Payees's dropdroplist control.
        /// </summary>
        protected void PayeeDataTransferBind()
        {
            IQueryable<BankAccount> bankAccounts = db.BankAccounts
                .Where(ba => ba.ClientId == constructorData.Client.ClientId && ba.AccountNumber != constructorData.BankAccount.AccountNumber);

            if (bankAccounts.Count() > 0)
            {
                BindingSource bankAccountPayeeBindingSource = new BindingSource();
                bankAccountPayeeBindingSource.DataSource = bankAccounts.ToList();
                cboPayeeAccount.DataBindings.Clear();
                ////////cboPayeeAccount.DataBindings.Add(new System.Windows.Forms.Binding("Text", bankAccountPayeeBindingSource, "AccountNumber", true));
                cboPayeeAccount.DataSource = bankAccountPayeeBindingSource;
                cboPayeeAccount.DisplayMember = "AccountNumber";
                cboPayeeAccount.ValueMember = "BankAccountId";
                PayeeControlsEnable(true);
                //lnkUpdate.Enabled = true;
                //lblNoAdditionalAccounts.Visible = false;
            }
            else
            {
                PayeeControlsEnable(false);
                //lnkUpdate.Enabled = false;
                //lblNoAdditionalAccounts.Visible = true;
            }
        }

        /// <summary>
        /// Binding from Payee data on Payees's dropdroplist control.
        /// </summary>
        protected void PayeeDataBillPaymentBind()
        {
            // Retrieves Payee and bind data on dropdownlist.
            IQueryable<Payee> payees = db.Payees;
            BindingSource payeeBindingSource = new BindingSource();
            payeeBindingSource.DataSource = payees.ToList();
            cboPayeeAccount.DataBindings.Clear();
            ///////cboPayeeAccount.DataBindings.Add(new System.Windows.Forms.Binding("Text", payeeBindingSource, "Description", true));
            cboPayeeAccount.DataSource = payeeBindingSource;
            cboPayeeAccount.DisplayMember = "Description";
            cboPayeeAccount.ValueMember = "PayeeId";
            PayeeControlsEnable(true);
            //lnkUpdate.Enabled = true;
            //lblNoAdditionalAccounts.Visible = false;
        }

        /// <summary>
        /// Disable/Enable Payee combo box and label controls.
        /// </summary>
        /// <param name="enable">true to enable/false to disable</param>
        protected void PayeeControlsEnable(bool enable)
        {
            cboPayeeAccount.Visible = enable;
            lblPayeeAccount.Visible = enable;
            lnkUpdate.Enabled = enable;
            lblNoAdditionalAccounts.Visible = !enable;
        }

        /// <summary>
        /// Update link clicked event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string notes;
            double amount;
            TransactionManagerServiceReference.TransactionManagerClient transactionManagerClient = new TransactionManagerServiceReference.TransactionManagerClient();

            if (String.IsNullOrEmpty(txtAmount.Text) || !Numeric.IsNumeric(txtAmount.Text, System.Globalization.NumberStyles.Number))
            {
                MessageBox.Show("Amount must be numeric value.", "Invalid Amount");
                return;
            }
            else
            {
                int transactionType = (int)descriptionComboBox.SelectedValue;
                amount = Convert.ToDouble(txtAmount.Text);

                if (transactionType == (int)TransactionTypeValues.BILL_PAYMENT ||
                    transactionType == (int)TransactionTypeValues.TRANSFER ||
                    transactionType == (int)TransactionTypeValues.WITHDRAWAL)
                {
                    if (amount > constructorData.BankAccount.Balance)
                    {
                        MessageBox.Show("Insufficient funds exist for requested transaction.", "Insufficient Funds");
                        return;
                    }
                }

                switch (transactionType)
                {
                    case (int)TransactionTypeValues.BILL_PAYMENT:
                        notes = "Bill Payment to : " + cboPayeeAccount.Text;
                        try
                        {
                            double? updatedBalance = transactionManagerClient.BillPayment(constructorData.BankAccount.BankAccountId, amount, notes);
                            UpdateBalance((double)updatedBalance);
                        }
                        catch 
                        {
                            MessageBox.Show("Error completing the Bill Payment transaction.", "Transaction Failure");
                        }
                        break;
                    case (int)TransactionTypeValues.TRANSFER:
                        int toAccountId = (int)cboPayeeAccount.SelectedValue;
                        notes = "Transfer from: " + constructorData.BankAccount.AccountNumber + " to : " + cboPayeeAccount.Text;
                        try
                        {
                            double? updatedBalance = transactionManagerClient.Transfer(constructorData.BankAccount.BankAccountId, toAccountId, amount, notes);
                            UpdateBalance((double)updatedBalance);
                        }
                        catch 
                        {
                            MessageBox.Show("Error completing the Transfer transaction.", "Transaction Failure");
                        }
                        break;
                    case (int)TransactionTypeValues.WITHDRAWAL:
                        notes = "Withdrawal amount: " + txtAmount.Text;
                        try
                        {
                            double? updatedBalance = transactionManagerClient.Withdrawal(constructorData.BankAccount.BankAccountId, amount, notes);
                            UpdateBalance((double)updatedBalance);
                        }
                        catch
                        {
                            MessageBox.Show("Error completing the Withdrawal transaction.", "Transaction Failure");
                        }
                        break;
                    case (int)TransactionTypeValues.DEPOSIT:
                        notes = "Deposit amount : " + txtAmount.Text;
                        try
                        {
                            double? updatedBalance = transactionManagerClient.Deposit(constructorData.BankAccount.BankAccountId, amount, notes);
                            UpdateBalance((double)updatedBalance);
                        }
                        catch
                        {
                            MessageBox.Show("Error completing the Deposit transaction.", "Transaction Failure");
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Update the updated balance on screen. 
        /// </summary>
        /// <param name="balance"></param>
        protected void UpdateBalance(double balance)
        {
            constructorData.BankAccount.Balance = balance;
            bankAccountBindingSource.DataSource = typeof(BankAccount);
            bankAccountBindingSource.DataSource = constructorData.BankAccount;
        }
    }
}
