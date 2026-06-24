// --------------------------------------------------------------------------
// <copyright file="OdbcDynamicsBcForeignKeysTool.cs" company="Devart">
//
// Copyright (c) Devart. ALL RIGHTS RESERVED
// Use of the source code is permitted under the license.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Devart.AI.McpServer.Extensions;
using Devart.AI.McpServer.Tools;
using Devart.AI.McpServer.Interfaces;

namespace Devart.AI.McpServer.Odbc.DynamicsBc.Tools
{
  internal sealed class OdbcDynamicsBcForeignKeysTool(McpConfiguration serverConfiguration) : ForeignKeysTool(serverConfiguration)
  {
    protected override async Task<DataTable> GetMetadataTable(
      DbConnection connection, 
      string schema, 
      string tableName, 
      IServiceProvider services, 
      CancellationToken cancellationToken)
    {
      const string sql =
"""
SELECT 
  CONSTRAINT_NAME       AS "FK_NAME",
  FIELD                 AS "FKCOLUMN_NAME",
  RELATED_OBJECT_SCHEMA AS "PKTABLE_SCHEM",
  RELATED_OBJECT        AS "PKTABLE_NAME",
  RELATED_FIELD         AS "PKCOLUMN_NAME",
  ''                    AS "UPDATE_RULE",
  ''                    AS "DELETE_RULE"
FROM SYS_REFERENTIAL_CONSTRAINTS
WHERE
  OBJECT_SCHEMA = ?
  AND OBJECT    = ?
ORDER BY
  RELATED_OBJECT_SCHEMA, RELATED_OBJECT
""";

      var database = services.GetRequiredService<IDatabase>();
      var commandHelper = services.GetRequiredService<ICommandHelper>();

      await using var reader = await database.ExecuteReaderAsync(
        connection,
        sql,
        cmd =>
        {
          commandHelper.AddParameter(cmd, schema);
          commandHelper.AddParameter(cmd, tableName);
        },
        cancellationToken
      ).ConfigureAwait(false);

      return await reader.ToDataTableAsync(OdbcConstants.ForeignKeysCollectionName, cancellationToken).ConfigureAwait(false);
    }
  }
}
