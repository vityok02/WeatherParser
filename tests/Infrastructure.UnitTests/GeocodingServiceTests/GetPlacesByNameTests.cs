﻿using Domain.Abstract;
using Domain.Locations;
using FluentAssertions;
using Infrastructure.Services.Geocoding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;

namespace Infrastructure.UnitTests.GeocodingServiceTests;

public class GetPlacesByNameTests
{
    private readonly Mock<ILogger<GeocodingService>> _loggerMock = new();
    private readonly Mock<IOptions<GeocodingConfiguration>> _options = new();
    private readonly MockHttpMessageHandler _handlerMock = new();

    private GeocodingConfiguration Cfg => _options.Object.Value;
    private const string _locationName = "Malynivka, Lityn";
    private readonly Uri _url;
    

    public GetPlacesByNameTests()
    {
        _options.Setup(c => c.Value).Returns(new GeocodingConfiguration
        {
            Path = "https://path",
            Token = "token"
        });

        _url = new Uri($"{Cfg.Path}/{_locationName}.json?key={Cfg.Token}");
    }

    [Fact]
    public async Task GetPlacesByName_Success_ReturnsLocations()
    {
        var content = "{\r\n    \"type\": \"FeatureCollection\",\r\n    \"features\": [\r\n        {\r\n            \"type\": \"Feature\",\r\n            \"properties\": {\r\n                \"ref\": \"osm:r7940992\",\r\n                \"country_code\": \"ua\",\r\n                \"wikidata\": \"Q4277876\",\r\n                \"kind\": \"admin_area\"\r\n            },\r\n            \"geometry\": {\r\n                \"type\": \"Point\",\r\n                \"coordinates\": [\r\n                    28.174439817667007,\r\n                    49.32964015394323\r\n                ]\r\n            },\r\n            \"bbox\": [\r\n                28.15541423857212,\r\n                49.32000700984586,\r\n                28.18631026893854,\r\n                49.339387414916\r\n            ],\r\n            \"center\": [\r\n                28.174439817667007,\r\n                49.32964015394323\r\n            ],\r\n            \"place_name\": \"Малинівка, Літинська селищна громада, Україна\",\r\n            \"place_type\": [\r\n                \"locality\"\r\n            ],\r\n            \"relevance\": 0.96,\r\n            \"id\": \"locality.134700\",\r\n            \"text\": \"Малинівка\",\r\n            \"matching_text\": \"Malynivka\",\r\n            \"matching_place_name\": \"Malynivka, Lityn Settlement Hromada, Україна\",\r\n            \"context\": [\r\n                {\r\n                    \"ref\": \"osm:r12410748\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"municipality.182984\",\r\n                    \"text\": \"Літинська селищна громада\",\r\n                    \"wikidata\": \"Q61955315\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r11914591\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"county.14718\",\r\n                    \"text\": \"Вінницький район\",\r\n                    \"wikidata\": \"Q102420838\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r90726\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"region.214\",\r\n                    \"text\": \"Вінницька область\",\r\n                    \"wikidata\": \"Q166709\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r60199\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"country.158\",\r\n                    \"text\": \"Україна\",\r\n                    \"wikidata\": \"Q212\",\r\n                    \"kind\": \"admin_area\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"type\": \"Feature\",\r\n            \"properties\": {\r\n                \"ref\": \"osm:n3009722175\",\r\n                \"country_code\": \"pl\",\r\n                \"kind\": \"place\",\r\n                \"osm:place_type\": \"hamlet\"\r\n            },\r\n            \"geometry\": {\r\n                \"type\": \"Point\",\r\n                \"coordinates\": [\r\n                    20.040732845664028,\r\n                    52.19598013687401\r\n                ]\r\n            },\r\n            \"bbox\": [\r\n                20.040732845664028,\r\n                52.19598013687401,\r\n                20.040732845664028,\r\n                52.19598013687401\r\n            ],\r\n            \"center\": [\r\n                20.040732845664028,\r\n                52.19598013687401\r\n            ],\r\n            \"place_name\": \"Litynek, Lenartów, Polska\",\r\n            \"place_type\": [\r\n                \"place\"\r\n            ],\r\n            \"relevance\": 0.5,\r\n            \"id\": \"place.51426\",\r\n            \"text\": \"Litynek\",\r\n            \"context\": [\r\n                {\r\n                    \"ref\": \"osm:r3950018\",\r\n                    \"country_code\": \"pl\",\r\n                    \"id\": \"municipal_district.16541\",\r\n                    \"text\": \"Lenartów\",\r\n                    \"wikidata\": \"Q6522285\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r1462125\",\r\n                    \"country_code\": \"pl\",\r\n                    \"id\": \"municipality.67095\",\r\n                    \"text\": \"gmina Kocierzew Południowy\",\r\n                    \"wikidata\": \"Q2264662\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r1403468\",\r\n                    \"country_code\": \"pl\",\r\n                    \"id\": \"county.1309\",\r\n                    \"text\": \"powiat łowicki\",\r\n                    \"wikidata\": \"Q1144093\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r224458\",\r\n                    \"country_code\": \"pl\",\r\n                    \"id\": \"region.161\",\r\n                    \"text\": \"województwo łódzkie\",\r\n                    \"wikidata\": \"Q54158\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r49715\",\r\n                    \"country_code\": \"pl\",\r\n                    \"id\": \"country.63\",\r\n                    \"text\": \"Polska\",\r\n                    \"wikidata\": \"Q36\",\r\n                    \"kind\": \"admin_area\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"type\": \"Feature\",\r\n            \"properties\": {\r\n                \"ref\": \"osm:n337672415\",\r\n                \"country_code\": \"ua\",\r\n                \"wikidata\": \"Q4277864\",\r\n                \"kind\": \"place\",\r\n                \"osm:place_type\": \"village\"\r\n            },\r\n            \"geometry\": {\r\n                \"type\": \"Point\",\r\n                \"coordinates\": [\r\n                    36.47631794214249,\r\n                    47.67139112038375\r\n                ]\r\n            },\r\n            \"bbox\": [\r\n                36.47631794214249,\r\n                47.67139112038375,\r\n                36.47631794214249,\r\n                47.67139112038375\r\n            ],\r\n            \"center\": [\r\n                36.47631794214249,\r\n                47.67139112038375\r\n            ],\r\n            \"place_name\": \"Малинівка, Малинівська сільська громада, Україна\",\r\n            \"place_type\": [\r\n                \"place\"\r\n            ],\r\n            \"relevance\": 0.48,\r\n            \"id\": \"place.204167\",\r\n            \"text\": \"Малинівка\",\r\n            \"matching_text\": \"Malynivka\",\r\n            \"matching_place_name\": \"Malynivka, Малинівська сільська громада, Україна\",\r\n            \"context\": [\r\n                {\r\n                    \"ref\": \"osm:r12316819\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"municipality.181952\",\r\n                    \"text\": \"Малинівська сільська громада\",\r\n                    \"wikidata\": \"Q63401067\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r11857109\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"county.14720\",\r\n                    \"text\": \"Пологівський район\",\r\n                    \"wikidata\": \"Q103821944\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r71980\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"region.74\",\r\n                    \"text\": \"Запорізька область\",\r\n                    \"wikidata\": \"Q171334\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r60199\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"country.158\",\r\n                    \"text\": \"Україна\",\r\n                    \"wikidata\": \"Q212\",\r\n                    \"kind\": \"admin_area\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"type\": \"Feature\",\r\n            \"properties\": {\r\n                \"ref\": \"osm:n256616338\",\r\n                \"country_code\": \"ua\",\r\n                \"wikidata\": \"Q4475102\",\r\n                \"kind\": \"place\",\r\n                \"osm:place_type\": \"village\"\r\n            },\r\n            \"geometry\": {\r\n                \"type\": \"Point\",\r\n                \"coordinates\": [\r\n                    37.40977965295315,\r\n                    48.335117919637405\r\n                ]\r\n            },\r\n            \"bbox\": [\r\n                37.40977965295315,\r\n                48.335117919637405,\r\n                37.40977965295315,\r\n                48.335117919637405\r\n            ],\r\n            \"center\": [\r\n                37.40977965295315,\r\n                48.335117919637405\r\n            ],\r\n            \"place_name\": \"Малинівка, Гродівська селищна громада, Україна\",\r\n            \"place_type\": [\r\n                \"place\"\r\n            ],\r\n            \"relevance\": 0.48,\r\n            \"id\": \"place.2034973\",\r\n            \"text\": \"Малинівка\",\r\n            \"matching_text\": \"Malynivka\",\r\n            \"matching_place_name\": \"Malynivka, Гродівська селищна громада, Україна\",\r\n            \"context\": [\r\n                {\r\n                    \"ref\": \"osm:r13285836\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"municipality.186392\",\r\n                    \"text\": \"Гродівська селищна громада\",\r\n                    \"wikidata\": \"Q108421488\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r11984324\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"county.14775\",\r\n                    \"text\": \"Покровський район\",\r\n                    \"wikidata\": \"Q103821232\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r71973\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"region.73\",\r\n                    \"text\": \"Донецька область\",\r\n                    \"wikidata\": \"Q2012050\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r60199\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"country.158\",\r\n                    \"text\": \"Україна\",\r\n                    \"wikidata\": \"Q212\",\r\n                    \"kind\": \"admin_area\"\r\n                }\r\n            ]\r\n        },\r\n        {\r\n            \"type\": \"Feature\",\r\n            \"properties\": {\r\n                \"ref\": \"osm:n256618266\",\r\n                \"country_code\": \"ua\",\r\n                \"wikidata\": \"Q4277894\",\r\n                \"kind\": \"place\",\r\n                \"osm:place_type\": \"village\"\r\n            },\r\n            \"geometry\": {\r\n                \"type\": \"Point\",\r\n                \"coordinates\": [\r\n                    37.778195478022106,\r\n                    48.73707460368861\r\n                ]\r\n            },\r\n            \"bbox\": [\r\n                37.778195478022106,\r\n                48.73707460368861,\r\n                37.778195478022106,\r\n                48.73707460368861\r\n            ],\r\n            \"center\": [\r\n                37.778195478022106,\r\n                48.73707460368861\r\n            ],\r\n            \"place_name\": \"Малинівка, Миколаївська міська громада, Україна\",\r\n            \"place_type\": [\r\n                \"place\"\r\n            ],\r\n            \"relevance\": 0.48,\r\n            \"id\": \"place.3256942\",\r\n            \"text\": \"Малинівка\",\r\n            \"matching_text\": \"Malynivka\",\r\n            \"matching_place_name\": \"Malynivka, Миколаївська міська громада, Україна\",\r\n            \"context\": [\r\n                {\r\n                    \"ref\": \"osm:r13630683\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"municipality.187061\",\r\n                    \"text\": \"Миколаївська міська громада\",\r\n                    \"wikidata\": \"Q32824157\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r11978037\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"county.14787\",\r\n                    \"text\": \"Краматорський район\",\r\n                    \"wikidata\": \"Q97495623\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r71973\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"region.73\",\r\n                    \"text\": \"Донецька область\",\r\n                    \"wikidata\": \"Q2012050\",\r\n                    \"kind\": \"admin_area\"\r\n                },\r\n                {\r\n                    \"ref\": \"osm:r60199\",\r\n                    \"country_code\": \"ua\",\r\n                    \"id\": \"country.158\",\r\n                    \"text\": \"Україна\",\r\n                    \"wikidata\": \"Q212\",\r\n                    \"kind\": \"admin_area\"\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"query\": [\r\n        \"malynivka\",\r\n        \"lityn\"\r\n    ],\r\n    \"attribution\": \"<a href=\\\"https://www.maptiler.com/copyright/\\\" target=\\\"_blank\\\">&copy; MapTiler</a> <a href=\\\"https://www.openstreetmap.org/copyright\\\" target=\\\"_blank\\\">&copy; OpenStreetMap contributors</a>\"\r\n}";

        _handlerMock.When(_url.ToString())
            .Respond(HttpStatusCode.OK, "application/json", content);

        var client = new HttpClient(_handlerMock);

        var service = new GeocodingService(
            _loggerMock.Object, _options.Object, client);

        //Act
        var result = await service.GetPlacesByName(_locationName, CancellationToken.None);

        //Assert
        result.Should()
            .BeOfType<Result<Location[]>>().Which.IsSuccess.Should().BeTrue();

        result.Value.Should()
            .BeOfType<Location[]>()
            .Should().NotBeNull();
    }

