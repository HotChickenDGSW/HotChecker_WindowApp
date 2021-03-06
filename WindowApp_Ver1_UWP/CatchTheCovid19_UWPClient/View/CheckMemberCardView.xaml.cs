﻿using CatchTheCovid10.InitData;
using CatchTheCovid19.RestClient.Option;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace CatchTheCovid19_UWPClient.View
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class CheckMemberCardView : Page
    {
        public delegate void ChangeScreen();
        public event ChangeScreen ChangeScreenEvent;

        MediaPlayer mediaPlayerBarcode = new MediaPlayer();

        bool IsReadComplete = false;
        //TextBox tbxBarInput = new TextBox();
        public CheckMemberCardView()
        {
            this.InitializeComponent();
            Loaded += CheckMemberCard_Loaded;
        }

        private async void CheckMemberCard_Loaded(object sender, RoutedEventArgs e)
        {
            await FocusOn();
            DataContext = App.checkMemberCardViewModel;
            mediaPlayerBarcode.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Voice/인식되었습니다.mp3"));
            App.checkMemberCardViewModel.BarcodeReadCompleteEvent += CheckMemberCardViewModel_BarcodeReadCompleteEvent;
            //App.checkMemberCardViewModel.StartReadCard();
        }

        private async void CheckMemberCardViewModel_BarcodeReadCompleteEvent(Member member)
        {
            if (member != null)
            {
                await ShowData(member);
            }
            else
            {
                tbDesc.Text = "등록되지 않은 멤버입니다.";
                await Task.Delay(2000);
                await Init();
            }
        }

        private async Task ShowData(Member member)
        {

            tbDesc.Visibility = Visibility.Collapsed;
            tbName.Visibility = Visibility.Visible;
            tbClassRoom.Visibility = Visibility.Visible;
            tbIsStudent.Visibility = Visibility.Visible;
         
            
            await Task.Delay(2000);
            ChangeScreenEvent?.Invoke();
            App.checkTemperatureViewModel.SetMemberData(member);
            //App.checkTemperatureViewModel.GetTemperatureData();
            
            
        }

        public async Task Init()
        {
            if (NetworkOptions.mode == 0)
            {
                App.checkTemperatureViewModel.PendingBarcode();
            }
            else if (NetworkOptions.mode == 1)
            {
                //App.checkTemperatureViewModel.
            }
             
           
             await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                IsReadComplete = false;
                App.checkMemberCardViewModel.CheckMemberCard = null;
                tbDesc.Visibility = Visibility.Visible;
                tbDesc.Text = "바코드를 인식시켜 주세요";
                tbName.Visibility = Visibility.Collapsed;
                tbClassRoom.Visibility = Visibility.Collapsed;
                tbIsStudent.Visibility = Visibility.Collapsed;
                //BarCodeReadOn();
               
                //MakeInputTbx();
                //tbxBarInput.IsFocusEngaged = true;
            });

            await FocusOn();
            //await TabInput();
        }
        //private async Task TabInput()
        //{
        //    await Task.Run(() =>
        //    {
        //        //InputInjector inputInjector = InputInjector.TryCreate();
        //        //var tab = new InjectedInputKeyboardInfo();
        //        //tab.VirtualKey = (ushort)(VirtualKey.Tab);
        //        //tab.KeyOptions = InjectedInputKeyOptions.None;
        //        //inputInjector.InjectKeyboardInput(new[] { tab });
        //        FocusOn();
        //    });
           
        //}
        private async Task FocusOn()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                tbxBarInput.Focus(FocusState.Programmatic);
            });

        }
        //public async Task BarCodeReadOn()
        //{
        //    GpioController gpio = GpioController.GetDefault();
        //    if (gpio == null) return;
        //    using (GpioPin pin = gpio.OpenPin(4))
        //    {
        //        while (true)
        //        {
        //            await Task.Run(() =>
        //            {
        //                pin.Write(GpioPinValue.High);
        //                pin.SetDriveMode(GpioPinDriveMode.Output);
        //            });
        //            await Task.Run(() =>
        //            {
        //                pin.Write(GpioPinValue.Low);
        //                pin.SetDriveMode(GpioPinDriveMode.Output);
        //                Task.Delay(2000);
        //            });

        //            if (IsReadComplete)
        //            {
        //                break;
        //            }
        //        }
       
        //    }
        //    BarCodeReadOff();

        //}
        //public void BarCodeReadOff()
        //{
        //    GpioController gpio = GpioController.GetDefault();
        //    if (gpio == null) return;
        //    using (GpioPin pin = gpio.OpenPin(4))
        //    {
        //        pin.Write(GpioPinValue.High);
        //        pin.SetDriveMode(GpioPinDriveMode.Output);
        //    }
        //}
        private async void tbxBarInput_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                IsReadComplete = true;
                mediaPlayerBarcode.Play();
                await App.checkMemberCardViewModel.SearchMember(tbxBarInput.Text);
                tbxBarInput.Text = "";
                if (NetworkOptions.mode == 0)
                {
                    App.checkTemperatureViewModel.StopBarcode();
                }
                else if (NetworkOptions.mode == 1)
                {
                    //App.checkTemperatureViewModel.
                }
                
                //tbxBarInput.Focus(FocusState.Programmatic);
                //await ShowData(App.checkMemberCardViewModel.CheckMemberCard);
            }
        }
    }
}
