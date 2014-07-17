$("#view").html("<canvas id='mapCanvas' width='0' height='0'></canvas>");

var colorSet = ["#e41a1c","#377eb8","#4daf4a","#984ea3","#ff7f00","#ffff33","#a65628","#f781bf","#999999"];
var symbolInfo = {offset:{x:0, y: -3}, font: "20px Calibri"};
var wallPositions = {
	"b":{x1: 0, y1: 1, x2: 1, y2: 1},
	"t":{x1: 0, y1: 0, x2: 1, y2: 0},
	"l":{x1: 0, y1: 0, x2: 0, y2: 1},
	"r":{x1: 1, y1: 0, x2: 1, y2: 1}
}

function Symbol(map, symbol, position)
{
	map.context.font = symbolInfo.font;
	map.context.textAlign = "center";
	map.context.fillStyle = colorSet[1];

	map.context.fillText(symbol,
			map.cellSize.x*(position.x+0.5)+symbolInfo.offset.x,
			map.cellSize.y*(position.y+1.0)+symbolInfo.offset.y);
}

function Wall(map, wall)
{
	var direction = wallPositions[wall.direction];
	map.context.moveTo((wall.x+direction.x1)*map.cellSize.x, (wall.y+direction.y1)*map.cellSize.y);
	map.context.lineTo((wall.x+direction.x2)*map.cellSize.x, (wall.y+direction.y2)*map.cellSize.y);
}

var map = data;

var TotalSize =
{
	x: map.cellSize.x*map.gridSize.x,
	y: map.cellSize.y*map.gridSize.y
};

var canvas = document.getElementById('mapCanvas');
$(canvas).attr("width", TotalSize.x);
$(canvas).attr("height", TotalSize.y);
map.context = canvas.getContext('2d');

map.context.beginPath();

//draw grid
for(var i = 1; i < map.gridSize.x; i++)
{
	map.context.moveTo(i * map.cellSize.x, 0);
	map.context.lineTo(i * map.cellSize.x, TotalSize.y);
}

for(var i = 1; i < map.gridSize.y; i++)
{
	map.context.moveTo(0, i * map.cellSize.y);
	map.context.lineTo(TotalSize.x, i * map.cellSize.y);
}

map.context.strokeStyle = colorSet[9];
map.context.stroke();

//draw walls
map.context.beginPath();
for(var i = 0; i < map.wallList.length; i++)
{
	Wall(map, map.wallList[i]);
}
map.context.lineWidth = 3;
map.context.strokeStyle = colorSet[0];
map.context.stroke();

//draw symbols
for(var i = 0; i < map.symbolList.length; i++)
{
	Symbol(map, map.symbolList[i].symbol, {x:map.symbolList[i].x, y:map.symbolList[i].y});
}

console.log("finished drawing on canvas");