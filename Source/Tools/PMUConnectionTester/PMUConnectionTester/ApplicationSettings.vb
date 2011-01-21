'******************************************************************************************************
'  ApplicationSettings.vb - Gbtc
'
'  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
'
'  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
'  the NOTICE file distributed with this work for additional information regarding copyright ownership.
'  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
'  not use this file except in compliance with the License. You may obtain a copy of the License at:
'
'      http://www.opensource.org/licenses/eclipse-1.0.php
'
'  Unless agreed to in writing, the subject software distributed under the License is distributed on an
'  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
'  License for the specific language governing permissions and limitations.
'
'  Code Modification History:
'  ----------------------------------------------------------------------------------------------------
'  02/13/2007 - J. Ritchie Carroll
'       Initial version of source generated.
'  01/21/2011 - AJ Stadlin
'       Added General Chart Setting to Show Messages Tab on Data Exception
'       Added Application Setting to not Show Config XML in Explorer after saving
'******************************************************************************************************

Imports System.Text
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Configuration
Imports System.Globalization
Imports TVA.Configuration

Public Class ApplicationSettings

    Inherits CategorizedSettingsBase

#Region " Default Setting Values "

    ' Default application settings
    Private Const DefaultMaximumFrameDisplayBytes As Integer = 128
    Private Const DefaultRestoreLastConnectionSettings As Boolean = True
    Private Const DefaultForceIPv4 As Boolean = False
    Private Const DefaultAllowedParsingExceptions As Integer = 10
    Private Const DefaultParsingExceptionWindow As Double = 5.0R
    Private Const DefaultShowConfigXmlExplorerAfterSave As Boolean = False

    ' Default attribute tree settings
    Private Const DefaultChannelNodeBackgroundColor As String = "Yellow"
    Private Const DefaultChannelNodeForegroundColor As String = "Black"
    Private Const DefaultInitialNodeState As String = "Collapsed"
    Private Const DefaultShowAttributesAsChildren As Boolean = True

    ' Default general chart settings
    Private Const DefaultChartRefreshRate As Single = 0.1
    Private Const DefaultBackgroundColor As String = "White"
    Private Const DefaultForegroundColor As String = "Navy"
    Private Const DefaultTrendLineWidth As Integer = 4
    Private Const DefaultShowDataPointsOnGraphs As Boolean = False
    Private Const DefaultShowMessagesTabOnDataException As Boolean = True

    ' Default connection settings
    Private Const DefaultMaximumConnectionAttempts As Integer = 1
    Private Const DefaultAutoStartDataParsingSequence As Boolean = True
    Private Const DefaultExecuteParseOnSeparateThread As Boolean = False
    Private Const DefaultSkipDisableRealTimeData As Boolean = False
    Private Const DefaultInjectSimulatedTimestamp As Boolean = False
    Private Const DefaultUseHighResolutionInputTimer As Boolean = False

    ' Default phase angle graph settings
    Private Const DefaultPhaseAngleGraphStyle As String = "Relative"
    Private Const DefaultShowPhaseAngleLegend As Boolean = True
    Private Const DefaultPhaseAnglePointsToPlot As Integer = 30
    Private Const DefaultLegendBackgroundColor As String = "AliceBlue"
    Private Const DefaultLegendForegroundColor As String = "Navy"
    Private Const DefaultPhaseAngleColors As String = "Black;Red;Green;SteelBlue;DarkGoldenrod;Brown;Coral;Purple"

    ' Default frequency graph settings
    Private Const DefaultFrequencyPointsToPlot As Integer = 30
    Private Const DefaultFrequencyColor As String = "SteelBlue"

#End Region

