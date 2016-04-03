﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace X.UI.Path
{
    public sealed class Path : Control
    {
        Windows.UI.Xaml.Shapes.Path _path;




        public PathType PathType
        {
            get { return (PathType)GetValue(PathTypeProperty); }
            set { SetValue(PathTypeProperty, value); }
        }

        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }
        
        public double PathHeight
        {
            get { return (double)GetValue(PathHeightProperty); }
            set { SetValue(PathHeightProperty, value); }
        }
        
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }







        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(Path), new PropertyMetadata(0));

        public static readonly DependencyProperty PathHeightProperty =
            DependencyProperty.Register("PathHeight", typeof(double), typeof(Path), new PropertyMetadata(20));
        
        public static readonly DependencyProperty PathTypeProperty =
            DependencyProperty.Register("PathType", typeof(PathType), typeof(Path), new PropertyMetadata(0, OnPathTypePropertyChanged));
        
        public static readonly DependencyProperty PathWidthProperty =
            DependencyProperty.Register("PathWidth", typeof(double), typeof(Path), new PropertyMetadata(20));





        private static void OnPathTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instanceOfPath = d as Path;
            if(instanceOfPath._path!=null)
                instanceOfPath.FillPathData(instanceOfPath._path, (PathType)e.NewValue);
        }

 


        public Path()
        {
            this.DefaultStyleKey = typeof(Path);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_path == null)
            {
                _path = GetTemplateChild("pth") as Windows.UI.Xaml.Shapes.Path;
                FillPathData(_path, PathType);
            }

        }

        private void FillPathData(Windows.UI.Xaml.Shapes.Path pathInstance, PathType typeOfPath){
            
            var dataPath = string.Empty ;

            if (typeOfPath == PathType.Book)
            {
                dataPath = "M8.15192985534668,0L8.16493034362793,0 8.16493034362793,39.189998626709C8.16493034362793,39.6419982910156 8.31793022155762,40.0549983978271 8.55592918395996,40.2599983215332 8.79993057250977,40.4699993133545 9.08692932128906,40.4329986572266 9.30992889404297,40.173999786377L15.2389297485352,33.1699991226196 20.8559303283691,40.1579990386963C20.9839305877686,40.3139991760254,21.1359310150146,40.3959999084473,21.2879314422607,40.3959999084473L21.6139316558838,40.2689990997314C21.8609313964844,40.0629997253418,22.0179309844971,39.6469993591309,22.0179309844971,39.189998626709L22.0179309844971,0 52.1599340438843,0C53.090934753418,0,53.8439350128174,0.757999420166016,53.8439350128174,1.6879997253418L53.8439350128174,49.3569984436035C53.8439350128174,50.2879981994629,53.090934753418,51.0459976196289,52.1599340438843,51.0459976196289L52.1399345397949,51.0410003662109C52.0039348602295,51.0699996948242,51.8759346008301,51.0909996032715,51.7449340820313,51.0909996032715L8.14793014526367,51.0909996032715C5.61592864990234,51.0909996032715 3.5399284362793,53.0789985656738 3.39693069458008,55.5789985656738 3.39693069458008,55.7309989929199 3.40092849731445,55.8460006713867 3.40092849731445,55.9209976196289L3.39292907714844,56.0529975891113C3.49493026733398,58.5929985046387,5.58692932128906,60.6279983520508,8.14793014526367,60.6279983520508L50.4719343185425,60.6279983520508 50.4719343185425,55.9669990539551C50.4719343185425,55.0359992980957 51.2299346923828,54.2779998779297 52.1599340438843,54.2779998779297 53.090934753418,54.2779998779297 53.8439350128174,55.0359992980957 53.8439350128174,55.9669990539551L53.8439350128174,62.3120002746582C53.8439350128174,63.246997833252,53.090934753418,64,52.1599340438843,64L7.89292907714844,64 7.63792991638184,63.9749984741211C3.3879280090332,63.7070007324219,0.00792694091796875,60.1749992370605,0.00792694091796875,55.8589973449707L0.0229301452636719,55.5669975280762C-0.0290718078613281,50.5599994659424,0.0229301452636719,12.4609990119934,0.0279273986816406,8.3179988861084L0.0119285583496094,8.14099884033203C0.0119285583496094,3.65299892425537,3.6649284362793,0,8.15192985534668,0z";

            }
            else if (typeOfPath == PathType.Key)
            {
                dataPath = "M16.547848,26.872497C14.451092,26.916562 12.365034,27.710413 10.729302,29.266098 7.2240894,32.589393 7.0834706,38.118687 10.403706,41.615683 13.72137,45.118677 19.252512,45.263676 22.752474,41.941881 26.247238,38.621584 26.401036,33.097193 23.078072,29.594298 21.314234,27.73567 18.92417,26.822555 16.547848,26.872497z M47.555126,0.0002117157C47.726013,0.0044841766,47.895291,0.073574066,48.021641,0.20638657L52.778168,5.1985388C53.03077,5.4641666,53.020371,5.888484,52.754769,6.1409225L52.232945,6.6370945 58.379608,13.115402C58.632122,13.382402,58.621121,13.806803,58.354809,14.058203L56.356011,15.956708C56.089798,16.207609,55.665879,16.197409,55.413365,15.930308L49.269745,9.4546347 48.00716,10.655153 52.407509,15.294587C52.660187,15.560289,52.649086,15.984593,52.382813,16.237495L50.384396,18.133013C50.118122,18.386015,49.694359,18.375215,49.441685,18.109612L45.04353,13.473104 30.99349,26.832494 31.170538,27.124847C35.031944,33.685067 34.017586,42.279621 28.253817,47.748075 21.549486,54.107365 10.954499,53.828564 4.5965569,47.125475 -1.7705653,40.417484 -1.4855343,29.826198 5.21898,23.462906 10.672487,18.294763 18.680851,17.507213 24.908787,20.994482L25.088602,21.09812 47.078934,0.18294907C47.211735,0.056484222,47.384236,-0.0040607452,47.555126,0.0002117157z";
            }
            else if (typeOfPath == PathType.Pin)
            {
                dataPath = "M537.76,0L767,237.883 699.552,307.874 654.177,260.828 449.89,472.781 460.135,483.414 447.983,639.291 336.562,523.67 0,767 265.122,449.537 154.528,334.849 304.815,322.276 311.431,329.066 336,303.562 336,300 339.432,300 515.648,117.076 470.311,69.9921z";
            }
            else if (typeOfPath == PathType.Camera)
            {
                dataPath = "M32.433098,16.311C39.318604,16.311 44.900002,21.891891 44.900002,28.777302 44.900002,35.662912 39.318604,41.245001 32.433098,41.245001 25.5485,41.245001 19.966999,35.662912 19.966999,28.777302 19.966999,27.701456 20.103268,26.657459 20.35948,25.661623L20.420795,25.435036 20.493568,25.619604C21.510777,28.024502 23.892122,29.712002 26.667551,29.712002 30.368025,29.712002 33.368,26.712002 33.368,23.011502 33.368,20.236127 31.680515,17.854783 29.275633,16.837572L29.091026,16.764781 29.317705,16.703444C30.313477,16.447252,31.35738,16.311,32.433098,16.311z M32.433102,11.324C22.793745,11.324 14.98,19.137912 14.98,28.777349 14.98,38.416887 22.793745,46.232 32.433102,46.232 42.072556,46.232 49.887001,38.416887 49.887001,28.777349 49.887001,19.137912 42.072556,11.324 32.433102,11.324z M6.3339348,10.896001C5.0713553,10.896001 4.0480003,11.919366 4.0480003,13.181001 4.0480003,14.443735 5.0713553,15.467001 6.3339348,15.467001 7.5964546,15.467001 8.6199999,14.443735 8.6199999,13.181001 8.6199999,11.919366 7.5964546,10.896001 6.3339348,10.896001z M21.6329,0L42.929802,0C44.881001,0,46.462402,1.582015,46.462402,3.5326004L47.086002,7.0652599C47.086002,7.126215,47.083851,7.1868101,47.079617,7.2470083L47.078251,7.2600002 64,7.2600002 64,50.897001 0,50.897001 0,7.2600002 17.277473,7.2600002 17.275982,7.2470083C17.271357,7.1868101,17.269001,7.126215,17.269001,7.0652599L18.100401,3.5326004C18.100401,1.582015,19.6819,0,21.6329,0z";
            }
            else if (typeOfPath == PathType.BatteryLow)
            {
                dataPath = "M8,8.0000002L18.666872,8.0000002 26.667,16 8,16z M5.3334079,5.3333104L5.3334079,18.666701 34.666943,18.666701 34.666943,5.3333104z M0,0L40.000282,0 40.000282,8.0000002 42.667,8.0000002 42.667,18.666701 40.000282,18.666701 40.000282,24 0,24z";
            }
            else if (typeOfPath == PathType.BatteryCharging)
            {
                dataPath = "M37.144343,6.3500005L22.345,23.449005 32.70436,18.68071 32.70436,28.712 48.16,11.775815 37.472542,16.378611z M7.0289994,0L64,0 64,35.514 7.0289994,35.514 7.0289994,26.636999 0,26.636999 0,8.509 7.0289994,8.509z";
            }
            else if (typeOfPath == PathType.Wifi1)
            {
                dataPath = "M32.207049,31.555002C34.397277,31.555002 36.171999,33.330445 36.171999,35.5207 36.171999,37.711457 34.397277,39.487003 32.207049,39.487002 30.017022,39.487003 28.240999,37.711457 28.240998,35.5207 28.240999,33.330445 30.017022,31.555002 32.207049,31.555002z M32.207049,21.118999C38.654932,21.118999,44.113132,25.358,45.948999,31.20124L42.235567,33.513794C41.303384,28.826783 37.167959,25.293499 32.207049,25.293499 27.16034,25.293499 22.971616,28.949085 22.134431,33.756L18.386999,31.459046C20.140868,25.483003,25.662967,21.118999,32.207049,21.118999z M32.056239,10.577002C42.306176,10.577002,51.110823,16.757293,54.952,25.593639L51.541821,27.718102C48.441538,19.996137 40.886885,14.543232 32.056239,14.543232 23.172193,14.543232 15.57844,20.062535 12.514758,27.859999L9.0889988,25.760336C12.890956,16.834692,21.742502,10.577002,32.056239,10.577002z M32.056,0C46.0938,0,58.2279,8.1408958,63.999998,19.958156L60.256598,22.290197C55.2787,11.709309 44.524799,4.3836174 32.056,4.3836174 19.509199,4.3836174 8.6953087,11.804411 3.7617188,22.496L0,20.190559C5.7253399,8.2477074,17.927099,0,32.056,0z";
            }
            else if (typeOfPath == PathType.Wifi2)
            {
                dataPath = "M0,42.393001L10.830997,42.393001 10.830997,55.657001 0,55.657001z M13.374001,31.669L24.205999,31.669 24.205999,55.658001 13.374001,55.658001z M26.639,21.056999L37.471,21.056999 37.471,55.658001 26.639,55.658001z M39.903999,10.446999L50.734999,10.446999 50.734999,55.657001 39.903999,55.657001z M53.167999,0L63.999,0 63.999,55.658001 53.167999,55.658001z";
            }
            else if (typeOfPath == PathType.More)
            {
                dataPath = "M2.8150252,17.479C4.3696795,17.479 5.6300002,18.739332 5.6300002,20.29395 5.6300002,21.848667 4.3696795,23.109001 2.8150252,23.109001 1.2603809,23.109001 6.2182664E-08,21.848667 0,20.29395 6.2182664E-08,18.739332 1.2603809,17.479 2.8150252,17.479z M2.8150252,8.7399998C4.3696795,8.7399998 5.6300002,10.000317 5.6300002,11.55504 5.6300002,13.109663 4.3696795,14.37 2.8150252,14.37 1.260381,14.37 6.2182664E-08,13.109663 0,11.55504 6.2182664E-08,10.000317 1.260381,8.7399998 2.8150252,8.7399998z M2.8150252,0C4.3696795,0 5.6300002,1.2605648 5.6300002,2.8150654 5.6300002,4.3696756 4.3696795,5.6300001 2.8150252,5.6300001 1.260381,5.6300001 6.2182664E-08,4.3696756 0,2.8150654 6.2182664E-08,1.2605648 1.260381,0 2.8150252,0z";
            }
            else if (typeOfPath == PathType.Sound)
            {
                dataPath = "M16.171074,7.0700579C16.337786,7.0700579 16.498596,7.1286578 16.624804,7.2327771 17.920488,8.1064921 18.716841,9.6833239 18.708941,11.356524 18.69994,13.053015 17.873085,14.631206 16.551998,15.486601 16.425591,15.577801 16.281082,15.626 16.125471,15.626 15.717344,15.6234 15.389823,15.270502 15.393023,14.838204 15.415125,14.514007 15.559734,14.278308 15.777848,14.156009 16.679607,13.563512 17.229143,12.494517 17.234344,11.352524 17.241444,10.188531 16.690708,9.1012964 15.79795,8.52318 15.514131,8.273181 15.428226,8.0661526 15.431425,7.8408737 15.431425,7.4150863 15.764748,7.0648079 16.171074,7.0700579z M20.489689,4.4790378C20.742159,4.4829378 20.987633,4.5688763 21.18491,4.7342839 23.166485,6.0662928 24.385147,8.4855042 24.375948,11.050604 24.35965,13.644262 23.095493,16.060925 21.071623,17.366903 20.879644,17.510101 20.65637,17.583 20.420597,17.583 19.796467,17.581701 19.295124,17.037409 19.299023,16.376019 19.33152,15.883827 19.550995,15.520533 19.884256,15.333036 21.267601,14.42555 22.109205,12.788877 22.118404,11.037504 22.127502,9.2537422 21.285698,7.5922685 19.915453,6.7069025L19.854959,6.6625934C19.803467,6.6326838 19.757872,6.5961843 19.717676,6.549315 19.485302,6.3214689 19.351818,6.0037937 19.353718,5.6652293 19.353718,5.0063696 19.868059,4.4738479 20.489689,4.4790378z M23.470684,0.44702169C23.747292,0.44830384 23.999298,0.5459564 24.201204,0.70610657 27.681704,2.7750652 30.002169,6.5783401 29.981867,10.923255 29.959766,15.2954 27.567799,19.097466 24.0273,21.124693 23.834694,21.256187 23.604088,21.334286 23.35548,21.332985 22.707563,21.331686 22.184248,20.804403 22.190048,20.159927 22.229849,19.632545 22.499256,19.274458 22.872368,19.099964 25.738348,17.419125 27.632202,14.386632 27.650501,10.907556 27.666801,7.3894611 25.754549,4.2750116 22.905569,2.6201008 22.454956,2.2894223 22.285151,1.9599641 22.288351,1.598047 22.29035,0.96262857 22.818865,0.44311603 23.470684,0.44702169z M13.04,0L13.04,20.862999 5.5265007,14.56099 0,14.56099 0,6.3019691 5.5278106,6.3019691z";
            }
            else if (typeOfPath == PathType.Toolbar)
            {
                dataPath = "M24.615999,1.112625E-06L50.332001,1.112625E-06 50.332001,16.223 24.615999,16.223z M1.3333365,1.112625E-06L22.615999,1.112625E-06 22.615999,16.223 1.3333365,16.223C0.59627062,16.223,0,15.626589,0,14.889575L0,1.3333354C0,0.59638644,0.59627062,4.2921431E-07,1.3333365,1.112625E-06z M52.332001,0L73.33358,0C74.070587,4.2921431E-07,74.667,0.59638644,74.667,1.3333354L74.667,14.889575C74.667,15.626589,74.070587,16.223,73.33358,16.223L52.332001,16.223z";
            }
            else if (typeOfPath == PathType.PageAdd)
            {
                dataPath = "M6.5429993,18.753L17.058942,21.793293 13.857844,23.564029 18.320001,27.855577 16.19107,30.069 11.729013,25.776151 9.990099,29.143232z M16.824935,6.1792896C16.004628,6.1792896 15.340523,6.8433621 15.340523,7.6636875 15.340523,8.4840229 16.004628,9.1480949 16.824935,9.1480949 17.645241,9.1480949 18.309347,8.4840229 18.309347,7.6636875 18.309347,6.8433621 17.645241,6.1792896 16.824935,6.1792896z M11.26369,6.0841877C10.442783,6.0841877 9.7786283,6.7496102 9.7786283,7.5699356 9.7786283,8.3877008 10.442783,9.0543439 11.26369,9.0543439 12.083397,9.0543439 12.747402,8.3877008 12.747402,7.5699356 12.747402,6.7496102 12.083397,6.0841877 11.26369,6.0841877z M5.7011166,5.9904363C4.8808384,5.9904363 4.2161636,6.6558588 4.2161636,7.4748433 4.2161636,8.2951696 4.8808384,8.9592416 5.7011166,8.9592416 6.520853,8.9592416 7.1855278,8.2951696 7.1855278,7.4748433 7.1855278,6.6558588 6.520853,5.9904363 5.7011166,5.9904363z M32.297604,3.2525597L32.297604,6.9205296 28.630203,6.9205296 28.630203,8.7525594 32.297604,8.7525594 32.297604,12.4193 34.129604,12.4193 34.129604,8.7525594 37.796903,8.7525594 37.796903,6.9205296 34.129604,6.9205296 34.129604,3.2525597z M4.0143032,2.4539998L24.884899,2.4539998C23.956492,3.9344976 23.410987,5.6767104 23.410987,7.5490654 23.410987,9.3186281 23.899291,10.97369 24.735798,12.400717L4.2421646,12.400717 4.2421646,33.84782 33.155866,33.84782 33.155866,17.173006C34.681877,17.154705,36.119988,16.777097,37.397999,16.124786L37.397999,34.076923C37.397999,36.293163,35.600385,38.089996,33.384267,38.089996L4.0143032,38.089996C1.796814,38.089996,0,36.293163,0,34.076923L0,6.4696953C0,4.2521236,1.796814,2.4539998,4.0143032,2.4539998z M33.038502,0C37.209004,-2.390463E-07 40.588002,3.3814697 40.588002,7.5494401 40.588002,11.7201 37.209004,15.099 33.038502,15.099 28.869802,15.099 25.489001,11.7201 25.489001,7.5494401 25.489001,3.3814697 28.869802,-2.390463E-07 33.038502,0z";
            }
            else if (typeOfPath == PathType.ColorPicker)
            {
                dataPath = "M37.504044,17.088L46.396004,27.132273 13.302087,56.809681 3.8254647,57.497185 1.0833369,59.479 0,58.447693 2.2642548,55.853973 3.8554957,47.441015z M39.387676,7.0736895C40.196007,7.045701,41.009876,7.3660183,41.588524,8.0201292L56.086048,24.39546C57.113354,25.556983 57.005356,27.330419 55.843845,28.359039 54.682335,29.38636 52.90892,29.278358 51.880211,28.116835L37.383888,11.741604C36.35548,10.579981 36.463482,8.8065252 37.624893,7.7779341 38.133621,7.3279305 38.758976,7.095458 39.387676,7.0736895z M59.950253,0.00055503845C61.701824,0.02347374 63.436821,0.75650024 64.687431,2.1697607 67.046822,4.8353758 66.799522,8.9031525 64.133934,11.262627L55.510468,18.897001 46.973999,9.2535048 55.596367,1.6189604C56.846329,0.51296234,58.404743,-0.019666672,59.950253,0.00055503845z";
            }
            else if (typeOfPath == PathType.Shape)
            {
                dataPath = "M39.560302,40.410383L39.560302,56.691446 59.561691,56.691446 59.561691,40.410383z M14.781027,38.220106L11.460794,44.89031 4.0802822,45.940344 9.4006519,51.150503 8.1205635,58.490725 14.721022,55.050619 21.301478,58.530726 20.071393,51.190504 25.421764,46.010344 18.051252,44.910311z M35.129995,34.349989L64.001999,34.349989 64.001999,63.221869 35.129995,63.221869z M0,34.349989L28.872004,34.349989 28.872004,63.220869 0,63.220869z M14.435997,5.7786094L9.4373837,14.4364 4.4387684,23.094189 14.435997,23.094189 24.433226,23.094189 19.434611,14.4364z M49.570838,5.1090471C44.410567,5.1090476 40.230223,9.2779047 40.230223,14.436257 40.230223,19.585333 44.410567,23.763952 49.570838,23.763952 54.72122,23.763952 58.901564,19.585333 58.901564,14.436257 58.901564,9.2779047 54.72122,5.1090476 49.570838,5.1090471z M0,8.4879663E-05L28.871994,8.4879663E-05 28.871994,28.872715 0,28.872715z M35.129893,0L64.001894,0 64.001894,28.872999 35.129893,28.872999z";
            }
            else if (typeOfPath == PathType.Image)
            {
                dataPath = "M19.127866,25.310001L23.765831,30.044474 26.567812,28.49755 37.869933,39.801126 45.692677,35.840065 56.513001,41.153945 56.513001,51.201002 7.2449999,51.201002 7.2449999,36.998981z M41.072999,23.409999L42.377999,23.409999 41.992444,25.68 41.461236,25.68z M46.198105,21.301L47.529,23.179733 47.154015,23.555999 45.275998,22.224118z M37.4299,21.301L38.352,22.224118 36.473973,23.555999 36.099,23.179733z M47.239998,17.146999L49.510998,17.533812 49.510998,18.065188 47.239998,18.451999z M36.227,17.146999L36.227,18.451999 33.956,18.065188 33.956,17.533812z M41.635785,13.912C43.771012,13.912 45.5,15.640979 45.5,17.77481 45.5,19.90871 43.771012,21.639 41.635785,21.638999 39.503125,21.639 37.773999,19.90871 37.773999,17.77481 37.773999,15.640979 39.503125,13.912 41.635785,13.912z M47.008332,12.172L47.385998,12.547014 46.05272,14.426 45.131001,13.504083z M36.328124,12.172L38.209,13.504083 37.286812,14.426 35.953,12.547014z M41.492733,10.047L42.023705,10.047 42.409001,12.318 41.105,12.318z M4.445323,4.7018394L4.445323,53.5182 58.929883,53.5182 58.929883,4.7018394z M0,0L63.758,0 63.758,63.999998 0,63.999998z";
            }
            else if (typeOfPath == PathType.Text)
            {
                dataPath = "M32.0068359375,12.6235275268555L33.062744140625,12.6357355117798 33.49609375,13.0629806518555 33.990478515625,16.5663986206055 34.1064453125,20.5825119018555 33.99658203125,21.1806564331055 33.49609375,21.4125900268555 32.745361328125,21.3393478393555 32.2998046875,20.7289962768555 31.15234375,18.0251388549805 29.8095703125,16.4077072143555 28.6209106445313,15.7561569213867 27.056884765625,15.2907638549805 25.1174926757813,15.0115280151367 22.802734375,14.9184494018555 21.234130859375,15.0283126831055 20.5810546875,15.3579025268555 20.5078125,15.7729415893555 20.5078125,40.1137619018555 20.69091796875,41.441276550293 21.240234375,42.2988204956055 22.332763671875,42.893913269043 24.1455078125,43.4340744018555 24.871826171875,43.5988693237305 25.1953125,43.7026290893555 25.439453125,44.6303634643555 24.90234375,45.2407150268555 24.627685546875,45.2346115112305 23.9013671875,45.1674728393555 18.017578125,44.8745040893555 13.2568359375,45.1186447143555 11.962890625,45.1186447143555 11.6455078125,44.5327072143555 11.761474609375,43.8796310424805 12.5,43.4829025268555 14.2578125,42.8908615112305 15.33203125,42.1889572143555 15.95458984375,41.0720138549805 16.162109375,39.4301681518555 16.162109375,15.7729415893555 15.9423828125,15.2114191055298 15.283203125,14.9428634643555 14.874267578125,14.9245529174805 13.8427734375,14.9184494018555 9.814453125,15.3212814331055 7.0068359375,16.5297775268555 5.438232421875,18.2265548706055 4.052734375,20.9243087768555 3.558349609375,21.5895919799805 2.9052734375,21.5834884643555 2.337646484375,21.2600021362305 2.294921875,20.6801681518555 2.93579077720642,17.5002384185791 3.14941382408142,13.2338790893555 3.52172827720642,12.8371524810791 4.638671875,12.7700119018555 10.19287109375,13.0263595581055 16.30859375,13.1118087768555 21.8307476043701,13.0812911987305 26.2878398895264,12.9897384643555 29.6798686981201,12.8371505737305 32.0068359375,12.6235275268555z";
            }
            else if (typeOfPath == PathType.Path)
            {
                dataPath = "M350.072,236.717C345.399,245.759,335.922,253.422,321.877,258.039L246.137,283.026C252.423,291.266,255.087,302.488,252.447,315.986L238.273,388.234C249.175,387.432,261.175,391.098,272.591,399.926L333.913,447.293C338.598,438.251,348.086,430.567,362.143,425.95L437.848,400.952C431.573,392.712,428.897,381.501,431.55,368.024L445.724,295.745C434.81,296.589,422.833,292.88,411.417,284.085z M367.231,0.000186C377.367,-0.030819,388.295,3.80765,398.728,11.8634L480.158,74.7378C508.003,96.2198,522.177,139.889,511.707,171.791L499.333,209.475C508.003,204.431,517.187,200.562,526.5,198.563L630.202,176.184C665.641,168.531,689.385,189.094,682.946,221.883L664.134,317.825C657.707,350.603,623.448,383.67,588.008,391.323L546.142,400.375C555.256,404.682,563.482,410.058,570.049,416.428L643.102,487.371C668.06,511.61,660.453,540.702,626.217,551.998L525.974,585.087C491.715,596.362,443.281,585.782,418.323,561.542L388.83,532.878C389.286,542.284,388.317,551.475,385.57,559.875L354.91,653.198C344.429,685.122,313.09,693.608,285.269,672.147L203.838,609.251C176.017,587.791,161.832,544.09,172.301,512.198L184.687,474.514C176.017,479.58,166.821,483.417,157.508,485.426L53.7834,507.827C18.367,515.458,-5.37651,494.917,1.05029,462.127L19.8743,366.196C26.3011,333.407,60.5372,300.34,95.9885,292.688L137.843,283.646C128.741,279.297,120.503,273.953,113.948,267.583L40.8952,196.607C15.925,172.368,23.5314,143.287,57.7793,131.991L158.034,98.9236C192.282,87.6269,240.727,98.2289,265.686,122.468L295.178,151.111C294.722,141.727,295.68,132.514,298.438,124.136L329.098,30.7909C335.642,10.8521,350.337,0.051882,367.231,0.000186z";
            }


            if (!string.IsNullOrEmpty(dataPath))
            {
                var b = new Binding { Source = dataPath };

                BindingOperations.SetBinding(pathInstance, Windows.UI.Xaml.Shapes.Path.DataProperty, b);
            }
        


        }

    }

    public enum PathType {
        None,
        Book,
        Key,
        Pin,
        Camera,
        BatteryLow,
        BatteryCharging,
        Wifi1,
        Wifi2,
        More,
        Sound,
        Toolbar,
        PageAdd,
        ColorPicker,
        Shape,
        Image,
        Text,
        Path
    }
}
