﻿/// <copyright company="JetBrains">
/// Copyright © 2003-2008 JetBrains s.r.o.
/// You may distribute under the terms of the GNU General Public License, as published by the Free Software Foundation, version 2 (see License.txt in the repository root folder).
/// </copyright>

using JetBrains.Omea.OpenAPI;

namespace JetBrains.Omea.MemoryWatchPlugin
{
    public class MemoryWatchPlugin : IPlugin
    {
        public void Register()
        {
        }

        public void Startup()
        {
        }

        public void Shutdown()
        {
        }
    }

    public class ShowMemoryWatchAction: SimpleAction
    {
        public override void Execute( IActionContext context )
        {
            MemoryWatch dlg = new MemoryWatch();
            dlg.Show();
        }
    }
}