#Region " Public Member Declarations "

    Public Event PhaseAngleColorsChanged()

    ' Configuration file categories
    Public Const ApplicationSettingsCategory As String = "Application Settings"
    Public Const AttributeTreeCategory As String = "Attribute Tree"
    Public Const ChartSettingsCategory As String = "Chart Settings"
    Public Const ConnectionSettingsCategory As String = "Connection Settings"
    Public Const PhaseAngleGraphCategory As String = "Phase Angle Graph"
    Public Const FrequencyGraphCategory As String = "Frequency Graph"

    Public Enum AngleGraphStyle
        Raw
        Relative
    End Enum

    Public Enum NodeState
        Expanded
        Collapsed
    End Enum

#Region " Color List with Content Cleared Notification "

    Public Class ColorListTypeConverter

        Inherits TypeConverter

        Private m_colorParser As New ColorConverter

        Public Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean

            If sourceType Is GetType(String) Then Return True
            Return MyBase.CanConvertFrom(context, sourceType)

        End Function

        Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As Object

            If TypeOf value Is String Then
                Dim colors As New ColorList

                For Each colorItem As String In CStr(value).Split(";"c)
                    colors.Add(CType(m_colorParser.ConvertFromString(colorItem.Trim()), Color))
                Next

                Return colors
            End If

            Return MyBase.ConvertFrom(context, culture, value)

        End Function

        Public Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object

            If destinationType Is GetType(String) Then
                With New StringBuilder
                    For Each colorItem As Color In CType(value, ColorList)
                        If .Length > 0 Then .Append(";"c)
                        .Append(m_colorParser.ConvertToString(colorItem))
                    Next

                    Return .ToString()
                End With
            End If

            Return MyBase.ConvertTo(context, culture, value, destinationType)

        End Function

    End Class

    ' This class exposes an event notification of when then list is cleared - this is really
    ' our only signal to know when a collection has been modified in the property grid

    <TypeConverter(GetType(ColorListTypeConverter))> _
    Public Class ColorList

        Inherits Collection(Of Color)

        Public Event ListContentCleared()

        Public Sub New(ByVal ParamArray colors As Color())

            For Each newColor As Color In colors
                Add(newColor)
            Next

        End Sub

        Protected Overrides Sub ClearItems()

            MyBase.ClearItems()
            RaiseEvent ListContentCleared()

        End Sub

    End Class

#End Region

#End Region

#Region " Private Member Declarations "

    ' Application settings
    Private m_maximumFrameDisplayBytes As Integer
    Private m_restoreLastConnectionSettings As Boolean
    Private m_forceIPv4 As Boolean
    Private m_allowedParsingExceptions As Integer
    Private m_parsingExceptionWindow As Double
    Private m_showConfigXmlExplorerAfterSave As Boolean

    ' Attribute tree settings
    Private m_channelNodeBackgroundColor As Color
    Private m_channelNodeForegroundColor As Color
    Private m_initialNodeState As NodeState
    Private m_showAttributesAsChildren As Boolean

    ' General chart settings
    Private m_refreshRate As Single
    Private m_backgroundColor As Color
    Private m_foregroundColor As Color
    Private m_trendLineWidth As Integer
    Private m_showDataPointsOnGraphs As Boolean
    Private m_showMessagesTabOnDataException As Boolean

    ' Connection settings
    Private m_maximumConnectionAttempts As Integer
    Private m_autoStartDataParsingSequence As Boolean
    Private m_executeParseOnSeparateThread As Boolean
    Private m_skipDisableRealTimeData As Boolean
    Private m_injectSimulatedTimestamp As Boolean
    Private m_useHighResolutionInputTimer As Boolean

    ' Phase angle graph settings
    Private m_phaseAngleGraphStyle As AngleGraphStyle
    Private m_showPhaseAngleLegend As Boolean
    Private m_phaseAnglePointsToPlot As Integer
    Private m_legendBackgroundColor As Color
    Private m_legendForegroundColor As Color
    Private WithEvents m_phaseAngleColors As ColorList

    ' Frequency graph settings
    Private m_frequencyPointsToPlot As Integer
    Private m_frequencyColor As Color

    ' Other members
    Private WithEvents m_eventDelayTimer As Timers.Timer

#End Region

