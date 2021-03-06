﻿using Business.DomainModel.Active;
using Business.DomainModel.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class AdditionalPathModel
    {
        public IList<IAdditionalIncome> AdditionalIncomes { get; set; }

        public IList<IAdditionalCost> AdditionalCosts { get; set; }
    }
}
