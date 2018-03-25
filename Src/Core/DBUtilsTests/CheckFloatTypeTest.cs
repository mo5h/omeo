﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

// This file was generated by C# Refactory.
// To modify this template, go to Tools/Options/C# Refactory/Code

using System;

using JetBrains.Omea.Database;

using NUnit.Framework;

namespace DBUtil
{
	[TestFixture]
	public class CheckFloatTypeTest
	{
		IDatabase m_database;
		ITable m_testTable;
		[SetUp]
		public void SetUp()
		{
			try
			{
				DBTest.RemoveDBFiles();
				DBStructure database = new DBStructure( "", "MyPal" );
				TableStructure tblPeople = database.CreateTable( "People" );
                tblPeople.CreateColumn( "Id", ColumnType.Integer, true );
                tblPeople.CreateColumn( "Name", ColumnType.String, true );
                tblPeople.CreateColumn( "Cash", ColumnType.Double, true );

				database.SaveStructure();
				m_database = database.OpenDatabase( );

				m_testTable = m_database.GetTable("People");
			}
			catch ( Exception exc )
			{
				Assert.Fail( exc.Message );
			}

		}

		[TearDown]
		public void TearDown()
		{
			try
			{
				m_database.Shutdown();
				DBTest.RemoveDBFiles();
			}
			catch ( Exception exc )
			{
				Assert.Fail( exc.Message );
			}
		}
		[Test]
		public void FloatTypeTest( )
		{
			
			for ( int i = 0; i < 20; i++ )
			{
				IRecord record = m_testTable.NewRecord();
				record.SetValue( 1, "Сергей" );
				record.SetValue( 2, ((double)i)/10.0 );
				record.Commit();
			}
			ICountedResultSet resultSet = 
				m_testTable.CreateResultSet( 2 );
			Assert.AreEqual( 20, resultSet.Count );
			for ( int i = 0; i < 20; i++ )
			{
				IRecord person = resultSet[i];
                Assert.AreEqual( "Сергей", person.GetStringValue( 1 ) );
                Assert.AreEqual( ((double)i)/10.0, person.GetDoubleValue( 2 ) );
			}
            resultSet.Dispose();
		}
	}
}