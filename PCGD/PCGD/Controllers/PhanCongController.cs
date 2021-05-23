﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCGD.Models;
using PCGD.Libs;

namespace PCGD.Controllers
{
    [Authentication]
    [Role("Admin", "User")]
    public class PhanCongController : Controller
    {
        private PCGDEntities db = new PCGDEntities();

        // GET: PhanCong
        public ActionResult Index()
        {
            var phanCong = db.PhanCong.Include(p => p.TongHop).OrderByDescending(x => x.TongHop.NamHoc).ThenByDescending(x => x.HocKi);
            return View(phanCong.ToList());
        }

        // GET: PhanCong/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanCong phanCong = db.PhanCong.Find(id);
            if (phanCong == null)
            {
                return HttpNotFound();
            }
            ViewNhiemVuModel viewNhiemVuModel = new ViewNhiemVuModel();
            viewNhiemVuModel.PhanCong = phanCong;
            viewNhiemVuModel.Lop = PhanCongLib.GetNhiemVuLopModel(Convert.ToInt64(id));
            viewNhiemVuModel.NhomHocPhan = PhanCongLib.GetNhiemVuNhomHocPhanModel(Convert.ToInt64(id));
            viewNhiemVuModel.NhiemVu = PhanCongLib.GetNhiemVuModel(Convert.ToInt64(id));
            return View(viewNhiemVuModel);
        }

