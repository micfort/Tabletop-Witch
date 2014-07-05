/**
 * Created by s120397 on 4-7-14.
 */
LoadPageData["logout.html"] =
{
	navbarItem: $("li.link[data-goto='logout.html']"),
	visibilityState: {"loggedIn": true, "loggedOut": false},
	ready: function()
	{
		$("#logout-btn").on('click', function()
		{
			Parse.User.logOut();

			LoadPageData["logout.html"].navbarItem.addClass("hidden");
			LoadPageData["login.html"].navbarItem.removeClass("hidden");
		});
	}
}