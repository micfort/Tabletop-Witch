/**
 * Created by s120397 on 4-7-14.
 */
LoadPageData["login.html"] =
{
	navbarItem: $("li.link[data-goto='login.html']"),
	visibilityState: {"loggedIn": false, "loggedOut": true},
	ready: function()
	{
		$("#btn-login").on('click', function()
		{
			//Parse.User.logIn($("#input-Login-Username").val(), $("#input-Login-Password").val(), {
			Parse.User.logIn("micfort", "rover7gom", {
				success: function(user) {
					ChangePage("home.html");
					$("#logout-username").text(user.get("username"));
					UpdateMenuView("loggedIn");
				},
				error: function(user, error) {
					alert("An error has occurred: " + JSON.stringify(error) + "!!");
				}
			});
		});
	}
}