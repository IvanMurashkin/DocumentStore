using System;
using System.Web.Mvc;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DocStore.Domain.Model;
using Services.Manager;

namespace DocStore.WebUI.Controllers {
    [Authorize]
    public class DocumentController : Controller {

        private IDocumentManager documentManager;
        private IUserManager userManager;

        public DocumentController(IDocumentManager documentManager,
            IUserManager userManager) {
            this.documentManager = documentManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index() {
            return View(documentManager.GetList());
        }

        [HttpGet]
        public PartialViewResult Save() {
            return PartialView(new Document());
        }

        [HttpPost]
        public ActionResult Save(Document doc, HttpPostedFileBase document = null) {

            if (!ModelState.IsValid) {
                return View(doc);
            }

            var currentUser = userManager.Get(HttpContext.User.Identity.Name) as User;
            if (currentUser == null) {
                ModelState.AddModelError("", "Что-то пошло не так!");
                return View(doc);
            }
            doc.User = currentUser;
            
            doc.Date = DateTime.Now;

            if (document != null) {
                doc.OriginalName = Path.GetFileName(document.FileName);
                document.SaveAs(Server.MapPath("~/Files/" + doc.OriginalName));

                doc.DocumentMimeType = document.ContentType;
                doc.DocumentData = new byte[document.ContentLength];
                document.InputStream.Read(doc.DocumentData, 0, document.ContentLength);
            } else {
                ModelState.AddModelError("", "Выберите файл для загрузки");
                return View(doc);
            }

            documentManager.Save(doc);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public PartialViewResult Search() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Search(string documentName) {

            var documents = documentManager.GetList() as IEnumerable<Document>;
            var result = documents.Where(d => d.Name == documentName);

            return PartialView("Documents", result);
        }

        [HttpPost]
        public ActionResult DeleteDoc(long id, string originalFileName) {

            var document = documentManager.Get(id) as Document;
            if (document == null) {
                PartialView("Documents");
            }

            documentManager.Delete(document);

            var fullPath = Request.MapPath("~/Files/" + originalFileName);
            if (System.IO.File.Exists(fullPath)) {
                System.IO.File.Delete(fullPath);
            }

            return PartialView("Documents", documentManager.GetList());
        }

        [HttpPost]
        public ActionResult GetFile(
            long id,
            string contentType, 
            string originalFileName, 
            string customFileName) {

            var filePath = Server.MapPath("~/Files/" + originalFileName);
            if (!System.IO.File.Exists(filePath)) {
                DeleteDoc(id, "");
                return View("Index");
            }
            var fileExtension = Path.GetExtension(filePath);

            return File(filePath, contentType, string.Format("{0}{1}", customFileName, fileExtension));

        }

    }
}