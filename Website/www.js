#!/usr/bin/env node
/**
 * Created by s120397 on 5-7-2014.
 */
var debug = require('debug')('my-application');
var app = require('./cloud/app.js');

app.set('port', process.env.PORT || 3000);

var server = app.listen(app.get('port'), function() {
	debug('Express server listening on port ' + server.address().port);
});
