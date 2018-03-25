﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

using System;
using System.Drawing;
using NUnit.Framework;

namespace JetBrains.JetListViewLibrary.Tests
{
	[TestFixture, Ignore( "avoid test failures on build machine")]
    public class JetListViewTests
	{
        private JetListView _jetListView;
        private JetListViewColumn _column;

        [SetUp] public void SetUp()
        {
            _jetListView = new JetListView();
            _column = new JetListViewColumn();
            _jetListView.Columns.Add( _column );
        }

        [Test] public void VScrollTest()
        {
            _column.SizeToContent = true;
            _jetListView.Size = new Size( 100, 20 );
            _jetListView.Nodes.Add( "Test" );
            _jetListView.Nodes.Add( "Test2" );

            Assert.IsTrue( _jetListView.VScrollbar.Visible );
            Assert.IsFalse( _jetListView.HScrollbar.Visible );
        }

        [Test] public void HScrollTest()
        {
            _column.SizeToContent = true;
            _jetListView.Size = new Size( 20, 100 );
            _jetListView.Nodes.Add( "Long Long Test" );
            _jetListView.Nodes.Add( "Too Long Long Test" );

            Assert.IsFalse( _jetListView.VScrollbar.Visible );
            Assert.IsTrue( _jetListView.HScrollbar.Visible );
        }
	}
}
