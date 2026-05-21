//******************************************************************************************************
//  ChannelIndexEditor.cs - Gbtc
//
//  Copyright © 2026, Grid Protection Alliance.  All Rights Reserved.
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
//  05/20/2026 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************
// ReSharper disable LocalizableElement

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ConnectionTester;

// Base UITypeEditor that pops up a CheckedListBox of "i: Label" entries when the user clicks
// the property cell. The selected indexes are written back as a canonical index-range string
// (e.g., "*", "0-2,4"). Channel labels are pulled off the live ApplicationSettings instance.
internal abstract class ChannelIndexEditor : UITypeEditor
{
    protected abstract string[] GetChannelLabels(ApplicationSettings settings);

    // Title shown at the top of the popup so the user knows which channel list they are editing
    protected abstract string Title { get; }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        if (provider?.GetService(typeof(IWindowsFormsEditorService)) is not IWindowsFormsEditorService editorService)
            return value;

        string[] labels = context?.Instance is ApplicationSettings settings ?
            GetChannelLabels(settings) :
            null;

        string current = value as string;

        if (labels is null || labels.Length == 0)
        {
            using ChannelIndexEmptyPanel emptyPanel = new();
            editorService.DropDownControl(emptyPanel);
            return value;
        }

        HashSet<int> selected = ApplicationSettings.ParseIndexList(current, labels.Length);

        using ChannelIndexCheckList list = new(Title, labels, selected);

        list.SelectionConfirmed += (_, _) => editorService.CloseDropDown();
        editorService.DropDownControl(list);

        return ApplicationSettings.FormatIndexList(list.GetCheckedIndexes(), labels.Length);
    }
}

internal sealed class PhasorIndexEditor : ChannelIndexEditor
{
    protected override string[] GetChannelLabels(ApplicationSettings settings)
    {
        return settings.PhasorChannelLabels;
    }

    protected override string Title => "Phasors to Plot";
}

internal sealed class AnalogIndexEditor : ChannelIndexEditor
{
    protected override string[] GetChannelLabels(ApplicationSettings settings)
    {
        return settings.AnalogChannelLabels;
    }

    protected override string Title => "Analogs to Plot";
}

// Popup contents when channel labels are available
internal sealed class ChannelIndexCheckList : UserControl
{
    private readonly CheckedListBox m_list;
    private readonly Button m_allButton;
    private readonly Button m_noneButton;
    private readonly Button m_okButton;

    public event EventHandler SelectionConfirmed;

    public ChannelIndexCheckList(string title, string[] labels, HashSet<int> selected)
    {
        Width = 280;
        Padding = new Padding(4);

        int listHeight = Math.Min(260, Math.Max(80, labels.Length * 18 + 4));

        Label titleLabel = new()
        {
            Dock = DockStyle.Top,
            Height = 20,
            Text = title,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font(SystemFonts.MessageBoxFont, FontStyle.Bold),
            Padding = new Padding(2, 0, 0, 0)
        };

        m_list = new CheckedListBox
        {
            CheckOnClick = true,
            IntegralHeight = false,
            Dock = DockStyle.Fill
        };

        bool selectAll = selected is null;

        for (int i = 0; i < labels.Length; i++)
        {
            string label = string.IsNullOrEmpty(labels[i]) ? $"Channel {i + 1}" : labels[i];
            m_list.Items.Add($"{i}: {label}", selectAll || selected.Contains(i));
        }

        Panel buttonRow = new()
        {
            Dock = DockStyle.Bottom,
            Height = 30
        };

        m_allButton = new Button
        {
            Text = "All",
            Left = 4,
            Top = 4,
            Width = 60
        };

        m_allButton.Click += (_, _) =>
        {
            for (int i = 0; i < m_list.Items.Count; i++)
                m_list.SetItemChecked(i, true);
        };

        m_noneButton = new Button
        {
            Text = "None",
            Left = 68,
            Top = 4,
            Width = 60
        };

        m_noneButton.Click += (_, _) =>
        {
            for (int i = 0; i < m_list.Items.Count; i++)
                m_list.SetItemChecked(i, false);
        };

        m_okButton = new Button
        {
            Text = "OK",
            Left = 200,
            Top = 4,
            Width = 60,
            DialogResult = DialogResult.OK
        };

        m_okButton.Click += (_, _) => SelectionConfirmed?.Invoke(this, EventArgs.Empty);

        buttonRow.Controls.Add(m_allButton);
        buttonRow.Controls.Add(m_noneButton);
        buttonRow.Controls.Add(m_okButton);

        // Add order matters for docking (processed last-added first): the Fill list is added
        // first so it docks last and fills the space left between the Top title and Bottom buttons
        Controls.Add(m_list);
        Controls.Add(buttonRow);
        Controls.Add(titleLabel);

        Height = listHeight + titleLabel.Height + buttonRow.Height + Padding.Top + Padding.Bottom;

        // Lock in the natural size so the control can't be collapsed when hosted
        // (e.g., inside a ToolStripControlHost on a chart right-click popup)
        MinimumSize = Size;
    }

    public IEnumerable<int> GetCheckedIndexes()
    {
        for (int i = 0; i < m_list.Items.Count; i++)
        {
            if (m_list.GetItemChecked(i))
                yield return i;
        }
    }
}

// Popup contents when no channel labels are available (no connection yet)
internal sealed class ChannelIndexEmptyPanel : UserControl
{
    public ChannelIndexEmptyPanel()
    {
        Width = 240;
        Height = 60;
        Padding = new Padding(8);

        Label label = new()
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = "Connect to a device to populate this list.",
            Font = new Font(SystemFonts.MessageBoxFont, FontStyle.Italic)
        };

        Controls.Add(label);
    }
}
