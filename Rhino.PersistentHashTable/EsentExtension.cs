using System;
using Microsoft.Isam.Esent.Interop;

namespace Rhino.PersistentHashTable
{
	[CLSCompliant(false)]
	public static class EsentExtension
    {
        public static void WithDatabase(this JET_INSTANCE instance, string database, Action<Session, JET_DBID> action)
        {
            using (var session = new Session(instance))
            {
                JET_DBID dbid;
                Api.JetOpenDatabase(session, database, "", out dbid, OpenDatabaseGrbit.None);
                try
                {
                    action(session, dbid);
                }
                finally
                {
                    Api.JetCloseDatabase(session, dbid, CloseDatabaseGrbit.None);
                }
            }
        }
    }
}