        // GET: PhanCong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhanCong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NamHoc,HocKi")] PhanCongModel phanCongModel)
        {
            if (ModelState.IsValid)
            {
                TongHop tongHop = db.TongHop.Where(x => x.NamHoc == phanCongModel.NamHoc).SingleOrDefault();
                if (tongHop == null)
                {
                    tongHop = new TongHop();
                    tongHop.NamHoc = phanCongModel.NamHoc;
                    db.TongHop.Add(tongHop);
                    db.SaveChanges();
                }
                if (PhanCongLib.ExistsPhanCong(tongHop.ID, phanCongModel.HocKi))
                {
                    ModelState.AddModelError("ThongBaoLoi", "Năm học " + phanCongModel.NamHoc + " - " + (phanCongModel.NamHoc + 1) + " đã có phân công học kì " + HocPhanLib.ConvertHocKi(phanCongModel.HocKi) + " này rồi!");
                    return View(phanCongModel);
                }
                PhanCong phanCong = new PhanCong();
                phanCong.TongHop_ID = tongHop.ID;
                phanCong.HocKi = phanCongModel.HocKi;
                db.PhanCong.Add(phanCong);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = phanCong.ID });
            }

            return View(phanCongModel);
        }

        // GET: PhanCong/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanCong phanCong = db.PhanCong.Find(id);
            if (phanCong == null)
            {
                return HttpNotFound();
            }
            PhanCongModel phanCongModel = new PhanCongModel();
            phanCongModel.ID = phanCong.ID;
            phanCongModel.NamHoc = db.TongHop.Find(phanCong.TongHop_ID).NamHoc;
            phanCongModel.HocKi = phanCong.HocKi;
            return View(phanCongModel);
        }

        // POST: PhanCong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NamHoc,HocKi")] PhanCongModel phanCongModel)
        {
            if (ModelState.IsValid)
            {
                PhanCong phanCong = db.PhanCong.Find(phanCongModel.ID);
                if (phanCong == null)
                {
                    return HttpNotFound();
                }
                TongHop tongHop = db.TongHop.Where(x => x.NamHoc == phanCongModel.NamHoc).SingleOrDefault();
                if (tongHop == null)
                {
                    tongHop = new TongHop();
                    tongHop.NamHoc = phanCongModel.NamHoc;
                    db.TongHop.Add(tongHop);
                    db.SaveChanges();
                }
                if (PhanCongLib.ExistsPhanCong(tongHop.ID, phanCongModel.HocKi, phanCong.ID))
                {
                    ModelState.AddModelError("ThongBaoLoi", "Năm học " + phanCongModel.NamHoc + " - " + (phanCongModel.NamHoc + 1) + " đã có phân công học kì " + HocPhanLib.ConvertHocKi(phanCongModel.HocKi) + " này rồi!");
                    return View(phanCongModel);
                }
                long tempID = phanCong.TongHop_ID;
                phanCong.HocKi = phanCongModel.HocKi;
                phanCong.TongHop_ID = tongHop.ID;
                db.Entry(phanCong).State = EntityState.Modified;
                db.SaveChanges();
                if (db.PhanCong.Where(x => x.TongHop_ID == tempID).Count() <= 0)
                {
                    TongHop th = db.TongHop.Find(tempID);
                    db.TongHop.Remove(th);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(phanCongModel);
        }

        // GET: PhanCong/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanCong phanCong = db.PhanCong.Find(id);
            if (phanCong == null)
            {
                return HttpNotFound();
            }
            return View(phanCong);
        }

        // POST: PhanCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PhanCong phanCong = db.PhanCong.Find(id);
            db.PhanCong.Remove(phanCong);
            db.SaveChanges();
            if (db.PhanCong.Where(x => x.TongHop_ID == phanCong.TongHop_ID).Count() <= 0)
            {
                TongHop th = db.TongHop.Find(phanCong.TongHop_ID);
                db.TongHop.Remove(th);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: PhanCong/Create
        public ActionResult ThemNhiemVu(long? phancong_id)
        {
            if (phancong_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanCong phanCong = db.PhanCong.Find(phancong_id);
            if (phanCong == null)
            {
                return HttpNotFound();
            }
            ThemNhiemVuModel themNhiemVuModel = new ThemNhiemVuModel();
            themNhiemVuModel.PhanCong_ID = phanCong.ID;
            return View(themNhiemVuModel);
        }

        // POST: PhanCong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhiemVu([Bind(Include = "PhanCong_ID,TenGV,MaHP,TenLop,LoaiPhong,NhomLT,NhomTH,GhiChu")] ThemNhiemVuModel themNhiemVuModel)
        {
            if (ModelState.IsValid)
            {
                NhiemVu nhiemVu = new NhiemVu();
                nhiemVu.PhanCong_ID = themNhiemVuModel.PhanCong_ID;
                GiangVien giangVien = db.GiangVien.Where(x => x.TenGV == themNhiemVuModel.TenGV).SingleOrDefault();
                if (giangVien == null)
                {
                    ModelState.AddModelError("TenGV", "Tên giảng viên không tồn tại trên hệ thống!");
                    return View(themNhiemVuModel);
                }
                nhiemVu.GiangVien_ID = giangVien.ID;
                HocPhan hocPhan = db.HocPhan.Where(x => x.MaHP == themNhiemVuModel.MaHP).SingleOrDefault();
                if (hocPhan == null)
                {
                    ModelState.AddModelError("MaHP", "Mã học phần không tồn tại trên hệ thống!");
                    return View(themNhiemVuModel);
                }
                if (!PhanCongLib.IsHocPhanOfGiangVien(giangVien.TenGV, hocPhan.MaHP))
                {
                    ModelState.AddModelError("MaHP", "Mã học phần này giảng viên \"" + giangVien.TenGV + "\" không có dạy");
                    return View(themNhiemVuModel);
                }
                nhiemVu.HocPhan_ID = hocPhan.ID;
                Lop lop = db.Lop.Where(x => x.TenLop == themNhiemVuModel.TenLop).SingleOrDefault();
                if (lop == null)
                {
                    ModelState.AddModelError("TenLop", "Tên lớp không tồn tại trên hệ thống!");
                    return View(themNhiemVuModel);
                }
                if (!PhanCongLib.IsLopOfHocPhan(hocPhan.MaHP, lop.TenLop))
                {
                    ModelState.AddModelError("TenLop", "Chương trình của tên lớp này không có học phần \"" + hocPhan.MaHP + "\"");
                    return View(themNhiemVuModel);
                }
                nhiemVu.Lop_ID = lop.ID;
                if (PhanCongLib.ExistsNhiemVu(nhiemVu.PhanCong_ID, nhiemVu.Lop_ID, nhiemVu.HocPhan_ID, nhiemVu.GiangVien_ID))
                {
                    ModelState.AddModelError("ThongBaoLoi", "Nhiệm vụ đã tồn tại trên trên hệ thống!");
                    return View(themNhiemVuModel);
                }
                nhiemVu.LoaiPhong = themNhiemVuModel.LoaiPhong;
                nhiemVu.NhomLT = themNhiemVuModel.NhomLT;
                nhiemVu.NhomTH = themNhiemVuModel.NhomTH;
                nhiemVu.GhiChu = themNhiemVuModel.GhiChu;
                db.NhiemVu.Add(nhiemVu);
                db.SaveChanges();
                if (db.ChiTietTongHop.Where(x => x.GiangVien_ID == nhiemVu.GiangVien_ID).Count() <= 0)
                {
                    ChiTietTongHop chiTietTongHop = new ChiTietTongHop();
                    chiTietTongHop.GiangVien_ID = nhiemVu.GiangVien_ID;
                    chiTietTongHop.TongHop_ID = db.PhanCong.Find(nhiemVu.PhanCong_ID).TongHop_ID;
                    db.ChiTietTongHop.Add(chiTietTongHop);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", new { id = themNhiemVuModel.PhanCong_ID });
            }

            return View(themNhiemVuModel);
        }

        // GET: PhanCong/Edit/5
        public ActionResult SuaNhiemVu(long? nhiemvu_id)
        {
            if (nhiemvu_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhiemVu nhiemVu = db.NhiemVu.Find(nhiemvu_id);
            if (nhiemVu == null)
            {
                return HttpNotFound();
            }
            SuaNhiemVuModel suaNhiemVuModel = new SuaNhiemVuModel();
            suaNhiemVuModel.ID = nhiemVu.ID;
            suaNhiemVuModel.PhanCong_ID = nhiemVu.PhanCong_ID;
            GiangVien giangVien = db.GiangVien.Find(nhiemVu.GiangVien_ID);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            suaNhiemVuModel.TenGV = giangVien.TenGV;
            HocPhan hocPhan = db.HocPhan.Find(nhiemVu.HocPhan_ID);
            if (hocPhan == null)
            {
                return HttpNotFound();
            }
            suaNhiemVuModel.MaHP = hocPhan.MaHP;
            Lop lop = db.Lop.Find(nhiemVu.Lop_ID);
            if (lop == null)
            {
                return HttpNotFound();
            }
            suaNhiemVuModel.TenLop = lop.TenLop;
            suaNhiemVuModel.LoaiPhong = nhiemVu.LoaiPhong;
            suaNhiemVuModel.NhomLT = nhiemVu.NhomLT;
            suaNhiemVuModel.NhomTH = nhiemVu.NhomTH;
            suaNhiemVuModel.GhiChu = nhiemVu.GhiChu;
            return View(suaNhiemVuModel);
        }

        // POST: PhanCong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaNhiemVu([Bind(Include = "ID,PhanCong_ID,TenGV,MaHP,TenLop,LoaiPhong,NhomLT,NhomTH,GhiChu")] SuaNhiemVuModel suaNhiemVuModel)
        {
            if (ModelState.IsValid)
            {
                NhiemVu nhiemVu = new NhiemVu();
                nhiemVu.ID = suaNhiemVuModel.ID;
                nhiemVu.PhanCong_ID = suaNhiemVuModel.PhanCong_ID;
                GiangVien giangVien = db.GiangVien.Where(x => x.TenGV == suaNhiemVuModel.TenGV).SingleOrDefault();
                if (giangVien == null)
                {
                    ModelState.AddModelError("TenGV", "Tên giảng viên không tồn tại trên hệ thống!");
                    return View(suaNhiemVuModel);
                }
                nhiemVu.GiangVien_ID = giangVien.ID;
                HocPhan hocPhan = db.HocPhan.Where(x => x.MaHP == suaNhiemVuModel.MaHP).SingleOrDefault();
                if (hocPhan == null)
                {
                    ModelState.AddModelError("MaHP", "Mã học phần không tồn tại trên hệ thống!");
                    return View(suaNhiemVuModel);
                }
                if (!PhanCongLib.IsHocPhanOfGiangVien(giangVien.TenGV, hocPhan.MaHP))
                {
                    ModelState.AddModelError("MaHP", "Mã học phần này giảng viên \"" + giangVien.TenGV + "\" không có dạy");
                    return View(suaNhiemVuModel);
                }
                nhiemVu.HocPhan_ID = hocPhan.ID;
                Lop lop = db.Lop.Where(x => x.TenLop == suaNhiemVuModel.TenLop).SingleOrDefault();
                if (lop == null)
                {
                    ModelState.AddModelError("TenLop", "Tên lớp không tồn tại trên hệ thống!");
                    return View(suaNhiemVuModel);
                }
                if (!PhanCongLib.IsLopOfHocPhan(hocPhan.MaHP, lop.TenLop))
                {
                    ModelState.AddModelError("TenLop", "Chương trình của tên lớp này không có học phần \"" + hocPhan.MaHP + "\"");
                    return View(suaNhiemVuModel);
                }
                nhiemVu.Lop_ID = lop.ID;
                nhiemVu.LoaiPhong = suaNhiemVuModel.LoaiPhong;
                nhiemVu.NhomLT = suaNhiemVuModel.NhomLT;
                nhiemVu.NhomTH = suaNhiemVuModel.NhomTH;
                nhiemVu.GhiChu = suaNhiemVuModel.GhiChu;
                db.Entry(nhiemVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = suaNhiemVuModel.PhanCong_ID });
            }

            return View(suaNhiemVuModel);
        }

        // GET: PhanCong/Delete/5
        public ActionResult XoaNhiemVu(long? nhiemvu_id)
        {
            if (nhiemvu_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhiemVu nhiemVu = db.NhiemVu.Find(nhiemvu_id);
            if (nhiemVu == null)
            {
                return HttpNotFound();
            }
            XoaNhiemVuModel xoaNhiemVuModel = new XoaNhiemVuModel();
            xoaNhiemVuModel.ID = nhiemVu.ID;
            xoaNhiemVuModel.PhanCong_ID = nhiemVu.PhanCong_ID;
            xoaNhiemVuModel.TenGV = nhiemVu.GiangVien.TenGV;
            return View(xoaNhiemVuModel);
        }

        // POST: PhanCong/Delete/5
        [HttpPost, ActionName("XoaNhiemVu")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNhiemVuConfirmed(long nhiemvu_id)
        {
            NhiemVu nhiemVu = db.NhiemVu.Find(nhiemvu_id);
            db.NhiemVu.Remove(nhiemVu);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = nhiemVu.PhanCong_ID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
