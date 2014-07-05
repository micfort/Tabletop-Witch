/**
 * Created by s120397 on 4-7-14.
 */

$(document).on("sessionReady", function(e)
{
	$("#btn-login").on('click', function()
	{
		//Parse.User.logIn($("#input-Login-Username").val(), $("#input-Login-Password").val(), {
		Parse.User.logIn("micfort", "rover7gom", {
			success: function(user) {
				session.token = user.getSessionToken();
				UpdateSessionStorage();
				console.log("Session="+JSON.stringify(session));
				console.log("Storage Session="+JSON.stringify(sessionStorage.getItem("session")));
				document.location.href = "index.html";
			},
			error: function(user, error) {
				alert("An error has occurred: " + JSON.stringify(error) + "!!");
			}
		});
	});
});