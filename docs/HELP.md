<strong>Note</strong>: The PMU Connection Tester was originally distributed via the Eastern Interconnection Phasor Project web site -&nbsp;the last publically distributed version of this older tool was 2.2.0.24862. The PMU Connection Tester is now its own stand-alone
 open source project and has been versioned starting at 4.1.<br>
<br>
<hr>

# Overview

The PMU Connection Tester verifies that the data stream from any known phasor measurement device is being received. Device types that can be tested through the PMU Connection Tester application may include:<br>
<ul>
<li>Phasor Measurement Unit (PMU) </li><li>Phasor Data Concentrator (PDC) </li><li>Digital Fault Recorder (DFR) </li><li>Power Quality Meter (PQ) </li></ul>
<br>
Any device that supports one of the following phasor protocols may be tested:<br>
<ul>
<li>IEEE C37.118.2-2011 </li><li>IEEE C37.118-2005 (Version 1/Draft 7, Draft 6) </li><li>IEC 61850-90-5 (starting with version 4.3) </li><li>IEEE 1344-1995 </li><li>BPA PDCstream (Revisions 0, 1, and 2, including PDCxchng formatted data) </li><li>UTK&nbsp;F-Net </li><li>SEL Fast Message </li><li>Macrodyne (M and G starting with version 4.3) </li></ul>
<hr>

# Navigation

<img src="Main.png" alt="Main.png"><br>
<br>
Some portions of the screen can be opened by clicking the Expand button (<img src="ButtonExpand.png" alt="ButtonExpand.png">) and closed by clicking the Contract button (<img src="ButtonContract.png" alt="ButtonContract.png">).
 Other sections are accessed by clicking on named tabs (<img src="Tabs.png" alt="Tabs.png">).<br>
<hr>

## Configuration Frame

<br>
The Configuration Frame displays the relevant information from the configured elements in the connected device.<br>
<br>
<img src="ConfigurationPane.png" alt="ConfigurationPane.png"><br>
<ul>
<li><strong>PMU</strong>: Lists all of the configured devices that are connected to the tested device.
</li><li><strong>ID Code</strong>: Displays the unique identifier of the tested device. This is populated from the Device ID Code entered on the Protocols tab in the Connection Parameters section.
</li><li><strong>Phasor</strong>: Lists the labels of all phasors measured by the selected device.
</li><li><strong>Phasors</strong>: Displays the total number of phasors in the selected device.
</li><li><strong>Analogs</strong>: Displays the total number of measured analog values in the selected device.
</li><li><strong>Digitals</strong>: Displays the total number of digital values in the selected device.
</li><li><strong>Nominal Frequency</strong>: </li><li><strong>Power (MW)</strong>: Displays the calculated megawatts, based on the Voltage Phasor and Current Phasor selections on the
<a href="#settings-tab">Settings tab</a>. </li><li><strong>Vars (MVars)</strong>: Displays the calculated megavars, based on the Voltage Phasor and Current Phasor selections on the
<a href="#settings-tab">Settings tab</a>. </li></ul>
<hr>

## Connection Parameters

<br>
The Connection Parameters screen section displays the details concerning the connection between the PMU Connection Tester and the tested device. The details differ depending upon the communication protocol selected.<br>

### Tcp

<img src="ConnTcp.png" alt="ConnTcp.png"><br>
<ul>
<li><strong>Host IP</strong>: The internet address of the device being tested. </li><li><strong>Port</strong>: The port number through which the PMU Connection Tester is connected to and receiving data from the tested device.
</li><li><strong>Establish Tcp Server</strong>: Select to establish a TCP Server style connection to listen for remote device data streams, such as from FNet devices.
</li></ul>

### Udp

<img src="ConnUdp.png" alt="ConnUdp.png"><br>
<ul>
<li><strong>Local Port</strong>: The port number through which the PMU Connection Tester is receiving data from the tested device.
</li><li><strong>Enable Multicast / Remote Udp</strong>: If the tested device listens on UDP, select this check box which makes the following screen elements available.
</li><li><strong>Host IP</strong>: The internet address of the device being tested. </li><li><strong>Remote Port</strong>: The port number on the tested device through which the PMU Connection Tester sends commands.
</li></ul>

