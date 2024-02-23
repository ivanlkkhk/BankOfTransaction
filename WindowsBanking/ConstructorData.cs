using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfBIT_KL.Models;

namespace WindowsBanking
{
    public class ConstructorData
    {
        /// <summary>
        /// Client data use among the forms.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// BankAccount data use among the forms.
        /// </summary>
        public BankAccount BankAccount { get; set; }
    }
}
