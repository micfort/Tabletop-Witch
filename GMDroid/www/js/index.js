Parse.initialize("14Q2hQn42Q77RxuEb19PghEVKWPfsr6UdSJCKxjc", "qpK5SxyggMmZqQ8tqNM60TRQPDaHVrMlzBdG0lUk");

if (typeof cordova !== "undefined")
{
	var deviceReadyDeferred = $.Deferred();
	var jqmReadyDeferred = $.Deferred();

	document.addEventListener("deviceReady", deviceReady, false);

	function deviceReady()
	{
		deviceReadyDeferred.resolve();
	}

	$(document).ready(function ()
	{
		jqmReadyDeferred.resolve();
	});

	$.when(deviceReadyDeferred, jqmReadyDeferred).then(deviceAndDomReady);
}
else
{
	$(document).ready(function ()
	{
		deviceAndDomReady();
	});
}


function deviceAndDomReady()
{
	$(".editButton").on( "click", function()
	{
		var TestObject = Parse.Object.extend("TestObject");
		var testObject = new TestObject();
		testObject.save({foo: "bar"}).then(function(object) {
			alert("yay! it worked");
		});
	});

	$.ajax({
		url: "https://micfort.cloudant.com/micfort_gmdroid_pf_monsters_r1/_design/names/_view/value",
		dataType: "jsonp",
		crossDomain: true,
		success: LoadList
	});
}

function LoadList(list)
{
	var HTML = "";
	var count = list.rows.length;
	list.rows.sort(
		function compare(a, b)
		{
			if (a.value < b.value)
				return -1;
			if (a.value > b.value)
				return 1;
			return 0;
		});

	for (var i = 0; i < count; i++)
	{
		HTML += "<li data-goto=\"monster\" class=\"nav monsterItem\" data-monsterid=\"" + list.rows[i].id + "\"><h3>" + list.rows[i].value + "</h3></li>";
	}
	$("#monsterList").html(HTML);
	$(".monsterItem").on( "click", function()
	{
		$("#monster").html("<section>loading</section>");
		$.ajax({
			url: "https://micfort.cloudant.com/micfort_gmdroid_pf_monsters_r1/"+$(this).data("monsterid"),
			dataType: "jsonp",
			crossDomain: true,
			success: loadMonster
		});
	});
}

function loadMonster(monster)
{
	$("#monster").html("<section>" + monster.html + "</section>");
}

document.addEventListener('deviceready', this.onDeviceReady, false);