### Serial

<img src="ConnSerial.png" alt="ConnSerial.png"><br>
<ul>
<li><strong>Port</strong>: The local serial port through which the PMU Connection Tester is receiving data from the tested device.
</li><li><strong>Baud Rate</strong>: Baud rate to use during transmission to the tested device.
</li><li><strong>Parity</strong>: Parity to use during transmission to the tested device.
</li><li><strong>Stop Bits</strong>: Number of stop bits to use during transmission to the tested device.
</li><li><strong>Data Bits</strong>: Number of data bits to use during transmission to the tested device.
</li><li><strong>DTR</strong>: Enables the Data Terminal Ready flag. </li><li><strong>RTS</strong>: Enables the Ready To Send flag. </li></ul>

### File

<img src="ConnFile.png" alt="ConnFile.png"><br>
<ul>
<li><strong>Filename</strong>: The name of the saved file created by recording data streams.
</li><li><strong>Frame Rate</strong>: Number of frames per second the saved data streams are displayed.
</li><li><strong>Auto-repeat captured file playback</strong>: Check this box to have the PMU Connection Tester return to the beginning of the sample file once it reaches the end.
</li></ul>

### Protocol and ID

<img src="Protocol.png" alt="Protocol.png"><br>
<ul>
<li><strong>Protocol</strong>: Lists the available phasor data protocols. </li><li><strong>Device ID Code</strong>: Specifies the identification code often needed to establish a connection.
</li></ul>

### Command

<img src="Command.png" alt="Command.png"><br>
<ul>
<li><strong>Command</strong>: Lists the available commands that the tested device will respond to.
</li></ul>
<br>
<img src="CommandList.png" alt="CommandList.png"><br>
<ul>
<li><strong>Send</strong>: Transmits the selected command to the device. </li></ul>
<hr>

## Graph Tab

<br>
Displays graphically the real-time data stream from the tested device.<br>
<br>
<img src="TabGraph.png" alt="TabGraph.png"><br>
<br>
The top graph shows the system frequency as measured by the tested device. The bottom graph displays the phase angles.<br>
<br>
If the <strong>Phase Angle Graph Style</strong> (<a href="#settings-tab">Settings tab</a>) is set to
<strong>Relative</strong>, all the displayed phase angles will be relative to the selected device in the
<a href="#configuration-frame">Configuration Frame</a>. If it is set to <strong>Raw</strong>, the phase angles are displayed as measured by the tested device.<br>
<br>
The legend on the bottom right of the Graph tab displays the configured devices connected to the tested device, as listed in the Phasor box on the
<a href="#configuration-frame">Configuration Frame</a>.<br>
<hr>

## Messages Tab

<br>
The Messages tab displays any relevant information about, or errors generated by, the connection to the tested device.<br>
<br>
<img src="TabMessages.png" alt="TabMessages.png"><br>
<hr>

## Protocol Specific Tab

<br>
Displays a hierarchy of the last parsed protocol specific frames, and includes all data values relevant to the protocol.<br>
<br>
<img src="ProtocolSpec.png" alt="ProtocolSpec.png"><br>
<br>
This tab enables quick validation of protocol-specific properties of vendor implementation.<br>
<hr>

## Settings Tab

