// --------------------------------------------------------------------------
// <copyright file="OdbcDynamicsBcRunCommand.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Devart.AI.McpServer.Odbc.CommandLine;
using Devart.AI.McpServer.Odbc.DynamicsBc.Properties;

namespace Devart.AI.McpServer.Odbc.DynamicsBc.CommandLine
{
  internal sealed class OdbcDynamicsBcRunCommand : OdbcRunCommand
  {
    protected override void RegisterTools(IMcpServerBuilder serverBuilder, McpConfiguration configuration)
      => serverBuilder.WithTools(OdbcDynamicsBcTools.CreateTools(configuration));

    protected override string ProductFullName => ProductInfo.ProductFullName;

    protected override McpAppSettings CreateAppSettings() => new OdbcDynamicsBcAppSettings();
  }
}