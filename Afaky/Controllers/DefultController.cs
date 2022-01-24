using Entities.DataAccess;
using Entities.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Afaky.Models;
using Infrastructure;

namespace Afaky.Controllers
{
    public class DefultController : Controller
    {
        private readonly AfakyContext afakyContext;
        private readonly IUnitOfWork<vInvoiceMaster> unitOfWorkMaster;
        private readonly IUnitOfWork<InvoiceMaster> unitOfWorkMasterd;
        private readonly IUnitOfWork<InvoiceDetails> unitOfWorkDetails;
        private readonly IUnitOfWork<Store> unitOfWorkStore;
        private readonly IUnitOfWork<Customers> unitOfWorkCustomer;
        private readonly IUnitOfWork<Product> unitOfWorkProduct;
        public DefultController(IUnitOfWork<vInvoiceMaster> _unitOfWorkMaster, IUnitOfWork<Store> _unitOfWorkStore, IUnitOfWork<Customers> _unitOfWorkCustomer, IUnitOfWork<Product> _unitOfWorkProduct, IUnitOfWork<InvoiceDetails> _unitOfWorkDetails, AfakyContext _afakyContext, IUnitOfWork<InvoiceMaster> _unitOfWorkMasterd)
        {
            unitOfWorkMaster = _unitOfWorkMaster;
            unitOfWorkStore = _unitOfWorkStore;
            unitOfWorkCustomer = _unitOfWorkCustomer;
            unitOfWorkProduct = _unitOfWorkProduct;
            unitOfWorkDetails = _unitOfWorkDetails;
            afakyContext = _afakyContext;
            unitOfWorkMasterd = _unitOfWorkMasterd;
        }
        public IActionResult Index()
        {

            var Invoice = unitOfWorkMaster.genricRepostitory.GetAll();
            List<InvoiceMasterViewModel> list = new List<InvoiceMasterViewModel>();
            foreach (var item in Invoice)
            {
                list.Add(new InvoiceMasterViewModel
                {
                    Id = item.Id,
                    CustomerId = item.CustomerId,
                    DateInvoice = item.DateInvoice
                   ,
                    DiscountPrecent = item.DiscountPrecent,
                    DiscountVal = item.DiscountVal,
                    Net = item.Net,
                    Notes = item.Notes,
                    StoreId = item.StoreId,
                    SumPrice = item.SumPrice,
                    CustomerName = item.CustomersName,
                    StoreName = item.StoresName
                    

                });
            }

            return View(list);
        }


        public IActionResult Create()
        {
            ViewBag.Store = unitOfWorkStore.genricRepostitory.GetAll();
            ViewBag.Customer = unitOfWorkCustomer.genricRepostitory.GetAll();
            ViewBag.Product = unitOfWorkProduct.genricRepostitory.GetAll();
            ViewBag.Invoice = unitOfWorkMaster.genricRepostitory.GetAll();
            InvoiceMasterViewModel viewdata = new InvoiceMasterViewModel
            {
                Id = 0,
                CustomerId = 1,
                DateInvoice =DateTime.UtcNow,
                DiscountPrecent = 0,
                DiscountVal = 0,
                Net = 0,
                Notes = "",
                StoreId = 1,
                SumPrice = 0,

            };
            return View(viewdata);
        }

