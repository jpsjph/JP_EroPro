using System.Collections.Generic;
using Common.Services.ViewModel;
using Domain.Model;
using Ero57_API.Tests.Configuration;
using Ero57_API.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ero57_API.Tests
{
    [TestClass]
   public class ScoreMatrixTest
    {
        private GenericConfiguration<DebtorPerformances> _debtorPerfConfig;
        private List<DebtorPerformances> _debtorPerformanceList;
        private DebtorPerformancesViewModel _debtorPerformanceModel;

        [TestInitialize]
        public void TearUp()
        {
            _debtorPerfConfig = new GenericConfiguration<DebtorPerformances>();
            _debtorPerfConfig.Setup();
            _debtorPerformanceList = DebtorPerformanceHelper.GetDebtorPerformanceList();
            _debtorPerformanceModel = DebtorPerformanceHelper.GetDebtorPerformanceViewModel();
        }
    }
}
