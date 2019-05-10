using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.BusinessLogic.Models.Enums;
using CodingChallenge.API.Common.Json;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace CodingChallenge.API.Tests.Models
{
    public class ChatbotRequestModelTests
    {
        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.NotSet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.NotSet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.NotSet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within 1 Month\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.WithInOneMonth)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"1-3 Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.OnetoThreeMonths)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"3-6 Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.ThreetoSixMonths)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Over 6 Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            PurchaseTimeFrames.OverSixMonths)]
        public void TestJsonMappingForPurchaseTimeFrameWithTolerantStringConversion(string json, PurchaseTimeFrames ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.PurchaseTimeFrame.Should().Be(ptf);
        }

        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LeadSourceBrandTypes.US)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"C-1CAN\"\r\n}",
            LeadSourceBrandTypes.CA)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"C-1\"\r\n}",
            LeadSourceBrandTypes.US)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n}",
            LeadSourceBrandTypes.US)]
        public void TestJsonMappingForLeadSourceBrandWithTolerantStringConversion(string json, LeadSourceBrandTypes ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.LeadSourceBrand.Should().Be(ptf);
        }

        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LevelTypes.NationalOrganic)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"CPTONE\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LevelTypes.NationalOrganic)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LevelTypes.NationalOrganic)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LevelTypes.NationalOrganic)]
        public void TestJsonMappingForLevelWithTolerantStringConversion(string json, LevelTypes ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.Level.Should().Be(ptf);
        }

        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            MarketingTypes.Internet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"CPTONE\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            MarketingTypes.Internet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            MarketingTypes.Internet)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            MarketingTypes.Internet)]
        public void TestJsonMappingForMarketingWithTolerantStringConversion(string json, MarketingTypes ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.MarketingType.Should().Be(ptf);
        }

        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LeadSourceTypes.CarpetOne)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CPTONE\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LeadSourceTypes.CarpetOne)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LeadSourceTypes.CarpetOne)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            LeadSourceTypes.CarpetOne)]
        public void TestJsonMappingForLeadSourceWithTolerantStringConversion(string json, LeadSourceTypes ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.LeadSource.Should().Be(ptf);
        }

        [Theory]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"Chatbot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            RequestTypes.Chatbot)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"CPTONE\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            RequestTypes.Chatbot)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            RequestTypes.Chatbot)]
        [InlineData(
            "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": null,\r\n  \"cleanHomePhone\": null,\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": null,\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"Within Two Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}",
            RequestTypes.Chatbot)]
        public void TestJsonMappingFoRequestTypeWithTolerantStringConversion(string json, RequestTypes ptf)
        {
            var obj = JsonConvert.DeserializeObject<ChatbotRequestModel>(json);
            obj.RequestType.Should().Be(ptf);
        }

        [Fact]
        public void TestConversion()
        {
            var serialized = JsonConvert.SerializeObject(
                new ChatbotRequestModel
                {
                    FirstName = "James",
                    LastName = "Test",
                    PostalCode = "11111",
                    StateProvince = "GA",
                    RequestType = RequestTypes.Chatbot,
                    LeadSource = LeadSourceTypes.CarpetOne,
                    MarketingType = MarketingTypes.Internet,
                    Level = LevelTypes.NationalOrganic,
                    InStoreWithinLast30Days = true,
                    LeadSourceUrl = "www.carpetone.com",
                    LeadSourceBrand = LeadSourceBrandTypes.US,
                    PurchaseTimeFrame = PurchaseTimeFrames.OnetoThreeMonths,
                    EmailAddress = "test@test.com",
                    ExpressedConsent = false,
                    HasOptedOutOfEmail = false
                    
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new NullToDefaultValueResolver(),
                    Formatting = Formatting.Indented
                });
            serialized.Should()
                .Be(
                    "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": \"\",\r\n  \"cleanHomePhone\": \"\",\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"ChatBot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": \"\",\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"1-3 Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"hasOptedOutOfEmail\": false,\r\n  \"expressedConsent\": false,\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}");

        }

        [Fact]
        public void TestConversionEmpty()
        {
            var serialized = JsonConvert.SerializeObject(
                new ChatbotRequestModel
                {
                    FirstName = "James",
                    LastName = "Test",
                    PostalCode = "11111",
                    StateProvince = "GA",
                    RequestType = RequestTypes.Chatbot,
                    LeadSource = LeadSourceTypes.CarpetOne,
                    MarketingType = MarketingTypes.Internet,
                    Level = LevelTypes.NationalOrganic,
                    InStoreWithinLast30Days = true,
                    LeadSourceUrl = "www.carpetone.com",
                    LeadSourceBrand = LeadSourceBrandTypes.US,
                    EmailAddress = "test@test.com",
                    ExpressedConsent = false,
                    HasOptedOutOfEmail = false
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new NullToDefaultValueResolver(),
                    Formatting = Formatting.Indented
                });
            serialized.Should()
                .Be(
                    "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": \"\",\r\n  \"cleanHomePhone\": \"\",\r\n  \"firstName\": \"James\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"ChatBot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": \"\",\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"hasOptedOutOfEmail\": false,\r\n  \"expressedConsent\": false,\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}");
        }

       
        [Fact]
        public void TestNullStringConversion()
        {
            var serialized = JsonConvert.SerializeObject(
                new ChatbotRequestModel
                {
                    FirstName = null,
                    LastName = "Test",
                    PostalCode = "11111",
                    StateProvince = "GA",
                    RequestType = RequestTypes.Chatbot,
                    LeadSource = LeadSourceTypes.CarpetOne,
                    MarketingType = MarketingTypes.Internet,
                    Level = LevelTypes.NationalOrganic,
                    InStoreWithinLast30Days = true,
                    LeadSourceUrl = "www.carpetone.com",
                    LeadSourceBrand = LeadSourceBrandTypes.US,
                    PurchaseTimeFrame = PurchaseTimeFrames.OnetoThreeMonths,
                    EmailAddress = "test@test.com"
                },
                new JsonSerializerSettings
                {
                    ContractResolver = new NullToDefaultValueResolver(),
                    Formatting = Formatting.Indented
                });
            serialized.Should()
                .Be(
                    "{\r\n  \"locationID\": null,\r\n  \"emailAddress\": \"test@test.com\",\r\n  \"city\": \"\",\r\n  \"cleanHomePhone\": \"\",\r\n  \"firstName\": \"\",\r\n  \"lastName\": \"Test\",\r\n  \"postalCode\": \"11111\",\r\n  \"stateProvince\": \"GA\",\r\n  \"requestType\": \"ChatBot\",\r\n  \"leadSource\": \"CarpetOne\",\r\n  \"marketing\": \"Internet\",\r\n  \"level\": \"NationalOrganic\",\r\n  \"opportunityNotes\": \"\",\r\n  \"inStoreWithinLast30Days\": true,\r\n  \"purchaseTimeFrame\": \"1-3 Months\",\r\n  \"leadSourceUrl\": \"www.carpetone.com\",\r\n  \"hasOptedOutOfEmail\": null,\r\n  \"expressedConsent\": null,\r\n  \"leadSourceBrand\": \"CPTONE\"\r\n}");
        }
    }
}