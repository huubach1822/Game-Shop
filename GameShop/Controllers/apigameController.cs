using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameShop.Models;


namespace GameShop.Controllers
{
    public class apigameController : ApiController
    {
        game_shop_webEntities db = new game_shop_webEntities();
        // GET: api/apigame

        public List<banner> Get()
        {
            List<banner> banners = db.banners.ToList();
            return banners;
        }

        public banner Get(int bannerID)
        {
            banner banners = db.banners.Where(n => n.id == bannerID).SingleOrDefault();
            banners.img_url = Url.Content(banners.img_url);
            return banners;
        }








    }
}
