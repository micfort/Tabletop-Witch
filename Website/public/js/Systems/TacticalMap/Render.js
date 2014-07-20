/**
 * Created by michiel on 18-7-2014.
 */

define(function()
{
	var colorSet = ["#e41a1c","#377eb8","#4daf4a","#984ea3","#ff7f00","#ffff33","#a65628","#f781bf","#999999"];
	var symbolInfo = {offset:{x:0, y: -3}, font: "20px Calibri"};
	var cD = 1;
	var wallPositions = {
		"b":{x1: 0, y1: 1, x2: 1, y2: 1, xT:  0, yT: -cD},
		"t":{x1: 0, y1: 0, x2: 1, y2: 0, xT:  0, yT:  cD},
		"l":{x1: 0, y1: 0, x2: 0, y2: 1, xT:  cD, yT: 0},
		"r":{x1: 1, y1: 0, x2: 1, y2: 1, xT:  -cD, yT: 0}
	}

	function DrawSymbol(context, map, symbol, position)
	{
		context.font = symbolInfo.font;
		context.textAlign = "center";
		context.fillStyle = colorSet[1];

		context.fillText(symbol,
				map.cellSize.x * (position.x + 0.5) + symbolInfo.offset.x,
				map.cellSize.y * (position.y + 1.0) + symbolInfo.offset.y);
	}

	function DrawWall(context, map, wall)
	{
		var direction = wallPositions[wall.direction];
		context.moveTo((wall.x + direction.x1) * map.cellSize.x + direction.xT, (wall.y + direction.y1) * map.cellSize.y + direction.yT);
		context.lineTo((wall.x + direction.x2) * map.cellSize.x + direction.xT, (wall.y + direction.y2) * map.cellSize.y + direction.yT);
	}

	function DrawMap(canvas, map)
	{
		var TotalSize =
		{
			x: map.cellSize.x * map.gridSize.x,
			y: map.cellSize.y * map.gridSize.y
		};

		canvas.width = TotalSize.x;
		canvas.height = TotalSize.y;

		var context = canvas.getContext('2d');

		context.clearRect(0, 0, canvas.width, canvas.height);

		//draw grid
		context.beginPath();
		for (var i = 1; i < map.gridSize.x; i++)
		{
			context.moveTo(i * map.cellSize.x, 0);
			context.lineTo(i * map.cellSize.x, TotalSize.y);
		}

		for (var i = 1; i < map.gridSize.y; i++)
		{
			context.moveTo(0, i * map.cellSize.y);
			context.lineTo(TotalSize.x, i * map.cellSize.y);
		}
		context.lineWidth = 1;
		context.strokeStyle = colorSet[9];
		context.stroke();

		//draw walls
		context.beginPath();
		for (var i = 0; i < map.wallList.length; i++)
		{
			DrawWall(context, map, map.wallList[i]);
		}
		context.lineWidth = 3;
		context.strokeStyle = colorSet[0];
		context.stroke();

		//draw symbols
		for (var i = 0; i < map.symbolList.length; i++)
		{
			DrawSymbol(context, map, map.symbolList[i].symbol, {x: map.symbolList[i].x, y: map.symbolList[i].y});
		}

		console.log("finished drawing on canvas");
	}

	return {DrawMap: DrawMap};
});