#Region " Constructors "

    Public Sub New()

        ' Specifiy default category
        MyBase.New("General")

        m_eventDelayTimer = New Timers.Timer

        With m_eventDelayTimer
            .Interval = 250
            .AutoReset = False
            .Enabled = False
        End With

    End Sub

#End Region

#Region " Application Settings "

    <Category(ApplicationSettingsCategory), _
    Description("Maximum encoded bytes to display for frames in the ""Real-time Frame Detail""."), _
    DefaultValue(DefaultMaximumFrameDisplayBytes), _
    UserScopedSetting()> _
    Public Property MaximumFrameDisplayBytes() As Integer
        Get
            Return m_maximumFrameDisplayBytes
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then
                m_maximumFrameDisplayBytes = DefaultMaximumFrameDisplayBytes
            Else
                m_maximumFrameDisplayBytes = value
            End If
        End Set
    End Property

    <Category(ApplicationSettingsCategory), _
    Description("Set to True to load previous connection settings at startup."), _
    DefaultValue(DefaultRestoreLastConnectionSettings), _
    UserScopedSetting()> _
    Public Property RestoreLastConnectionSettings() As Boolean
        Get
            Return m_restoreLastConnectionSettings
        End Get
        Set(ByVal value As Boolean)
            m_restoreLastConnectionSettings = value
        End Set
    End Property

    <Category(ApplicationSettingsCategory), _
    Description("Set to True to force use of IPv4."), _
    DefaultValue(DefaultForceIPv4), _
    UserScopedSetting()> _
    Public Property ForceIPv4() As Boolean
        Get
            Return m_forceIPv4
        End Get
        Set(ByVal value As Boolean)
            m_forceIPv4 = value
        End Set
    End Property

    <Category(ApplicationSettingsCategory), _
    Description("Defines the number of parsing exceptions allowed during ParsingExceptionWindow before connection is reset."), _
    DefaultValue(DefaultAllowedParsingExceptions), _
    UserScopedSetting()> _
    Public Property AllowedParsingExceptions() As Integer
        Get
            Return m_allowedParsingExceptions
        End Get
        Set(ByVal value As Integer)
            m_allowedParsingExceptions = value
        End Set
    End Property

    <Category(ApplicationSettingsCategory), _
    Description("Defines time duration, in seconds, to monitor parsing exceptions."), _
    DefaultValue(DefaultParsingExceptionWindow), _
    UserScopedSetting()> _
    Public Property ParsingExceptionWindow() As Double
        Get
            Return m_parsingExceptionWindow
        End Get
        Set(ByVal value As Double)
            m_parsingExceptionWindow = value
        End Set
    End Property

    <Category(ApplicationSettingsCategory), _
    Description("Show the Configuration XML File in Explorer After Saving."), _
    DefaultValue(DefaultShowConfigXmlExplorerAfterSave), _
    UserScopedSetting()> _
    Public Property ShowConfigXmlExplorerAfterSave() As Boolean
        Get
            Return m_showConfigXmlExplorerAfterSave
        End Get
        Set(ByVal value As Boolean)
            m_showConfigXmlExplorerAfterSave = value
        End Set
    End Property

#End Region

