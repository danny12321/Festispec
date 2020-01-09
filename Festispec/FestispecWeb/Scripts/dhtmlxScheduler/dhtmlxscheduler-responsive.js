function initResponsive(scheduler) {
		// regular header height for Terrace or Flat skin
	var navbarHeight = 59,
		//regular header for Glossy or Standart skin
		navbarClassicHeight = 23,
		// height for Terrace and Flat in Mobile mode
		navbarMobileHeight = 130,
		// height for Glossy and Standart in Mobile mode
		navbarClassicMobileHeight = 140,
		// load QuickInfo if mobile browser detected
		loadQuickInfo = true,
		// navbar label formats for Regular and Mobile modes
		scaleDate = "%F, %D %d",
		scaleDateMobile = "%D %d";
	// we’ll need to handle Glossy and Standart(classic) skins
	// a bit differently from Terrace and Flat
	var classic = { "glossy": true, "classic": true };



	function setSizes(navHeight, navHeightClassicSkin, scaleDate) {
		scheduler.xy.nav_height = navHeight;

		if (classic[scheduler.skin]) {
			scheduler.xy.nav_height = navHeightClassicSkin;
		}

		scheduler.templates.week_scale_date = function (date) {
			return scheduler.date.date_to_str(scaleDate)(date);
		};
	}

	scheduler.attachEvent("onBeforeViewChange", function setNavbarHeight() {
		/* set sizes based on screen size */
		if (typeof scheduler !== "undefined") {

			if (window.innerWidth >= 768) {
				setSizes(navbarHeight, navbarClassicHeight, scaleDate);
			} else {
				setSizes(navbarMobileHeight, navbarClassicMobileHeight, scaleDateMobile);
			}
		}
		return true;
	});

	scheduler.attachEvent("onSchedulerResize", function () {
		scheduler.setCurrentView();
	});

	scheduler.attachEvent("onTemplatesReady", function () {
		if (classic[scheduler.skin]) {
			addCss("../Content/dhtmlxScheduler/dhtmlxscheduler-responsive-classic.css");
		}
	});

	function addCss(source) {
		var cssFileTag = document.createElement("link");
		cssFileTag.setAttribute("rel", "stylesheet");
		cssFileTag.setAttribute("type", "text/css");
		cssFileTag.setAttribute("href", source);

		document.getElementsByTagName("head")[0].appendChild(cssFileTag);
	}

	function addJS(url, callback) {
		var head = document.getElementsByTagName('head')[0];
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.src = url;
		script.onreadystatechange = callback;
		script.onload = callback;
		head.appendChild(script);
	}


	if (/Android|webOS|iPhone|iPad|iPod|IEMobile/i.test(navigator.userAgent) && loadQuickInfo) {
		addJS("../Scripts/dhtmlxScheduler/ext/dhtmlxscheduler_quick_info.js", function () {
			scheduler.config.touch = "force";
			scheduler.xy.menu_width = 0;
		});
	}
}


















