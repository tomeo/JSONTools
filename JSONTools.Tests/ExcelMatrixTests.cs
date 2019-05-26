using System;
using System.Collections.Generic;
using Xunit;

namespace JSONTools.Tests
{
    public class ExcelMatrixTests
    {
        public class ToJsonMethod
        {
            [Fact]
            public void ShouldHandleNullInput()
            {
                Assert.Throws<ArgumentException>(() => ExcelMatrix.ToJson(null));
            }

            [Fact]
            public void ShouldHandleStringsAndNumericTypes()
            {
                var values = new List<List<object>>
                {
                    new List<object> {"header1", "header2", "header3", "header4"},
                    new List<object> {"r1v1", "r1v2", "r1v3", 1},
                    new List<object> {"r2v1", "r2v2", "r2v3", 2},
                    new List<object> {"r3v1", "r3v2", "r3v3", 3},
                };

                var result = ExcelMatrix.ToJson(values);

                Assert.Equal(
                    "[{\"header1\":\"r1v1\",\"header2\":\"r1v2\",\"header3\":\"r1v3\",\"header4\":1},{\"header1\":\"r2v1\",\"header2\":\"r2v2\",\"header3\":\"r2v3\",\"header4\":2},{\"header1\":\"r3v1\",\"header2\":\"r3v2\",\"header3\":\"r3v3\",\"header4\":3}]",
                    result);
            }
        }

        public class DeserializeMethod
        {
            [Fact]
            public void ShouldHandleNullInput()
            {
                Assert.Throws<ArgumentException>(() => ExcelMatrix.Deserialize<MockData>(null));
            }

            [Fact]
            public void ShouldDeserializeToType()
            {
                var values = new List<List<object>>
                {
                    new List<object> {"header1", "header2", "header3", "header4"},
                    new List<object> {"r1v1", "r1v2", "r1v3", 1},
                    new List<object> {"r2v1", "r2v2", "r2v3", 2},
                    new List<object> {"r3v1", "r3v2", "r3v3", 3},
                };

                var result = ExcelMatrix.Deserialize<MockData>(values);

                Assert.Equal("r1v1", result[0].Header1);
                Assert.Equal("r1v2", result[0].Header2);
                Assert.Equal("r1v3", result[0].Header3);
                Assert.Equal(1, result[0].Header4);

                Assert.Equal("r2v1", result[1].Header1);
                Assert.Equal("r2v2", result[1].Header2);
                Assert.Equal("r2v3", result[1].Header3);
                Assert.Equal(2, result[1].Header4);

                Assert.Equal("r3v1", result[2].Header1);
                Assert.Equal("r3v2", result[2].Header2);
                Assert.Equal("r3v3", result[2].Header3);
                Assert.Equal(3, result[2].Header4);
            }
        }
    }
    
    public class MockData
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public int Header4 { get; set; }
    }
}