#Region " Attribute Tree Settings "

    <Category(AttributeTreeCategory), _
    Description("Defines the highlight background color for channel node entries on the attribute tree."), _
    DefaultValue(GetType(Color), DefaultChannelNodeBackgroundColor), _
    UserScopedSetting()> _
    Public Property ChannelNodeBackgroundColor() As Color
        Get
            Return m_channelNodeBackgroundColor
        End Get
        Set(ByVal value As Color)
            m_channelNodeBackgroundColor = value
        End Set
    End Property

    <Category(AttributeTreeCategory), _
    Description("Defines the highlight foreground color for channel node entries on the attribute tree."), _
    DefaultValue(GetType(Color), DefaultChannelNodeForegroundColor), _
    UserScopedSetting()> _
    Public Property ChannelNodeForegroundColor() As Color
        Get
            Return m_channelNodeForegroundColor
        End Get
        Set(ByVal value As Color)
            m_channelNodeForegroundColor = value
        End Set
    End Property

    <Category(AttributeTreeCategory), _
    Description("Defines the initial state for nodes when added to the attribute tree.  Note that a fully expanded tree will take much longer to initialize."), _
    DefaultValue(GetType(NodeState), DefaultInitialNodeState), _
    UserScopedSetting()> _
    Public Property InitialNodeState() As NodeState
        Get
            Return m_initialNodeState
        End Get
        Set(ByVal value As NodeState)
            m_initialNodeState = value
        End Set
    End Property

    <Category(AttributeTreeCategory), _
    Description("Set to True to show attributes as children of their channel entries."), _
    DefaultValue(DefaultShowAttributesAsChildren), _
    UserScopedSetting()> _
    Public Property ShowAttributesAsChildren() As Boolean
        Get
            Return m_showAttributesAsChildren
        End Get
        Set(ByVal value As Boolean)
            m_showAttributesAsChildren = value
        End Set
    End Property

#End Region

#Region " General Chart Settings "

    <Category(ChartSettingsCategory), _
    Description("Chart refresh rate in seconds. Typical setting is 0.1, increase this number if app runs slow."), _
    DefaultValue(DefaultChartRefreshRate), _
    UserScopedSetting()> _
    Public Property RefreshRate() As Single
        Get
            Return m_refreshRate
        End Get
        Set(ByVal value As Single)
            If value <= 0 Then
                m_refreshRate = DefaultChartRefreshRate
            Else
                m_refreshRate = value
            End If
        End Set
    End Property

    <Category(ChartSettingsCategory), _
    Description("Background color for graph region."), _
    DefaultValue(GetType(Color), DefaultBackgroundColor), _
    UserScopedSetting()> _
    Public Property BackgroundColor() As Color
        Get
            Return m_backgroundColor
        End Get
        Set(ByVal value As Color)
            m_backgroundColor = value
        End Set
    End Property

    <Category(ChartSettingsCategory), _
    Description("Foreground color for graph region (axes, legend border, text, etc.)"), _
    DefaultValue(GetType(Color), DefaultForegroundColor), _
    UserScopedSetting()> _
    Public Property ForegroundColor() As Color
        Get
            Return m_foregroundColor
        End Get
        Set(ByVal value As Color)
            m_foregroundColor = value
        End Set
    End Property

    <Category(ChartSettingsCategory), _
    Description("Trend line graphing width (in pixels)."), _
    DefaultValue(DefaultTrendLineWidth), _
    UserScopedSetting()> _
    Public Property TrendLineWidth() As Integer
        Get
            Return m_trendLineWidth
        End Get
        Set(ByVal value As Integer)
            If value <= 0 Then
                m_trendLineWidth = DefaultTrendLineWidth
            Else
                m_trendLineWidth = value
            End If
        End Set
    End Property

    <Category(ChartSettingsCategory), _
    Description("Set to True to show data points on graphs."), _
    DefaultValue(DefaultShowDataPointsOnGraphs), _
    UserScopedSetting()> _
    Public Property ShowDataPointsOnGraphs() As Boolean
        Get
            Return m_showDataPointsOnGraphs
        End Get
        Set(ByVal value As Boolean)
            m_showDataPointsOnGraphs = value
        End Set
    End Property

    <Category(ChartSettingsCategory), _
    Description("Set to True to change to the Messages tab on Data Exception."), _
    DefaultValue(DefaultShowMessagesTabOnDataException), _
    UserScopedSetting()> _
    Public Property ShowMessagesTabOnDataException() As Boolean
        Get
            Return m_showMessagesTabOnDataException
        End Get
        Set(ByVal value As Boolean)
            m_showMessagesTabOnDataException = value
        End Set
    End Property

#End Region

