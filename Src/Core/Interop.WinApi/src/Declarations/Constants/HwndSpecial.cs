﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

using System;

namespace JetBrains.Interop.WinApi
{
	/// <summary>
	/// Special HWND values.
	/// Cast to an <see cref="IntPtr"/> or a <see cref="Void"/>* when using.
	/// </summary>
	public enum HwndSpecial
	{
		HWND_TOP = 0,

		HWND_BOTTOM = 1,

		HWND_TOPMOST = -1,

		HWND_NOTOPMOST = -2,

		HWND_MESSAGE = -3,
	}
}