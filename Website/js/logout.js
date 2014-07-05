/**
 * Created by s120397 on 4-7-14.
 */

$(document).on("sessionReady", function (e)
{
	$("#logout-btn").on('click', function ()
	{
		Parse.User.logOut();
		session.token = undefined;
		UpdateSessionStorage();
		document.location.href = "index.html";
	});
});