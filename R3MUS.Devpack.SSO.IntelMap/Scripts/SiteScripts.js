var svgDoc, beepData, orig, chat;
var beepPrefix = 'data:audio/wav;base64,';

$(window).load(function () {
	try {
		chat = $.connection.intelHub;
		console.log($.connection.url);
		$('#regionName').text(mapName.replace(/\_/g, ' '));
		orig = 'fill: #008000;';

		if (svgDoc == null) {
			LoadSVG();
		};

		chat.client.pingGroupLoggerCount = pingLoggerCount;
		chat.client.pingUserCount = pingUserCount;
		chat.client.pingGroupUserCount = pingGroupUserCount;
		chat.client.pingIntel = pingIntel;
		chat.client.sendSystemBeepData = storeSystemBeepData;

		$.connection.hub.start().done(function () {
			$('#status').text('Connected to Hub');
			$('#status').attr('style', 'color: green;');

			chat.server.sendMeUserCount();
			chat.server.sendMeHistory();
		});
	}
	catch (e) {
		alert(e.message);
	}
});

function storeSystemBeepData(data) {
	beepData = data;
}

function pingIntel(logLines) {
	if (svgDoc == null) {
		LoadSVG();
	};
	for (var i = 0; i < logLines.length; i++) {
		Execute(logLines[i]);
	};
}
function Execute(message) {
	var splitMsg = message.Message.split(' ');

	if ((splitMsg.indexOf('clr') == -1)
		&& (splitMsg.indexOf('clear') == -1)
		&& (splitMsg.indexOf('status') == -1)
		&& (splitMsg.indexOf('status?') == -1)) {
		$.each(splitMsg, function (index, value) {
			if (value.length >= 3) {
				if (IsValidSystem(value)) {
					Flash(value);
					$('#log').prepend('<h5>' + message.LogDateTime + '<br />' + message.UserName + '<br /><strong>' + message.Message + '</strong></h5>');
				}
			}
		});
	}
}

function IsValidSystem(sysName) {
	if (svgDoc == null) {
		LoadSVG();
	}
	for (var i = 0; i < $(svgDoc.getElementsByTagName('symbol')).length; i++) {
		try {
			if ($(svgDoc.getElementsByTagName('symbol'))[i].getElementsByTagName('a')[0].getElementsByTagName('text')[0].textContent.toUpperCase().indexOf(sysName.replace('*', '').toUpperCase()) > -1) {
				return true;
			}
		}
		catch (e) { }
	}
	return false;
}

function Flash(sysName) {
	var beepNoise, systemObject;
	if (svgDoc == null) {
		LoadSVG();
	}
	for (var i = 0; i < $(svgDoc.getElementsByTagName('symbol')).length; i++) {
		try {
			if ($(svgDoc.getElementsByTagName('symbol'))[i].getElementsByTagName('a')[0].getElementsByTagName('text')[0].textContent.toUpperCase().indexOf(sysName.replace('*', '').toUpperCase()) > -1) {
				systemObject = $(svgDoc.getElementsByTagName('symbol'))[i].getElementsByTagName('a')[0].getElementsByTagName('text')[0].previousElementSibling;
				
				for (var i = 0; i < beepData.length; i++) {
					if (beepData[i].SystemNames.indexOf(sysName) > -1) {
						beepNoise = beepData[i].Noise;
						break;
					}
				}
				SetFlashy(systemObject, beepNoise);
				break;
			}
		}
		catch (e) { }
	}
}
function SetFlashy(system, beepNoise) {
	var flashyTimer;
	var counter = 0;
	if (beepNoise != null) {
		beepNoise = beepPrefix + beepNoise;
		flashyTimer = 39;
		var snd = new Audio(beepNoise);
		snd.play();
	}
	else {
		flashyTimer = 20;
	}
	var interval = setInterval(function () {
		if (counter < flashyTimer) {
			if (($(system)).attr('style') != 'fill: #ff0000') {
				($(system)).attr('style', 'fill: #ff0000');
			}
			else {
				($(system)).attr('style', 'fill: #ff8080');
			}
			($(system).next().text($(system).next().text()))
			counter++;
		}
		else {
			($(system)).attr('style', 'fill: #ff8080');
			($(system).next().text($(system).next().text()))
			clearInterval(interval);
			interval = setTimeout(function () {
				($(system)).attr('style', 'fill: #ff944d');
				interval = setTimeout(function () {
					($(system)).attr('style', orig);
					($(system).next().text($(system).next().text()))
				}, 300000);
			}, 300000);
		}
	}, 1000);
}

function HideMenu() {
	$('.dropdown').finish().slideUp('fast');
	$('.arrow').removeClass('up').addClass('down');
}
function ShowHideMenu() {
	$('.dropdown').finish().slideToggle('fast');
	if ($('.arrow').hasClass('up')) {
		$('.arrow').removeClass('up').addClass('down');
	}
	else {
		$('.arrow').removeClass('down').addClass('up');
	}
}
function pingLoggerCount(userCount) {
	$('#loggerCount').text(userCount);
	if (userCount == 0) {
		$('#no-logger-warning').css('display', 'block');
	}
	else if (userCount == 0) {
		$('#no-logger-warning').css('display', 'none');
	}
}
function pingUserCount(userCount) {
	$('#connectionCount').text(userCount);
}
function pingGroupUserCount(userCount) {
	$('#groupConnectionCount').text(userCount);
}
function LoadSVG() {
	svgDoc = document.getElementById("map").contentDocument;
	for (var i = 0; i < $(svgDoc.getElementsByTagName('symbol')).length; i++) {
		try {
			var elem1 = $(svgDoc.getElementsByTagName('symbol'))[i];
			var elem2 = elem1.getElementsByTagName('a')[0];
			var elem3 = elem2.getElementsByTagName('text')[0];
			var elem4 = elem3.previousElementSibling;
			$(elem4).attr('style', orig);
			$(elem3).text($(elem3).text());
		}
		catch (e) {
		}
	}
}

function displayAboutThis() {
	window.open(window.location.protocol + "//" + window.location.host + '/Help/About', 'newwindow', 'width=800, height=500');
}