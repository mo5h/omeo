﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

// JetIePlugin.cpp : Implementation of DLL Exports.
// 
// CJetIePlugin implements the DLLs main object that is in charge of registering and unregistering it,
// and provides the proper class factory for creating the objects. Note that the need in
// the dynamic class factory is detected in here as well, thus this object takes part in IE controls
// customization process.
//
// Also, some additional objects are registered here, such as the IE Helper object.
//
// © JetBrains Inc, 2005
// Written by (H) Serge Baltic

#include "StdAfx.h"
#include "CommonResource.h"

#include "JetIePlugin.h"
#include "JetIe.h"
#include "MainToolbarButton.h"
#include "DynamicClassFactory.h"

// Get the proper Helper Object declaration
#ifdef JETIE_OMEA
#include "IexploreOmea\OmeaHelper.h"
typedef COmeaHelper CSomeHelper;
#endif
#ifdef JETIE_BEELAXY
#include "IexploreBeelaxy\BeelaxyHelper.h"
typedef CBeelaxyHelper CSomeHelper;
#endif

// The module attribute causes DllMain, DllRegisterServer and DllUnregisterServer to be automatically implemented for you
#ifdef JETIE_OMEA
[ module(dll, uuid = "{633820F7-C04E-4152-B64F-1147B881F998}", 
		 name = "IexploreOmea", 
		 helpstring = "Internet Explorer Omea Add-on Type Library",
		 resource_name = "IDR_IEXPLOREJETPLUGIN")
]
#endif
#ifdef JETIE_BEELAXY
[ module(dll, uuid = "{633820F8-C04E-4152-B64F-1147B881F998}", 
		 name = "IexploreBeelaxy", 
		 helpstring = "Internet Explorer Beelaxy Add-on Type Library",
		 resource_name = "IDR_IEXPLOREJETPLUGIN")
]
#endif
class CJetIePlugin
{
public:
// Override CAtlDllModuleT members
	CJetIePlugin()
	{
	}

	virtual ~CJetIePlugin()
	{
	}

// Implementation
protected:

// Inherits
public:
	STDMETHOD(GetClassObject)( REFCLSID rclsid, REFIID riid, LPVOID* ppv )
	{
		// First, try to instantiate a normal COM object that resides in this DLL
		HRESULT	hRet = CAtlDllModuleT<CJetIePlugin>::GetClassObject(rclsid, riid, ppv);
		if(SUCCEEDED(hRet))
			return hRet;

		IRawActionManagerPtr	oActionManager;

		// Now try to instantiate a dynamic control handler
		try
		{
			oActionManager = CJetIe::GetActionManager();
			// Try to get the control. On failure will throw, on success continue the flow
			oActionManager->ControlFromGuid(rclsid);

			// Yes, it is, create a factory
			// Create the generic button handler object class factory
			CHECK(CComCreator< CComObject<CDynamicClassFactory> >::CreateInstance( NULL, riid, ppv ));	// Create a new instance of the class factory … so that each instance of the CMainToolbarButton class had its own class factory because they should be tuned up to represent different buttons

			// Assign it the control GUID
			static_cast<CDynamicClassFactory*>(*ppv)->SetClsid(rclsid);

			return S_OK;
		}
		COM_CATCH_SILENT();

		// Try a dynamic toolbar
		try
		{
			oActionManager->ControlFamilyFromGuid(rclsid, L"Toolbar");
			CHECK(CComCreator< CComObject<CDynamicClassFactory> >::CreateInstance( NULL, riid, ppv ));
			static_cast<CDynamicClassFactory*>(*ppv)->SetClsid(rclsid);
			return S_OK;
		}
		COM_CATCH_SILENT();

		return CLASS_E_CLASSNOTAVAILABLE;	// Could not instantiate
	}	

	STDMETHOD(RegisterServer)(BOOL bRegTypeLib = FALSE, const CLSID* pCLSID = NULL)
	{
		TRACE(L"Started registering the DLL as a COM server.");
		try
		{
			// Invoke default ATL autogenerated registration scripts
			TRACE(L"Invoking ATL registration mechanisms.");
			_com_util::CheckError( CAtlDllModuleT<CJetIePlugin>::RegisterServer( bRegTypeLib, pCLSID ) );

			CoInitialize(NULL);

			// Register the Browser Helper Object
			RegisterBrowserHelperObject(true);

			// Register the Internet Explorer UI Controls
			CJetIe::GetActionManager()->RegisterControls();

			CoUninitialize();
		}
		COM_CATCH_RETURN_RAW();
		TRACE(L"Completed registering the DLL as a COM server.");

		return S_OK;
	}

	STDMETHOD(UnregisterServer)(BOOL bRegTypeLib = FALSE, const CLSID* pCLSID = NULL)
	{
		TRACE(L"Started unregistering the DLL as a COM server.");
		try
		{
			CoInitialize(NULL);

			// Unregister the Internet Explorer UI Controls
			CJetIe::GetActionManager()->UnregisterControls();

			// Unregister the Browser Helper Object
			RegisterBrowserHelperObject(true);

			CoUninitialize();

			// Invoke default ATL autogenerated registration scripts
			TRACE(L"Invoking ATL registration mechanisms.");
			_com_util::CheckError( CAtlDllModuleT<CJetIePlugin>::UnregisterServer( bRegTypeLib, pCLSID ) );
		}
		COM_CATCH_RETURN_RAW();
		TRACE(L"Completed unregistering the DLL as a COM server.");

		return S_OK;
	}

	/// Registers or unregisters the browser helper object
	void RegisterBrowserHelperObject(bool bRegister)
	{
		TRACE(L"Writing/erasing the Browser Helper Object registration information.");

		// Convert its GUID to a string
		CStringW	sGuid;
		int	nSize = 0x400;
		StringFromGUID2(__uuidof(CSomeHelper), sGuid.GetBuffer(nSize), nSize);
		sGuid.ReleaseBuffer();

		// Write to the Registry or erase
		if(bRegister)
		{	// Write
			CRegKey	rk;
			rk.Create(HKEY_LOCAL_MACHINE, CW2T(L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects\\" + sGuid));	// TODO: HKLM or HKCU?
			rk.SetStringValue(NULL, CJetIe::LoadStringT(IDS_PLUGIN_NAME));	// Mark with the plugin name
			rk.Close();
		}
		else	// Erase
			CRegKey(HKEY_LOCAL_MACHINE).RecurseDeleteKey(CW2T(L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects\\" + sGuid));	// TODO: HKLM or HKCU?
	}
};
