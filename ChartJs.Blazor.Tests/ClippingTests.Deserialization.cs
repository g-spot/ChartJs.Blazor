﻿using ChartJs.Blazor.ChartJS.LineChart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace ChartJs.Blazor.Tests
{
    public partial class ClippingTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        [InlineData(100)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void Deserialize_AllEdgesEqual_FromRoot(int value)
        {
            // Arrange
            string json = value.ToString(CultureInfo.InvariantCulture);
            Clipping expected = new Clipping(value);

            // Act
            Clipping deserialized = JsonConvert.DeserializeObject<Clipping>(json);

            // Assert
            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void Deserialize_DifferentEdges_FromRoot()
        {
            // Arrange
            const string json = "{\"Bottom\":10,\"Left\":-100,\"Top\":0,\"Right\":false}";
            Clipping expected = new Clipping(10, -100, 0, null);

            // Act
            Clipping deserialized = JsonConvert.DeserializeObject<Clipping>(json);

            // Assert
            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void Deserialize_DifferentEdges_MissingMembers_FromRoot()
        {
            // Arrange
            const string json = "{\"Bottom\":-123,\"Right\":false}";
            Clipping expected = new Clipping(-123, null, null, null);

            // Act
            Clipping deserialized = JsonConvert.DeserializeObject<Clipping>(json);

            // Assert
            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void Deserialize_DifferentEdges_AdditionalMembers_FromRoot()
        {
            // Arrange
            const string json = "{\"Left\":500,\"Top\":false,\"ABC\":19.2}";
            Clipping expected = new Clipping(null, 500, null, null);

            // Act
            Clipping deserialized = JsonConvert.DeserializeObject<Clipping>(json);

            // Assert
            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void Deserialize_Double_ThrowsJsonSerializationException()
        {
            // Arrange
            const string json = "19.2";

            // Act & Assert
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<Clipping>(json));
        }

        [Fact]
        public void Deserialize_Array_ThrowsJsonSerializationException()
        {
            // Arrange
            const string json = "[]";

            // Act & Assert
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<Clipping>(json));
        }

        [Fact]
        public void Deserialize_String_ThrowsJsonSerializationException()
        {
            // Arrange
            const string json = "\"asdf\"";

            // Act & Assert
            Assert.Throws<JsonSerializationException>(() => JsonConvert.DeserializeObject<Clipping>(json));
        }
    }
}
