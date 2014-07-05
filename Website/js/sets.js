/**
 * Created by s120397 on 4-7-14.
 */
LoadPageData["sets.html"] =
{
	navbarItem: $("li.link[data-goto='sets.html']"),
	visibilityState: {"loggedIn": true, "loggedOut": false},
	ready: function()
	{
		var currentUser = Parse.User.current();
		$("#sets-username").text(currentUser.get("username"));

		var Set = Parse.Object.extend("Set");
		var SetQuery = new Parse.Query(Set);
		var userID = currentUser.id;
		SetQuery.equalTo("owner",currentUser);
		SetQuery.find({
			success:function(results)
			{
				var html = "";
				for(var i = 0; i < results.length; i++)
				{
					html = html + "<p>" + results[i].get("name") + "</p>";
				}
				$("#sets-list").html(html);
			},
			error: function(error)
			{
				alert("Error: " + error.code + " " + error.message);
			}
		})
	}
}