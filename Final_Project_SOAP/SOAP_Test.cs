using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceReference1;
using System;
using System.Collections.Generic;

namespace Final_Project_SOAP
{
    [TestClass]
    public class SOAP_Test
    {
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryList =
                new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void CountryNameandISOCode()
        {
            var locList = ListOfCountryNamesByCode();
            var locListRandom = GetRandomRecord(locList);

            var fullCountryInfo = countryList.FullCountryInfo(locListRandom.sISOCode);

            Assert.AreEqual(fullCountryInfo.sISOCode, locListRandom.sISOCode);
            Assert.AreEqual(fullCountryInfo.sName, locListRandom.sName);
        }

        [TestMethod]
        public void CountryISOCode()
        {
            var locList = ListOfCountryNamesByCode();
            List<tCountryCodeAndName> countryRecords = new List<tCountryCodeAndName>();

            for (int record = 0; record < 5; record++)
            {
                countryRecords.Add(GetRandomRecord(locList));
            }

            foreach (var countryRecord in countryRecords)
            {
                var isoCode = countryList.CountryISOCode(countryRecord.sName);
                Assert.AreEqual(isoCode, countryRecord.sISOCode);
            }
        }

        public tCountryCodeAndName GetRandomRecord(tCountryCodeAndName[] data)
        {
            var random = new Random();
            int next = random.Next(data.Length);

            var locList = data[next];

            return locList;
        }

        public tCountryCodeAndName[] ListOfCountryNamesByCode()
        {
            var locList = countryList.ListOfCountryNamesByCode();

            return locList;
        }
    }
}