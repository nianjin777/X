﻿using CoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeakEvent;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using X.Browser;
using X.Services.Data;

namespace X.Extensions.UIComponentExtensions
{
    public sealed partial class InstalledExtensionList : UserControl, IExtensionContent
    {

        public ObservableCollection<ExtensionViewModel> Extensions { get; set; }

        public InstalledExtensionList()
        {
            this.InitializeComponent();

            tlMain.AddTab("Installed Extensions", true);
            tlMain.AddTab("Store");

            layoutRoot.DataContext = this;
        }

        private readonly WeakEventSource<EventArgs> _SendMessageSource = new WeakEventSource<EventArgs>();
        public event EventHandler<EventArgs> SendMessage
        {
            add { _SendMessageSource.Subscribe(value); }
            remove { _SendMessageSource.Unsubscribe(value); }
        }

        public void RecieveMessage(object message)
        {
            if (message is ResponseListOfInstalledExtensionsEventArgs ) {
                var ea = (ResponseListOfInstalledExtensionsEventArgs)message;
                tbExtensionCount.Text = ea.ExtensionsMetadata.Count() + " extensions";


                if (Extensions == null) Extensions = new ObservableCollection<ExtensionViewModel>();
                else Extensions.Clear();

                var extensionsInStorage = X.Services.Data.StorageService.Instance.Storage.RetrieveList<ExtensionManifestDataModel>();
                foreach (IExtensionManifest emd in ea.ExtensionsMetadata) {

                    var evm = new ExtensionViewModel();
                    evm.Load(emd);
                    
                    var uid = evm.TitleHashed;
                    var found = extensionsInStorage.Where(x => x.Uid == uid).ToList();
                    if (found != null && found.Count() > 0) {
                        evm.Load(found.First());
                    }

                    Extensions.Add(evm);
                }


                //icMain.ItemsSource = ea.ExtensionsMetadata;
                icMain.ItemsSource = Extensions;
            }
        }

        public void Unload()
        {
            
        }

        private void layoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            _SendMessageSource?.Raise(this, new RequestListOfInstalledExtensionsEventArgs() { ReceiverType = ExtensionType.UIComponent });


        }

        private void butEnable_Click(object sender, RoutedEventArgs e)
        {
            ExtensionViewModel item = ((Button)sender).DataContext as ExtensionViewModel;
            if (item.Id > 0)
                X.Services.Data.StorageService.Instance.Storage.UpdateFieldById<ExtensionManifestDataModel>(item.Id, "IsExtEnabled", 1);
            else
            {
                var newItem = new ExtensionManifestDataModel() { Uid = item.TitleHashed, IsExtEnabled = true , FoundInToolbarPositions = (int)item.FoundInToolbarPositions , LaunchInDockPositions = (int)item.LaunchInDockPositions };
                X.Services.Data.StorageService.Instance.Storage.Insert(newItem);
                item.Id = newItem.Id;
            }

            item.IsExtEnabled = true;
            item.ExternalRaisePropertyChanged("IsExtEnabled");

            X.Services.Extensions.ExtensionsService.Instance.UpdateExtension(item);

            _SendMessageSource?.Raise(this, new RequestRefreshToolbarExtensionsEventArgs() { ReceiverType = ExtensionType.UIComponent });
        }


        private void butDisable_Click(object sender, RoutedEventArgs e)
        {
            ExtensionViewModel item = ((Button)sender).DataContext as ExtensionViewModel;
            if (item.Id > 0)
                X.Services.Data.StorageService.Instance.Storage.UpdateFieldById<ExtensionManifestDataModel>(item.Id, "IsExtEnabled", 0);
            else
            {
                var newItem = new ExtensionManifestDataModel() { Uid = item.TitleHashed, IsExtEnabled = false , FoundInToolbarPositions = (int)item.FoundInToolbarPositions, LaunchInDockPositions = (int)item.LaunchInDockPositions };
                X.Services.Data.StorageService.Instance.Storage.Insert(newItem);
                item.Id = newItem.Id;
            }
                

            item.IsExtEnabled = false;
            item.ExternalRaisePropertyChanged("IsExtEnabled");

            X.Services.Extensions.ExtensionsService.Instance.UpdateExtension(item);

            _SendMessageSource?.Raise(this, new RequestRefreshToolbarExtensionsEventArgs() { ReceiverType = ExtensionType.UIComponent });
        }