#Region " Connection Settings "

    <Category(ConnectionSettingsCategory), _
    Description("Maximum number of times to attempt connection before giving up; set to -1 to continue connection attempt indefinitely."), _
    DefaultValue(DefaultMaximumConnectionAttempts), _
    UserScopedSetting()> _
    Public Property MaximumConnectionAttempts() As Integer
        Get
            Return m_maximumConnectionAttempts
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                m_maximumConnectionAttempts = -1
            ElseIf value > 0 Then
                m_maximumConnectionAttempts = value
            Else
                m_maximumConnectionAttempts = DefaultMaximumConnectionAttempts
            End If
        End Set
    End Property

    <Category(ConnectionSettingsCategory), _
    Description("Set to True to automatically send commands for ConfigFrame2 and EnableRealTimeData."), _
    DefaultValue(DefaultAutoStartDataParsingSequence), _
    UserScopedSetting()> _
    Public Property AutoStartDataParsingSequence() As Boolean
        Get
            Return m_autoStartDataParsingSequence
        End Get
        Set(ByVal value As Boolean)
            m_autoStartDataParsingSequence = value
        End Set
    End Property

    <Category(ConnectionSettingsCategory), _
    Description("Allows frame parsing to be executed on a separate thread (other than communications thread) - typically only needed when data frames are very large.  This change will happen dynamically, even if a connection is active."), _
    DefaultValue(DefaultExecuteParseOnSeparateThread), _
    UserScopedSetting()> _
    Public Property ExecuteParseOnSeparateThread() As Boolean
        Get
            Return m_executeParseOnSeparateThread
        End Get
        Set(ByVal value As Boolean)
            m_executeParseOnSeparateThread = value
        End Set
    End Property

    <Category(ConnectionSettingsCategory), _
    Description("Defines flag to skip automatic disabling of the real-time data stream on shutdown or startup. Useful when using UDP multicast with several subscribed clients."), _
    DefaultValue(DefaultSkipDisableRealTimeData), _
    UserScopedSetting()> _
    Public Property SkipDisableRealTimeData() As Boolean
        Get
            Return m_skipDisableRealTimeData
        End Get
        Set(ByVal value As Boolean)
            m_skipDisableRealTimeData = value
        End Set
    End Property

    <Category(ConnectionSettingsCategory), _
    Description("Defines flag to inject a simulated timestamp into incoming streams - this will override any existing incoming timestamp. Useful when using file based input to simulate real-time data."), _
    DefaultValue(DefaultInjectSimulatedTimestamp), _
    UserScopedSetting()> _
    Public Property InjectSimulatedTimestamp() As Boolean
        Get
            Return m_injectSimulatedTimestamp
        End Get
        Set(ByVal value As Boolean)
            m_injectSimulatedTimestamp = value
        End Set
    End Property

    <Category(ConnectionSettingsCategory), _
    Description("Defines flag that determines if a high-resolution precision timer should be used for file based input. Useful when input frames need be accurately time-aligned to the local clock to better simulate an input device and calculate downstream latencies."), _
    DefaultValue(DefaultUseHighResolutionInputTimer), _
    UserScopedSetting()> _
    Public Property UseHighResolutionInputTimer() As Boolean
        Get
            Return m_useHighResolutionInputTimer
        End Get
        Set(ByVal value As Boolean)
            m_useHighResolutionInputTimer = value
        End Set
    End Property

#End Region

