//******************************************************************************************************
//  ApplicationSettings.cs - Gbtc
//
//  Copyright © 2022, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may not use this
//  file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  02/13/2007 - J. Ritchie Carroll
//       Initial version of source generated.
//  01/21/2011 - AJ Stadlin
//       Added General Chart Setting to Show Messages Tab on Data Exception
//       Added Application Setting to not Show Config XML in Explorer after saving
//  03/26/2022 - J. Ritchie Carroll
//       Migrated code from VB to C# assisted with Code Converter (VB - C#):
//       https://marketplace.visualstudio.com/items?itemName=SharpDevelopTeam.CodeConverter
//
//******************************************************************************************************

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Timers;
using GSF.Configuration;

namespace ConnectionTester;

public class ApplicationSettings : CategorizedSettingsBase
{

    #region [ Default Setting Values ]

    // Default application settings
    private const int DefaultMaximumFrameDisplayBytes = 128;
    private const bool DefaultRestoreLastConnectionSettings = true;
    private const bool DefaultForceIPv4 = false;
    private const int DefaultAllowedParsingExceptions = 10;
    private const double DefaultParsingExceptionWindow = 5.0d;
    private const bool DefaultShowConfigXmlExplorerAfterSave = false;

    // Default attribute tree settings
    private const string DefaultChannelNodeBackgroundColor = "Yellow";
    private const string DefaultChannelNodeForegroundColor = "Black";
    private const string DefaultInitialNodeState = "Collapsed";
    private const bool DefaultShowAttributesAsChildren = true;

    // Default general chart settings
    private const float DefaultChartRefreshRate = 0.1f;
    private const string DefaultBackgroundColor = "White";
    private const string DefaultForegroundColor = "Navy";
    private const int DefaultTrendLineWidth = 4;
    private const bool DefaultShowDataPointsOnGraphs = false;
    private const bool DefaultShowMessagesTabOnDataException = true;

    // Default connection settings
    private const int DefaultMaximumConnectionAttempts = 1;
    private const bool DefaultAutoStartDataParsingSequence = true;
    private const bool DefaultSkipDisableRealTimeData = false;
    private const bool DefaultDisableRealTimeDataOnStop = true;
    private const bool DefaultInjectSimulatedTimestamp = false;
    private const bool DefaultUseHighResolutionInputTimer = false;
    internal const string DefaultAlternateInterfaces = "Default|0.0.0.0|::0";
    private const bool DefaultKeepCommandChannelOpen = true;

    // Default phase angle graph settings
    private const string DefaultPhaseAngleGraphStyle = "Relative";
    private const bool DefaultShowPhaseAngleLegend = true;
    private const int DefaultPhaseAnglePointsToPlot = 30;
    private const string DefaultLegendBackgroundColor = "AliceBlue";
    private const string DefaultLegendForegroundColor = "Navy";
    private const string DefaultPhaseAngleColors = "Black;Red;Green;SteelBlue;DarkGoldenrod;Brown;Coral;Purple";

    // Default frequency graph settings
    private const int DefaultFrequencyPointsToPlot = 30;
    private const string DefaultFrequencyColor = "SteelBlue";

    #endregion

    #region [ Public Member Declarations ]

    public event PhaseAngleColorsChangedEventHandler PhaseAngleColorsChanged;

    public delegate void PhaseAngleColorsChangedEventHandler();

    // Configuration file categories
    public const string ApplicationSettingsCategory = "Application Settings";
    public const string AttributeTreeCategory = "Attribute Tree";
    public const string ChartSettingsCategory = "Chart Settings";
    public const string ConnectionSettingsCategory = "Connection Settings";
    public const string PhaseAngleGraphCategory = "Phase Angle Graph";
    public const string FrequencyGraphCategory = "Frequency Graph";

    public enum AngleGraphStyle
    {
        Raw,
        Relative
    }

    public enum NodeState
    {
        Expanded,
        Collapsed
    }

    #region [ Color List with Content Cleared Notification ]

