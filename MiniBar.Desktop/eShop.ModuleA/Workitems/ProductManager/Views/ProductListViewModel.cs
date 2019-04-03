using AutoMapper;
using Infrastructure.Helpers;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using Prism.Events;
using Security.Api;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    class ProductListViewModel : BaseViewModel, IObjectListManager
    {
        ProductService ProductService { get; set; }

        public ProductListViewModel(ProductService productService): base()
        {
            ProductService = productService;
        }


        private ObservableCollection<ProductViewModel> products;

        public ObservableCollection<ProductViewModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value, nameof(Products)); }
        }

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value, nameof(IsEnabled)); }
        }



        private bool isListLoading;

        public bool IsListLoading
        {
            get { return isListLoading; }
            set { SetProperty(ref isListLoading, value, nameof(IsListLoading)); }
        }


        private ProductViewModel currentItem;
        public ProductViewModel CurrentItem
        {
            get { return currentItem; }
            set {
                currentItem = value;
                SetProperty(ref currentItem, value, nameof(CurrentItem));
                whenCurrentItemChanges.OnNext(value);
                Debug.WriteLine(value == null? "null": ""+value.ID);
            }
        }

        #region IObjectListManager

        object IObjectListManager.CurrentItem
        {
            get
            {
                return CurrentItem;
            }
        }

        void IObjectListManager.RemoveByID(int id)
        {
            if (Products != null)
            {
                ProductViewModel prod = Products.Where(p => p.ID == id).FirstOrDefault();
                if (prod != null)
                {
                    int index = Products.IndexOf(prod);
                    UIHelper.ExecuteUIThread(() => {
                        Products.RemoveAt(index);
                    });

                    if (Products.Count > 0)
                    {
                        CurrentItem = Products[Math.Max(0, index - 1)];
                    }
                }
            }
        }

        private Subject<object> whenCurrentItemChanges = new Subject<object>();
        IObservable<object> IObjectListManager.WhenCurrentItemChanges
        {
            get
            {
                return whenCurrentItemChanges.Throttle(TimeSpan.FromSeconds(0.2)).AsObservable();
            }
        }

        void IObjectListManager.Disable()
        {
            IsEnabled = false;
        }

        void IObjectListManager.Enable()
        {
            IsEnabled = true;
        }


        void IObjectListManager.RefreshItems(int? selectID = null)
        {
            IsListLoading = true;
            IObservable<List<ProductDTO>> obs = Observable.FromAsync(() => ProductService.GetAll());
            obs.Subscribe(list => {
                Products = Mapper.Map<ObservableCollection<ProductViewModel>>(list);

                ProductViewModel selection = Products.FirstOrDefault(p => p.ID == selectID);
                if (selection != null)
                    CurrentItem = selection;
                else
                    CurrentItem = Products.FirstOrDefault();
                IsListLoading = false;
            }, ex =>
            {
                ApiHelper.HandleApiException(ex);
                IsListLoading = false;
            });
        }
        #endregion
    }
}
