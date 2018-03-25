﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

#include "Enumerator.h"

using namespace JetBrains::Omea::Base;
using namespace JetBrains::Omea::Containers;

namespace DBIndex
{
	static void TranslateNativeKey2ManagedKey( int type, const BTreeKeyBase* nativeKey, KeyPair* managedKey )
	{
		switch( type )
		{
			case int_Key:
			{
				const BTreeKey<int>* intKey = static_cast< const BTreeKey<int>* >( nativeKey );
				managedKey->_key->SetIntKey( intKey->GetKey() );
				break;
			}
			case long_Key:
			{
				const BTreeKey<long>* longKey = static_cast< const BTreeKey<long>* >( nativeKey );
				managedKey->_key->Key = dynamic_cast< System::IComparable* >( __box( longKey->GetKey() ) );
				break;
			}
            case int_int_Key:
            {
				const BTreeKey< CompoundKey<int,int> >* cKey =
                    static_cast< const BTreeKey< CompoundKey<int,int> >* >( nativeKey );
				Compound* compound = dynamic_cast<Compound*>( managedKey->_key->Key );
				compound->_key1 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._first ) );
                compound->_key2 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._second ) );
				break;
            }
			case int_int_int_Key:
			{
				const BTreeKey< CompoundKeyWithValue<int,int,int> >* cKey =
					static_cast< const BTreeKey< CompoundKeyWithValue<int,int,int> >* >( nativeKey );
				CompoundAndValue* theKey = dynamic_cast< CompoundAndValue* >( managedKey->_key->Key );
				theKey->_key1 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._first ) );
                theKey->_key2 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._second ) );
				theKey->_value = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey().GetValue() ) );
				break;
			}
			case int_int_datetime_Key:
			{
				const BTreeKey< CompoundKeyWithValue<int,int,long> >* cKey = 
					static_cast< const BTreeKey< CompoundKeyWithValue<int,int,long> >* >( nativeKey );
				CompoundAndValue* theKey = dynamic_cast< CompoundAndValue* >( managedKey->_key->Key );
				theKey->_key1 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._first ) );
                theKey->_key2 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._second ) );
				theKey->_value = __box( DateTime( cKey->GetKey().GetValue() ) );
				break;
			}
			case int_datetime_int_Key:
			{
				const BTreeKey< CompoundKeyWithValue<int,long,int> >* cKey = 
					static_cast< const BTreeKey< CompoundKeyWithValue<int,long,int> >* >( nativeKey );
				CompoundAndValue* theKey = dynamic_cast< CompoundAndValue* >( managedKey->_key->Key );
				theKey->_key1 = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey()._first ) );
				theKey->_key2 = __box( DateTime( cKey->GetKey()._second ) );
				theKey->_value = dynamic_cast<IComparable*>( IntInternalizer::Intern( cKey->GetKey().GetValue() ) );
				break;
			}
			default:
			{
                throw new System::Exception( String::Format( "Key type {0} is not supported!", __box( type ) ) );
			}
		}
		managedKey->_offset = nativeKey->GetOffset();
	}


	///////////////////////////////////////////////////////////////////////////
	// GetAllKeysEnumerator implementation
	///////////////////////////////////////////////////////////////////////////

	GetAllKeysEnumerator::GetAllKeysEnumerator( OmniaMeaBTree* bTree )
	{
		_bTree = bTree;
		_btreeHeaderIterator = bTree->_btreeHeaderIterator;
		_current = new KeyPair();
		_current->_key = _bTree->_factoryKey;
		_currentPageKeys = (const BTreeKeyBase**)
			DBIndexHeapObject::operator new( MAX_KEYS_IN_PAGE * sizeof( const BTreeKeyBase* ) );
		Reset();
	}

	Object* GetAllKeysEnumerator::get_Current()
	{
		TranslateNativeKey2ManagedKey( _bTree->_keyType, _currentPageKeys[ _currentPageIndex ] , _current );
		return _current;
	}
	
	bool GetAllKeysEnumerator::MoveNext()
	{
		while( ++_currentPageIndex >= _currentPageCount )
		{
			if( _btreeHeaderIterator->Exhausted() )
			{
				return false;
			}
			BTreePageBase* page = _bTree->GetPageByOffset( _btreeHeaderIterator->GetCurrentOffset() );
			_currentPageCount = (short) page->GetAllKeys( _currentPageKeys );
			if( _currentPageCount > MAX_KEYS_IN_PAGE )
			{
				throw new BadIndexesException( "BTree contains cycles. Possible memory corruption." );
			}
			_currentPageIndex = -1;
			_btreeHeaderIterator->MoveNextPage();
		}
		return true;
	}

	void GetAllKeysEnumerator::Reset()
	{
		_bTree->_btreeHeader->GetMinimumPage( *_btreeHeaderIterator );
		_currentPageIndex = -1;
		_currentPageCount = 0;
	}

	void GetAllKeysEnumerator::Dispose()
	{
		if( _currentPageKeys )
		{
			DBIndexHeapObject::operator delete( _currentPageKeys );
			_currentPageKeys = 0;
		}
		else
		{
			throw new System::ObjectDisposedException( "GetAllKeysEnumerator.Dispose(): object is already disposed." );
		}
	}

	
	///////////////////////////////////////////////////////////////////////////
	// SearchForRangeEnumerator implementation
	///////////////////////////////////////////////////////////////////////////

	SearchForRangeEnumerator::SearchForRangeEnumerator()
	{
		_current = new KeyPair();
		_currentPageKeys = (const BTreeKeyBase**)
			DBIndexHeapObject::operator new( MAX_KEYS_IN_PAGE * sizeof( const BTreeKeyBase*) );
	}

	void SearchForRangeEnumerator::Init( OmniaMeaBTree* bTree, IFixedLengthKey* beginKey, IFixedLengthKey* endKey )
	{
		_bTree = bTree;
		_beginKey = beginKey;
		_endKey = endKey;
		_btreeHeaderIterator = bTree->_btreeHeaderIterator;
		_current->_key = _bTree->_factoryKey;
		Reset();
	}

	Object* SearchForRangeEnumerator::get_Current()
	{
		TranslateNativeKey2ManagedKey( _bTree->_keyType, _currentPageKeys[ _currentPageIndex ] , _current );
		return _current;
	}
	
	bool SearchForRangeEnumerator::MoveNext()
	{
		while( ++_currentPageIndex >= _currentPageCount )
		{
			if( _btreeHeaderIterator->Exhausted() )
			{
				return false;
			}
			const BTreeKeyBase& lastKey = *( _bTree->_lastKey );
			BTreeKeyBase& headerKey = *( _bTree->_headerKey );
			_btreeHeaderIterator->GetCurrentKey( headerKey );
			if( _bTree->_keyComparer->Less( lastKey, headerKey ) )
			{
				return false;
			}
			BTreePageBase* page = _bTree->GetPageByOffset( _btreeHeaderIterator->GetCurrentOffset() );
			_currentPageCount =
				(short) page->SearchForRange( *( _bTree->_firstKey ), lastKey, _currentPageKeys );
			if( _currentPageCount > MAX_KEYS_IN_PAGE )
			{
				throw new BadIndexesException( "BTree contains cycles. Possible memory corruption." );
			}
			_currentPageIndex = -1;
			_btreeHeaderIterator->MoveNextPage();
		}
		return true;
	}

	void SearchForRangeEnumerator::Reset()
	{
		_bTree->SetFirstAndLastKeys( _beginKey, _endKey );
		_bTree->_btreeHeader->GetPage( *( _bTree->_firstKey ), *_btreeHeaderIterator );
		_currentPageIndex = -1;
		_currentPageCount = 0;
	}

	void SearchForRangeEnumerator::Dispose()
	{
		SearchForRangeEnumerable::_enumeratorPool->Dispose( this );
	}


	///////////////////////////////////////////////////////////////////////////
	// SearchForRangeEnumerable implementation
	///////////////////////////////////////////////////////////////////////////

	void SearchForRangeEnumerable::Init( IFixedLengthKey* beginKey, IFixedLengthKey* endKey )
	{
		if( _enumeratorPool == 0 )
		{
			_enumeratorPool = new ObjectPool(
				64, new ObjectPool::CreateObjectDelegate( 0, &SearchForRangeEnumerable::CreateNewEnumerator ), 0, 0 );
		}
		_beginKey = beginKey;
		_endKey = endKey;
	}

	System::Object* SearchForRangeEnumerable::CreateNewEnumerator()
	{
		return new SearchForRangeEnumerator();
	}

	IEnumerator* SearchForRangeEnumerable::GetEnumerator()
	{
		SearchForRangeEnumerator* result = dynamic_cast<SearchForRangeEnumerator*> ( _enumeratorPool->Alloc() );
		result->Init( _bTree, _beginKey, _endKey );
		return result;
	}
}