    public class ColorListTypeConverter : TypeConverter
    {
        private readonly ColorConverter m_colorParser = new();

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string colorList)
            {
                ColorList colors = new();

                foreach (string colorItem in colorList.Split(';'))
                {
                    if (m_colorParser.ConvertFromString(colorItem.Trim()) is Color color)
                        colors.Add(color);
                }

                return colors;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                StringBuilder colorList = new();

                foreach (Color colorItem in (ColorList)value)
                {
                    if (colorList.Length > 0)
                        colorList.Append(';');

                    colorList.Append(m_colorParser.ConvertToString(colorItem));
                }

                return colorList.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    // This class exposes an event notification of when then list is cleared - this is really
    // our only signal to know when a collection has been modified in the property grid

    [TypeConverter(typeof(ColorListTypeConverter))]
    public class ColorList : Collection<Color>
    {
        public event ListContentClearedEventHandler ListContentCleared;

        public delegate void ListContentClearedEventHandler();

        public ColorList(params Color[] colors)
        {
            foreach (Color newColor in colors)
                Add(newColor);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            ListContentCleared?.Invoke();
        }
    }

    #endregion

    #endregion

    #region [ Private Member Declarations ]

    // Application settings
    private int m_maximumFrameDisplayBytes;

    // General chart settings
    private float m_refreshRate;
    private int m_trendLineWidth;

    // Connection settings
    private int m_maximumConnectionAttempts;
    private int m_phaseAnglePointsToPlot;
    private ColorList m_phaseAngleColors;

    // Frequency graph settings
    private int m_frequencyPointsToPlot;

    // Other members
    private readonly Timer m_eventDelayTimer;

    #endregion

    #region [ Constructors ]

    // Specifiy default category
    public ApplicationSettings() : base("General")
    {
        m_eventDelayTimer = new()
        {
            Interval = 250d,
            AutoReset = false,
            Enabled = false
        };

        m_eventDelayTimer.Elapsed += m_eventDelayTimer_Elapsed;
    }

    #endregion

    #region [ Application Settings ]

    [Category(ApplicationSettingsCategory)]
    [Description("Maximum encoded bytes to display for frames in the \"Real-time Frame Detail\".")]
    [DefaultValue(DefaultMaximumFrameDisplayBytes)]
    [UserScopedSetting]
    public int MaximumFrameDisplayBytes
    {
        get => m_maximumFrameDisplayBytes;
        set => m_maximumFrameDisplayBytes = value < 1 ? DefaultMaximumFrameDisplayBytes : value;
    }

    [Category(ApplicationSettingsCategory)]
    [Description("Set to True to load previous connection settings at startup.")]
    [DefaultValue(DefaultRestoreLastConnectionSettings)]
    [UserScopedSetting]
    public bool RestoreLastConnectionSettings { get; set; }
    [Category(ApplicationSettingsCategory)]
    [Description("Set to True to force use of IPv4.")]
    [DefaultValue(DefaultForceIPv4)]
    [UserScopedSetting]
    public bool ForceIPv4 { get; set; }
    [Category(ApplicationSettingsCategory)]
    [Description("Defines the number of parsing exceptions allowed during ParsingExceptionWindow before connection is reset.")]
    [DefaultValue(DefaultAllowedParsingExceptions)]
    [UserScopedSetting]
    public int AllowedParsingExceptions { get; set; }
    [Category(ApplicationSettingsCategory)]
    [Description("Defines time duration, in seconds, to monitor parsing exceptions.")]
    [DefaultValue(DefaultParsingExceptionWindow)]
    [UserScopedSetting]
    public double ParsingExceptionWindow { get; set; }
    [Category(ApplicationSettingsCategory)]
    [Description("Show the Configuration XML File in Explorer After Saving.")]
    [DefaultValue(DefaultShowConfigXmlExplorerAfterSave)]
    [UserScopedSetting]
    public bool ShowConfigXmlExplorerAfterSave { get; set; }

    #endregion

    #region [ Attribute Tree Settings ]

    [Category(AttributeTreeCategory)]
    [Description("Defines the highlight background color for channel node entries on the attribute tree.")]
    [DefaultValue(typeof(Color), DefaultChannelNodeBackgroundColor)]
    [UserScopedSetting]
    public Color ChannelNodeBackgroundColor { get; set; }
    [Category(AttributeTreeCategory)]
    [Description("Defines the highlight foreground color for channel node entries on the attribute tree.")]
    [DefaultValue(typeof(Color), DefaultChannelNodeForegroundColor)]
    [UserScopedSetting]
    public Color ChannelNodeForegroundColor { get; set; }
    [Category(AttributeTreeCategory)]
    [Description("Defines the initial state for nodes when added to the attribute tree.  Note that a fully expanded tree will take much longer to initialize.")]
    [DefaultValue(typeof(NodeState), DefaultInitialNodeState)]
    [UserScopedSetting]
    public NodeState InitialNodeState { get; set; }
    [Category(AttributeTreeCategory)]
    [Description("Set to True to show attributes as children of their channel entries.")]
    [DefaultValue(DefaultShowAttributesAsChildren)]
    [UserScopedSetting]
    public bool ShowAttributesAsChildren { get; set; }

    #endregion

    #region [ General Chart Settings ]

    [Category(ChartSettingsCategory)]
    [Description("Chart refresh rate in seconds. Typical setting is 0.1, increase this number if app runs slow.")]
    [DefaultValue(DefaultChartRefreshRate)]
    [UserScopedSetting]
    public float RefreshRate
    {
        get => m_refreshRate;
        set => m_refreshRate = value <= 0f ? DefaultChartRefreshRate : value;
    }

    [Category(ChartSettingsCategory)]
    [Description("Background color for graph region.")]
    [DefaultValue(typeof(Color), DefaultBackgroundColor)]
    [UserScopedSetting]
    public Color BackgroundColor { get; set; }
    [Category(ChartSettingsCategory)]
    [Description("Foreground color for graph region (axes, legend border, text, etc.)")]
    [DefaultValue(typeof(Color), DefaultForegroundColor)]
    [UserScopedSetting]
    public Color ForegroundColor { get; set; }

    [Category(ChartSettingsCategory)]
    [Description("Trend line graphing width (in pixels).")]
    [DefaultValue(DefaultTrendLineWidth)]
    [UserScopedSetting]
    public int TrendLineWidth
    {
        get => m_trendLineWidth;
        set => m_trendLineWidth = value <= 0 ? DefaultTrendLineWidth : value;
    }

    [Category(ChartSettingsCategory)]
    [Description("Set to True to show data points on graphs.")]
    [DefaultValue(DefaultShowDataPointsOnGraphs)]
    [UserScopedSetting]
    public bool ShowDataPointsOnGraphs { get; set; }
    [Category(ChartSettingsCategory)]
    [Description("Set to True to change to the Messages tab on Data Exception.")]
    [DefaultValue(DefaultShowMessagesTabOnDataException)]
    [UserScopedSetting]
    public bool ShowMessagesTabOnDataException { get; set; }

    #endregion

    #region [ Connection Settings ]

    [Category(ConnectionSettingsCategory)]
    [Description("Maximum number of times to attempt connection before giving up; set to -1 to continue connection attempt indefinitely.")]
    [DefaultValue(DefaultMaximumConnectionAttempts)]
    [UserScopedSetting]
    public int MaximumConnectionAttempts
    {
        get => m_maximumConnectionAttempts;
        set
        {
            m_maximumConnectionAttempts = value switch
            {
                < 0 => -1,
                > 0 => value,
                _   => DefaultMaximumConnectionAttempts
            };
        }
    }

    [Category(ConnectionSettingsCategory)]
    [Description("Set to True to automatically send commands for ConfigFrame2 and EnableRealTimeData.")]
    [DefaultValue(DefaultAutoStartDataParsingSequence)]
    [UserScopedSetting]
    public bool AutoStartDataParsingSequence { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines flag to skip automatic disabling of the real-time data stream on shutdown or startup. Useful when using serial or UDP multicast with several subscribed clients.")]
    [DefaultValue(DefaultSkipDisableRealTimeData)]
    [UserScopedSetting]
    public bool SkipDisableRealTimeData { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines flag to control disabling of real-time data stream on shutdown. Useful when using serial or UDP multicast with several subscribed clients.")]
    [DefaultValue(DefaultDisableRealTimeDataOnStop)]
    [UserScopedSetting]
    public bool DisableRealTimeDataOnStop { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines flag to inject a simulated timestamp into incoming streams - this will override any existing incoming timestamp. Useful when using file based input to simulate real-time data.")]
    [DefaultValue(DefaultInjectSimulatedTimestamp)]
    [UserScopedSetting]
    public bool InjectSimulatedTimestamp { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines flag that determines if a high-resolution precision timer should be used for file based input. Useful when input frames need be accurately time-aligned to the local clock to better simulate an input device and calculate downstream latencies.")]
    [DefaultValue(DefaultUseHighResolutionInputTimer)]
    [UserScopedSetting]
    public bool UseHighResolutionInputTimer { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines alternate network interface addresses that can be used for sourcing socket connections. Use the format \"Name|IPv4Addy|IPv6Addy\" (IPv6 address specification is optional) separating multiple alternate interfaces with a semi-colon \";\"")]
    [DefaultValue(DefaultAlternateInterfaces)]
    [UserScopedSetting]
    public string AlternateInterfaces { get; set; }
    [Category(ConnectionSettingsCategory)]
    [Description("Defines flag that determines if alternate command channel will remain open after successfully connecting device.")]
    [DefaultValue(DefaultKeepCommandChannelOpen)]
    [UserScopedSetting]
    public bool KeepCommandChannelOpen { get; set; }

    #endregion

    #region [ Phase Angle Graph Settings ]

    [Category(PhaseAngleGraphCategory)]
    [Description("Sets the phase angle graph to plot either raw or relative phase angles.")]
    [DefaultValue(typeof(AngleGraphStyle), DefaultPhaseAngleGraphStyle)]
    [UserScopedSetting]
    public AngleGraphStyle PhaseAngleGraphStyle { get; set; }
    [Category(PhaseAngleGraphCategory)]
    [Description("Set to True to show phase angle graph legend.")]
    [DefaultValue(DefaultShowPhaseAngleLegend)]
    [UserScopedSetting]
    public bool ShowPhaseAngleLegend { get; set; }

    [Category(PhaseAngleGraphCategory)]
    [Description("Sets the total number of phase angle points to display.")]
    [DefaultValue(DefaultPhaseAnglePointsToPlot)]
    [UserScopedSetting]
    public int PhaseAnglePointsToPlot
    {
        get => m_phaseAnglePointsToPlot;

        set => m_phaseAnglePointsToPlot = value < 2 ? DefaultPhaseAnglePointsToPlot : value;
    }

    [Category(PhaseAngleGraphCategory)]
    [Description("Background color for phase angle legend.")]
    [DefaultValue(typeof(Color), DefaultLegendBackgroundColor)]
    [UserScopedSetting]
    public Color LegendBackgroundColor { get; set; }
    [Category(PhaseAngleGraphCategory)]
    [Description("Foreground color for phase angle legend text.")]
    [DefaultValue(typeof(Color), DefaultLegendForegroundColor)]
    [UserScopedSetting]
    public Color LegendForegroundColor { get; set; }

    [Category(PhaseAngleGraphCategory)]
    [Description("Possible foreground colors for phase angle trends.")]
    [DefaultValue(typeof(ColorList), DefaultPhaseAngleColors)]
    [UserScopedSetting]
    public ColorList PhaseAngleColors
    {
        get => m_phaseAngleColors;
        set
        { 
            if (m_phaseAngleColors is not null)
                m_phaseAngleColors.ListContentCleared -= m_phaseAngleColors_ListContentCleared;

            m_phaseAngleColors = value;
            
            if (m_phaseAngleColors is not null)
                m_phaseAngleColors.ListContentCleared += m_phaseAngleColors_ListContentCleared;
        }
    }

    #endregion

    #region [ Frequency Graph Settings ]

    [Category(FrequencyGraphCategory)]
    [Description("Sets the total number of frequency points to display.")]
    [DefaultValue(DefaultFrequencyPointsToPlot)]
    [UserScopedSetting]
    public int FrequencyPointsToPlot
    {
        get => m_frequencyPointsToPlot;
        set => m_frequencyPointsToPlot = value < 2 ? DefaultFrequencyPointsToPlot : value;
    }

    [Category(FrequencyGraphCategory)]
    [Description("Foreground color for frequency trend.")]
    [DefaultValue(typeof(Color), DefaultFrequencyColor)]
    [UserScopedSetting]
    public Color FrequencyColor { get; set; }

    #endregion

    #region [ Private Method Implementation ]

    private void m_phaseAngleColors_ListContentCleared()
    {
        // Updates to a collection from a PropertyGrid don't get a normal "PropertyValueChanged" notification,
        // so you're stuck with detecting a call to "Clear" in your personal collection.  However, the update
        // is not complete until a call to "Add" for each updated item, so we need to wait for a moment to
        // allow all of the adds to finish - this isn't exact science - someone didn't think through this one.
        m_eventDelayTimer.Enabled = true;
    }

    private void m_eventDelayTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        PhaseAngleColorsChanged?.Invoke();
    }

    #endregion
}