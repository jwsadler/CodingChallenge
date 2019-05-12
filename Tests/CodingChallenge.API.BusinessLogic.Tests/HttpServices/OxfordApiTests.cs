using System.Net;
using System.Net.Http;
using CodingChallenge.API.BusinessLogic.CustomSection;
using CodingChallenge.API.BusinessLogic.HttpServices.Oxford;
using CodingChallenge.API.BusinessLogic.Interfaces;
using CodingChallenge.API.BusinessLogic.Interfaces.Oxford;
using CodingChallenge.API.BusinessLogic.Models;
using CodingChallenge.API.Common.Interfaces;
using FluentAssertions;
using Moq;
using RichardSzalay.MockHttp;
using Xunit;

namespace CodingChallenge.API.BusinessLogic.Tests.HttpServices
{
    public class OxfordApiTests
    {
        [Theory]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.MethodNotAllowed)]
        public void Post_to_Oxford_Not_Success_Test(HttpStatusCode status)
        {

           
            var stubHttpClient = new Mock<IOxfordHttpWrapper>();
            stubHttpClient.Setup(p => p.GetOxfordResponse(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(status)
            {
                Content = new StringContent(OXFORD_RESPONSE)
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection {APILogging = new APILoggingElement {VerboseLogging = true}});

            var loggingService = new Mock<ILoggingService>();

            var logger = new Mock<ICodingChallengeApiLogger>();
            logger.Setup(p => p.Log()).Returns(loggingService.Object);
            
            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new HttpClient().DefaultRequestHeaders);

            var oxfordApiWrapper = new OxfordApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new OxfordApiServices(oxfordApiWrapper);

            var response = apiService.Oxford(new CodingChallengeRequestModel { Query = "red car" },true);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(status);
        }

        [Fact]
        public void Post_to_Oxford_Test()
        {
            var stubHttpClient = new Mock<IOxfordHttpWrapper>();
            stubHttpClient.Setup(p => p.GetOxfordResponse(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(OXFORD_RESPONSE)
            });

            var apiConfigurationHelper = new Mock<IAPIConfigurationHelper>();

            apiConfigurationHelper.Setup(p => p.APIConfiguration)
                .Returns(new APIConfigurationSection {APILogging = new APILoggingElement {VerboseLogging = true}});

            var logger = new Mock<ICodingChallengeApiLogger>();

            stubHttpClient.Setup(p => p.DefaultRequestHeaders)
                .Returns(new HttpClient().DefaultRequestHeaders);

            var oxfordApiWrapper = new OxfordApiWrapper(stubHttpClient.Object, apiConfigurationHelper.Object, logger.Object);

            var apiService = new OxfordApiServices(oxfordApiWrapper);

            var response = apiService.Oxford(new CodingChallengeRequestModel{Query="red car"},true);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        private const string OXFORD_RESPONSE = @"{
    ""id"": ""red"",
    ""metadata"": {
        ""operation"": ""retrieve"",
        ""provider"": ""Oxford University Press"",
        ""schema"": ""RetrieveEntry""
    },
    ""results"": [
        {
            ""id"": ""red"",
            ""language"": ""en-gb"",
            ""lexicalEntries"": [
                {
                    ""derivatives"": [
                        {
                            ""id"": ""reddy"",
                            ""text"": ""reddy""
                        },
                        {
                            ""id"": ""redly"",
                            ""text"": ""redly""
                        }
                    ],
                    ""entries"": [
                        {
                            ""etymologies"": [
                                ""Old English rēad, of Germanic origin; related to Dutchrood and Germanrot, from an Indo-European root shared by Latinrufus, ruber, Greekeruthros, and Sanskritrudhira‘red’""
                            ],
                            ""senses"": [
                                {
                                    ""definitions"": [
                                        ""of a colour at the end of the spectrum next to orange and opposite violet, as of blood, fire, or rubies""
                                    ],
                                    ""id"": ""m_en_gbus0852700.008"",
                                    ""shortDefinitions"": [
                                        ""of colour at end of spectrum next to orange and opposite violet, as of blood or rubies""
                                    ],
                                    ""subsenses"": [
                                        {
                                            ""definitions"": [
                                                ""(of a person or their face) flushed or rosy, especially with embarrassment, anger, or heat""
                                            ],
                                            ""id"": ""m_en_gbus0852700.012"",
                                            ""shortDefinitions"": [
                                                ""flushed or rosy""
                                            ],
                                            ""thesaurusLinks"": [
                                                {
                                                    ""entry_id"": ""red"",
                                                    ""sense_id"": ""t_en_gb0012232.002""
                                                }
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""(of a person's eyes) bloodshot or having pink rims, especially with tiredness or crying""
                                            ],
                                            ""examples"": [
                                                {
                                                    ""text"": ""her eyes were red and swollen""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.013"",
                                            ""shortDefinitions"": [
                                                ""bloodshot or having pink rims""
                                            ],
                                            ""thesaurusLinks"": [
                                                {
                                                    ""entry_id"": ""red"",
                                                    ""sense_id"": ""t_en_gb0012232.003""
                                                }
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""(of hair or fur) of a reddish-brown or orange-brown colour""
                                            ],
                                            ""id"": ""m_en_gbus0852700.014"",
                                            ""shortDefinitions"": [
                                                ""of reddish-brown colour""
                                            ],
                                            ""thesaurusLinks"": [
                                                {
                                                    ""entry_id"": ""red"",
                                                    ""sense_id"": ""t_en_gb0012232.004""
                                                }
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""(of a people) having reddish skin.""
                                            ],
                                            ""id"": ""m_en_gbus0852700.015"",
                                            ""registers"": [
                                                {
                                                    ""id"": ""dated"",
                                                    ""text"": ""Dated""
                                                },
                                                {
                                                    ""id"": ""offensive"",
                                                    ""text"": ""Offensive""
                                                }
                                            ],
                                            ""shortDefinitions"": [
                                                ""having reddish skin""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""of or denoting the suits hearts and diamonds in a pack of cards""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""cards"",
                                                    ""text"": ""Cards""
                                                }
                                            ],
                                            ""examples"": [
                                                {
                                                    ""text"": ""a red queen""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.017"",
                                            ""shortDefinitions"": [
                                                ""of suits hearts and diamonds in pack of cards""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""(of wine) made from dark grapes and coloured by their skins""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""wine"",
                                                    ""text"": ""Wine""
                                                }
                                            ],
                                            ""examples"": [
                                                {
                                                    ""text"": ""a glass of red wine""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.018"",
                                            ""shortDefinitions"": [
                                                ""(of wine) made from dark grapes and coloured by their skins""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""denoting a red light or flag used as a signal to stop.""
                                            ],
                                            ""id"": ""m_en_gbus0852700.019"",
                                            ""shortDefinitions"": [
                                                ""denoting red light or flag used as signal to stop""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""used to denote something forbidden, dangerous, or urgent""
                                            ],
                                            ""examples"": [
                                                {
                                                    ""text"": ""the force went on red alert""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.020"",
                                            ""shortDefinitions"": [
                                                ""used to denote something forbidden or urgent""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""(of a ski run) of the second-highest level of difficulty, as indicated by red markers positioned along it.""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""skiing"",
                                                    ""text"": ""Skiing""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.021"",
                                            ""shortDefinitions"": [
                                                ""(of ski run) of second-highest level of difficulty, as indicated by red markers""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""denoting one of three colours of quark.""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""physics"",
                                                    ""text"": ""Physics""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.022"",
                                            ""shortDefinitions"": [
                                                ""denoting one of three colours of quark""
                                            ]
                                        }
                                    ],
                                    ""thesaurusLinks"": [
                                        {
                                            ""entry_id"": ""red"",
                                            ""sense_id"": ""t_en_gb0012232.001""
                                        }
                                    ]
                                },
                                {
                                    ""definitions"": [
                                        ""communist or socialist (used especially during the Cold War with reference to the Soviet Union)""
                                    ],
                                    ""domains"": [
                                        {
                                            ""id"": ""politics"",
                                            ""text"": ""Politics""
                                        }
                                    ],
                                    ""examples"": [
                                        {
                                            ""text"": ""the era of nuclear anxiety, the red scare and covert CIA plots""
                                        }
                                    ],
                                    ""id"": ""m_en_gbus0852700.024"",
                                    ""registers"": [
                                        {
                                            ""id"": ""informal"",
                                            ""text"": ""Informal""
                                        },
                                        {
                                            ""id"": ""derogatory"",
                                            ""text"": ""Derogatory""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""communist or socialist""
                                    ],
                                    ""variantForms"": [
                                        {
                                            ""text"": ""Red""
                                        }
                                    ]
                                },
                                {
                                    ""definitions"": [
                                        ""involving bloodshed or violence""
                                    ],
                                    ""id"": ""m_en_gbus0852700.028"",
                                    ""registers"": [
                                        {
                                            ""id"": ""archaic"",
                                            ""text"": ""Archaic""
                                        },
                                        {
                                            ""id"": ""literary"",
                                            ""text"": ""Literary""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""involving bloodshed or violence""
                                    ]
                                },
                                {
                                    ""crossReferenceMarkers"": [
                                        ""Contrasted with school""
                                    ],
                                    ""crossReferences"": [
                                        {
                                            ""id"": ""school"",
                                            ""text"": ""school"",
                                            ""type"": ""relates to""
                                        }
                                    ],
                                    ""definitions"": [
                                        ""(of a Xhosa) coming from a traditional tribal culture""
                                    ],
                                    ""examples"": [
                                        {
                                            ""text"": ""a red Xhosa wife spends several years in her mother-in-law's homestead""
                                        }
                                    ],
                                    ""id"": ""m_en_gbus0852700.031"",
                                    ""regions"": [
                                        {
                                            ""id"": ""south_african"",
                                            ""text"": ""South_African""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""coming from traditional tribal culture""
                                    ],
                                    ""variantForms"": [
                                        {
                                            ""text"": ""red-blanket""
                                        }
                                    ]
                                }
                            ]
                        }
                    ],
                    ""language"": ""en-gb"",
                    ""lexicalCategory"": {
                        ""id"": ""adjective"",
                        ""text"": ""Adjective""
                    },
                    ""pronunciations"": [
                        {
                            ""audioFile"": ""http://audio.oxforddictionaries.com/en/mp3/red_gb_5.mp3"",
                            ""dialects"": [
                                ""British English""
                            ],
                            ""phoneticNotation"": ""IPA"",
                            ""phoneticSpelling"": ""rɛd""
                        }
                    ],
                    ""text"": ""red""
                },
                {
                    ""derivatives"": [
                        {
                            ""id"": ""reddy"",
                            ""text"": ""reddy""
                        },
                        {
                            ""id"": ""redly"",
                            ""text"": ""redly""
                        }
                    ],
                    ""entries"": [
                        {
                            ""senses"": [
                                {
                                    ""definitions"": [
                                        ""red colour or pigment""
                                    ],
                                    ""id"": ""m_en_gbus0852700.036"",
                                    ""notes"": [
                                        {
                                            ""text"": ""mass noun"",
                                            ""type"": ""grammaticalNote""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""red colour or pigment""
                                    ],
                                    ""subsenses"": [
                                        {
                                            ""definitions"": [
                                                ""red clothes or material""
                                            ],
                                            ""id"": ""m_en_gbus0852700.039"",
                                            ""shortDefinitions"": [
                                                ""red clothes or material""
                                            ]
                                        }
                                    ]
                                },
                                {
                                    ""definitions"": [
                                        ""a red thing""
                                    ],
                                    ""id"": ""m_en_gbus0852700.042"",
                                    ""shortDefinitions"": [
                                        ""red thing""
                                    ],
                                    ""subsenses"": [
                                        {
                                            ""definitions"": [
                                                ""a red wine""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""wine"",
                                                    ""text"": ""Wine""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.043"",
                                            ""shortDefinitions"": [
                                                ""red wine""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""a red ball in snooker or billiards.""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""billiards"",
                                                    ""text"": ""Billiards""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.044"",
                                            ""shortDefinitions"": [
                                                ""red ball in snooker or billiards""
                                            ]
                                        },
                                        {
                                            ""definitions"": [
                                                ""a red light.""
                                            ],
                                            ""domains"": [
                                                {
                                                    ""id"": ""motoring"",
                                                    ""text"": ""Motoring""
                                                }
                                            ],
                                            ""id"": ""m_en_gbus0852700.045"",
                                            ""shortDefinitions"": [
                                                ""red light""
                                            ]
                                        }
                                    ]
                                },
                                {
                                    ""definitions"": [
                                        ""a communist or socialist.""
                                    ],
                                    ""domains"": [
                                        {
                                            ""id"": ""politics"",
                                            ""text"": ""Politics""
                                        }
                                    ],
                                    ""id"": ""m_en_gbus0852700.047"",
                                    ""registers"": [
                                        {
                                            ""id"": ""informal"",
                                            ""text"": ""Informal""
                                        },
                                        {
                                            ""id"": ""derogatory"",
                                            ""text"": ""Derogatory""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""communist or socialist""
                                    ],
                                    ""thesaurusLinks"": [
                                        {
                                            ""entry_id"": ""red"",
                                            ""sense_id"": ""t_en_gb0012232.005""
                                        }
                                    ],
                                    ""variantForms"": [
                                        {
                                            ""text"": ""Red""
                                        }
                                    ]
                                },
                                {
                                    ""definitions"": [
                                        ""the situation of owing money to a bank or making a loss in a business operation""
                                    ],
                                    ""domains"": [
                                        {
                                            ""id"": ""commerce"",
                                            ""text"": ""Commerce""
                                        }
                                    ],
                                    ""examples"": [
                                        {
                                            ""text"": ""the company was £4 million in the red""
                                        },
                                        {
                                            ""text"": ""moving the health authority out of the red will be a huge challenge""
                                        },
                                        {
                                            ""text"": ""small declines in revenue can soon send an airline plunging into the red""
                                        }
                                    ],
                                    ""id"": ""m_en_gbus0852700.050"",
                                    ""notes"": [
                                        {
                                            ""text"": ""the red"",
                                            ""type"": ""wordFormNote""
                                        }
                                    ],
                                    ""shortDefinitions"": [
                                        ""situation of owing money to bank or making loss in business""
                                    ]
                                }
                            ]
                        }
                    ],
                    ""language"": ""en-gb"",
                    ""lexicalCategory"": {
                        ""id"": ""noun"",
                        ""text"": ""Noun""
                    },
                    ""pronunciations"": [
                        {
                            ""audioFile"": ""http://audio.oxforddictionaries.com/en/mp3/red_gb_5.mp3"",
                            ""dialects"": [
                                ""British English""
                            ],
                            ""phoneticNotation"": ""IPA"",
                            ""phoneticSpelling"": ""rɛd""
                        }
                    ],
                    ""text"": ""red""
                }
            ],
            ""type"": ""headword"",
            ""word"": ""red""
        }
    ],
    ""word"": ""red""
}";
    }
}