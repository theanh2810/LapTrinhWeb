using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021007.BusinessLayers;
using _19T1021007.DomainModels;

namespace _19T1021007.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string PRODUCT_SEARCH = "ProductSearchCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[PRODUCT_SEARCH] as Models.PaginationSearchInput;

            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            };

            return View(condition);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung mặt hàng";

            Product data = new Product()
            {
                ProductID = 0,
                CategoryID = 0,
                SupplierID = 0,
                Price = 0,
                Unit = "",
                Photo = ""
            };
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = ProductDataService.GetProduct(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhập mặt hàng";
            return View(data);
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(string id)
        {
            int ProductID = Convert.ToInt32(id);

            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(ProductID);
                return RedirectToAction("Index");
            }
            else
            {
                var data = ProductDataService.GetProduct(ProductID);
                return View(data);
            }
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    ProductPhoto dataAdd = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID= productID
                    };
                    return View(dataAdd);
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    ProductPhoto dataEdit = ProductDataService.GetPhoto(photoID);
                    return View(dataEdit);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, long attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    ProductAttribute dataAdd = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID= productID
                    };
                    return View(dataAdd);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    ProductAttribute dataEdit = ProductDataService.GetAttribute(attributeID);
                    return View(dataEdit);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product data, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError(nameof(data.ProductName), "Tên mặt hàng không được để trống");
            }
            if (data.CategoryID == 0)
            {
                ModelState.AddModelError(nameof(data.CategoryID), "Vui lòng chọn loại hàng");
            }
            if (data.SupplierID == 0)
            {
                ModelState.AddModelError(nameof(data.SupplierID), "Vui lòng chọn nhà cung cấp");
            }
            if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính không được để trống");
            }
            

            if (uploadPhoto != null)
            {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                string path = Server.MapPath("~/Photo/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            if (data.Price == 0)
            {
                ModelState.AddModelError(nameof(data.Price), "Giá hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Photo))
            {
                ModelState.AddModelError(nameof(data.Photo), "Vui lòng chọn ảnh");
                data.Photo = "";
            }

            /*if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính không được để trống");
            }
                        
            if(data.Price == 0)
            {
                ModelState.AddModelError(nameof(data.Price), "Vui lòng nhập giá hàng");
            }*/

            if (!ModelState.IsValid)
            {
                if(data.ProductID == 0)
                {
                    return View("Create", data);
                }
                else
                {
                    return View("Edit", data);
                }
            }

            if (data.ProductID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
            {
                ProductDataService.UpdateProduct(data);
            }

            
            return RedirectToAction("Index");
        }

        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue,0,0, out rowCount);
            //int page, int pageSize, string searchValue, int categoryID, int supplierID, out int rowCount
            var result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["ProductSearchCondition"] = condition;
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            if (string.IsNullOrWhiteSpace(data.AttributeName))
            {
                ModelState.AddModelError(nameof(data.AttributeName), "Tên thuộc tính không được để trống");
            }
            
            if (string.IsNullOrWhiteSpace(data.AttributeValue))
            {
                ModelState.AddModelError(nameof(data.AttributeValue), "Gía trị thuộc tính không được để trống");
            }
            if(data.DisplayOrder == 0)
            {
                ModelState.AddModelError(nameof(data.DisplayOrder), "Thứ tự hiển thị không được để trống");
            }
            
            /*if (!ModelState.IsValid)
            {
                if (data.AttributeID == 0)
                {
                    return View("Create", data);
                }
                else
                {
                    return View("Edit", data);
                }
            }*/

            if (data.AttributeID == 0)
            {
                ProductDataService.AddAttribute(data);
            }
            else
            {
                ProductDataService.UpdateAttribute(data);
            }

            return RedirectToAction($"Edit/{data.ProductID}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            // Kiểm soát đầu vào có hợp lệ hay không
            if (string.IsNullOrWhiteSpace(data.Description))
                ModelState.AddModelError(nameof(data.Description), "Mô tả không được để trống");

            if (data.DisplayOrder == 0)
            {
                ModelState.AddModelError(nameof(data.DisplayOrder), "Thứ tự hiển thị không được để trống");
            }

            if (string.IsNullOrWhiteSpace(data.Photo))
            {
                ModelState.AddModelError(nameof(data.Photo), "Vui lòng chọn ảnh");
                data.Photo = "";
            }
                
            if (uploadPhoto != null)
            {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                string path = Server.MapPath("~/Photo/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }

            /*if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }*/
            
            if (data.PhotoID == 0)
            {
                ProductDataService.AddPhoto(data);
            }
            else
            {
                ProductDataService.UpdatePhoto(data);
            }
            
            return RedirectToAction($"Edit/{data.ProductID}");

        }
    }
}