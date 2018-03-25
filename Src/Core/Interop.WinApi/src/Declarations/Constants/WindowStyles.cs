﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

using System;

namespace JetBrains.Interop.WinApi
{
	/// <summary>
	/// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
	/// </summary>
	[Flags]
	public enum WindowStyles : uint
	{
		DS_3DLOOK = 0x0004,
		DS_FIXEDSYS = 0x0008,
		DS_NOFAILCREATE = 0x0010,
		DS_CONTROL = 0x0400,
		DS_CENTER = 0x0800,
		DS_CENTERMOUSE = 0x1000,
		DS_CONTEXTHELP = 0x2000,

		WS_OVERLAPPED = 0x00000000,
		WS_POPUP = 0x80000000,
		WS_CHILD = 0x40000000,
		WS_MINIMIZE = 0x20000000,
		WS_VISIBLE = 0x10000000,
		WS_DISABLED = 0x08000000,
		WS_CLIPSIBLINGS = 0x04000000,
		WS_CLIPCHILDREN = 0x02000000,
		WS_MAXIMIZE = 0x01000000,
		WS_CAPTION = WS_BORDER | WS_DLGFRAME,
		WS_BORDER = 0x00800000,
		WS_DLGFRAME = 0x00400000,
		WS_VSCROLL = 0x00200000,
		WS_HSCROLL = 0x00100000,
		WS_SYSMENU = 0x00080000,
		WS_THICKFRAME = 0x00040000,
		WS_GROUP = 0x00020000,
		WS_TABSTOP = 0x00010000,

		WS_MINIMIZEBOX = 0x00020000,
		WS_MAXIMIZEBOX = 0x00010000,

		WS_TILED = WS_OVERLAPPED,
		WS_ICONIC = WS_MINIMIZE,
		WS_SIZEBOX = WS_THICKFRAME,
		WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

		WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

		WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

		WS_CHILDWINDOW = WS_CHILD,
	}
}