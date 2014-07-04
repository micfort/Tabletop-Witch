/**
 * Created by s120397 on 4-7-14.
 */
LoadPageData["login.html"] =
{
	navbarItem: $("li.link[data-goto='login.html']"),
	ready: function()
	{
		$("#btn-login").on('click', function()
		{
			//Parse.User.logIn($("#input-Login-Username").val(), $("#input-Login-Password").val(), {
			Parse.User.logIn("micfort", "rover7gom", {
				success: function(user) {
					ChangePage("home.html");
					$("#logout-username").text(user.get("username"));
					LoadPageData["logout.html"].navbarItem.removeClass("collapse");
					LoadPageData["login.html"].navbarItem.addClass("collapse");
				},
				error: function(user, error) {
					alert("An error has occurred: " + JSON.stringify(error) + "!!");
				}
			});
		});
	}
}