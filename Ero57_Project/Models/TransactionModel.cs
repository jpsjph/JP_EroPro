using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Services.Map;
using AutoMapper;
using Domain.Model;

namespace Ero57_Project.Models
{
    public class TransactionModel : ICustomMapping
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
            configuration.CreateMap<TransactionPayment, TransactionModel>()
                         .ForMember(m => m.TransactionId, opt => opt.MapFrom(s => s.TransactionPaymentId))
                         .ForMember(m => m.Reference, opt => opt.MapFrom(s => s.Reference))
                         .ForMember(m => m.InvoiceNumber, opt => opt.MapFrom(s => s.InvoiceNumber))
                         .ForMember(m => m.DateReceived, opt => opt.MapFrom(s => s.DateReceived))
                         .ForMember(m => m.DueDate, opt => opt.MapFrom(s => s.DueDate))
                         .ForMember(m => m.Amount, opt => opt.MapFrom(s => s.Amount))
                         .ForMember(m => m.OpeningAmount, opt => opt.MapFrom(s => s.OpeningAmount))
                         .ForMember(m => m.CurrencyAmount, opt => opt.MapFrom(s => s.CurrencyAmount))
                         .ForMember(m => m.OpeningCurrencyAmount, opt => opt.MapFrom(s => s.OpeningCurrencyAmount))
                         .ForMember(m => m.StatusDate, opt => opt.MapFrom(s => s.StatusDate))
                         .ForMember(m => m.IsClosed, opt => opt.MapFrom(s => s.IsClosed));
        }
    }
}