<br>
The Settings tab allows the user to configure various settings related to the PMU Connection Tester display.<br>
<br>
<img src="TabSettings.png" alt="TabSettings.png"><br>
<ul>
<li><strong>Voltage Phasor</strong>: Lists the voltage phasor used in the megawatt and megavar calculation displayed on the
<a href="#configuration-frame">Configuration Frame</a>. </li><li><strong>Current Phasor</strong>: Lists the current phasor used in the megawatt and megavar calculation displayed on the
<a href="#configuration-frame">Configuration Frame</a>. </li><li><strong>Get Parsing Status</strong>: Displays the current connection status on the
<a href="#messages-tab">Messages tab</a>. </li><li><strong><a href="#application-settings">Application Settings</a></strong>: Defines general application settings.
</li><li><strong><a href="#attribute-tree">Attribute Tree</a></strong>: Defines settings associated with the attribute tree visible on the
<a href="#protocol-specific-tab">Protocol Specific tab</a>. </li><li><strong><a href="#chart-settings">Chart Settings</a></strong>: Defines general chart-related settings.
</li><li><strong><a href="#frequency-graph">Frequency Graph</a></strong>: Defines settings related to the frequency graph.
</li><li><strong><a href="#phase-angle-graph">Phase Angle Graph</a></strong>: Defines settings related to the phase angle graph.
</li></ul>

### Application Settings

<img src="ApplicationSettings.png" alt="ApplicationSettings.png"><br>
<ul>
<li><strong>AutoStartDataParsingSequence</strong> [Range: <span style="text-decoration:underline">
True</span>/False]: Set to True to automatically send commands for ConfigFrame2 and EnableRealTimeData.
</li><li><strong>ExecuteParseOnSeparateThread</strong> [Range: True/<span style="text-decoration:underline">False</span>]: Allows frame parsing to be executed on a separate thread (other than communications thread) - typically only needed when data frames are very
 large. This change will happen dynamically, even if a connection is active. </li><li><strong>MaximumConnectionAttempts</strong> [Range: 1-n]: Maximum number of times to attempt connection before giving up. Set the value to -1 to continue connection attempt indefinitely.
</li><li><strong>MaximumFrameDisplayBytes</strong> [Range: 1-n]: Maximum encoded bytes to display for frames in the
<a href="#real-time-frame-detail">Real-time Frame Detail</a>. </li><li><strong>RestoreLastConnectionSettings</strong> [Range: <span style="text-decoration:underline">
True</span>/False]: Set to True to load previous connection settings at startup. </li></ul>

### Attribute Tree

<img src="AttributeTree.png" alt="AttributeTree.png"><br>
<ul>
<li><strong>ChannelNodeBackgroundColor</strong>: Defines the highlight background color for channel node entries on the attribute tree.
</li><li><strong>ChannelNodeForegroundColor</strong>: Defines the highlight foreground color for channel node entries on the attribute tree.
</li><li><strong>InitialNodeState</strong> [Range: <span style="text-decoration:underline">
Collapsed</span>/Expanded]: Defines the initial state for nodes when added to the attribute tree. Note that a fully expanded tree will take much longer to initialize.
</li><li><strong>ShowAttributesAsChildren</strong> [Range: <span style="text-decoration:underline">
True</span>/False]: Set to <span>True </span>to show attributes as children of their channel entries.
</li></ul>

### Chart Settings

<img src="ChartSettings.png" alt="ChartSettings.png"><br>
<ul>
<li><strong>BackgroundColor</strong>: Select the background color for the <a href="#graph-tab">
graph region</a>. </li><li><strong>ForegroundColor</strong>: Select the foreground color for <a href="#graph-tab">
graph region</a> (axes, legend border, text, etc.). </li><li><strong>RefreshRate</strong> [Range: 0.1-n]: Chart refresh rate in seconds. The typical setting is
<span>0.1 </span>, increase this number if the application runs slowly. </li><li><strong>ShowDataPointsOnGraphs</strong> [Range: <span style="text-decoration:underline">
True</span>/False]: Set to <span>True </span>to show data points on graphs. </li><li><strong>TrendLineWidth</strong> [Range: 1-n]: Enter the trend line graphing width (in pixels).
</li></ul>

### Frequency Graph

<img src="FrequencyGraph.png" alt="FrequencyGraph.png"><br>
<ul>
<li><strong>FrequencyColor</strong>: Foreground color for frequency trend. </li><li><strong>FrequencyPointsToPlot</strong> [Range: 1-n]: Sets the total number of frequency points to display.
</li></ul>

