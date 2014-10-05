using Common.Concrete;
using Common.Services;
using Common.Services.ViewModel;
using Domain.Model;
using Ero57_API.Controllers;
using Ero57_API.Tests.Configuration;
using Ero57_API.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetSpell.SpellChecker.Dictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http.Results;

namespace Ero57_API.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        private GenericConfiguration<Transaction> _transConfiguration;
        private List<Transaction> _transList;
        private TransactionModel _transactionModel;

        [TestInitialize]
        public void TearUp()
        {
            _transConfiguration = new GenericConfiguration<Transaction>();
            _transConfiguration.Setup();
            _transList = TransactionHelper.GetTransactionList();
            _transactionModel = TransactionHelper.GetTransactionModel();
        }

        [TestMethod]
        public void GetTransactionDetails_Should_Return_Valid_Data()
        {
            const string exceptedTransAccountCode = "12455145442";
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionDetails(exceptedTransAccountCode, exceptedTransactionRef);
            _transConfiguration.MockEntity.VerifyAll();
            Assert.IsNotNull(returnValue);
        }

        [TestMethod]
        [Ignore]
        public void GetTransactionDetails_ControllerMethod_Should_Return_CorrectTransaction()
        {
            const string exceptedTransAccountCode = "12455145442";
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var controller = new TransactionsController(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var returnValue = controller.GetTransactionDetails(exceptedTransAccountCode, exceptedTransactionRef, "v1");
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTransactionDetails_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidTransactionAccountCode = null;
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.GetTransactionDetails(invalidTransactionAccountCode, exceptedTransactionRef);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCreditNoteTransaction_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddCreditNoteTransaction(_transactionModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDebtorNoteTransaction_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddDebtorNoteTransaction(_transactionModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUnallocatedCashTransactionToAccount_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddUnallocatedCashTransactionToAccount(_transactionModel);
        }

        [TestMethod]
        public void AddTransaction_Should_Save_AtLeast_Once()
        {
            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
        }

        [TestMethod]
        public void UpdateTransaction_Should_Update_AtLeast_Once()
        {
            _transactionModel.Id = 2;
            _transactionModel.Closed = false;

            _transConfiguration.SetupMocForPersistence();
            _transConfiguration.MockEntity.Setup(s => s.Find(It.IsAny<Int32>()))
                .Returns(_transList.FirstOrDefault(x => x.ID == _transactionModel.Id));

            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.UpdateTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Billed, _transactionModel.Billed);
        }

        [TestMethod]
        public void AddCreditNoteTransaction_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = -1500;
            _transactionModel.Value = -1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddCreditNoteTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }

        [TestMethod]
        public void AddDebtorNoteTransaction_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.DBN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = 1500;
            _transactionModel.OpeningCurrencyValue = 1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddDebtorNoteTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }

        [TestMethod]
        public void AddUnallocatedCashTransactionToAccount_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.UNC.ToString();
            _transactionModel.OpeningValue = -1500;
            _transactionModel.Value = -1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddUnallocatedCashTransactionToAccount(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }

        [TestMethod]
        public void test()
        {
            WordDictionary oDict = new WordDictionary();
            var wordResult = new SortedList<string,string>();
            oDict.DictionaryFile = "en-GB.dic";
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling spell = new NetSpell.SpellChecker.Spelling();
            spell.Dictionary=oDict;
            char [] chdelimit={' ','\n', '\t', '\r'};

            using (var st=File.OpenText(@"C:\Users\Jean-PierreSegikwiye\Downloads\words-english.txt"))
            {
                string s = string.Empty;
                while ((s = st.ReadLine()) != null)
                {
                    foreach (var item in s.Split(chdelimit))
                    {
                        if (item.Length == 4  && spell.TestWord(item))
                        {
                            //wordResult.Add(s);
                            wordResult[s] = s;
                        }
                    }
                }
		 
	        }



            var d = wordResult.TakeWhile(x => x.Key != "Adam").SkipWhile(x => x.Key == "Abel").ToList();
          
            var startword = "abel";
            var endword="Adam";
            
            var result = new List<string>();
            result.Add(startword);
            var tt = d.FirstOrDefault().Key;
            foreach (var item in d)
            { 
                if (item.Key.Except(tt).Count()==1)
                {
                    result.Add(tt);                   
                }
                tt = item.Key;
            }
            result.Add(endword);
        }
    }
}