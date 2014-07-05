var LoadPageData = {};

$(document).ready(function ()
{
	Parse.initialize("14Q2hQn42Q77RxuEb19PghEVKWPfsr6UdSJCKxjc", "qpK5SxyggMmZqQ8tqNM60TRQPDaHVrMlzBdG0lUk");

	//menu events
	$(".link").on('click', function ()
	{
		var page = $(this).data("goto");
		if (page !== undefined)
		{
			ChangePage(page);
		}
	});

	//change current page to the home page
	ChangePage($(".link.active").data("goto"));

	/*$("#signup").on('click', function()
	 {
	 var user = new Parse.User();
	 user.set("username", "micfort");
	 user.set("password", "rover7gom");
	 user.set("email", "michiel.fortuin@gmail.com");

	 // other fields can be set just like with Parse.Object
	 //user.set("phone", "415-392-0202");

	 user.signUp(null, {
	 success: function(user) {
	 $("#signup").text("signed up")
	 },
	 error: function(user, error) {
	 // Show the error message somewhere and let the user try again.
	 alert("Error: " + error.code + " " + error.message);
	 }
	 });

	 });

	 $("#signin").on('click', function()
	 {
	 Parse.User.logIn("micfort", "rover7gom", {
	 success: function(user) {
	 $("#signin").text("Succes");
	 },
	 error: function(user, error) {
	 $("#signin").text("failed");
	 }
	 });
	 });

	 $("#create").on('click', function()
	 {
	 var Set = Parse.Object.extend("Set");
	 var set = new Set();
	 var currentUser = Parse.User.current();
	 set.set("owner", currentUser);
	 set.set("name", "Pathfinder OGL")

	 set.save(null, {
	 success: function(gameScore) {
	 $("#create").text("Set is saved");
	 },
	 error: function(gameScore, error) {
	 // Execute any logic that should take place if the save fails.
	 // error is a Parse.Error with an error code and description.
	 $("#create").text('Failed to create new object, with error code: ' + error.message);
	 }
	 });
	 });

	 $("#system").on('click', function()
	 {
	 var System = Parse.Object.extend("System");
	 var SystemQuery = new Parse.Query(System);
	 SystemQuery.get("SjhaJujyTT",
	 {
	 success: function(system)
	 {
	 var Set = Parse.Object.extend("Set");
	 var SetQuery = new Parse.Query(Set);
	 SetQuery.get("L0hSHZDtsd",
	 {
	 success: function(collection)
	 {
	 collection.set("system", system);
	 collection.save(null,{
	 success:function(collection2)
	 {
	 $("#system").text("Set save succes");
	 },
	 error:function(object, error)
	 {
	 $("#system").text("Error set save");
	 }
	 });
	 },
	 error: function(object, error)
	 {
	 $("#system").text("Error set query");
	 }});
	 },
	 error: function(object, error)
	 {
	 $("#system").text("Error system query");
	 }
	 })


	 });*/
});

function ChangePage(page)
{
	$("#page").load(page, function(responseText, textStatus, jqXHR)
	{
		var LoadPageInformation = LoadPageData[page];
		if(LoadPageInformation !== undefined)
		{
			$(".link").removeClass("active");
			LoadPageInformation.navbarItem.addClass("active");
			LoadPageInformation.ready();
		}
	});
}

function UpdateMenuView(state)
{
	for (var key in LoadPageData)
	{
		if(LoadPageData[key].visibilityState[state])
		{
			LoadPageData[key].navbarItem.removeClass("hidden");
		}
		else
		{
			LoadPageData[key].navbarItem.addClass("hidden");
		}
	}
}