using Npgsql;
using Respawn;

public static class DatabaseCleaner
{
    public static async Task ResetDatabaseAsync(string connectionString)
    {
        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        var checkpoint = new Checkpoint
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"],
        };

        await checkpoint.Reset(conn);

        const string resetSequencesSql = @"
        DO $$ DECLARE
            seq RECORD;
        BEGIN
            FOR seq IN
                SELECT c.relname as seqname
                FROM pg_class c
                JOIN pg_namespace n ON n.oid = c.relnamespace
                WHERE c.relkind = 'S'
                    AND n.nspname = 'public'
            LOOP
                EXECUTE format('ALTER SEQUENCE %I RESTART WITH 1', seq.seqname);
            END LOOP;
        END $$;
        ";

        await using var cmd = new NpgsqlCommand(resetSequencesSql, conn);
        await cmd.ExecuteNonQueryAsync();
    }
}
