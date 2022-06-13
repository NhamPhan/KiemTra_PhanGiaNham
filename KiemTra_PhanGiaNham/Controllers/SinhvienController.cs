using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_PhanGiaNham.Models;

namespace KiemTra_PhanGiaNham.Controllers
{
    public class SinhvienController : Controller
    {
        // GET: Sinhvien
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult ListSinhvien()
        {
            var all_sv = from ss in data.SinhViens select ss;

            return View(all_sv);
        }
        public ActionResult Detail(string id)
        {
            var D_sv = data.SinhViens.Where(m => m.MaSV == id).FirstOrDefault();
            return View(D_sv);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_masv = Convert.ToString(collection["masv"]);
            var E_hoten = collection["Hoten"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinh = (collection["hinh"]);
            var E_manganh = Convert.ToString(collection["manganh"]);
            if (string.IsNullOrEmpty(E_masv))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.HoTen = E_hoten.ToString();
                s.GioiTinh = E_gioitinh;
                s.NgaySinh = E_ngaysinh;
                s.Hinh = E_hinh;
                s.MaNganh = E_manganh.ToString();

                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListSinhvien");
            }
            return this.Create();
        }
        public ActionResult Edit(string id)
        {
            var E_sv = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sv);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            var E_hoten = collection["hoten"];
            var E_gioitinh = collection["gioitinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["ngaysinh"]);
            var E_hinh = collection["hinh"];
            var E_manganh = Convert.ToString(collection["manganh"]);
            E_sinhvien.MaSV = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sinhvien.HoTen = E_hoten;
                E_sinhvien.GioiTinh = E_gioitinh;
                E_sinhvien.NgaySinh = E_ngaysinh;
                E_sinhvien.Hinh = E_hinh;
                E_sinhvien.MaNganh = E_manganh;
                UpdateModel(E_sinhvien);
                data.SubmitChanges();
                return RedirectToAction("ListSinhvien");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(string id)
        {
            var D_sv = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sv);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_sv = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_sv);
            data.SubmitChanges();
            return RedirectToAction("ListSinhvien");
        }
    }
}



