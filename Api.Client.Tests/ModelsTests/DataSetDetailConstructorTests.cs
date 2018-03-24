using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nexosis.Api.Client.Model;
using Xunit;

namespace Api.Client.Tests.ModelsTests
{
    public class DataSetDetailConstructorTests
    {
        #region classes used for testing
        internal class TestObject
        {
            public string Foo { get; set; }
            public int Bar { get; set; }
        }

        internal class ComplexTestObject
        {
            public TestObject Complex { get; set; }
            public string Abc { get; set; }
        }
        #endregion

        [Fact]
        public void PopulatedByObjects()
        {
            // arrange
            var testObject1 = new TestObject {Foo = Guid.NewGuid().ToString(), Bar = 123 };
            var testObject2 = new TestObject {Foo = Guid.NewGuid().ToString(), Bar = 456 };
            var list = new List<dynamic> {testObject1, testObject2};

            // act
            var dataSetDetail = new DataSetDetail(list);

            // assert
            Assert.True(dataSetDetail.Data.Any(d => d.ContainsKey("Foo")));
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Foo") && d.ContainsValue(testObject1.Foo)) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Bar") && d.ContainsValue(testObject1.Bar.ToString())) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Foo") && d.ContainsValue(testObject2.Foo)) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Bar") && d.ContainsValue(testObject2.Bar.ToString())) != null);
        }

        [Fact]
        public void CantBePopulatedByComplexObjects()
        {
            // arrange
            var testObject1 = new ComplexTestObject
            {
                Abc = Guid.NewGuid().ToString(),
                Complex = new TestObject {Foo = Guid.NewGuid().ToString(), Bar = 789 }

            };
            var list = new List<dynamic> {testObject1 };

            // act
            void Act() { new DataSetDetail(list); }

            // assert
            Assert.Throws<JsonReaderException>((Action) Act);
        }

        [Fact]
        public void PopulatedByAnonymousObjects()
        {
            // arrange
            var testObject1 = new { Foo = Guid.NewGuid().ToString(), Bar = 777 };
            var testObject2 = new { Foo = Guid.NewGuid().ToString(), Bar = 888 };
            var list = new List<dynamic> { testObject1, testObject2 };

            // act
            var dataSetDetail = new DataSetDetail(list);

            // assert
            Assert.True(dataSetDetail.Data.Any(d => d.ContainsKey("Foo")));
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Foo") && d.ContainsValue(testObject1.Foo)) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Bar") && d.ContainsValue(testObject1.Bar.ToString())) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Foo") && d.ContainsValue(testObject2.Foo)) != null);
            Assert.True(dataSetDetail.Data.SingleOrDefault(d => d.ContainsKey("Bar") && d.ContainsValue(testObject2.Bar.ToString())) != null);
        }
    }
}