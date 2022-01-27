using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnalizatorTest
{
    [TestClass]
    public class AnalizatorTests
    {
        [TestMethod]
        public void Analizator_NullAnalizarot()
        {
            string Analizator = null;
            String Analiz = "Вич";
            string resault  = "(400, $Analyzer with name " + Analizator + " not found)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator,Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }

        [TestMethod]
        public void Analizator_AnalizatorVipolnayetZakaz()
        {
            string Analizator = "Улучшеный";
            String Analiz = "Вич";
            string resault = "(400, Analyzer is busy)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }

        [TestMethod]
        public void Analizator_AnalizatorCanNotThisYslug()
        {
            string Analizator = "Стандарный";
            String Analiz = "Спид";
            string resault ="(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatorCanNotThisYslugSecond()
        {
            string Analizator = "Стандарный";
            String Analiz = "Альбумин";
            string resault = "(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatorCanNotThisYslugThird()
        {
            string Analizator = "Упрощённый";
            String Analiz = "Вич";
            string resault = "(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatorCanNotThisYslugFourth()
        {
            string Analizator = "Упрощённый";
            String Analiz = "Альбумин";
            string resault = "(400,Analyzer can not do this order.May be order contains services which analyzer does not support.)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatordoThisYslugFirst()
        {
            string Analizator = "Стандарный";
            String Analiz = "Вич";
            string resault = "True";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatordoThisYslugSecond()
        {
            string Analizator = "Стандарный";
            String Analiz = "Гепатит";
            string resault = "True";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatordoThisYslugThird()
        {
            string Analizator = "Упрощённый";
            String Analiz = "Спид";
            string resault = "True";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
        [TestMethod]
        public void Analizator_AnalizatorNotWorking()
        {
            string Analizator = "Устаревший";
            String Analiz = "Спид";
            string resault = "(400,Analyzer is not working.)";
            //act
            LaboratoryAnalizatorTest.LaboratoryAnalizator c = new LaboratoryAnalizatorTest.LaboratoryAnalizator();
            string actual = c.Analizator(Analizator, Analiz);
            //assert
            Assert.AreEqual(resault, actual);
        }
    }

    }

