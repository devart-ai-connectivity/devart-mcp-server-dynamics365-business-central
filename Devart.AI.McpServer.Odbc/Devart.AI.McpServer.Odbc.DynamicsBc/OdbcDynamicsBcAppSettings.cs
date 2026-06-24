// --------------------------------------------------------------------------
// <copyright file="OdbcDynamicsBcAppSettings.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

namespace Devart.AI.McpServer.Odbc.DynamicsBc
{
  internal sealed class OdbcDynamicsBcAppSettings : McpAppSettings
  {
    public override string ServerName => $"Devart {Properties.ProductInfo.ProductFullName}";

    public override string SourceName => "Dynamics 365 Business Central";

    public override bool OnPremise => false;
  }
}
