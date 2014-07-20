/**
 * Created by michiel on 20-7-2014.
 */
define(function ()
{
	var states =
	{
		"empty": {
			description: "",
			click: function (map, x, y, direction)
			{
			}
		},
		"addWall":{
			description: "Add walls...",
			click: function (map, x, y, direction)
			{
				map.wallList[map.wallList.length] = {"direction": direction, "x": x, "y": y};
			}
		},
		"removeWall":{
			description: "Remove walls...",
			click: function (map, x, y, direction)
			{
				for (var i = 0; i < map.wallList.length; i++)
				{
					var tmp = map.wallList[i];
					if (tmp["direction"] == direction && tmp["x"] == x && tmp["y"] == y)
					{
						map.wallList.splice(i, 1);
					}
				}
			}
		},
		"addSymbol":{
			description: "Add symbols...",
			click: function (map, x, y, direction)
			{
				var symbol = window.prompt("What symbol(s) do you want to add in this cell?", "");
				map.symbolList[map.symbolList.length] = {"symbol": symbol, "x": x, "y": y};
			}
		},
		"removeSymbol":{
			description: "Remove symbols...",
			click: function (map, x, y, direction)
			{
				for (var i = 0; i < map.symbolList.length; i++)
				{
					var tmp = map.symbolList[i];
					if (tmp["x"] == x && tmp["y"] == y)
					{
						map.symbolList.splice(i, 1);
					}
				}
			}
		}
	};

	var module = {
		currentState: states["empty"],
		changeState: function (newState, stateDescription)
		{
			this.currentState = states[newState];
			stateDescription.html(this.currentState.description);
		}
	};
	return module;
});