/**
 * Created by s120397 on 4-7-14.
 */
LoadPageData["register.html"] =
{
	navbarItem: $("li.link[data-goto='register.html']"),
	visibilityState: {"loggedIn": false, "loggedOut": true},
	ready: function()
	{
		$("#register-btn").on('click', function()
		{
			$("#register-message-success").addClass("hidden");
			$("#register-message-error").addClass("hidden");

			var user = new Parse.User();
			user.set("username", $("#input-register-Username").val());
			user.set("password", $("#input-register-Password").val());
			user.set("email", $("#input-register-mail").val());

			user.signUp(null,{
				success: function(user) {
					$("#register-message-success").removeClass("hidden");
					$("#input-register-Username").prop("disabled", true);
					$("#input-register-Password").prop("disabled", true);
					$("#input-register-mail").prop("disabled", true);
					$("#register-btn").prop("disable", true);
				},
				error: function(user, error) {
					$("#register-message-error").text("Error: " + error.code + " " + error.message);
					$("#register-message-error").removeClass("hidden");
				}
			});
		});
	}
}