        public JsonResult GetProductPrice(int id)
        {
            try
            {
                var product = unitOfWorkProduct.genricRepostitory.GetById(id);
               
                return Json(product);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult SaveInvoice(InvoiceMasterViewModel invoiceMasterViewModel)
        {


                if (invoiceMasterViewModel.Id != 0)
                {
                    unitOfWorkDetails.genricRepostitory.DeletByMasterId(invoiceMasterViewModel.Id);
                    unitOfWorkDetails.Save();
                    var datasend = new InvoiceMaster
                    {
                        Id = invoiceMasterViewModel.Id,
                        CustomerId = invoiceMasterViewModel.CustomerId,
                        DateInvoice = invoiceMasterViewModel.DateInvoice,
                        DiscountPrecent = invoiceMasterViewModel.DiscountPrecent,
                        DiscountVal = invoiceMasterViewModel.DiscountVal,
                        Net = invoiceMasterViewModel.Net,
                        Notes = invoiceMasterViewModel.Notes ==null?"": invoiceMasterViewModel.Notes,
                        StoreId = invoiceMasterViewModel.StoreId,
                        SumPrice = invoiceMasterViewModel.Details.Sum(q => q.Qty)
                    };

                    var res = afakyContext.InvoiceMasters.FirstOrDefault(q => q.Id == invoiceMasterViewModel.Id);
                    res.CustomerId = invoiceMasterViewModel.CustomerId;
                    res.DateInvoice = invoiceMasterViewModel.DateInvoice;
                    res.DiscountPrecent = invoiceMasterViewModel.DiscountPrecent;
                    res.DiscountVal = invoiceMasterViewModel.DiscountVal;
                    res.Net = invoiceMasterViewModel.Net;
                    res.Notes = invoiceMasterViewModel.Notes;
                    res.StoreId = invoiceMasterViewModel.StoreId;
                    res.SumPrice = invoiceMasterViewModel.SumPrice;
                    afakyContext.SaveChanges();
                    foreach (var item in invoiceMasterViewModel.Details)
                    {
                        InvoiceDetails invoiceDetails = new InvoiceDetails
                        {
                            Id = item.Id,
                            InvoiceMasterId = datasend.Id,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Qty = item.Qty,
                            Total = item.Total

                        };

                        unitOfWorkDetails.genricRepostitory.Add(invoiceDetails);
                        unitOfWorkDetails.Save();


                    }
                    return Json("تم التعديل");

                }
                else
                {

                    var datasend = new InvoiceMaster
                    {
                        Id = invoiceMasterViewModel.Id,
                        CustomerId = invoiceMasterViewModel.CustomerId,
                        DateInvoice = invoiceMasterViewModel.DateInvoice,
                        DiscountPrecent = invoiceMasterViewModel.DiscountPrecent,
                        DiscountVal = invoiceMasterViewModel.DiscountVal,
                        Net = invoiceMasterViewModel.Net,
                        Notes = invoiceMasterViewModel.Notes == null ? "" : invoiceMasterViewModel.Notes,
                        StoreId = invoiceMasterViewModel.StoreId,
                        SumPrice = invoiceMasterViewModel.SumPrice
                    };

                    unitOfWorkMasterd.genricRepostitory.Add(datasend);
                    unitOfWorkMasterd.Save();
                    foreach (var item in invoiceMasterViewModel.Details)
                    {
                        InvoiceDetails invoiceDetails = new InvoiceDetails
                        {
                            Id = item.Id,
                            InvoiceMasterId = datasend.Id,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Qty = item.Qty,
                            Total = item.Total

                        };
                        unitOfWorkDetails.genricRepostitory.Add(invoiceDetails);
                        unitOfWorkDetails.Save();


                    }

                }
            return Json("تم الحفظ");



        }


        public IActionResult Delete(int id)
        {
            try
            {
                var invoice = unitOfWorkMaster.genricRepostitory.GetById(id);
                var details = unitOfWorkDetails.genricRepostitory.GetByMasterId(id);
                InvoiceMasterViewModel invoiceMasterViewModel = new InvoiceMasterViewModel();
                List<InvoiceDetailsViewModel> invoiceDetailsViewModel = new List<InvoiceDetailsViewModel>();
                foreach (var item in details)
                {
                    InvoiceDetailsViewModel invoiceDetails = new InvoiceDetailsViewModel()
                    {
                        Id = item.Id,
                        InvoiceMasterId = item.InvoiceMasterId,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        ProductName = item.ProductName,
                        Qty = item.Qty,
                        Total = item.Total
                    };
                    invoiceDetailsViewModel.Add(invoiceDetails);
                }

                invoiceMasterViewModel.Id = invoice.Id;
                invoiceMasterViewModel.CustomerId = invoice.CustomerId;
                invoiceMasterViewModel.CustomerName = invoice.CustomersName;
                invoiceMasterViewModel.StoreName = invoice.StoresName;
                invoiceMasterViewModel.DateInvoice = invoice.DateInvoice;
                invoiceMasterViewModel.StoreId = invoice.StoreId;
                invoiceMasterViewModel.DiscountPrecent = invoice.DiscountPrecent;
                invoiceMasterViewModel.DiscountVal = invoice.DiscountVal;
                invoiceMasterViewModel.Net = invoice.Net;
                invoiceMasterViewModel.Notes = invoice.Notes;
                invoiceMasterViewModel.SumPrice = invoice.SumPrice;
                invoiceMasterViewModel.Details = invoiceDetailsViewModel;
                return View(invoiceMasterViewModel);

            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public IActionResult Deleted(int id)
        {
            unitOfWorkDetails.genricRepostitory.DeletByMasterId(id);
            unitOfWorkMasterd.genricRepostitory.Delet(id);
            unitOfWorkMasterd.Save();
            return RedirectToAction("Index");

        }
    }
}
