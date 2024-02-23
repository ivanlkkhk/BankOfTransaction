namespace WindowsBanking
{
    partial class ProcessTransaction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label clientNumberLabel;
            System.Windows.Forms.Label fullNameLabel;
            System.Windows.Forms.Label accountNumberLabel;
            System.Windows.Forms.Label balanceLabel;
            System.Windows.Forms.Label descriptionLabel;
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.balanceLabel1 = new System.Windows.Forms.Label();
            this.bankAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.accountNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.fullNameLabel1 = new System.Windows.Forms.Label();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.grpTransaction = new System.Windows.Forms.GroupBox();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.transactionTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lnkReturn = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.cboPayeeAccount = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblPayeeAccount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNoAdditionalAccounts = new System.Windows.Forms.Label();
            clientNumberLabel = new System.Windows.Forms.Label();
            fullNameLabel = new System.Windows.Forms.Label();
            accountNumberLabel = new System.Windows.Forms.Label();
            balanceLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            this.grpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            this.grpTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // clientNumberLabel
            // 
            clientNumberLabel.AutoSize = true;
            clientNumberLabel.Location = new System.Drawing.Point(97, 30);
            clientNumberLabel.Name = "clientNumberLabel";
            clientNumberLabel.Size = new System.Drawing.Size(76, 13);
            clientNumberLabel.TabIndex = 0;
            clientNumberLabel.Text = "Client Number:";
            // 
            // fullNameLabel
            // 
            fullNameLabel.AutoSize = true;
            fullNameLabel.Location = new System.Drawing.Point(344, 30);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Size = new System.Drawing.Size(57, 13);
            fullNameLabel.TabIndex = 2;
            fullNameLabel.Text = "Full Name:";
            // 
            // accountNumberLabel
            // 
            accountNumberLabel.AutoSize = true;
            accountNumberLabel.Location = new System.Drawing.Point(97, 64);
            accountNumberLabel.Name = "accountNumberLabel";
            accountNumberLabel.Size = new System.Drawing.Size(90, 13);
            accountNumberLabel.TabIndex = 4;
            accountNumberLabel.Text = "Account Number:";
            // 
            // balanceLabel
            // 
            balanceLabel.AutoSize = true;
            balanceLabel.Location = new System.Drawing.Point(344, 64);
            balanceLabel.Name = "balanceLabel";
            balanceLabel.Size = new System.Drawing.Size(49, 13);
            balanceLabel.TabIndex = 6;
            balanceLabel.Text = "Balance:";
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(151, 55);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(93, 13);
            descriptionLabel.TabIndex = 7;
            descriptionLabel.Text = "Transaction Type:";
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(balanceLabel);
            this.grpClient.Controls.Add(this.balanceLabel1);
            this.grpClient.Controls.Add(accountNumberLabel);
            this.grpClient.Controls.Add(this.accountNumberMaskedTextBox);
            this.grpClient.Controls.Add(fullNameLabel);
            this.grpClient.Controls.Add(this.fullNameLabel1);
            this.grpClient.Controls.Add(clientNumberLabel);
            this.grpClient.Controls.Add(this.clientNumberMaskedTextBox);
            this.grpClient.Location = new System.Drawing.Point(47, 48);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(694, 101);
            this.grpClient.TabIndex = 0;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Client Data";
            // 
            // balanceLabel1
            // 
            this.balanceLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Balance", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.balanceLabel1.Location = new System.Drawing.Point(407, 64);
            this.balanceLabel1.Name = "balanceLabel1";
            this.balanceLabel1.Size = new System.Drawing.Size(182, 23);
            this.balanceLabel1.TabIndex = 7;
            this.balanceLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bankAccountBindingSource
            // 
            this.bankAccountBindingSource.DataSource = typeof(BankOfBIT_KL.Models.BankAccount);
            // 
            // accountNumberMaskedTextBox
            // 
            this.accountNumberMaskedTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.accountNumberMaskedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.accountNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "AccountNumber", true));
            this.accountNumberMaskedTextBox.Location = new System.Drawing.Point(193, 64);
            this.accountNumberMaskedTextBox.Name = "accountNumberMaskedTextBox";
            this.accountNumberMaskedTextBox.Size = new System.Drawing.Size(100, 20);
            this.accountNumberMaskedTextBox.TabIndex = 5;
            // 
            // fullNameLabel1
            // 
            this.fullNameLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fullNameLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "FullName", true));
            this.fullNameLabel1.Location = new System.Drawing.Point(407, 30);
            this.fullNameLabel1.Name = "fullNameLabel1";
            this.fullNameLabel1.Size = new System.Drawing.Size(182, 23);
            this.fullNameLabel1.TabIndex = 3;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataSource = typeof(BankOfBIT_KL.Models.Client);
            // 
            // clientNumberMaskedTextBox
            // 
            this.clientNumberMaskedTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.clientNumberMaskedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clientNumberMaskedTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "ClientNumber", true));
            this.clientNumberMaskedTextBox.Location = new System.Drawing.Point(193, 30);
            this.clientNumberMaskedTextBox.Mask = "0000-0000";
            this.clientNumberMaskedTextBox.Name = "clientNumberMaskedTextBox";
            this.clientNumberMaskedTextBox.Size = new System.Drawing.Size(100, 20);
            this.clientNumberMaskedTextBox.TabIndex = 1;
            this.clientNumberMaskedTextBox.ValidatingType = typeof(int);
            // 
            // grpTransaction
            // 
            this.grpTransaction.Controls.Add(descriptionLabel);
            this.grpTransaction.Controls.Add(this.descriptionComboBox);
            this.grpTransaction.Controls.Add(this.lnkReturn);
            this.grpTransaction.Controls.Add(this.lnkUpdate);
            this.grpTransaction.Controls.Add(this.cboPayeeAccount);
            this.grpTransaction.Controls.Add(this.txtAmount);
            this.grpTransaction.Controls.Add(this.lblPayeeAccount);
            this.grpTransaction.Controls.Add(this.label1);
            this.grpTransaction.Controls.Add(this.lblNoAdditionalAccounts);
            this.grpTransaction.Location = new System.Drawing.Point(47, 187);
            this.grpTransaction.Name = "grpTransaction";
            this.grpTransaction.Size = new System.Drawing.Size(694, 208);
            this.grpTransaction.TabIndex = 1;
            this.grpTransaction.TabStop = false;
            this.grpTransaction.Text = "Perform Transaction";
            // 
            // descriptionComboBox
            // 
            ///////////////this.descriptionComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.transactionTypeBindingSource, "Description", true));
            this.descriptionComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.descriptionComboBox.DataSource = this.transactionTypeBindingSource;
            this.descriptionComboBox.DisplayMember = "Description";
            this.descriptionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Location = new System.Drawing.Point(303, 52);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(201, 21);
            this.descriptionComboBox.TabIndex = 8;
            this.descriptionComboBox.ValueMember = "TransactionTypeId";
            this.descriptionComboBox.SelectedIndexChanged += new System.EventHandler(this.descriptionComboBox_SelectedIndexChanged);
            // 
            // transactionTypeBindingSource
            // 
            this.transactionTypeBindingSource.DataSource = typeof(BankOfBIT_KL.Models.TransactionType);
            // 
            // lnkReturn
            // 
            this.lnkReturn.AutoSize = true;
            this.lnkReturn.Location = new System.Drawing.Point(319, 174);
            this.lnkReturn.Name = "lnkReturn";
            this.lnkReturn.Size = new System.Drawing.Size(80, 13);
            this.lnkReturn.TabIndex = 5;
            this.lnkReturn.TabStop = true;
            this.lnkReturn.Text = "Return to Client";
            this.lnkReturn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkReturn_LinkClicked);
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Location = new System.Drawing.Point(221, 174);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(42, 13);
            this.lnkUpdate.TabIndex = 4;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "Update";
            this.lnkUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdate_LinkClicked);
            // 
            // cboPayeeAccount
            // 
            this.cboPayeeAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayeeAccount.FormattingEnabled = true;
            this.cboPayeeAccount.Location = new System.Drawing.Point(303, 121);
            this.cboPayeeAccount.Name = "cboPayeeAccount";
            this.cboPayeeAccount.Size = new System.Drawing.Size(201, 21);
            this.cboPayeeAccount.TabIndex = 3;
            this.cboPayeeAccount.Visible = false;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Location = new System.Drawing.Point(303, 90);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(201, 20);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayeeAccount
            // 
            this.lblPayeeAccount.AutoSize = true;
            this.lblPayeeAccount.Location = new System.Drawing.Point(204, 121);
            this.lblPayeeAccount.Name = "lblPayeeAccount";
            this.lblPayeeAccount.Size = new System.Drawing.Size(40, 13);
            this.lblPayeeAccount.TabIndex = 1;
            this.lblPayeeAccount.Text = "Payee:";
            this.lblPayeeAccount.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amount:";
            // 
            // lblNoAdditionalAccounts
            // 
            this.lblNoAdditionalAccounts.Location = new System.Drawing.Point(300, 153);
            this.lblNoAdditionalAccounts.Name = "lblNoAdditionalAccounts";
            this.lblNoAdditionalAccounts.Size = new System.Drawing.Size(207, 33);
            this.lblNoAdditionalAccounts.TabIndex = 6;
            this.lblNoAdditionalAccounts.Text = "No accounts available to receive transfer.";
            this.lblNoAdditionalAccounts.Visible = false;
            // 
            // ProcessTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpTransaction);
            this.Controls.Add(this.grpClient);
            this.Name = "ProcessTransaction";
            this.Text = "ProcessTransaction";
            this.Load += new System.EventHandler(this.ProcessTransaction_Load);
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            this.grpTransaction.ResumeLayout(false);
            this.grpTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.GroupBox grpTransaction;
        private System.Windows.Forms.Label lblNoAdditionalAccounts;
        private System.Windows.Forms.LinkLabel lnkReturn;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.ComboBox cboPayeeAccount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblPayeeAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fullNameLabel1;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private System.Windows.Forms.MaskedTextBox clientNumberMaskedTextBox;
        private System.Windows.Forms.Label balanceLabel1;
        private System.Windows.Forms.BindingSource bankAccountBindingSource;
        private System.Windows.Forms.MaskedTextBox accountNumberMaskedTextBox;
        private System.Windows.Forms.ComboBox descriptionComboBox;
        private System.Windows.Forms.BindingSource transactionTypeBindingSource;
    }
}