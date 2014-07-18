require(["js/TacticalMap.js"], function (tacticalMap)
{
	$("#view").html("<canvas id='mapCanvas' width='0' height='0' style='display: none;'></canvas><div id='outputImage'></div>");

	var canvas = document.getElementById('mapCanvas');

	tacticalMap.DrawMap(canvas, data);

	var dataUrl = canvas.toDataURL("image/png");
	console.log("data url = " + dataUrl);
	$("#outputImage").html("<img src=\"" + dataUrl + "\" />");
});
