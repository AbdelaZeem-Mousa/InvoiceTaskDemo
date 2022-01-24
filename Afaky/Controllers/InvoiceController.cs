using Afaky.Models;
using Entities.DataAccess;
using Entities.InterfaceRepository;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afaky.Controllers
{
    public class InvoiceController : Controller
    {

        private readonly IUnitOfWork<InvoiceMaster> unitOfWorkMaster;
        private readonly IUnitOfWork<InvoiceDetails> unitOfWorkDetails;
        private readonly IUnitOfWork<Store> unitOfWorkStore;
        private readonly IUnitOfWork<Customers> unitOfWorkCustomer;
        private readonly IUnitOfWork<Product> unitOfWorkProduct;
        public InvoiceController(IUnitOfWork<InvoiceMaster> _unitOfWorkMaster, IUnitOfWork<Store> _unitOfWorkStore, IUnitOfWork<Customers> _unitOfWorkCustomer, IUnitOfWork<Product> _unitOfWorkProduct, IUnitOfWork<InvoiceDetails> _unitOfWorkDetails /*AfakyContext _afakyContext*/)
        {
            unitOfWorkMaster = _unitOfWorkMaster;
            unitOfWorkStore = _unitOfWorkStore;
            unitOfWorkCustomer = _unitOfWorkCustomer;
            unitOfWorkProduct = _unitOfWorkProduct;
            unitOfWorkDetails = _unitOfWorkDetails;

        }
        public IActionResult Index( int?id)
        {
            ViewBag.Store = unitOfWorkStore.genricRepostitory.GetAll();
            ViewBag.Customer = unitOfWorkCustomer.genricRepostitory.GetAll();
            ViewBag.Product = unitOfWorkProduct.genricRepostitory.GetAll();
            ViewBag.Invoice = unitOfWorkMaster.genricRepostitory.GetAll();

            InvoiceMasterViewModel invoiceMasterViewModel = new InvoiceMasterViewModel();
            List<InvoiceDetailsViewModel> dd = new List<InvoiceDetailsViewModel>();
            dd.Add(new InvoiceDetailsViewModel
            {
                ProductId = 1,
                Price = 1,
                Qty = 1,
                Total = 1
            });
            invoiceMasterViewModel.Details = dd;
            if (id != null)
            {
                var invoice = unitOfWorkMaster.genricRepostitory.GetById(id);
                var details = unitOfWorkDetails.genricRepostitory.GetByMasterId(id);
                List<InvoiceDetailsViewModel> InvoiceDetailsViewModel = new List<InvoiceDetailsViewModel>();
                foreach (var item in details)
                {
                    InvoiceDetailsViewModel.Add(new Models.InvoiceDetailsViewModel
                    {
                        Id = item.Id,
                        InvoiceMasterId = item.InvoiceMasterId,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        ProductName = item.ProductName,
                        Qty = item.Qty,
                        Total = item.Total
                    });
                }

                invoiceMasterViewModel.Id = invoice.Id;
                invoiceMasterViewModel.CustomerId = invoice.CustomerId;
                invoiceMasterViewModel.CustomerName = invoice.Customers.Name;
                invoiceMasterViewModel.DateInvoice = invoice.DateInvoice;
                invoiceMasterViewModel.StoreId = invoice.StoreId;
                invoiceMasterViewModel.StoreName = invoice.Store.Name;
                invoiceMasterViewModel.DiscountPrecent = invoice.DiscountPrecent;
                invoiceMasterViewModel.DiscountVal = invoice.DiscountVal;
                invoiceMasterViewModel.Net = invoice.Net;
                invoiceMasterViewModel.Notes = invoice.Notes;
                invoiceMasterViewModel.SumPrice = invoice.SumPrice;
                invoiceMasterViewModel.Details = InvoiceDetailsViewModel;


            }

            return View(invoiceMasterViewModel);
        }
        [HttpPost]
        public IActionResult Index(InvoiceMasterViewModel invoiceMasterViewModel)
        {
            ViewBag.Invoice = unitOfWorkMaster.genricRepostitory.GetAll();
            if (ModelState.IsValid)
            {
                if (invoiceMasterViewModel.Id != 0)
                {
                    unitOfWorkDetails.genricRepostitory.DeletByMasterId(invoiceMasterViewModel.Id);
                    unitOfWorkDetails.Save();
                    var datasend = new InvoiceMaster
                    {
                        Id= invoiceMasterViewModel.Id,
                        CustomerId = invoiceMasterViewModel.CustomerId,
                        DateInvoice = invoiceMasterViewModel.DateInvoice,
                        DiscountPrecent = invoiceMasterViewModel.DiscountPrecent,
                        DiscountVal = invoiceMasterViewModel.DiscountVal,
                        Net= invoiceMasterViewModel.Net,
                        Notes= invoiceMasterViewModel.Notes,
                        StoreId= invoiceMasterViewModel.StoreId,
                        SumPrice = invoiceMasterViewModel.Details.Sum(q=>q.Qty)
                    };

                    unitOfWorkMaster.genricRepostitory.Edit(datasend);
                    unitOfWorkDetails.Save();
                    foreach (var item in invoiceMasterViewModel.Details)
                    {
                        InvoiceDetails invoiceDetails = new InvoiceDetails
                        {
                            Id = item.Id,
                            InvoiceMasterId = datasend.Id,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Qty = item.Qty,
                            Total = item.Price * item.Qty

                        };

                        unitOfWorkDetails.genricRepostitory.Add(invoiceDetails);
                        unitOfWorkDetails.Save();


                    }
                    InvoiceMasterViewModel nn = new InvoiceMasterViewModel();
                    List<InvoiceDetailsViewModel> dd = new List<InvoiceDetailsViewModel>();
                    dd.Add(new InvoiceDetailsViewModel
                    {
                        ProductId = 1,
                        Price = 1,
                        Qty = 1,
                        Total = 1
                    });
                    nn.Details = dd;
                    return RedirectToAction("Index", "Home");
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
                        Notes = invoiceMasterViewModel.Notes,
                        StoreId = invoiceMasterViewModel.StoreId,
                        SumPrice = invoiceMasterViewModel.SumPrice
                    };

                    unitOfWorkMaster.genricRepostitory.Add(datasend);
                    unitOfWorkMaster.Save();
                    foreach (var item in invoiceMasterViewModel.Details)
                    {
                        InvoiceDetails invoiceDetails = new InvoiceDetails
                        {
                            Id = item.Id,
                            InvoiceMasterId = datasend.Id,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Qty = item.Qty,
                            Total = item.Price * item.Qty

                        };
                        unitOfWorkDetails.genricRepostitory.Add(invoiceDetails);
                        unitOfWorkDetails.Save();


                    }
                    InvoiceMasterViewModel nn = new InvoiceMasterViewModel();
                    List<InvoiceDetailsViewModel> dd = new List<InvoiceDetailsViewModel>();
                    dd.Add(new InvoiceDetailsViewModel
                    {
                        ProductId = 1,
                        Price = 1,
                        Qty = 1,
                        Total = 1
                    });
                    nn.Details = dd;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index","Home");
            }


        }

        public JsonResult GetBuyId (int id)
        {
            try
            {
                var invoice = unitOfWorkMaster.genricRepostitory.GetById(id);
                var details = unitOfWorkDetails.genricRepostitory.GetByMasterId(id);
                InvoiceMasterViewModel invoiceMasterViewModel = new InvoiceMasterViewModel();
                //InvoiceDetailsViewModel invoiceDetails = new InvoiceDetailsViewModel();
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
                invoiceMasterViewModel.DateInvoice = invoice.DateInvoice;
                invoiceMasterViewModel.StoreId = invoice.StoreId;
                invoiceMasterViewModel.DiscountPrecent = invoice.DiscountPrecent;
                invoiceMasterViewModel.DiscountVal = invoice.DiscountVal;
                invoiceMasterViewModel.Net = invoice.Net;
                invoiceMasterViewModel.Notes = invoice.Notes;
                invoiceMasterViewModel.SumPrice = invoice.SumPrice;
                invoiceMasterViewModel.Details = invoiceDetailsViewModel;
                return Json(invoiceMasterViewModel);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
