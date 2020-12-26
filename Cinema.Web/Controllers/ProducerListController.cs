using Cinema.Web.Models;
using Cinema.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    
    public class ProducerListController : Controller
    {
        private readonly IListService<Producer> listProducer;

        public ProducerListController(IListService<Producer> listService)
        {
            listProducer = listService;
        }

        // GET: ProducerController
        public ActionResult Index()
        {
            return View(listProducer.GetAll());
        }

        // GET: ProducerController/Details/5
        public ActionResult Details(int id)
        {
            return View(listProducer.Find(id));
        }

        // GET: ProducerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producer producer)
        {
            try
            {
                if(listProducer.Find(producer.Id) != null)
                {
                    ModelState.AddModelError("Id", "Id Already exist");
                }
                

                if (ModelState.IsValid) { 
                    listProducer.Add(producer); 

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(listProducer.Find(id));
        }

        // POST: ProducerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producer producer)
        {
            try
            {
                listProducer.Edit(producer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(listProducer.Find(id));
        }

        // POST: ProducerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Producer producer)
        {
            try
            {
                listProducer.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
