using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    public class DisplayUserController : Controller
    {
        

        // GET: DisplayUser
        public ActionResult Index()
        {

            IEnumerable<UserViewModel> users = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60517/");
                //HTTP GET
                var responseTask = client.GetAsync("api/User");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();

                    users = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    users = Enumerable.Empty<UserViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(users);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60517/api/user");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<UserViewModel>("User", user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60517/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("user/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public async System.Threading.Tasks.Task<ActionResult> Edit(int id)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Uri uri = new Uri("http://localhost:60517/api/user?userid=" + id.ToString());

            HttpResponseMessage response1;

            response1 = await client.GetAsync(uri);

            var readTask = response1.Content.ReadAsAsync<List<UserViewModel>>().Result;

            UserViewModel user = readTask[0];
            
            return View(user);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60517/api/user");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<UserViewModel>("user", user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }
    }

}

