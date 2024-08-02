﻿using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain;

public class Weather
{
    public double? CurrentTemperature { get; private set; }
    public double? MinTemperature { get; private set; }
    public double? MaxTemperature { get; private set; }
    public double? WindSpeed { get; private set; }
    public string? WindDirection { get; private set; }
    public double? Humidity { get; private set; }
    public double? Cloud { get; private set; }
    public string? ConditionText { get; private set; }
    public bool? IsDay { get; private set; }
    [DataType(DataType.Time)]
    public DateTime? ObservationTime { get; private set; } = default!;
    public string? Location { get; private set; } = default!;

    public Weather(
        double? currentTemperature,
        double? windSpeed,
        string? windDirection,
        double? humidity,
        double? cloud,
        string? conditionText,
        bool? isDay,
        DateTime? observationTime)
    {
        CurrentTemperature = currentTemperature;
        WindSpeed = windSpeed;
        WindDirection = windDirection;
        Humidity = humidity;
        Cloud = cloud;
        ConditionText = conditionText;
        IsDay = isDay;
        ObservationTime = observationTime;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb
            .AppendLineIfNotNull(CurrentTemperature, $"Temperature: {Convert.ToInt32(CurrentTemperature)}{"\u00B0"}C")
            .AppendLineIfNotNull(Humidity, $"Humidity: {Humidity}%")
            .AppendLineIfNotNull(WindSpeed, $"Wind speed: {WindSpeed} km/h")
            .AppendLineIfNotNull(WindDirection, $"Wind directoin: {WindDirection}")
            .AppendLineIfNotNull(Cloud, $"Cloudiness: {Cloud}%")
            .AppendLineIfNotNull(ConditionText, $"Description: {ConditionText}")
            .AppendLineIfNotNull(ObservationTime, $"Update time: {TimeOnly.FromDateTime(ObservationTime!.Value)}");

        return sb.ToString();
    }
}
