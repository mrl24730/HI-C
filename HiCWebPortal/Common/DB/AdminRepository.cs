using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebPortal.Common;

namespace WebPortal.DB
{
    public static class AdminRepository
    {
        public static IEnumerable<dynamic> GetStats()
        {
            string sql = @"
                SELECT * FROM 
	            (
		            SELECT count(id) AS created_count, CAST((DATEADD(hour, 8, [createdAtUTC])) AS DATE) AS created_date from [tbl_message] GROUP BY CAST((DATEADD(hour, 8, [createdAtUTC])) AS DATE)
	            ) as t1
	            FULL OUTER JOIN
	            (
		            SELECT count(id) AS received_count, CAST((DATEADD(hour, 8, [receivedAtUTC])) AS DATE) AS received_date from [tbl_message] GROUP BY CAST((DATEADD(hour, 8, [receivedAtUTC])) AS DATE)
	            ) as t2 ON t1.created_date = t2.received_date
            ";

            using (IDbConnection conn = DBHelper.CreateConnection())
            {
                IEnumerable<dynamic> stats = conn.Query(sql);

                return stats;
            }
        }
    }
}
