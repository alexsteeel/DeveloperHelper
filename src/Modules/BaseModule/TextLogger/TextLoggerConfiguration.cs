using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Drawing;

public class TextLoggerConfiguration
{
    public int EventId { get; set; }

    public Dictionary<LogLevel, Color> LogLevels { get; set; } = new()
    {
        [LogLevel.Information] = Color.Black,
        [LogLevel.Warning] = Color.Blue,
        [LogLevel.Error] = Color.Red,
    };
}