    [Fact]
    public async Task GetPlacesByName_ContentEmpty_ReturnsFailure()
    {
        _handlerMock.When(_url.ToString())
            .Respond(HttpStatusCode.OK, "application/json", "{}");

        var service = new GeocodingService(
            _loggerMock.Object, _options.Object, _handlerMock.ToHttpClient());

        var result = await service.GetPlacesByName(_locationName, CancellationToken.None);

        result.Should()
            .BeOfType<Result<Location[]>>().Which.IsFailure.Should().BeTrue();

        result.Error.Should()
            .Be(GeocodingErrors.LocationsNull);
    }

    [Fact]
    public async Task GetPlacesByName_HttpException_ReturnsFailure()
    {
        _handlerMock.When(_url.ToString())
            .Respond(HttpStatusCode.InternalServerError);

        var service = new GeocodingService(
            _loggerMock.Object, _options.Object, _handlerMock.ToHttpClient());

        var result = await service.GetPlacesByName(_locationName, CancellationToken.None);

        result.Should()
            .BeOfType<Result<Location[]>>().Which.IsFailure.Should().BeTrue();

        result.Error.Should()
            .Be(GeocodingErrors.HttpRequestError);
    }

    [Fact]
    public async Task GetPlacesByName_JsonException_ReturnsFailure()
    {
        _handlerMock.When(_url.ToString())
            .Respond(HttpStatusCode.OK, "application/json", "{fake}");

        var service = new GeocodingService(
            _loggerMock.Object, _options.Object, _handlerMock.ToHttpClient());

        var result = await service.GetPlacesByName(_locationName, CancellationToken.None);

        result.Should()
            .BeOfType<Result<Location[]>>().Which.IsFailure.Should().BeTrue();

        result.Error.Should()
            .Be(GeocodingErrors.DeserializationError);
    }

    [Fact]
    public async Task GetPlacesByName_UnexpectedException_ReturnsFailure()
    {
        _handlerMock.When(_url.ToString())
            .Respond(HttpStatusCode.OK, "application/json", "{}")
            .Throw(new Exception());

        var service = new GeocodingService(
            _loggerMock.Object, _options.Object, _handlerMock.ToHttpClient());

        var result = await service.GetPlacesByName(_locationName, CancellationToken.None);

        result.Should()
            .BeOfType<Result<Location[]>>().Which.IsFailure.Should().BeTrue();

        result.Error.Should()
            .Be(GeocodingErrors.UnexpectedError);
    }
}