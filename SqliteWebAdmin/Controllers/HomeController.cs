using System;
using System.Web.Mvc;
using Dapper;
using SqliteWebAdmin.Models;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
namespace SqliteWebAdmin.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{           
            return View();
		}

        public ActionResult Tables(Query m)
        {
            if (m.Db() == null) return Content(null);
            m.Sql = m.Sql ?? Sql.TABLES;            
            return Content(m.Db().Query<string>(m.Sql).ToJson());
        }

        public ActionResult Query(Query m)
        {
            if (m.Db() == null) return Content(null);
            m.Sql = m.Sql ?? Sql.TABLES;            
            JsConfig.IncludeNullValues = true;
            try {
                var r = m.Db().Query(m.Sql) as IEnumerable<IDictionary<string, object>>;
                return Content(new { cols = r.First().Keys, rows = r.Select(x => x.Values) }.ToJson());                                
            } catch(Exception e) {
                return Content(new { message = e.Message }.ToJson());
            }
        }
	}
}
