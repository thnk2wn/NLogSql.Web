using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnsureThat;
using NLogSql.Domain;
using NLogSql.Services;
using NLogSql.Services.Mapping;
using NLogSql.Web.Infrastructure.Diagnostics.Logging;
using NLogSql.Web.Models;

namespace NLogSql.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IMappingService _mappingService;
        private readonly ILog _log;
        private readonly IMusicService _musicService;

        public HomeController(IMusicService musicService, ILog log, IMappingService mappingService)
        {
            _mappingService = Ensure.That(mappingService, "mappingService").IsNotNull().Value;
            _log = Ensure.That(log, "log").IsNotNull().Value;
            _musicService = Ensure.That(musicService, "musicService").IsNotNull().Value;
        }

        public virtual ActionResult Index()
        {
            GenerateTestCookie();

            _log.Debug("Retrieving genres");
            var genres = GetGenres().Result;
            _log.Info("Retrieved {0} music genres from the database", genres.Count);

            var model = new HomeModel
            {
                Genres = _mappingService.MapList<Genre, HomeModel.GenreModel>(genres)
            };
            var randomGenre = genres[new Random().Next(0, genres.Count - 1)];
            ViewBag.Message = string.Format("Feeling lucky? How about some {0} music?", randomGenre.Name);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult GenerateTestException(string someData)
        {
            throw new NullReferenceException("Something was null. Well not really but this is a test");
        }

        private async Task<IList<Genre>> GetGenres()
        {
            return await _musicService.GetGenresAsync();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public virtual ActionResult BatchTest()
        {
            var sw = Stopwatch.StartNew();
            const int count = 10000;
            for (var i = 0; i < count; i++)
            {
                _log.Trace("Testing {0}", i);
            }
            sw.Stop();
            var msg = string.Format("{0} log invokes in {1:##0.000} seconds", count, sw.Elapsed.TotalSeconds);
            _log.Info(msg);

            return new ContentResult
                {
                    Content = msg
                };
        }

        private void GenerateTestCookie()
        {
            const string cookieName = "TestCookie";
            var myCookie = Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
            myCookie.Values["LastVisit"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            myCookie.Expires = DateTime.Now.AddDays(365);
            Response.Cookies.Add(myCookie);
        }
    }
}
