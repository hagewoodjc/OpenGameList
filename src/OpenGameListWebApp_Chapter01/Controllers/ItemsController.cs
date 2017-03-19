using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenGameList.ViewModels;
using Newtonsoft.Json;
using OpenGameList.Data;
using Nelibur.ObjectMapper;
using OpenGameList.Data.Items;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenGameList.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private ApplicationDbContext DbContext;

        public ItemsController(ApplicationDbContext context)
        {
            DbContext = context;
        }

        #region RESTful Conventions

        /// <summary>
        /// GET: api/items
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>Nothing: this method will raise a HttpNotFound HTTP exception, since we're not supporting this API call</returns>

        [HttpGet()]
        public IActionResult Get()
        {
            return NotFound(new { Error = "not found" });
        }

        /// <summary>
        /// GET: api/items/{id}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>A Json-serialized object representing a single item. </returns>
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = DbContext.Items.Where(i => i.Id == id).FirstOrDefault();

            if (item != null)
                return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
            else
                return NotFound(new
                {
                    Error = String.Format("Item ID {0} has not been found", id)
                });
        }

        [HttpPost()]
        [Authorize]
        public IActionResult Add([FromBody] ItemViewModel ivm)
        {
            if(ivm != null)
            {
                // create a new Item with the client-sent json data
                var item = TinyMapper.Map<Item>(ivm);

                // override any property that could be wise to set from server-side only
                item.CreateDate = item.LastModifiedDated = DateTime.Now;

                // TODO: replace the following with the current user's id when authentication will be available
                item.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                // add the new item
                DbContext.Items.Add(item);
                DbContext.SaveChanges();

                return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
            }

            return new StatusCodeResult(500);
        }

        ///<summary>
        ///PUT: api/items/{id}
        /// </summary>
        /// <returns>Updates an existing Item and return it accordingly.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]ItemViewModel ivm)
        {
            if(ivm != null)
            {
                var item = DbContext.Items.Where(i => i.Id == id).FirstOrDefault();

                if(item != null)
                {
                    item.UserId = ivm.UserId;
                    item.Description = ivm.Description;
                    item.Flags = ivm.Flags;
                    item.Notes = ivm.Notes;
                    item.Text = ivm.Text;
                    item.Title = ivm.Title;
                    item.Type = ivm.Types;
                    item.LastModifiedDated = DateTime.Now;

                    DbContext.SaveChanges();

                    return new JsonResult(TinyMapper.Map<ItemViewModel>(item), DefaultJsonSettings);
                }                
            }

            return NotFound(new { Error = String.Format("ItemID {0} has not been found.", id) });
        }

        ///<summary>
        ///DELETE: api/items/{id}
        /// </summary>
        /// <returns>Deletes an Item, returning an HTTP status 200 (ok) when done.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var item = DbContext.Items.Where(i => i.Id == id).FirstOrDefault();

            if(item != null)
            {
                DbContext.Items.Remove(item);
                DbContext.SaveChanges();
                return new OkResult();
            }

            return NotFound(new { Error = String.Format("Item ID {0} has not been found.", id) });
        }
             
        #endregion

        #region Attribute-based routing
        /// <summary>
        /// GET: api/items/GetLatest
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An Array of a default number of Json-serialized objects representing the last inserted items.</returns>
        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            return GetLatest(DefaultNumberOfItems);
        }        

        /// <summary>
        /// GET: api/items/GetLatest/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An array of {n} Json-serialized objects representing the last inserted items.</returns>
        [HttpGet("GetLatest/{n}")]
        public IActionResult GetLatest(int n)
        {
            if (n > MaxNumberOfItems)
                n = MaxNumberOfItems;

            var items = DbContext.Items.OrderByDescending(i => i.CreateDate).Take(n).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        /// <summary>
        /// GET: api/items/GetMostViewed/
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An array of default number of Json-serialized objects representing the items with most user views. </returns>
        [HttpGet("GetMostViewed")]
        public IActionResult GetMostViewed()
        {
            return GetMostViewed(DefaultNumberOfItems);
        }

        /// <summary>
        /// GET: api/items/GetMostViewed/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An array of {n} Json-serialized objects representing the items with most user views. </returns>
        [HttpGet("GetMostViewed/{n}")]
        public IActionResult GetMostViewed(int n)
        {
            if (n > MaxNumberOfItems)
                n = MaxNumberOfItems;

            var items = DbContext.Items.OrderByDescending(i => i.ViewCount).Take(n).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        /// <summary>
        /// GET: api/items/GetRandom
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An array of default number of Json-serialized objects representing some randomly-picked items. </returns>
        [HttpGet("GetRandom")]
        public IActionResult GetRandon()
        {
            return GetRandom(DefaultNumberOfItems);
        }

        /// <summary>
        /// GET: api/items/GetRandom/{n}
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <returns>An array of {n} Json-serialized objects representing some randomly-picked items</returns>
        [HttpGet("GetRandom/{n}")]
        public IActionResult GetRandom(int n)
        {
            if (n > MaxNumberOfItems)
                n = MaxNumberOfItems;

            var items = DbContext.Items.OrderBy(i => Guid.NewGuid()).Take(n).ToArray();
            return new JsonResult(ToItemViewModelList(items), DefaultJsonSettings);
        }

        #endregion

        /// <summary>
        /// Maps a collection of Item entities into a list of ItemViewModel objects.
        /// </summary>
        /// <param name="items">An IEnumerable collection of item entities</param>
        /// <returns>A ampped list of ItemViewModel objects</returns>
        private List<ItemViewModel> ToItemViewModelList(IEnumerable<Item> items)
        {
            var lst = new List<ItemViewModel>();
            foreach(var i in items)
            {
                lst.Add(TinyMapper.Map<ItemViewModel>(i));                
            }

            return lst;
        }

        /// <summary>
        /// Generate a sample array of source Items to emulate a database (for testing purposes only).
        /// ROUTING TYPE: attribute-based
        /// </summary>
        /// <param name="num">The number of items to generate: default is 999.</param>
        /// <returns>a defined number of mock items (for testing purposes only). </returns>
        private List<ItemViewModel> GetSampleItems(int num = 999)
        {
            var lst = new List<ItemViewModel>();
            var date = new DateTime(2015, 12, 31).AddDays(-num);

            for(int id = 1; id <= num; id++)
            {
                lst.Add(new ItemViewModel()
                {
                    Id = id,
                    Title = String.Format("Item {0} Title", id),
                    Description = String.Format("This is a sample description for item {0}: Lorem ipsum dolor sit amet.", id),
                    CreatedDate = date.AddDays(id),
                    ViewCount = num - id
                });
            }
            return lst;
        }

        /// <summary> Returns a suitable JsonSerializerSetting object that can gbe used to generate the JsonResult return value for this Controller's methods. </summary>
        private JsonSerializerSettings DefaultJsonSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
            }
        }

        /// <summary> Returns the default number of items to retrieve when using the parameterless overlaods of the API methods retrieving item lists.  </summary>
        private int DefaultNumberOfItems
        {
            get
            {
                return 5;
            }
        }

        /// <summary> Return the maximum number of items to retrieve when using the API methods retrieving item lists </summary>
        private int MaxNumberOfItems
        {
            get
            {
                return 100;
            }
        }

        //// GET api/items/GetLatest/5
        //[HttpGet("GetLatest/{num}")]
        //public JsonResult GetLatest(int num)
        //{
        //    var arr = new List<ItemViewModel>();
        //    for(int i = 1; i <= num; i++)
        //    {
        //        arr.Add(new ItemViewModel()
        //        {
        //            Id = i,
        //            Title = String.Format("Item {0} Description", i)
        //        });
        //    }

        //    var settings = new JsonSerializerSettings()
        //    {
        //        Formatting = Formatting.Indented
        //    };

        //    return new JsonResult(arr, settings);
        //}
    }
}