### Phase Angle Graph

<img src="PhaseAngleGraph.png" alt="PhaseAngleGraph.png"><br>
<ul>
<li><strong>LegendBackgroundColor</strong>: Background color for phase angle legend.
</li><li><strong>LegendForegroundColor</strong>: Foreground color for phase angle legend text.
</li><li><strong>PhaseAngleColors</strong> [Range: Collection/]: Possible foreground colors for phase angle trends.
</li><li><strong>PhaseAngleGraphStyle</strong> [Range: Raw/<span style="text-decoration:underline">Relative</span>]: Sets the phase angle graph to plot either raw or relative phase angles.
</li><li><strong>PhaseAnglePointsToPlot</strong> [Range: 1-n]: Sets the total number of phase angle points to display.
</li><li><strong>ShowPhaseAngleLegend</strong> [Range: <span style="text-decoration:underline">
True</span>/False]: Set to <span>True </span>to show phase angle graph legend. </li></ul>
<hr>

## Header Frame

<br>
The Header Frame displays the user-configured information file, usually in the form of a text file, that is stored on the tested device.<br>
<br>
<img src="HeaderFrame.png" alt="HeaderFrame.png"><br>
<hr>

## Real-time Frame Detail

<br>
The Real-time Frame Detail section displays details about the measurements received from the tested device.<br>
<br>
<img src="RealTimeFrameDetail.png" alt="RealTimeFrameDetail.png"><br>
<ul>
<li><strong>Frame type</strong>: Displays the phasor protocol frame type of the frame received from the tested device.
</li><li><strong>Time</strong>: Displays the time value parsed from the received frame, displayed in Universal Coordinated Time (UTC).
</li><li><strong>Frequency</strong>: Displays the measured system frequency received in the frame.
</li><li><strong>Angle</strong>: Displays the measured phase angle of the currently selected Phasor (<a href="#configuration-frame">Configuration Frame</a>) received in the frame.
</li><li><strong>Magnitude</strong>: Displays the measured magnitude of the currently selected Phasor (<a href="#configuration-frame">Configuration Frame</a>) received in the frame (The parenthetical value is the calculated line-to-neutral value when the selected phasor
 is a voltage.). </li><li><strong>Display</strong>: Lists the various formats in which the binary data in the received frame can be shown.
</li></ul>
<br>
<img src="ListDisplay.png" alt="ListDisplay.png"><br>
<hr>

# Operations


## Capturing Sample Frames

<br>
The PMU Connection Tester allows the user to debug and analyze protocol-specific phasor frames from the tested device. Note that only one of each type of frame can be collected during the capture sequence.<br>
<ol>
<li>From the File menu, point to Capture and then select the Capture Sample Frames menu option.
</li><li>In the displayed save screen, enter the desired File name of the output file.
</li><li>Click OK on the displayed message (Sample frame collection will complete upon reception of a configuration frame).
</li><li>The file will be displayed in a separate window as a .txt file. </li></ol>
<br>
<a href="SampleFrames.txt">Click to view a sample file.</a><br>
<hr>

## Connecting to Devices

### Connecting via TCP
<ol>
<li>Click the <strong>Tcp</strong> tab. </li><li>Enter the <strong>Host IP</strong> address of the device being tested. </li><li>Enter the <strong>Port</strong> number through which the PMU Connection Tester is receiving the signal from the tested device.
</li><li>Click <strong>Connect</strong>. </li></ol>

### Connecting via UDP

<ol>
<li>Click the <strong>Udp</strong> tab. </li><li>Enter the <strong>Local Port</strong> number through which the PMU Connection Tester is receiving the signal from the tested device.
</li><li>Click the <strong>Remote device listens on Udp</strong> check box, if applicable, and then enter the
<strong>Host IP</strong> and <strong>Remote Port</strong> of the tested device. </li><li>Click <strong>Connect</strong>. </li></ol>

### Connecting via a Serial Port