        private void butToggleFoundIn_Click(object sender, RoutedEventArgs e)
        {
            ExtensionViewModel item = ((Button)sender).DataContext as ExtensionViewModel;

            if (item.FoundInToolbarPositions == ExtensionInToolbarPositions.Left) item.FoundInToolbarPositions = ExtensionInToolbarPositions.Top;
            else if (item.FoundInToolbarPositions == ExtensionInToolbarPositions.Top) item.FoundInToolbarPositions = ExtensionInToolbarPositions.Right;
            else if (item.FoundInToolbarPositions == ExtensionInToolbarPositions.Right) item.FoundInToolbarPositions = ExtensionInToolbarPositions.Bottom;
            else item.FoundInToolbarPositions = ExtensionInToolbarPositions.Left;

            item.ExternalRaisePropertyChanged("FoundInToolbarPositions");

            if (item.Id > 0)
                X.Services.Data.StorageService.Instance.Storage.UpdateFieldById<ExtensionManifestDataModel>(item.Id, "FoundInToolbarPositions", (int)item.FoundInToolbarPositions);
            else
            {
                var newItem = new ExtensionManifestDataModel() { Uid = item.TitleHashed, IsExtEnabled = item.IsExtEnabled, FoundInToolbarPositions = (int)item.FoundInToolbarPositions, LaunchInDockPositions = (int)item.LaunchInDockPositions };
                X.Services.Data.StorageService.Instance.Storage.Insert(newItem);
                item.Id = newItem.Id;
            }

            X.Services.Extensions.ExtensionsService.Instance.UpdateExtension(item);

            _SendMessageSource?.Raise(this, new RequestRefreshToolbarExtensionsEventArgs() { ReceiverType = ExtensionType.UIComponent });
        }

        private void butToggleLaunchIn_Click(object sender, RoutedEventArgs e)
        {
            ExtensionViewModel item = ((Button)sender).DataContext as ExtensionViewModel;

            if (item.LaunchInDockPositions == ExtensionInToolbarPositions.Left) item.LaunchInDockPositions = ExtensionInToolbarPositions.Top;
            else if (item.LaunchInDockPositions == ExtensionInToolbarPositions.Top) item.LaunchInDockPositions = ExtensionInToolbarPositions.Right;
            else if (item.LaunchInDockPositions == ExtensionInToolbarPositions.Right) item.LaunchInDockPositions = ExtensionInToolbarPositions.Bottom;
            else if (item.LaunchInDockPositions == ExtensionInToolbarPositions.Bottom) item.LaunchInDockPositions = ExtensionInToolbarPositions.BottomFull;
            else item.LaunchInDockPositions = ExtensionInToolbarPositions.Left;

            item.ExternalRaisePropertyChanged("LaunchInDockPositions");

            if (item.Id > 0)
                X.Services.Data.StorageService.Instance.Storage.UpdateFieldById<ExtensionManifestDataModel>(item.Id, "LaunchInDockPositions", (int)item.LaunchInDockPositions);
            else
            {
                var newItem = new ExtensionManifestDataModel() { Uid = item.TitleHashed, IsExtEnabled = item.IsExtEnabled, FoundInToolbarPositions = (int)item.FoundInToolbarPositions, LaunchInDockPositions = (int)item.LaunchInDockPositions };
                X.Services.Data.StorageService.Instance.Storage.Insert(newItem);
                item.Id = newItem.Id;
            }

            X.Services.Extensions.ExtensionsService.Instance.UpdateExtension(item);

            _SendMessageSource?.Raise(this, new RequestRefreshToolbarExtensionsEventArgs() { ReceiverType = ExtensionType.UIComponent });
        }

        //private void butClose_PointerReleased(object sender, PointerRoutedEventArgs e)
        //{
        //    SendMessage?.Invoke(this, new CloseExtensionEventArgs() { ReceiverType = ExtensionType.UIComponent, ExtensionUniqueGuid = ExtensionManifest.UniqueID });
        //}

    }
}
