/**
 * Created by s120397 on 5-7-14.
 */

var session;
var state;

$(document).on("sessionReady", function()
{
	CreateMenu();
	if(state == "loggedIn")
		$("#logout-username").text(Parse.User.current().getUsername());
});

$(document).ready(function ()
{
	Parse.initialize("14Q2hQn42Q77RxuEb19PghEVKWPfsr6UdSJCKxjc", "qpK5SxyggMmZqQ8tqNM60TRQPDaHVrMlzBdG0lUk");

	console.log("checking session...");
	session = sessionStorage.getItem("session");
	if(session != null)
	{
		session = JSON.parse(session);
		console.log("Session exist " + JSON.stringify(session) + ", checking token...");
		if(session.token !== undefined)
		{
			console.log("Token="+session.token);
			state = "loggedIn";
			Parse.User.become(session.token).then(
			function(user)
			{
				console.log("Token correct, initilize rest");
				$.event.trigger({
					type: "sessionReady",
					message: "",
					time: new Date()
				});
			},
			function(error)
			{
				state = "loggedOut";
				console.log("Token incorrect, initilize rest");
				session = {};
				$.event.trigger({
					type: "sessionReady",
					message: "",
					time: new Date()
				});
			});
		}
		else
		{
			console.log("No token exist, creating new session");
			state = "loggedOut";
			session = {};
			$.event.trigger({
				type: "sessionReady",
				message: "",
				time: new Date()
			});
		}
	}
	else
	{
		console.log("No session exist, creating new one, initize rest");
		state = "loggedOut";
		session = {};
		$.event.trigger({
			type: "sessionReady",
			message: "",
			time: new Date()
		});
	}
	CreateMenu();
});

function UpdateSessionStorage()
{
	if(session.token != undefined)
		console.log("Update session, session.token="+session.token);
	else
		console.log("Update session, session.token=undefined");
	sessionStorage.setItem("session", JSON.stringify(session));
}

function CreateMenu()
{
	var menu =
	[
		{
			htmlText: "Home",
			link: "index.html",
			visibilityState: {"loggedIn":true, "loggedOut":true}
		},
		{
			htmlText: "Sets",
			link: "sets.html",
			visibilityState: {"loggedIn":true, "loggedOut":false}
		},
		{
			htmlText: "About",
			link: "about.html",
			visibilityState: {"loggedIn":true, "loggedOut":true}
		},
		{
			htmlText: "Login",
			link: "login.html",
			visibilityState: {"loggedIn":false, "loggedOut":true}
		},
		{
			htmlText: "Register",
			link: "register.html",
			visibilityState: {"loggedIn":false, "loggedOut":true}
		},
		{
			htmlText: "Logout <span id=\"logout-username\"></span>",
			link: "logout.html",
			visibilityState: {"loggedIn":true, "loggedOut":false}
		},
	];

	var html = "";
	for(var i = 0; i < menu.length; i++)
	{
		if(menu[i].visibilityState[state])
			html += "<li class=\"link\"><a href=\""+menu[i].link+"\">"+menu[i].htmlText+"</a></li>";
	}
	$("#menu").html(html);
}