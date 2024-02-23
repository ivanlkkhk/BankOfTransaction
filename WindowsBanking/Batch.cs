using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using BankOfBIT_KL.Data;
using BankOfBIT_KL.Models;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utility;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Pipes;

namespace WindowsBanking
{
    public class Batch
    {
        /// <summary>
        /// The Database Context object.
        /// </summary>
        private BankOfBIT_KLContext db = new BankOfBIT_KLContext();
        
        /// <summary>
        /// The institution number of the institution to be process. 
        /// </summary>
        private string institutionNumber;

        /// <summary>
        /// The name of the xml input file.
        /// </summary>
        private String inputFileName;

        /// <summary>
        /// The name of the log file.
        /// </summary>
        private String logFileName;

        /// <summary>
        /// The data to be written to the log file.
        /// </summary>
        private String logData;

        /// <summary>
        /// This method will process all detail errors found within the current file being processed.
        /// </summary>
        /// <param name="beforeQuery">Represents the records that existed before the round of validation.</param>
        /// <param name="afterQuery">Represents the records that remained following the round of validation.</param>
        /// <param name="message">Represents the error message that is to be written to the log file based on the record(s) failing the round of validation.</param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {
            // Compare and get the error transactions.
            IEnumerable<XElement> errors = beforeQuery.Except(afterQuery);

            
            foreach (XElement record in errors)
            {
                logData += "\r\n------ERROR------";
                logData += "\r\nFile: " + inputFileName;
                logData += "\r\nInstitution: " + record.Element("institution");
                logData += "\r\nAccount Number: " + record.Element("account_no");
                logData += "\r\nTransaction Type:: " + record.Element("type");
                logData += "\r\nAmount: " + record.Element("amount");
                logData += "\r\nNote: " + record.Element("notes");
                logData += "\r\nNodes: " + record.Nodes().Count();
                logData += "\r\n: " + message;
                logData += "\r\n================\r\n";
            }
        }

        /// <summary>
        /// Verify the attributes of the xml file’s root element. If any of the attributes produce an error, the file is NOT to be processed.
        /// </summary>
        private void ProcessHeader()
        {
            XDocument inputFile = XDocument.Load(inputFileName);
            XElement root = inputFile.Element("account_update");

            // Check if attribute equal to 3.
            if (root.Attributes().Count() != 3)
            {
                throw new Exception("ERROR: account_update element must have 3 attributes.");
            }

            // Check the date attribute of the XElement object must be equal to today’s date.
            XAttribute dateAttr = root.Attribute("date");
            //if (dateAttr.Value != String.Format("{0:u}", DateTime.Today).Substring(0, 10))
            if (!DateTime.Today.Equals(DateTime.Parse(dateAttr.Value)))
            {
                throw new Exception(String.Format("ERROR: The date: {0} in file {1} is invalid.", dateAttr.Value, inputFileName));
            }

            // Check the institution attribute exists within the Institutions table.
            XAttribute institutionAttr = root.Attribute("institution");
            institutionNumber = institutionAttr.Value;

            
            Institution institution = db.Institutions.Where(x => x.InstitutionNumber.ToString() == institutionNumber).SingleOrDefault();
            
            if (institution == null)
            {
                throw new Exception(String.Format("ERROR: The institution: {0} in file {1} does not exist.", institutionNumber, inputFileName));
            }

            // Check the checksum attribute must match the sum of all account_no elements in the file.
            int accNumSum = 0;
            XAttribute checksumAttr = root.Attribute("checksum");
            IEnumerable<XElement> filteredElecments = inputFile.Descendants().Where(d => d.Name == "account_no");

            foreach (XElement accNum in filteredElecments)
            {
                accNumSum += int.Parse(accNum.Value);
            }
            
            if (accNumSum != int.Parse(checksumAttr.Value))
            {
                throw new Exception(String.Format("ERROR: The checksum: {0} in file {1} is invalid", checksumAttr.Value, inputFileName));
            }

            
        }

