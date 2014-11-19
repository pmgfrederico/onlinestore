using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgGroup.OnlineStore.WebHost.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products
        public string AnonymousId()
        {
            return this.Request.AnonymousID;
        }
    }
}