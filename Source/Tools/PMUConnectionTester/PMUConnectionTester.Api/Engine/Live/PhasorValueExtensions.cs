using GSF.PhasorProtocols;
using GSF.PhasorProtocols.IEEEC37_118;
using GSF.Units;

namespace ConnectionTester.Api.Engine.Live;

/// <summary>
/// Scaling adjustments for raw phasor values - mirrors <c>IDataCellValueExtensions</c> from the
/// WinForms application ( <c>PMUConnectionTester.cs</c>), duplicated here since the API project
/// cannot reference the WinForms executable (it drags in <c>System.Windows.Forms</c>/Infragistics dependencies).
/// </summary>
internal static class PhasorValueExtensions
{
    public static Angle AdjustedAngle(this IPhasorValue phasor)
    {
        if (phasor.Definition is not PhasorDefinition3 definition)
            return phasor.Angle;

        return definition.AngleAdder + phasor.Angle;
    }

    public static double AdjustedMagnitude(this IPhasorValue phasor)
    {
        if (phasor.Definition is not PhasorDefinition3 definition)
            return phasor.Magnitude;

        return definition.MagnitudeMultiplier * phasor.Magnitude;
    }
}