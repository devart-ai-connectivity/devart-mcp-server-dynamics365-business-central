// --------------------------------------------------------------------------
// <copyright file="OdbcDynamicsBcPrimaryKeysTool.cs" company="Devart">
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
using Devart.AI.McpServer.Interfaces;
using Devart.AI.McpServer.Tools;

namespace Devart.AI.McpServer.Odbc.DynamicsBc.Tools
{
  internal sealed class OdbcDynamicsBcPrimaryKeysTool(McpConfiguration serverConfiguration) : PrimaryKeysTool(serverConfiguration)
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
  KSU.CONSTRAINT_NAME AS "PK_NAME",
  KSU.COLUMN_NAME     AS "COLUMN_NAME"
FROM
  SYS_KEY_COLUMN_USAGE KSU
INNER JOIN
  SYS_TABLE_CONSTRAINTS TC ON KSU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
WHERE
  TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
  AND KSU.TABLE_SCHEMA = ?
  AND KSU.TABLE_NAME = ?
ORDER BY
  KSU.CONSTRAINT_NAME, KSU.ORDINAL_POSITION
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

      return await reader.ToDataTableAsync(OdbcConstants.PrimaryKeysCollectionName, cancellationToken).ConfigureAwait(false);
    }
  }
}
