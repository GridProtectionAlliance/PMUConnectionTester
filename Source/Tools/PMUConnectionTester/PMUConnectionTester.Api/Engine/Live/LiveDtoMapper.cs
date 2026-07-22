using ConnectionTester.Api.Models;
using GSF.PhasorProtocols;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Builds the public configuration DTO from a raw GSF configuration frame.
/// </summary>
internal static class LiveDtoMapper
{
    public static PmuConfigurationDto BuildConfiguration(IConfigurationFrame frame)
    {
        PmuConfigurationDto dto = new()
        {
            Result = "Success",
            FrameRate = frame.FrameRate
        };

        foreach (IConfigurationCell cell in frame.Cells)
        {
            PmuConfigurationCellDto cellDto = new()
            {
                IdCode = cell.IDCode,
                StationName = cell.StationName,
                Stat = 0 // Not present on the configuration frame - only reported per data frame.
            };

            if (cell.PhasorDefinitions is not null)
            {
                foreach (IPhasorDefinition phasor in cell.PhasorDefinitions)
                    cellDto.Phasors.Add(new PmuPhasorLabelDto
                    {
                        Label = phasor.Label,
                        Type = phasor.PhasorType == GSF.Units.EE.PhasorType.Current ? "I" : "V"
                    });
            }

            if (cell.AnalogDefinitions is not null)
            {
                foreach (IAnalogDefinition analog in cell.AnalogDefinitions)
                    cellDto.AnalogLabels.Add(analog.Label);
            }

            if (cell.DigitalDefinitions is not null)
            {
                foreach (IDigitalDefinition digital in cell.DigitalDefinitions)
                    cellDto.DigitalLabels.Add(digital.Label);
            }

            dto.Cells.Add(cellDto);
        }

        return dto;
    }
}