// These two lines are required to initialize Express in Cloud Code.
var express = require('express');
var app = express();

var runOnParse = false;
if (express.static == undefined)
	runOnParse = true;

if (runOnParse == false)
{
	var Parse = require("parse-cloud").Parse;
	global.Parse = Parse;
	Parse.initialize("14Q2hQn42Q77RxuEb19PghEVKWPfsr6UdSJCKxjc", "qpK5SxyggMmZqQ8tqNM60TRQPDaHVrMlzBdG0lUk", "OY4EfESu8ur3bVHqRclv8yMyG8XneUzUDwCKiTmm");
	Parse.User.logIn("micfort", "test", {success: function (user) {},error: function (user, error){}});

	app.use(express.static('public'));
}
else
{
	var parseExpressHttpsRedirect = require('parse-express-https-redirect');
	var parseExpressCookieSession = require('parse-express-cookie-session');

	app.use(parseExpressHttpsRedirect());  // Require user to be on HTTPS.
	app.use(express.cookieParser('C2HMW1yzaUQfVNxIZMv4tWfL7pspdELKrBVrzCXgklN6RHHa9uPDtNgcsd7rP3LeenRKyyQVhqoPqmC5D8xLFEXekgOVsru3KBTIjH90Rfzo2X5r429OKhLpYLdgwiGWOXCcQr03eQe9ltiYWLQadgUEfW6Xc96s4KMQgA2YhY1dJVTjM5r6NCOC6lPtFKwhFE46m2pd'));
	app.use(parseExpressCookieSession({ cookie: { maxAge: 3600000 } }));
}

// Global app configuration section
app.set('views', 'cloud/views');  // Specify the folder to find templates
app.set('view engine', 'ejs');    // Set the template engine
app.use(express.bodyParser());    // Middleware for reading request body

function GGVRO(o)
{
	o.user = Parse.User.current();
	return o;
}

app.get('/', function (req, res)
{
	res.render('index', GGVRO({ }));
});

app.get('/login', function (req, res)
{
	res.render('login', GGVRO({ error: null, succes: null }));
});

app.post('/login', function (req, res)
{
	Parse.User.logIn(req.body.username, req.body.password, {
		success: function (user)
		{
			res.render('login',GGVRO(
				{
					error: null,
					succes: "You have been logged in as " + req.body.username
				}));
		},
		error: function (user, error)
		{
			res.render('login', GGVRO(
				{
					error: "Error: " + error.code + " " + error.message,
					succes: null
				}));
		}
	});
});

app.get('/register', function (req, res)
{
	res.render('register', GGVRO({ error: null, succes: null }));
});

app.post('/register', function (req, res)
{
	var user = new Parse.User();
	user.set("username", req.body.username);
	user.set("password", req.body.password);
	user.set("email", req.body.mail);

	user.signUp(null, {
		success: function(user) {
			res.render('register', GGVRO(
				{
					error: null,
					succes: "You have been registered as " + req.body.username
				}));
		},
		error: function(user, error) {
			res.render('register', GGVRO(
				{
					error: "Error: " + error.code + " " + error.message,
					succes: null
				}));
		}
	});
});

app.get('/about', function (req, res)
{
	res.render('about', GGVRO({ }));
});

app.get('/logout', function (req, res)
{
	res.render('logout', GGVRO({ message: null }));
});

app.post('/logout', function (req, res)
{
	Parse.User.logOut();
	res.render('logout', GGVRO({ message: "You have been logged out." }));
});

app.get('/sets', function (req, res)
{
	var Set = Parse.Object.extend("Set");
	var SetQuery = new Parse.Query(Set);
	SetQuery.equalTo("owner",Parse.User.current());

	SetQuery.find({
		success:function(results)
		{
			res.render('sets', GGVRO({ list: results, error: null }));
		},
		error: function(error)
		{
			res.render('sets', GGVRO({ list: null, error: "Error: " + error.code + " " + error.message }));
		}
	});
});

app.get('/set', function (req, res)
{
	var Set = Parse.Object.extend("Set");
	var SetQuery = new Parse.Query(Set);
	SetQuery.get(req.query.id, {
		success: function(set_)
		{
			var SetItem = Parse.Object.extend("SetItem");
			var SetItemQuery = new Parse.Query(SetItem);
			SetItemQuery.equalTo("Set",set_);

			SetItemQuery.find({
				success:function(results)
				{
					res.render('set', GGVRO({ list: results, error: null, setName: set_.get("name") }));
				},
				error: function(error)
				{
					res.render('set', GGVRO({ list: null, error: "Error: " + error.code + " " + error.message, setName: "" }));
				}
			});
		},
		error: function(object, error) {
			res.render('set', GGVRO({ list: null, error: "Error: " + error.code + " " + error.message, setName: "" }));
		}
	});


});

app.get('/setItem', function(req, res)
{
	var SetItem = Parse.Object.extend("SetItem");
	var SetItemQuery = new Parse.Query(SetItem);
	SetItemQuery.include("classification");
	SetItemQuery.get(req.query.id, {
		success:function(setItem)
		{
			var classification = setItem.get("classification");
			res.render('setItemView', GGVRO({ data: setItem.get("Description"), viewers: classification.get("viewer"), className: classification.get("name"), error: null }));
		},
		error: function(error)
		{
			res.render('setItemView', GGVRO({ data: null, viewers: null, className: null, error: "Error: " + error.code + " " + error.message}));
		}
	});
});

// // This is an example of hooking up a request handler with a specific request
// // path and HTTP verb using the Express routing API.
// app.get('/hello', function(req, res) {
//   res.render('hello', { message: 'Congrats, you just set up your app!' });
// });

// // Example reading from the request query string of an HTTP get request.
// app.get('/test', function(req, res) {
//   // GET http://example.parseapp.com/test?message=hello
//   res.send(req.query.message);
// });

// // Example reading from the request body of an HTTP post request.
// app.post('/test', function(req, res) {
//   // POST http://example.parseapp.com/test (with request body "message=hello")
//   res.send(req.body.message);
// });

// Attach the Express app to Cloud Code.
if (runOnParse == true)
{
	app.listen();
}
else
{
	module.exports = app;
}