#Region " Phase Angle Graph Settings "

    <Category(PhaseAngleGraphCategory), _
    Description("Sets the phase angle graph to plot either raw or relative phase angles."), _
    DefaultValue(GetType(AngleGraphStyle), DefaultPhaseAngleGraphStyle), _
    UserScopedSetting()> _
    Public Property PhaseAngleGraphStyle() As AngleGraphStyle
        Get
            Return m_phaseAngleGraphStyle
        End Get
        Set(ByVal value As AngleGraphStyle)
            m_phaseAngleGraphStyle = value
        End Set
    End Property

    <Category(PhaseAngleGraphCategory), _
    Description("Set to True to show phase angle graph legend."), _
    DefaultValue(DefaultShowPhaseAngleLegend), _
    UserScopedSetting()> _
    Public Property ShowPhaseAngleLegend() As Boolean
        Get
            Return m_showPhaseAngleLegend
        End Get
        Set(ByVal value As Boolean)
            m_showPhaseAngleLegend = value
        End Set
    End Property

    <Category(PhaseAngleGraphCategory), _
    Description("Sets the total number of phase angle points to display."), _
    DefaultValue(DefaultPhaseAnglePointsToPlot), _
    UserScopedSetting()> _
    Public Property PhaseAnglePointsToPlot() As Integer
        Get
            Return m_phaseAnglePointsToPlot
        End Get
        Set(ByVal value As Integer)
            If value < 2 Then
                m_phaseAnglePointsToPlot = DefaultPhaseAnglePointsToPlot
            Else
                m_phaseAnglePointsToPlot = value
            End If
        End Set
    End Property

    <Category(PhaseAngleGraphCategory), _
    Description("Background color for phase angle legend."), _
    DefaultValue(GetType(Color), DefaultLegendBackgroundColor), _
    UserScopedSetting()> _
    Public Property LegendBackgroundColor() As Color
        Get
            Return m_legendBackgroundColor
        End Get
        Set(ByVal value As Color)
            m_legendBackgroundColor = value
        End Set
    End Property

    <Category(PhaseAngleGraphCategory), _
    Description("Foreground color for phase angle legend text."), _
    DefaultValue(GetType(Color), DefaultLegendForegroundColor), _
    UserScopedSetting()> _
    Public Property LegendForegroundColor() As Color
        Get
            Return m_legendForegroundColor
        End Get
        Set(ByVal value As Color)
            m_legendForegroundColor = value
        End Set
    End Property

    <Category(PhaseAngleGraphCategory), _
    Description("Possible foreground colors for phase angle trends."), _
    DefaultValue(GetType(ColorList), DefaultPhaseAngleColors), _
    UserScopedSetting()> _
    Public Property PhaseAngleColors() As ColorList
        Get
            Return m_phaseAngleColors
        End Get
        Set(ByVal value As ColorList)
            m_phaseAngleColors = value
        End Set
    End Property

#End Region

#Region " Frequency Graph Settings "

    <Category(FrequencyGraphCategory), _
    Description("Sets the total number of frequency points to display."), _
    DefaultValue(DefaultFrequencyPointsToPlot), _
    UserScopedSetting()> _
    Public Property FrequencyPointsToPlot() As Integer
        Get
            Return m_frequencyPointsToPlot
        End Get
        Set(ByVal value As Integer)
            If value < 2 Then
                m_frequencyPointsToPlot = DefaultFrequencyPointsToPlot
            Else
                m_frequencyPointsToPlot = value
            End If
        End Set
    End Property

    <Category(FrequencyGraphCategory), _
    Description("Foreground color for frequency trend."), _
    DefaultValue(GetType(Color), DefaultFrequencyColor), _
    UserScopedSetting()> _
    Public Property FrequencyColor() As Color
        Get
            Return m_frequencyColor
        End Get
        Set(ByVal value As Color)
            m_frequencyColor = value
        End Set
    End Property

#End Region

#Region " Private Method Implementation "

    Private Sub m_phaseAngleColors_ListContentCleared() Handles m_phaseAngleColors.ListContentCleared

        ' Updates to a collection from a PropertyGrid don't get a normal "PropertyValueChanged" notification,
        ' so you're stuck with detecting a call to "Clear" in your personal collection.  However, the update
        ' is not complete until a call to "Add" for each updated item, so we need to wait for a moment to
        ' allow all of the adds to finish - this isn't exact science - someone didn't think through this one.
        m_eventDelayTimer.Enabled = True

    End Sub

    Private Sub m_eventDelayTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles m_eventDelayTimer.Elapsed

        RaiseEvent PhaseAngleColorsChanged()

    End Sub

#End Region

End Class
