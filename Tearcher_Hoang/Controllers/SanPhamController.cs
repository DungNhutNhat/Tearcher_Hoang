using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Tearcher_Hoang.Models;

namespace Tearcher_Hoang.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public SanPhamController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SanPham viewModel)
        {
            var product = new SanPham
            {
                ID = viewModel.ID,
                Ten = viewModel.Ten,
                GiaTien = viewModel.GiaTien,
                TrangThai = viewModel.TrangThai,
                IDNhaSanXuat = viewModel.IDNhaSanXuat
            };
            await dbContext.sanPhams.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List"); // Chuyển hướng đến trang danh sách sau khi thêm mới thành công
        }


        //lấy ra ds sản phẩm
        [HttpGet]
        public async Task<IActionResult> list()
        {
            var products = await dbContext.sanPhams.ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await dbContext.sanPhams.FindAsync(id);
            return View(product);
        }
        //[HttpPost]
        //public async Task<IActionResult> Edit(SanPham viewModel)
        //{
        //    var product = await dbContext.sanPhams.FindAsync(viewModel.ID);
        //    if (product is not null)
        //    {
        //        product.Ten = viewModel.Ten;
        //        product.GiaTien = viewModel.GiaTien;
        //        product.TrangThai = viewModel.TrangThai;
        //        product.IDNhaSanXuat = viewModel.IDNhaSanXuat;
        //        await dbContext.SaveChangesAsync();
        //    }
        //    return RedirectToAction("List");
        //}

        [HttpPost]
        public async Task<IActionResult> Edit(SanPham viewModel)
        {
            var product = await dbContext.sanPhams.FindAsync(viewModel.ID);
            if (product is not null)
            {
                // Lưu dữ liệu cũ vào session
                HttpContext.Session.SetString("OldProductData", JsonConvert.SerializeObject(product));

                // Cập nhật dữ liệu mới
                product.Ten = viewModel.Ten;
                product.GiaTien = viewModel.GiaTien;
                product.TrangThai = viewModel.TrangThai;
                product.IDNhaSanXuat = viewModel.IDNhaSanXuat;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }


        [HttpPost]
        public async Task<IActionResult> Rollback(string id)
        {
            var oldProductData = HttpContext.Session.GetString("OldProductData");
            if (!string.IsNullOrEmpty(oldProductData))
            {
                var oldProduct = JsonConvert.DeserializeObject<SanPham>(oldProductData);
                if (oldProduct.ID == id)
                {
                    var product = await dbContext.sanPhams.FindAsync(id);
                    if (product is not null)
                    {
                        product.Ten = oldProduct.Ten;
                        product.GiaTien = oldProduct.GiaTien;
                        product.TrangThai = oldProduct.TrangThai;
                        product.IDNhaSanXuat = oldProduct.IDNhaSanXuat;
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("List");
        }



        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await dbContext.sanPhams
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await dbContext.sanPhams
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await dbContext.sanPhams.FindAsync(id);
            if (product != null)
            {
                dbContext.sanPhams.Remove(product);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }

    }
}