        /// <summary>
        /// This method is used to verify the contents of the detail records in the input file. If any of the records produce an error, 
        /// that record will be skipped, but the file processing will continue.
        /// </summary>
        private void ProcessDetails()
        {
            // Load the xml into xDocument.
            XDocument inputFile = XDocument.Load(inputFileName);

            // Populate all transactions into transactions variable.
            IEnumerable<XElement> transactions = inputFile.Descendants()
                                                    .Where(d => d.Name == "transaction");

            // Start Validation
            // Transactions with five elements.
            IEnumerable<XElement> transWithFiveElements = transactions
                                                            .Where(x => x.Elements().Nodes().Count() == 5);

            // Call ProcessErros to handle the error elements.
            ProcessErrors(transactions, transWithFiveElements, "Transaction elements not contains 5 child elements.");

            // Transactions elements whose institution node matches the institution attribute of the root element.
            IEnumerable<XElement> institutionElements = transWithFiveElements
                                                            .Where(x => x.Element("institution").Value == institutionNumber);

            // Call ProcessErros to handle the error elements.
            ProcessErrors(transWithFiveElements, institutionElements, "The institution attribute does not match.");

            // The type and amount nodes within each transaction node musts be numeric.
            IEnumerable<XElement> numericElements = institutionElements
                                                            .Where(x => Numeric.IsNumeric(x.Element("type").Value, NumberStyles.Integer) &
                                                                    Numeric.IsNumeric(x.Element("amount").Value, NumberStyles.Float));

            ProcessErrors(institutionElements, numericElements, "The type or amount is not numeric value.");

            // The type node within each transaction node must have a value of 2 (withdrawal) or 6 (interest).
            List<int> neededType = new List<int> { (int)TransactionTypeValues.WITHDRAWAL, (int)TransactionTypeValues.INTEREST };
            IEnumerable<XElement> typeElements = numericElements
                                                            .Where(x => neededType.Contains(int.Parse(x.Element("type").Value)));

            ProcessErrors(numericElements, typeElements, "Invalid Transaction Type.");

            // The amount node for interest calculations (type node = 6) within each transaction node must have a value of 0.
            // The amount node for withdrawals (type node = 2) within each transaction node must have a value greater than 0..
            IEnumerable<XElement> amountElements = typeElements
                                                            .Where(x => ((x.Element("type").Value == "6" &&
                                                                        x.Element("amount").Value == "0")
                                                                        ||
                                                                        (x.Element("type").Value == "2" &&
                                                                        Decimal.Parse(x.Element("amount").Value) > 0)));

            ProcessErrors(typeElements, amountElements, "Incorrect Amount value for Type specified");

            // The account_no node within each transaction node must exist in the database.
            //List<long> accountNumbers = ((IQueryable<long>)db.BankAccounts.Select(x => x.AccountNumber )).ToList();
            IEnumerable<long> accountNumbers = ((IQueryable<long>)db.BankAccounts.Select(x => x.AccountNumber)).ToList();
            IEnumerable<XElement> accountElements = amountElements.Where(x => accountNumbers.Contains(long.Parse(x.Element("account_no").Value)));

            ProcessErrors(amountElements, accountElements, "The account number must be exist in the database.");

            // Process the error free result set
            ProcessTransactions(accountElements);

            // End Validation
        }

        /// <summary>
        /// This method is used to process all valid transaction records.
        /// </summary>
        /// <param name="transactionRecords"></param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            //TransactionManagerServiceReference.TransactionManagerClient transactionManagerClient = new TransactionManagerServiceReference.TransactionManagerClient();

            using (TransactionManagerServiceReference.TransactionManagerClient transactionManagerClient = new TransactionManagerServiceReference.TransactionManagerClient())
            {

                foreach (XElement record in transactionRecords)
                {
                    string accountNum = record.Element("account_no").Value;
                    long accNum = long.Parse(accountNum);
                    BankAccount bankAccount = db.BankAccounts.Where(x => x.AccountNumber == accNum).SingleOrDefault();

                    TransactionTypeValues transactionType = (TransactionTypeValues)int.Parse(record.Element("type").Value);
                    double amount = double.Parse(record.Element("amount").Value);
                    string notes = record.Element("notes").Value;
                    double? updatedBalance;

                    switch (transactionType)
                    {
                        case TransactionTypeValues.WITHDRAWAL:
                            updatedBalance = transactionManagerClient.Withdrawal(bankAccount.BankAccountId, amount, notes);
                            if (updatedBalance.HasValue)
                            {
                                logData += string.Format("\r\nTransaction completed successfully: Withdrawal - {0} applied to account {1}.", amount, accountNum);
                            }
                            else
                            {
                                logData += "Transaction completed unsuccessfully.";
                            }
                            break;
                        case TransactionTypeValues.INTEREST:
                            updatedBalance = transactionManagerClient.CalculateInterest(bankAccount.BankAccountId, notes);
                            if (updatedBalance.HasValue)
                            {
                                logData += string.Format("\r\nTransaction completed successfully: Interest - *** applied to account {0}.", accountNum);
                            }
                            else
                            {
                                logData += "\r\nTransaction completed unsuccessfully.";
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// This method will be called upon completion of a file being processed.
        /// </summary>
        /// <returns></returns>
        public String WriteLogData()
        {
            string rtnLogData = logData;
            FileStream fileStream = null;
            // Create log file.
            try
            {
                fileStream = new FileStream(logFileName, FileMode.Append);

                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(rtnLogData);
                }
            }
            finally
            {
                if (fileStream != null) fileStream.Dispose();
            }
            logData = "";
            logFileName = "";
            return rtnLogData;
        }

        /// <summary>
        /// Initiate the batch process by determining the appropriate filename and 
        /// then proceeding with the header and detail processing.
        /// </summary>
        /// <param name="institution"></param>
        /// <param name="key"></param>
        public void ProcessTransmission(String institution, String key)
        {
            String fileName;
            String encryptedFileName;

            fileName = String.Format("{0:D4}-{1:D3}-{2}", DateTime.Today.Year, DateTime.Today.DayOfYear, institution);
            
            inputFileName = String.Concat(fileName, ".xml");
            encryptedFileName = String.Concat(inputFileName, ".encrypted");
            logFileName = @String.Concat("LOG ", fileName, ".txt");

            if (File.Exists(encryptedFileName))
            {
                try
                {
                    Encryption.Decrypt(inputFileName, encryptedFileName, key);
                    if (File.Exists(inputFileName))
                    {
                        try
                        {
                            ProcessHeader();
                            ProcessDetails();
                        }
                        catch (Exception ex)
                        {
                            logData += "\r\n" + ex.Message;
                        }
                    }
                    else
                    {
                        logData += "\r\n" + inputFileName + " does not exist!";
                    }
                } catch (Exception ex)
                {
                    logData += "\r\n" + ex.Message;
                    inputFileName = "";
                }
            }
            else
            {
                logData += "\r\n" + encryptedFileName + " does not exist!";
            }
            
            
        }
    }
}
