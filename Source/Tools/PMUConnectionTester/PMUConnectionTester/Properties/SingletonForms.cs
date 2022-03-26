//******************************************************************************************************
//  SingletonForms.cs - Gbtc
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
//  03/26/2022 - J. Ritchie Carroll
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace ConnectionTester
{
    internal class SingletonForms
    {
        [ThreadStatic]
        private static HashSet<Type> s_formBeingCreated;

        private static T CreateInstance<T>() where T : Form, new()
        {
            if ((s_formBeingCreated ??= new()).Contains(typeof(T)))
                throw new InvalidOperationException("WinForms_RecursiveFormCreate");

            s_formBeingCreated.Add(typeof(T));

            try
            {
                return new T();
            }
            catch (TargetInvocationException ex) when (ex.InnerException is not null)
            {
                throw new InvalidOperationException(ex.InnerException.Message, ex.InnerException);
            }
            finally
            {
                s_formBeingCreated.Remove(typeof(T));
            }
        }

        private static void DisposeInstance<T>(ref T instance) where T : Form
        {
            instance.Dispose();
            instance = null;
        }

        private static void SetInstance<T>(T value, ref T instance) where T : Form
        {
            if (ReferenceEquals(value, instance))
                return;

            if (value is not null)
                throw new ArgumentException("Property can only be set to null");

            DisposeInstance(ref instance);
        }

        internal new Type GetType() => typeof(SingletonForms);

        private PMUConnectionTester m_pmuConnectionTester;

        public PMUConnectionTester PMUConnectionTester
        {
            get => m_pmuConnectionTester ??= CreateInstance<PMUConnectionTester>();
            set => SetInstance(value, ref m_pmuConnectionTester);
        }

        private AlternateCommandChannel m_alternateCommandChannel;

        public AlternateCommandChannel AlternateCommandChannel
        {
            get => m_alternateCommandChannel ??= CreateInstance<AlternateCommandChannel>();
            set => SetInstance(value, ref m_alternateCommandChannel);
        }

        private MulticastSourceSelector m_multicastSourceSelector;

        public MulticastSourceSelector MulticastSourceSelector
        {
            get => m_multicastSourceSelector ??= CreateInstance<MulticastSourceSelector>();
            set => SetInstance(value, ref m_multicastSourceSelector);
        }

        private NetworkInterfaceSelector m_networkInterfaceSelector;

        public NetworkInterfaceSelector NetworkInterfaceSelector
        {
            get => m_networkInterfaceSelector ??= CreateInstance<NetworkInterfaceSelector>();
            set => SetInstance(value, ref m_networkInterfaceSelector);
        }

        private ReceiveFromSourceSelector m_receiveFromSourceSelector;

        public ReceiveFromSourceSelector ReceiveFromSourceSelector
        {
            get => m_receiveFromSourceSelector ??= CreateInstance<ReceiveFromSourceSelector>();
            set => SetInstance(value, ref m_receiveFromSourceSelector);
        }

        private SplashScreen m_splashScreen;

        public SplashScreen SplashScreen
        {
            get => m_splashScreen ??= CreateInstance<SplashScreen>();
            set => SetInstance(value, ref m_splashScreen);
        }
    }
}