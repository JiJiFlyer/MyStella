<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="maptest.aspx.vb" Inherits="Jamada_4_0.maptest" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>maptest</title>
    <script src="echars/echarts.js" charset='utf-8'></script>
    <script src="http://api.map.baidu.com/api?v=2.0&ak=53oVIOgmSIejwV7EfphPgTynOZbIiVYu"></script>
    <script src="echars/bmap.min.js" charset='utf-8'></script>
    <!--加载鼠标绘制工具-->
    <script type="text/javascript" src="http://api.map.baidu.com/library/DrawingManager/1.4/src/DrawingManager_min.js"></script>
	<link rel="stylesheet" href="http://api.map.baidu.com/library/DrawingManager/1.4/src/DrawingManager_min.css" />
	<!--加载检索信息窗口-->
	<script type="text/javascript" src="http://api.map.baidu.com/library/SearchInfoWindow/1.4/src/SearchInfoWindow_min.js"></script>
	<link rel="stylesheet" href="http://api.map.baidu.com/library/SearchInfoWindow/1.4/src/SearchInfoWindow_min.css" />
    <style type="text/css">  
        html,body {  
            height: 100%;  
            width:100%  
        }  </style>
</head>
<body>
    <div id="map" style="width: 100%; height: 90%;"></div>
    <div id="result">
		<input type="button" value="获取绘制的标记个数" onclick="alert(overlays.length)"/>
		<input type="button" value="清除全部自定义标记" onclick="clearAll()"/>
        <input type="button" value="撤销" onclick="clearLast()"/>
	</div>
    <script type="text/javascript">
        // 百度地图API功能
        var map = new BMap.Map('map',{ minZoom: 4, maxZoom: 18 });
        map.addControl(new BMap.NavigationControl());
        map.enableScrollWheelZoom(true);
        var point = new BMap.Point(121.667261, 29.998284);
        map.centerAndZoom(point, 13);
        var tileLayer = new BMap.TileLayer({ isTransparentPng: true });
        tileLayer.getTilesUrl = function (tileCoord, zoom) {
            var x = tileCoord.x;
            var y = tileCoord.y;
            return 'tiles/' + zoom + '/tile' + x + '_' + y + '.png';  //根据当前坐标，选取合适的瓦片图
        }
        map.addTileLayer(tileLayer);
        var marker1 = new BMap.Marker(point);  // 创建标注
        var marker2 = new BMap.Marker(new BMap.Point(121.679261, 29.990284));  // 创建标注
        var marker3 = new BMap.Marker(new BMap.Point(121.651261, 29.999455));  // 创建标注        
        map.addOverlay(marker1);
        map.addOverlay(marker2);
        map.addOverlay(marker3);        // 将标注添加到地图中
        var markers = [];
        markers.push(marker1);
        markers.push(marker2);
        markers.push(marker3);
        /*markers.addEventListener("click", getAttr);
        function getAttr() {
            var p = marker.getPosition();       //获取marker的位置
            alert("marker的位置是" + p.lng + "," + p.lat);
        }
        
        var markerClusterer = new BMapLib.MarkerClusterer(map, { markers: markers });*/
        var overlays = [];
        var overlaycomplete = function (e) {
            overlays.push(e.overlay);
        };
        var styleOptions = {
            strokeColor: "red",    //边线颜色。
            fillColor: "red",      //填充颜色。当参数为空时，圆形将没有填充效果。
            strokeWeight: 3,       //边线的宽度，以像素为单位。
            strokeOpacity: 0.8,	   //边线透明度，取值范围0 - 1。
            fillOpacity: 0.3,      //填充的透明度，取值范围0 - 1。
            strokeStyle: 'solid' //边线的样式，solid或dashed。
        }
        //实例化鼠标绘制工具
        var drawingManager = new BMapLib.DrawingManager(map, {
            isOpen: false, //是否开启绘制模式
            enableDrawingTool: true, //是否显示工具栏
            drawingToolOptions: {
                anchor: BMAP_ANCHOR_TOP_RIGHT, //位置
                offset: new BMap.Size(5, 5), //偏离值
            },
            circleOptions: styleOptions, //圆的样式
            polylineOptions: styleOptions, //线的样式
            polygonOptions: styleOptions, //多边形的样式
            rectangleOptions: styleOptions //矩形的样式
        });
        //添加鼠标绘制工具监听事件，用于获取绘制结果
        drawingManager.addEventListener('overlaycomplete', overlaycomplete);
        function clearAll() {
            for (var i = 0; i < overlays.length; i++) {
                map.removeOverlay(overlays[i]);
            }
            overlays.length = 0
        }
        function clearLast() {
                map.removeOverlay(overlays[overlays.length-1]);
                overlays.length = overlays.length - 1;
        }
    </script>
</body>
</html>
