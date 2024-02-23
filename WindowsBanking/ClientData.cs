using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankOfBIT_KL.Data;
using BankOfBIT_KL.Models;

namespace WindowsBanking
{
    public partial class ClientData : Form
    {
        ConstructorData constructorData = new ConstructorData();
        BankOfBIT_KLContext db = new BankOfBIT_KLContext();
        
        /// <summary>
        /// This constructor will execute when the form is opened
        /// from the MDI Frame.
        /// </summary>
        public ClientData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This constructor will execute when the form is opened by
        /// returning from the History or Transaction forms.
        /// </summary>
        /// <param name="constructorData">Populated ConstructorData object.</param>
        public ClientData(ConstructorData constructorData)
        {
            //Given:
            InitializeComponent();
            this.constructorData = constructorData;

            //More code to be added:
            lnkDetails.Enabled = false;
            lnkProcess.Enabled = false;
            if (constructorData != null)
            {
                clientNumberMaskedTextBox.Text = constructorData.Client.ClientNumber.ToString();
                clientNumberMaskedTextBox_Leave(null, null);
            }

        }

        /// <summary>
        /// Open the Transaction form passing ConstructorData object.
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Given, more code to be added.
            setConstuctorData();
            ProcessTransaction transaction = new ProcessTransaction(constructorData);
            transaction.MdiParent = this.MdiParent;
            transaction.Show();
            this.Close();
        }

        /// <summary>
        /// Open the History form passing ConstructorData object.
        /// </summary>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Given, more code to be added.
            setConstuctorData();
            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }

        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void ClientData_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }

        /// <summary>
        /// Set constuctor data for passing data across windows.
        /// </summary>
        private void setConstuctorData()
        {
            constructorData.Client = (Client)clientBindingSource.DataSource;
            constructorData.BankAccount = (BankAccount)accountNumberComboBox.SelectedItem;
        }

        private void linksEnable(bool enable)
        {
            lnkDetails.Enabled = enable;
            lnkProcess.Enabled = enable;
        }

        /// <summary>
        /// Client Number leave's event. 
        /// </summary>
        private void clientNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(clientNumberMaskedTextBox.Text))
            {
                String stringClientNumber = clientNumberMaskedTextBox.Text;
                int clientNumber = int.Parse(stringClientNumber);

                if (clientNumber > 0)
                {
                    Client client = db.Clients
                        .Where(x => x.ClientNumber == clientNumber)
                        .SingleOrDefault();

                    if (client != null)
                    {
                        clientBindingSource.DataSource = client;

                        IQueryable<BankAccount> bankAccounts = db.BankAccounts
                            .Where(ba => ba.ClientId == client.ClientId);

                        if (bankAccounts.Count() > 0)
                        {
                            bankAccountBindingSource.DataSource = bankAccounts.ToList();
                            linksEnable(true);

                            if (constructorData.BankAccount != null)
                            {
                                accountNumberComboBox.Text = constructorData.BankAccount.AccountNumber.ToString();
                            }
                        }
                        else
                        {
                            linksEnable(false);
                            bankAccountBindingSource.DataSource = typeof(BankAccount);
                        }
                    }
                    else
                    {
                        linksEnable(false);
                        clientBindingSource.DataSource = typeof(Client);
                        bankAccountBindingSource.DataSource = typeof(BankAccount);
                        clientNumberMaskedTextBox.Focus();
                        MessageBox.Show("Client Number: " + stringClientNumber + " does not exist.", "Invalid Client Number");

                    }
                }
            }
        }
    }
}
