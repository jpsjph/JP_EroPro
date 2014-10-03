﻿using AutoMapper;
using Common.Services.Map;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ero57_Project.Models
{
    public class TransactionModel:ICustomMapping
    {

        public int TransactionId { get; set; }
        public string Reference { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal OpeningAmount { get; set; }
        public decimal CurrencyAmount { get; set; }
        public decimal OpeningCurrencyAmount { get; set; }
        public DateTime StatusDate { get; set; }
        public bool IsClosed { get; set; }
    

        public void CreateMappings(IConfiguration configuration)
        {
           configuration.CreateMap<TransactionPayment,TransactionModel>()
               .ForMember(x=>x.)
        }
    }
}