<ol>
<li>Click the <strong>Serial</strong> tab. </li><li>Enter the <strong>Port</strong> through which the PMU Connection Tester is receiving the signal from the tested device.
</li><li>Enter the configuration details the data transfer between the PMU Connection Tester and the tested device.
</li><li>Click Connect. </li></ol>
<hr>

## Loading Configuration Files

<ol>
<li>From the <strong>File</strong> menu, point to <strong>Config File</strong> and then select the
<strong>Load</strong> menu option. </li><li>In the displayed window, select the saved configuration file (XML format), and click
<strong>Open</strong>. </li></ol>
<hr>

## Recording and Replaying Data Streams

<br>
The PMU Connection Tester allows the user to record all streaming data to a file for later playback and analysis.<br>
<ol>
<li>From the <strong>File</strong> menu, point to <strong>Capture</strong> and then select the
<strong>Start Capture</strong> menu option. </li><li>In the displayed save screen, enter the desired <strong>File name</strong> of the output file.
</li><li>The capture process will continue until you select the <strong>File &gt; Capture &gt; Stop Capture</strong> menu option.
</li></ol>
<br>
To replay the data stream:<br>
<ol>
<li>Click the <strong>File</strong> tab. </li><li>Enter or select the saved <strong>Filename</strong>. </li><li>Select the <strong>Frame Rate</strong> for the display. </li><li>Verify the <strong>Protocol</strong> on the <a href="#connection-parameters">
Connection Parameters</a> screen section. </li><li>Click <strong>Connect</strong>. </li></ol>
<hr>

## Saving Configuration Files

<br>
To save a configuration (Only available when connected, and after a configuration frame has been received):<br>
<ol>
<li>From the <strong>File</strong> menu, point to <strong>Config File</strong> and then select the
<strong>Save</strong> menu option. </li><li>In the displayed save screen, enter the desired <strong>File name</strong> of the output file, and click
<strong>Save</strong>. </li></ol>
<br>
<a href="SampleFile.xml">Click to view a sample configuration file.</a><br>
<hr>

## Using Previous Connections

<br>
The PMU Connection Tester automatically saves the last connection by default. To save a connection for later use:<br>
<ol>
<li>From the <strong>File</strong> menu, point to <strong>Connection</strong> and then select the
<strong>Save</strong> menu option. </li><li>In the displayed save screen, enter the desired <strong>File name</strong> of the output file, and click
<strong>Save</strong>. </li></ol>
<br>
To use a previous connection:<br>
<ol>
<li>From the <strong>File</strong> menu, point to <strong>Connection</strong> and then select the
<strong>Load</strong> menu option. </li><li>In the displayed window, select the saved connection file, and click <strong>
Open</strong>. </li><li>Click <strong>Connect</strong>. </li></ol>
<br>
Connection files have a <span>.PMUConnection </span>extension. When installed, the PMU Connection Tester associates itself with files of that extension.<br>
<hr>

## Stream Debug Capturing

<br>
The PMU Connection Tester is capable of capturing raw data to a CSV file for analysis. To capture data to a CSV file:<br>
<ol>
<li>From the <strong>File</strong> menu, point to <strong>Capture</strong> and then select the
<strong>Start Stream Debug Capture...</strong> menu option. </li><li>In the displayed save screen, enter the desired <strong>File name</strong> of the output file.
</li><li>The capture process will continue until you select the <strong>File &gt; Capture &gt; Stop Stream Debug Capture</strong> menu option.
</li></ol>
<br>
Once you've captured the data, you can open the file using Microsoft Office Excel and follow these steps to format the timestamps to be human readable.<br>
<ol>
<li>Select the entire first column, right-click the column header, and select &quot;Format Cells&quot;.
</li><li>In the &quot;Category&quot; list, select &quot;Custom&quot;. </li><li>Enter &quot;mm/dd/yyyy h:mm:ss.000&quot; into the &quot;Type&quot; text box and select the &quot;OK&quot; button.
</li></ol>
