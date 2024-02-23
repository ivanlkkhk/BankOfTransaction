using BankOfBIT_KL.Data;
using BankOfBIT_KL.Models;
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

namespace WindowsBanking
{
    public partial class BatchProcess : Form
    {
        private BankOfBIT_KLContext db = new BankOfBIT_KLContext();
        

        public BatchProcess()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Always display the form in the top right corner of the frame.
        /// </summary>
        private void BatchProcess_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0,0);

            Batch batch = new Batch();
            IQueryable<Institution> institutions = db.Institutions;
            descriptionComboBox.DataSource = institutions.ToList();
            descriptionComboBox.Enabled = false;

        }

        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            string keyValue="";
            //given:  Ensure key has been entered.  Note: for use with Assignment 9
            if(txtKey.Text.Length == 0)
            {
                MessageBox.Show("Please enter a key to decrypt the input file(s).", "Key Required");
            }else
            {
                keyValue = txtKey.Text;
            }


            if (radSelect.Checked)
            {
                // Single transmission checked.
                string institutionNumber;
                institutionNumber = ((int)descriptionComboBox.SelectedValue).ToString();
                callProcessTransmission(institutionNumber, keyValue);
            }
            else
            {
                // Single transmission checked.
                foreach (Institution institution in descriptionComboBox.Items)
                {
                    callProcessTransmission(institution.InstitutionNumber.ToString(), keyValue);
                }


            }

        }

        /// <summary>
        /// This method created for call the ProcessTransmission and WriteLogData then it will assign the log data to the richtext control.
        /// </summary>
        /// <param name="institutionNumber"></param>
        /// <param name="keyValue"></param>
        protected void callProcessTransmission(string institutionNumber, string keyValue)
        {
            Batch batch = new Batch();
            batch.ProcessTransmission(institutionNumber, keyValue);
            string logData = batch.WriteLogData();
            rtxtLog.Text += logData;
        }

        private void radSelect_CheckedChanged(object sender, EventArgs e)
        {
            descriptionComboBox.Enabled = radSelect.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = {"2022-342-32958.xml", "2022-342-34439.xml", "2022-342-35918.xml", "2022-342-38028.xml",
                                "2022-342-41980.xml", "2022-342-44982.xml", "2022-342-45910.xml" };
            string keyValue = "";
            //given:  Ensure key has been entered.  Note: for use with Assignment 9
            if (txtKey.Text.Length == 0)
            {
                MessageBox.Show("Please enter a key to decrypt the input file(s).", "Key Required");
            }
            else
            {
                keyValue = txtKey.Text;

                foreach (string file in files)
                {
                    Encryption.Encrypt(file, file + ".encrypted", keyValue);
                }
            }
        }
    }
}
