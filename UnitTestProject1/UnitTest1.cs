using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfApp1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private CalculationService _calculationService;

        [TestInitialize]
        public void Setup()
        {
            _calculationService = new CalculationService();
        }

        

        [TestMethod]
        public void Calculate_WithNegativeNumbers_ReturnsNull()
        {
            double width = -10.5;
            double height = 20.0;
            bool isAluminum = true;

            var result = _calculationService.Calculate(width, height, isAluminum);

            Assert.IsNull(result);
            Assert.AreEqual(0, _calculationService.CalculationHistory.Count);
        }

        [TestMethod]
        public void Calculate_WithEmptyOrNullInputs_ReturnsNull()
        {
            // Оба параметра равны 0 (аналог "пустых" полей)
            var result1 = _calculationService.Calculate(0, 0, true);
            Assert.IsNull(result1);

            // Один параметр равен 0, другой очень маленькое значение
            var result2 = _calculationService.Calculate(0, 0.0001, false);
            Assert.IsNull(result2);

            // Проверка что метод не должен падать при минимальных double значениях
            var result3 = _calculationService.Calculate(double.MinValue, double.MinValue, true);
            Assert.IsNull(result3); // Ожидаем null для отрицательных значений
        }

        [TestMethod]
        public void Calculate_WithVeryLargePositiveNumbers_HandlesCorrectly()
        {
            double width = 1e308; // Очень большое число
            double height = 10.0;
            bool isAluminum = true;

            var result = _calculationService.Calculate(width, height, isAluminum);

            // Проверяем либо корректный расчет, либо null при переполнении
            if (result != null)
            {
                Assert.IsTrue(double.IsInfinity(result.TotalCost) ||
                             result.TotalCost > 0);
            }
            // Если сервис возвращает null при переполнении - это тоже валидно
        }

        
    }
}