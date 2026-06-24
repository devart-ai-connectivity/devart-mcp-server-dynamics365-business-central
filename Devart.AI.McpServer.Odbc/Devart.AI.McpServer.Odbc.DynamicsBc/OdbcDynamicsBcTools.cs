// --------------------------------------------------------------------------
// <copyright file="OdbcDynamicsBcTools.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using ModelContextProtocol.Server;
using Devart.AI.McpServer.Odbc.DynamicsBc.Tools;

namespace Devart.AI.McpServer.Odbc.DynamicsBc
{
  internal static class OdbcDynamicsBcTools
  {
    public static List<McpServerTool> CreateTools(McpConfiguration configuration)
      => OdbcTools.CreateBuilder(configuration)
        .Add(new OdbcDynamicsBcPrimaryKeysTool(configuration))
        .Add(new OdbcDynamicsBcForeignKeysTool(configuration))
        .Build();
  }
}
